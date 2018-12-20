using System;
using System.Collections.Generic;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missionManagerAccepanceTests.Tests
{
    [TestClass]
    public class InitParkingMissionTest:BaseAcceptanceTest
    {
        [TestMethod]
        public void closeParkingTest()
        {
            
            Point loc = (Point)getLocation().response.Data;
            initParkMission(makeParking(loc.lat, loc.lng, 0.0001));
        }
        private Parking makeParking(double lat, double lng, double distance)
        {
            lat += distance;
            lng += distance;
            List<Point> border = new List<Point>();
            border.Add(new Point(lat, lng));
            border.Add(new Point(lat + distance, lng));
            border.Add(new Point(lat, lng + distance));
            border.Add(new Point(lat + distance, lng + distance));
            return new Parking("test", border);
        }
    }
}
