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
        private static LocationManager instance = null;
        private static Timer aTimer = null;
        private static Boolean is_location_verification_enabled;
        public static Point HomeLocation;
        public static Point current_position = null;

        private LocationManager()
        {
            is_location_verification_enabled = Boolean.Parse(Configuration.getInstance().get("enable_location_manager_verification"));
            double timer_interval = Double.Parse(Configuration.getInstance().get("getLocationInterval"));

            Logger.getInstance().debug("start get location timer with interval " + timer_interval);

            aTimer = new Timer(timer_interval);
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
            GetLocation mission = new GetLocation();
            mission.register_to_notification(update_location);
            Logger.getInstance().debug("send get location to drone");
            if (Comm.CommManager.getInstance().isSocketInitiated)
                mission.execute();
            else
                Logger.getInstance().error("Location manager want to get location however the drone is not connected yet");
        }

        private static void update_location(Response response)
        {
            Point p = (Point)response.Data;
            current_position = p;
            Logger.getInstance().debug("update map location with this params :" + p.lat + " " + p.lng);
            BLManagger.getInstance().setLocation(p.lat, p.lng);
            if (HomeLocation == null)
            {
                HomeLocation = p;
            }
        }
        

    }
}
