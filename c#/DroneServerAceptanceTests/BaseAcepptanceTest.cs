using DroneServer.BL;
using DroneServer.BL.Comm;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Device.Location;

namespace DroneServerIntegration
{
    [TestClass]
    public class BaseAcepptanceTest
    {
        DroneSimulator drone = null;
        protected Random rnd = new Random();

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            BLManagger bl = BLManagger.getInstance();

            bl.init();
            LocationManager.init();

        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            CommManager.getInstance().shutDown();
        }
        
        private Point gen_near_random_point(Point location)
        {
            GeoCoordinate geo_point_src = new GeoCoordinate(location.lat, location.lng);
            double range = rnd.NextDouble() * 30; // range is 0-30 meters
            double bearing = rnd.NextDouble() * 2 * Math.PI;
            GeoCoordinate geo_point_dst = LngLatHelper.getLocationByBearingAndDistance(geo_point_src, range, bearing);

            return new Point(geo_point_dst.Longitude, geo_point_dst.Latitude);
        }

        protected Parking genRandomParking(Point location)
        {
            List<Point> borders = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                borders.Add(gen_near_random_point(location));
            }

            return new Parking("random parking", borders);
        }

        protected MissionWraper getLocation(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new GetLocation());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

    }
}
