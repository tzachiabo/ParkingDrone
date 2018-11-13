﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneServer.SharedClasses;
using DroneServer.BL;
using DroneServer.BL.Missions;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class initParkingMissionTest
    {
        [TestMethod]
        public void getBaseLocationWith4GPSPoints()
        {
            Point point1 = new Point(0,0,0);
            Point point2 = new Point(0, 3, 0);
            Point point3 = new Point(4, 0, 0);
            Point point4 = new Point(4, 3, 0);
            List<Point> points = new List<Point> { point1, point2, point3, point4 };
            Point basePoint = InitParkingMission.GetBaseLocation(points);
            Assert.IsTrue(basePoint.x == 2);
            Assert.IsTrue(basePoint.y == 1.5);
            Assert.IsTrue(basePoint.z == 5/2.144);
        }
    }
}
