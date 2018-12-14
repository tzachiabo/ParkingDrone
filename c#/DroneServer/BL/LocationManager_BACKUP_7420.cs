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
        private static Parking parking;

        private LocationManager(Parking p)
        {
            double timer_interval = Double.Parse(Configuration.getInstance().get("getLocationInterval"));

            Logger.getInstance().debug("start get location timer with interval " + timer_interval);

            aTimer = new Timer(timer_interval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            parking = p;
        }

        public static void init(Parking p)
        {
            if (instance == null)
            {
                instance = new LocationManager(p);
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
<<<<<<< HEAD
            Logger.getInstance().debug("update map location with this params :" + p.lat + " " + p.lng);
            BLManagger.getInstance().setLocation(p.lat, p.lng);
=======
            Logger.getInstance().debug("update map location with this params :" + p.y + " " + p.x);
            BLManagger.getInstance().setLocation(p.y, p.x);

            if (BLManagger.getInstance().getSafeZone())
                Assertions.verify(validateLocation(p), "The drone is running away!");

            

        }
        private static bool validateLocation(Point position)
        {
            Point tl = new Point(parking.border[0].x, parking.border[0].y);
            Point br = new Point(parking.border[0].x, parking.border[0].y);

            for (int i = 1; i < parking.border.Count; i++)//axis grows right and down
            {
                if (parking.border[i].x > br.x)
                    br.x = parking.border[i].x;
                else if (parking.border[i].x < tl.x)
                    tl.x = parking.border[i].x;
                if (parking.border[i].y > br.y)
                    br.y = parking.border[i].y;
                else if (parking.border[i].y < tl.y)
                    tl.y = parking.border[i].y;
            }

            if (tl.x<=position.x && br.x>=position.x && tl.y <= position.y && br.y >= position.y)
                return true;

            Logger.getInstance().error("drone cross the border and got to mexico");
            BLManagger.getInstance().stop();

            return false;
>>>>>>> 59641c8... location manager verify position
        }


    }
}
