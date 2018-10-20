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
        private NetworkStream ns;
        private ConcurrentDictionary<int, Mission> m_missions;
        private ConcurrentQueue<Response> m_main_responses;
        private ConcurrentQueue<Response> m_status_responses;

        private Thread m_reader;
        private Thread m_main_mission_handler;
        private Thread m_status_mission_handler;


        private CommManager()
        {
            Console.WriteLine("start comm manager");
            m_missions = new ConcurrentDictionary<int, Mission>();
            m_main_responses = new ConcurrentQueue<Response>();
            m_status_responses = new ConcurrentQueue<Response>();

            Configuration conf = Configuration.getInstance();
            int port = Int32.Parse(conf.get("port"));

            TcpListener server = new TcpListener(IPAddress.Any, port);

            server.Start();

            Console.WriteLine("start listening");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("recived connection");

            ns = client.GetStream();

            //TODO Refactor
            m_reader = new Thread(() =>
            {
                String data = "";
                while (true)
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = ns.Read(bytes, 0, bytes.Length);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    Console.WriteLine(data);
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
            });

            m_main_mission_handler = new Thread(() =>
            {
                while (true)
                {
                    Response current_response;
                    if(m_main_responses.TryDequeue(out current_response))
                    {
                        Mission mission;

                        bool res = m_missions.TryRemove(current_response.Key, out mission);
                        Assertions.verify(res, "main mission thread tried faild to remove a response from queue");

                        mission.done(); //TODO may do done on ack message
                    }
                }
            });

            m_status_mission_handler = new Thread(() =>
            {
                while (true)
                {
                    Response current_response;
                    if (m_status_responses.TryDequeue(out current_response))//TODO else yeild
                    {
                        Mission mission;

                        bool res = m_missions.TryRemove(current_response.Key, out mission); //TODO assert
                        Assertions.verify(res, "status mission thread tried faild to remove a response from queue");

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
            bool res = m_missions.TryAdd(mission.m_index, mission);
            Assertions.verify(res, "failed when trying to add mission to comm map missions");

            String message_to_android = mission.encode();

            Console.WriteLine(message_to_android);

            byte[] to_send = Encoding.UTF8.GetBytes(message_to_android);
            ns.Write(to_send, 0 , to_send.Length);
        }

    }

}
