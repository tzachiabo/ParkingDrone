using DroneServer.BL;
using DroneServer.BL.Comm;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using DroneServerIntegration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class BaseIntegrationTest
    {
        protected DroneSimulator drone = null;
        protected Random rnd = new Random();

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            BLManagger bl = BLManagger.getInstance();
            bl.init();
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            drone = new DroneSimulator();
            drone.start_drone();
        }

        [TestCleanup]
        public void testClean()
        {
            drone.close_drone();
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            CommManager.getInstance().shutDown();
        }

        protected bool is_close(double a, double b, double delta=3)
        {
            return System.Math.Abs(a - b) < delta;
        }

        protected MissionWraper takeoff(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new TakeOff());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper landing(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new Landing());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper move(Direction direction, double amount, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new MoveMission(direction, amount));
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper goHome(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new ComplexGoHome());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
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

        protected MissionWraper getToCertainHeight(double height_destination, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new GetToCertainHeight(height_destination));
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper verifyLocationMission(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new VerifyLocation());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper absoluteRotateMission(double destinated_bearing, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new AbsoulteRotateMission(destinated_bearing));
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper goToCarMission(Point curr_position, Car car, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new GoToCar(curr_position, car));
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper scanSingleCarMission(Point curr_position, Car car, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new ScanSingleCar(curr_position, car));
            if (!is_async)
            {
                mission.Wait(60*3);
            }
            return mission;
        }


        protected MissionWraper initParkingMission(Parking parking, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new InitParkingMission(parking));
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper parkingMission(Parking parking, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new ParkingMission(parking));
            if (!is_async)
            {
                mission.Wait(60*5);
            }
            return mission;
        }

        private Point gen_near_random_point(Point location)
        {
            GeoCoordinate geo_point_src = new GeoCoordinate(location.lat, location.lng);
            double range = rnd.NextDouble() * 100; // range is 0-100 meters
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

        

    }
}
