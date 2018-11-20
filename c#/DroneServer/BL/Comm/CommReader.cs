using DroneServer.SharedClasses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneServer.BL.Comm
{
    class CommReader
    {
        private ConcurrentQueue<Response> m_main_responses;
        private ConcurrentQueue<Response> m_status_responses;
        private Boolean running;

        Thread thread;

        public CommReader(ConcurrentQueue<Response> main_responses, 
                          ConcurrentQueue<Response> status_responses)
        {
            running = true;
            m_main_responses = main_responses;
            m_status_responses = status_responses;

            thread = new Thread(() =>
            {
                String data = "";
                while (running)
                {
                    NetworkStream ns = CommManager.getInstance().m_ns;
                    byte[] bytes = new byte[124];
                    int bytesRec = ns.Read(bytes, 0, bytes.Length);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    while (data.Contains('%'))
                    {
                        Logger.getInstance().info("receive this message from Android : " + data);
                        string[] messages = data.Split('%');

                        for (int i = 0; i < messages.Length - 1; i++)
                        {
                            EncodeMission(messages[i]);
                        }

                        data = messages[messages.Length - 1];

                    }
    
                }
                Logger.getInstance().warn("Comm reader has been shut down");

            });

            thread.Start();
        }

        private void EncodeMission(String data)
        {
            Response res = Decoder.decode(data);
            switch (res.Type)
            {
                case MissionType.MainMission:
                    m_main_responses.Enqueue(res);
                    break;

                case MissionType.StateMission:
                    m_status_responses.Enqueue(res);
                    break;

                case MissionType.EndOfSocket:
                    CommManager.getInstance().ClientDisconnect();
                    break;
            }
        }

        public void shutDown()
        {
            running = false;
        }
    }
}
