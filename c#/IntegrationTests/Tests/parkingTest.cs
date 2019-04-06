using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class parkingTest : BaseIntegrationTest
    {
        [TestMethod]
        public void simpleParkingMission()
        {
            Point location = (Point)getLocation().m_res.Data;
            Parking parking = genRandomParking(location);

            parkingMission(parking);
        }

        [TestMethod]
        public void ParkingMissionWithRemoteParking()
        {
            Point location = (Point)getLocation().m_res.Data;
            GeoCoordinate geo_point_src = new GeoCoordinate(location.lat, location.lng);
            Random rnd = new Random();
            double range = 100; 
            double bearing = 0;
            GeoCoordinate geo_point_dst = LngLatHelper.getLocationByBearingAndDistance(geo_point_src, range, bearing);

            Parking parking = genRandomParking(new Point(geo_point_dst.Longitude, geo_point_dst.Latitude));

            parkingMission(parking);
        }
    }
}
