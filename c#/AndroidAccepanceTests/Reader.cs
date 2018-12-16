using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DroneServer.BL.Comm;
using DroneServer.SharedClasses;

namespace AndroidAccepanceTests
{
    class Reader
    {
        private NetworkStream m_ns;
        Thread thread;
        private ConcurrentDictionary<int, CompletionHanlder> m_missions;

        public Reader(NetworkStream ns)
        {
            m_missions = new ConcurrentDictionary<int, CompletionHanlder>();
            m_ns = ns;

            thread = new Thread(() =>
            {
                String data = "";
                while (true)
                {
                    byte[] bytes = new byte[124];
                    try
                    {
                        int bytesRec = m_ns.Read(bytes, 0, bytes.Length);
                        data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        while (data.Contains('%'))
                        {
                            int index = data.IndexOf('%');
                            String curr_message = data.Substring(0, index);
                            if (!curr_message.Contains("getStatus"))
                                Logger.getInstance().info("recevied from android : " + curr_message);
                            Response r = DroneServer.BL.Comm.Decoder.decode(curr_message);

                            CompletionHanlder comp_out;
                            Assert.IsTrue(m_missions.TryRemove(r.Key, out comp_out));
                            comp_out.response = r;

                            data = data.Substring(index + 1);
                        }

                    }
                    catch (System.IO.IOException)
                    {
                        Assert.Fail();
                    }

                }
            });

            thread.Start();
        }

        public void addMission(int i, CompletionHanlder comp_handler)
        {
            bool res = m_missions.TryAdd(i, comp_handler);
            Assert.IsTrue(res);
        }



    }
}
