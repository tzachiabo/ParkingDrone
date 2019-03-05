using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    public class CompletionHandler
    {
        private long max_wait_time = 120;
        public Response response;
        private int mission_index;

        public CompletionHandler(int mission_index,int max_wait_time = 120)
        {
            this.mission_index = mission_index;
            this.max_wait_time = max_wait_time;
            response = null;
        }

        public void wait()
        {
            long start_time = get_time();
            while (response == null)
            {
                //TODO: change assert;
                Assertions.verify(get_time() - start_time < max_wait_time, "mission index:" + mission_index + "time out");
            }
        }

        private long get_time()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }


    }
}
