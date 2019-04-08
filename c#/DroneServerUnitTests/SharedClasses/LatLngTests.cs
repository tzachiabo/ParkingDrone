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
        public void getBearingBetweenMarginPointsOf0Degree()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(10, 0, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 0);
        }

        [TestMethod]
        public void getBearingBetweenMarginPointsOf90Degree()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(0, 10, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 90);
        }
        [TestMethod]
        public void getBearingBetweenMarginPointsOf180Degree()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(-10, 0, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 180);
        }

        [TestMethod]
        public void getBearingBetweenMarginPointsOf270Degree()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(0, -10, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p2);
            Assert.AreEqual(bearing, 270);
        }

        [TestMethod]
        public void getBearingBetweenMarginSamePoint()
        {
            Point p1 = new Point(0, 0, 0);

            double bearing = LngLatHelper.getBearingBetweenMarginPoints(p1, p1);
            Assert.AreEqual(bearing, 0);
        }


        [TestMethod]
        public void getDistanceBetweenMarginX()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(10, 0, 0);

            double distance = LngLatHelper.getDistanceBetweenMarginPoints(p1, p2);
            Assert.AreEqual(distance, 10);
        }

        [TestMethod]
        public void getDistanceBetweenMarginMinusX()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(-10, 0, 0);

            double distance = LngLatHelper.getDistanceBetweenMarginPoints(p1, p2);
            Assert.AreEqual(distance, 10);
        }


        [TestMethod]
        public void getDistanceBetweenMarginY()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(0, 10, 0);

            double distance = LngLatHelper.getDistanceBetweenMarginPoints(p1, p2);
            Assert.AreEqual(distance, 10);
        }

        [TestMethod]
        public void getDistanceBetweenMarginMinusY()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(0, -10, 0);

            double distance = LngLatHelper.getDistanceBetweenMarginPoints(p1, p2);
            Assert.AreEqual(distance, 10);
        }


        [TestMethod]
        public void getDistanceBetweenMarginXAndY()
        {
            Point p1 = new Point(0, 0, 0);
            Point p2 = new Point(10, 10, 0);

            double distance = LngLatHelper.getDistanceBetweenMarginPoints(p1, p2);
            Assert.AreEqual(distance, Math.Sqrt(200));
        }

        [TestMethod]
        public void getDistanceBetweenMarginSamePoint()
        {
            Point p1 = new Point(0, 0, 0);

            double distance = LngLatHelper.getDistanceBetweenMarginPoints(p1, p1);
            Assert.AreEqual(distance, 0);
        }


    }
}
