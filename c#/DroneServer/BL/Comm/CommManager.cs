using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using DroneServer.BL.Missions;
using System.Threading;
using DroneServer.SharedClasses;
using System.Collections.Concurrent;

namespace DroneServer.BL.Comm
{
    class CommManager
    {
        private static CommManager m_instance = null;
        private Socket m_socket;
        private ConcurrentDictionary<String, Mission> m_missions;
        private ConcurrentQueue<Response> m_main_responses;
        private ConcurrentQueue<Response> m_status_responses;

        private Thread m_reader;
        private Thread m_main_mission_handler;
        private Thread m_status_mission_handler;


        private CommManager()
        {
            m_missions = new ConcurrentDictionary<String, Mission>();
            m_main_responses = new ConcurrentQueue<Response>();
            m_status_responses = new ConcurrentQueue<Response>();

            Configuration conf = Configuration.getInstance();
            int port = Int32.Parse(conf.get("port"));
            IPAddress ipAddress = IPAddress.Parse(conf.get("address"));

            IPEndPoint ipe = new IPEndPoint(ipAddress, port);
            m_socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            m_socket.Connect(ipe);

            //TODO Refactor
            m_reader = new Thread(() =>
            {
                String data = "";
                while (true)
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = m_socket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("\n") > -1)
                    {
                        Response res = Decoder.decode(data);
                        if (res.Type == MissionType.MainMission)
                        {
                            m_main_responses.Enqueue(res);
                        }
                        else
                        {
                            m_status_responses.Enqueue(res);
                        }
                        data = "";
                    }
                }
            });

            m_main_mission_handler = new Thread(() =>
            {
                while (true)
                {
                    Response current_response;
                    if(m_main_responses.TryDequeue(out current_response))
                    {
                        Mission mission;
                        m_missions.TryRemove(current_response.Key, out mission); //TODO assert
                        mission.done(); //TODO may do done on ack message
                    }
                }
            });

            m_status_mission_handler = new Thread(() =>
            {
                while (true)
                {
                    Response current_response;
                    if (m_status_responses.TryDequeue(out current_response))
                    {
                        Mission mission;
                        m_missions.TryRemove(current_response.Key, out mission); //TODO assert
                        mission.done(); //TODO may do done on ack message
                    }
                }
            });

            m_reader.Start();
            m_main_mission_handler.Start();
            m_status_mission_handler.Start();
        }

        public static CommManager getInstance()
        {
            if(m_instance == null)
            {
                m_instance = new CommManager();
            }
            return m_instance;
        }

        public void execMission(LeafMission mission)
        {
            String message_to_android = mission.encode();

            m_socket.Send(Encoding.ASCII.GetBytes(message_to_android));
        }

    }

}
