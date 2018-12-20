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
        private ConcurrentDictionary<int, CompletionHandler> compHandlerMap;
        public ResponseConsumer(ConcurrentDictionary<int, Mission> missions, 
                                ConcurrentQueue<Response> responses, ConcurrentDictionary<int, CompletionHandler> compHandlerMap)
        {
            running = true;
            m_missions = missions;
            m_responses = responses;
            this.compHandlerMap = compHandlerMap;

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
                        CompletionHandler compHandler;
                        res = this.compHandlerMap.TryGetValue(current_response.Key,out compHandler);
                        Assertions.verify(res, "main mission thread tried faild to get from response the completion handler");
                        compHandler.response = current_response;
                        mission.done(current_response); 
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
