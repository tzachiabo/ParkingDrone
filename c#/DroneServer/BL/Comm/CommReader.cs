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
        private NetworkStream m_ns;
        private ConcurrentQueue<Response> m_main_responses;
        private ConcurrentQueue<Response> m_status_responses;

        Thread thread;

        public CommReader(NetworkStream ns, 
                          ConcurrentQueue<Response> main_responses, 
                          ConcurrentQueue<Response> status_responses)
        {
            m_ns = ns;
            m_main_responses = main_responses;
            m_status_responses = status_responses;

            thread = new Thread(() =>
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

            thread.Start();
        }
    }
}
