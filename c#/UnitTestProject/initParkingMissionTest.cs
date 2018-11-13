using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneServer.SharedClasses;
using DroneServer.BL;
using DroneServer.BL.Missions;

namespace UnitTestProject
{
    [TestClass]
    public class initParkingMissionTest
    {
        [TestMethod]
        public void getBaseLocationWith4GPSPoints()
        {
            GPSPoint point1 = new GPSPoint(0,0,0);
            GPSPoint point2 = new GPSPoint(0, 3, 0);
            GPSPoint point3 = new GPSPoint(4, 0, 0);
            GPSPoint point4 = new GPSPoint(4, 3, 0);
            GPSPoint basePoint = InitParkingMission.getBaseLocation(point1, point2, point3, point4);
            Assert.IsTrue(basePoint.x == 2);
            Assert.IsTrue(basePoint.y == 1.5);
            Assert.IsTrue(basePoint.z == 5/2.144);
        }
    }
}
