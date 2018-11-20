using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DroneServer.BL
{
    class statusManager
    {
        private static statusManager instance;
        private static System.Timers.Timer aTimer;

        private statusManager()
        {
            double timer_interval = Double.Parse(Configuration.getInstance().get("getStatusInterval"));

            Logger.getInstance().debug("start get location timer with interval " + timer_interval);

            aTimer = new System.Timers.Timer(timer_interval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static void init()
        {
            if (instance == null)
            {
                instance = new statusManager();
            }
        }

        public static void shutDown()
        {
            if (instance != null)
                instance.stop();
        }

        private void stop()
        {
            Logger.getInstance().warn("location manager has stopped");
            aTimer.Stop();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            getDroneStatus mission = new getDroneStatus();
            Logger.getInstance().debug("send get status to drone");
            mission.execute();
        }
    }
}
