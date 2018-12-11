using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class MoveToGPSTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void simpleMoveByGPS()
        {
            take_off();
            Point loc = getLocation();
            move(DroneServer.SharedClasses.Direction.left, 100);
            move(DroneServer.SharedClasses.Direction.up, 60);
            MoveByGPS(loc.x, loc.y, loc.z);
            loc = getLocation();

            landing();
        }
    }
}
