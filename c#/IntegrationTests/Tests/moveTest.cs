using DroneServer.BL;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class moveTest : BaseIntegrationTest
    {
        private Parking parking;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            takeoff();
            absoluteRotateMission(0);
        }

        [TestMethod]
        public void forwardMove()
        {
            move(Direction.forward, 10);
        }

        [TestMethod]
        public void backwardMove()
        {
            move(Direction.backward, 10);
        }

        [TestMethod]
        public void leftMove()
        {
            move(Direction.left, 10);
        }

        [TestMethod]
        public void rightMove()
        {
            move(Direction.right, 10);
        }

        [TestMethod]
        public void upMove()
        {
            move(Direction.up, 10);
        }

        [TestMethod]
        public void downMove()
        {
            move(Direction.up, 10);
            move(Direction.down, 10);
        }

        [TestMethod]
        public void rotateMove()
        {
            move(Direction.rotate, 10);
        }
    }
}
