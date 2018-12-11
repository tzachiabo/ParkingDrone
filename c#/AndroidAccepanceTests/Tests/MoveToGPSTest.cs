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
            move(DroneServer.SharedClasses.Direction.left, 30);
            move(DroneServer.SharedClasses.Direction.up, 20);
            MoveByGPS(loc.lat, loc.lng, 10);
            loc = getLocation();

            landing();
        }
    }
}
