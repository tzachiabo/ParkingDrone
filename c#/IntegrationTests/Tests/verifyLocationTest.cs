using DroneServer.BL;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class verifyLocationTest : BaseIntegrationTest
    {
        private Parking parking;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            takeoff();
            Point location = (Point)getLocation().m_res.Data;
            parking = genRandomParking(location);
            BLManagger.getInstance().set_parking(parking);
            initParkingMission(parking);
            PixelConverterHelper.init(parking.getBasePoint().alt);
            absoluteRotateMission(0);
        }

        [TestMethod]
        public void verifyLocationWithForwardMove()
        {
            Point before_move_location = (Point)verifyLocationMission().m_res.Data;
            move(Direction.forward, 10);
            Point after_move_location = (Point)verifyLocationMission().m_res.Data;

            Assert.IsTrue(is_close(before_move_location.lng, after_move_location.lng, 5));
            Assert.IsTrue(is_close(before_move_location.lat, after_move_location.lat + 10, 5));
        }

        [TestMethod]
        public void verifyLocationWithBackwardMove()
        {
            Point before_move_location = (Point)verifyLocationMission().m_res.Data;
            move(Direction.backward, 10);
            Point after_move_location = (Point)verifyLocationMission().m_res.Data;

            Assert.IsTrue(is_close(before_move_location.lng, after_move_location.lng, 5));
            Assert.IsTrue(is_close(before_move_location.lat, after_move_location.lat - 10, 5));
        }

        [TestMethod]
        public void verifyLocationWithLaftMove()
        {
            Point before_move_location = (Point)verifyLocationMission().m_res.Data;
            move(Direction.left, 10);
            Point after_move_location = (Point)verifyLocationMission().m_res.Data;

            Assert.IsTrue(is_close(before_move_location.lng, after_move_location.lng + 10, 5));
            Assert.IsTrue(is_close(before_move_location.lat, after_move_location.lat, 5));
        }

        [TestMethod]
        public void verifyLocationWithRightMove()
        {
            Point before_move_location = (Point)verifyLocationMission().m_res.Data;
            move(Direction.right, 10);
            Point after_move_location = (Point)verifyLocationMission().m_res.Data;

            Assert.IsTrue(is_close(before_move_location.lng, after_move_location.lng - 10, 5));
            Assert.IsTrue(is_close(before_move_location.lat, after_move_location.lat, 5));
        }

        [TestMethod]
        public void verifyLocationWithUpMove()
        {
            Point before_move_location = (Point)verifyLocationMission().m_res.Data;
            move(Direction.up, 10);
            Point after_move_location = (Point)verifyLocationMission().m_res.Data;

            Assert.IsTrue(is_close(before_move_location.lng, after_move_location.lng, 5));
            Assert.IsTrue(is_close(before_move_location.lat, after_move_location.lat, 5));
        }

        [TestMethod]
        public void verifyLocationWithDownMove()
        {
            move(Direction.up, 10);
            Point before_move_location = (Point)verifyLocationMission().m_res.Data;
            move(Direction.down, 10);
            Point after_move_location = (Point)verifyLocationMission().m_res.Data;

            Assert.IsTrue(is_close(before_move_location.lng, after_move_location.lng, 5));
            Assert.IsTrue(is_close(before_move_location.lat, after_move_location.lat, 5));
        }
    }
}
