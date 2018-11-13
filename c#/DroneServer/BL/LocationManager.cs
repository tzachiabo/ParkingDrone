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
    class LocationManager
    {
        private static LocationManager instance;
        private static System.Timers.Timer aTimer;

        private LocationManager()
        {
            double timer_interval = Double.Parse(Configuration.getInstance().get("getLocationInterval"));

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
                instance = new LocationManager();
            }
        }

        public static void shutDown()
        {
            instance.stop();
        }

        private void stop()
        {
            Logger.getInstance().warn("location manager has stopped");
            aTimer.Stop();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            GetLocation mission = new GetLocation();
            Logger.getInstance().debug("send get location to drone");
            if (Comm.CommManager.getInstance().isSocketInitiated)
                mission.execute();
            else
                Logger.getInstance().error("Location manager want to get location however the drone is not connected yet");
        }


    }
}
