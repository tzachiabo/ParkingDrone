using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests.Tests
{
    [TestClass]
    public class StopTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void stopLanding()
        {
            take_off();
            move(DroneServer.SharedClasses.Direction.up, 30);
            CompletionHanlder isLanded = startLanding(true);
            stop();
            System.Threading.Thread.Sleep(30);
            Assert.IsNull(isLanded.response);
            restore();
            move(Direction.right, 5);
            landing();
        }


        [TestMethod]
        public void stopGoToGps()
        {
            take_off();
            move(DroneServer.SharedClasses.Direction.up, 30);
            Point loc = getLocation();
            CompletionHanlder ch = MoveByGPS(loc.lat, loc.lng, loc.alt,true);
            stop();
            System.Threading.Thread.Sleep(30);
            Assert.IsNull(ch.response);
            restore();
            move(Direction.backward, 10);
            landing();
        }
    }
}
