using DroneServer.BL;
using DroneServer.BL.Comm;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AndroidAccepanceTests
{
    class Comm
    {
        private static Comm instance;
        NetworkStream m_ns;
        Reader m_reader;
        PicTransferServer pic_server;
        public Loader m_load;


        private Comm()
        {
            init();
            pic_server = new PicTransferServer();
        }

        public static Comm getInstance()
        {
            if (instance == null)
            {
                instance = new Comm();
            }
            return instance;
        }

        private void init()
        {
            int port = Int32.Parse(Configuration.getInstance().get("port"));

            TcpListener server = new TcpListener(IPAddress.Any, 3000);
            server.Start();

            TcpClient client = server.AcceptTcpClient();

            m_ns = client.GetStream();

            m_reader = new Reader(m_ns);

            getDroneStatus drone_status_mission = new getDroneStatus();
            CompletionHandler drone_status = sendMission(drone_status_mission);
            drone_status.wait();
            while((DroneStatus)drone_status.response.Data != DroneStatus.Connected)
            {
                System.Threading.Thread.Sleep(10000);
                drone_status_mission = new getDroneStatus();
                drone_status = sendMission(drone_status_mission);
                drone_status.wait();
            }

            //m_load = new Loader(100);

        }

        public CompletionHandler sendMission(LeafMission mission, bool isAsync=false)
        {
            String message_to_android = mission.encode();
            CompletionHandler comp_handler = sendString(mission.m_index, message_to_android);
            if (!isAsync)
            {
                comp_handler.wait();
            }

            return comp_handler;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CompletionHandler sendString(int index, String message_to_android)
        {
            CompletionHandler comp_handler = new CompletionHandler(index);

            m_reader.addMission(index, comp_handler);


            byte[] to_send = Encoding.UTF8.GetBytes(message_to_android + "%");

            try
            {
                if (!message_to_android.Contains("getStatus"))
                    Logger.getInstance().info("send to android : " + message_to_android);
                m_ns.Write(to_send, 0, to_send.Length);
                m_ns.Flush();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            return comp_handler;
        }

    }
}
