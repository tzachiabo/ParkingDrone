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
using System.Runtime.CompilerServices;

namespace DroneServer.BL.Comm
{
    class CommManager
    {
        private bool isSocketInitiated;

        private static CommManager m_instance = null;
        internal NetworkStream m_ns;
        private ConcurrentDictionary<int, Mission> m_missions;
        internal ConcurrentQueue<Response> m_main_responses;
        internal ConcurrentQueue<Response> m_status_responses;
        private TcpListener m_server;
        private Boolean running;
        private CommReader comm_reader;
        private ResponseConsumer m_main_mission_consumer;
        private ResponseConsumer m_status_mission_consumer;

        private CommManager()
        {
            running = true;
            isSocketInitiated = false;
            Logger.getInstance().debug("Initiate Comm manager");

            m_missions = new ConcurrentDictionary<int, Mission>();
            m_main_responses = new ConcurrentQueue<Response>();
            m_status_responses = new ConcurrentQueue<Response>();

            Configuration conf = Configuration.getInstance();
            int port = Int32.Parse(conf.get("port"));

            m_server = new TcpListener(IPAddress.Any, port);

            m_server.Start();
            Thread Initiator = new Thread(() => 
            {
                Logger.getInstance().debug("start listening at port : " + port);
                TcpClient client;
                try
                {
                    client = m_server.AcceptTcpClient();
                }
                catch (System.Net.Sockets.SocketException)
                {
                    Assertions.verify(running == false, "socket got unexpected exception");
                    return;
                }
                Logger.getInstance().debug("recevied a connction from the drown");

                m_ns = client.GetStream();

                isSocketInitiated = true;
                Logger.getInstance().debug("socket has been initiated successfully");

                comm_reader = new CommReader(m_main_responses, m_status_responses);

                m_main_mission_consumer = new ResponseConsumer(m_missions, m_main_responses);

                m_status_mission_consumer = new ResponseConsumer(m_missions, m_status_responses);
            });

            Initiator.Start();
        }

        public static CommManager getInstance()
        {
            if (m_instance == null)
            {
                m_instance = new CommManager();
            }
            return m_instance;
        }

        internal void ClientDisconnect()
        {
            Logger.getInstance().error("client has disconnect");
            Response res;

            Logger.getInstance().info("clear mission map and responses queue");
            m_missions.Clear();
            while (m_main_responses.TryDequeue(out res)) ;
            while (m_status_responses.TryDequeue(out res)) ;

            Logger.getInstance().info("move to new Mission Version");
            BLManagger.getInstance().increment_version();
            TcpClient client;
            try
            {
                client = m_server.AcceptTcpClient();
            }
            catch (System.Net.Sockets.SocketException)
            {
                Assertions.verify(running == false, "socket got unexpected exception");
                return;
            }
            Logger.getInstance().info("client has reconnected");
            m_ns = client.GetStream();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void execMission(LeafMission mission)
        {
            Assertions.verify(isSocketInitiated, "socket was not initiated");

            if (!mission.validate_version())
            {
                Logger.getInstance().info("mission was out of version");
                return;
            }

            bool res = m_missions.TryAdd(mission.m_index, mission);
            Assertions.verify(res, "failed when trying to add mission to comm map missions");

            String message_to_android = mission.encode();

            Logger.getInstance().info("send this message to Android : " + message_to_android);

            byte[] to_send = Encoding.UTF8.GetBytes(message_to_android);
            m_ns.Write(to_send, 0, to_send.Length);
        }

        public void shutDown()
        {
            Logger.getInstance().info("Shutting down Comm layer");

            running = false;
            m_server.Stop();

            if (comm_reader != null)
                comm_reader.shutDown();

            if (m_main_mission_consumer != null)
                m_main_mission_consumer.shutDown();

            if (m_status_mission_consumer != null)
                m_status_mission_consumer.shutDown();
        }

    }

}
