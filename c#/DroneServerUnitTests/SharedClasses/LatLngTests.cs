using System;
using System.Collections.Generic;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.SharedClasses
{
    [TestClass]
    public class LatLngTests
    {
        [TestMethod]
        public void getBearingBetweenMarginPointsSimple()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(10, 10, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 45);
        }

        [TestMethod]
        public void getBearingBetweenMarginPointsMoreThan90Degree()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(-10, 10, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 135);
        }

        [TestMethod]
        public void getBearingBetweenMarginPointsMoreThan180Degree()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(-10, -10, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 225);
        }

        [TestMethod]
        public void getBearingBetweenMarginPointsMoreThan270Degree()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(10, -10, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 315);
        }

        [TestMethod]
        public void getBearingBetweenMarginSamePoint()
        {
            Point p1 = new Point(0, 0, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p1);
            Assert.AreEqual(bearing, 0);
        }


    }
}
