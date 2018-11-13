using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneServer.BL.Comm
{
    class ResponseConsumer
    {
        private ConcurrentDictionary<int, Mission> m_missions;
        private ConcurrentQueue<Response> m_responses;
        private Thread thread;
        private Boolean running;

        public ResponseConsumer(ConcurrentDictionary<int, Mission> missions, 
                                ConcurrentQueue<Response> responses)
        {
            running = true;
            m_missions = missions;
            m_responses = responses;

            thread = new Thread(() =>
            {
                while (running)
                {
                    Response current_response;
                    if (m_responses.TryDequeue(out current_response))
                    {
                        Mission mission;

                        bool res = m_missions.TryRemove(current_response.Key, out mission);
                        Assertions.verify(res, "main mission thread tried faild to remove a response from queue");

                        mission.done(); 
                    }
                }

                Logger.getInstance().warn("Response consumer has been shut down");
            });

            thread.Start();
        }

        public void shutDown()
        {
            running = false;
        }

    }
}
