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
        private NetworkStream m_ns;
        private ConcurrentDictionary<int, Mission> m_missions;
        private ConcurrentQueue<Response> m_main_responses;
        private ConcurrentQueue<Response> m_status_responses;

        private CommReader comm_reader;
        private ResponseConsumer m_main_mission_consumer;
        private ResponseConsumer m_status_mission_consumer;
        
        private CommManager()
        {
            Logger.getInstance().debug("Initiate Comm manager");

            m_missions = new ConcurrentDictionary<int, Mission>();
            m_main_responses = new ConcurrentQueue<Response>();
            m_status_responses = new ConcurrentQueue<Response>();

            Configuration conf = Configuration.getInstance();
            int port = Int32.Parse(conf.get("port"));

            TcpListener server = new TcpListener(IPAddress.Any, port);

            server.Start();
            Thread Initiator = new Thread(() => 
            {
                Logger.getInstance().debug("start listening at port : " + port);
                TcpClient client = server.AcceptTcpClient();
                Logger.getInstance().debug("recevied a connction from the drown");

                m_ns = client.GetStream();

                comm_reader = new CommReader(m_ns, m_main_responses, m_status_responses);

                m_main_mission_consumer = new ResponseConsumer(m_missions, m_main_responses);

                m_status_mission_consumer = new ResponseConsumer(m_missions, m_status_responses);
            });

            Initiator.Start();
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
            m_ns.Write(to_send, 0 , to_send.Length);
        }

    }

}
