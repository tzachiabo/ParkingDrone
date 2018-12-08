using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidAccepanceTests
{
    class CompletionHanlder
    {
        private static long MAX_WAIT_TIME = 60000;

        public Response response;
        private int mission_index;

        public CompletionHanlder(int mission_index)
        {
            this.mission_index = mission_index;
            response = null;
        }

        public void wait()
        {
            long start_time = get_time();
            while (response == null)
            {
                Assert.IsTrue(get_time() - start_time < MAX_WAIT_TIME);
                System.Threading.Thread.Sleep(100);
            }
        }

        private long get_time()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }


    }
}
