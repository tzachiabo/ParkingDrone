using DroneServer.BL;
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

        private Comm()
        {
            init();
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
            CompletionHanlder drone_status = sendMission(drone_status_mission);
            drone_status.wait();
            while((DroneStatus)drone_status.response.Data != DroneStatus.Connected)
            {
                drone_status_mission = new getDroneStatus();
                drone_status = sendMission(drone_status_mission);
                drone_status.wait();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CompletionHanlder sendMission(LeafMission mission)
        {
            CompletionHanlder comp_handler = new CompletionHanlder(mission.m_index);
            m_reader.addMission(mission.m_index, comp_handler);

            String message_to_android = mission.encode();
            byte[] to_send = Encoding.UTF8.GetBytes(message_to_android + "%");

            try
            {
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
