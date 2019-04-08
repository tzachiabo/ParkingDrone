using System;
using System.Collections.Generic;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.SharedClasses
{
    [TestClass]
    public class ParkingTests
    {
        [TestMethod]
        public void goodParking()
        {
            Parking park = new Parking("aviad", makeBorder());
            Point basePoint = park.getBasePoint();
            Assert.AreEqual(24, basePoint.alt, 0.5);
            Assert.AreEqual(35, basePoint.lat, 0.5);
            Assert.AreEqual(31, basePoint.lng, 0.5);
            Assert.AreEqual("aviad", park.name);

        }

        private List<Point> makeBorder()
        {
            List<Point> border = new List<Point>();
            border.Add(new Point(31.2649332655875, 34.8064202070236));
            border.Add(new Point(31.2647498502423, 34.8063397407532));
            border.Add(new Point(31.264896582547, 34.8066079616547));
            border.Add(new Point(31.2647269232991, 34.8065274953842));
            return border;
        }

        private List<Point> makeSmallBorder()
        {
            List<Point> border = new List<Point>();
            border.Add(new Point(31.2647498502423, 34.8064202070236));
            border.Add(new Point(31.2647498502523, 34.8064202070236));
            border.Add(new Point(31.2647498502423, 34.8064202070536));
            border.Add(new Point(31.2647498502523, 34.8064202070536));
            return border;
        }

        private List<Point> makeBigBorder()
        {
            List<Point> border = new List<Point>();
            border.Add(new Point(31.2647498502423, 34.8064202070236));
            border.Add(new Point(32.2647498502423, 34.8064202070236));
            border.Add(new Point(31.2647498502423, 35.8064202070236));
            border.Add(new Point(32.2647498502423, 35.8064202070236));
            return border;
        }

    }
}
