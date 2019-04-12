using DroneServer.BL;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class absoluteRotateTest : BaseIntegrationTest
    {
        private Parking parking;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            takeoff();
        }

        [TestMethod]
        public void simpleAbsoluteRotate()
        {
            absoluteRotateMission(50);
            Point location = (Point)getLocation().m_res.Data;

            Assert.IsTrue(is_close_angle(location.bearing, 50, 5));
        }

        [TestMethod]
        public void simpleAbsoluteRotateToCurrentBearing()
        {
            absoluteRotateMission(10);
            absoluteRotateMission(10);
            Point location = (Point)getLocation().m_res.Data;

            Assert.IsTrue(is_close_angle(location.bearing, 10, 5));
        }

        [TestMethod]
        public void simpleAbsoluteRotateToZeroBearing()
        {
            absoluteRotateMission(0);
            Point location = (Point)getLocation().m_res.Data;

            Assert.IsTrue(is_close_angle(location.bearing, 0, 5));
        }

        [TestMethod]
        public void simpleAbsoluteRotateToalmost180Bearing()
        {
            absoluteRotateMission(179);
            Point location = (Point)getLocation().m_res.Data;


            Assert.IsTrue(is_close_angle(location.bearing, 179, 5));
        }

        [TestMethod]
        public void simpleAbsoluteRotateToalmost360Bearing()
        {
            absoluteRotateMission(359);
            Point location = (Point)getLocation().m_res.Data;

            Assert.IsTrue(is_close_angle(location.bearing, -1, 5));
        }

    }
}
