using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
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
            System.Threading.Thread.Sleep(3000); // talk to bar about this
            stop();
            System.Threading.Thread.Sleep(30000);
            Assert.IsNull(isLanded.response);
            restore();
            move(Direction.right, 5);
            landing();
        }


        [TestMethod]
        public void stopGoToGps()
        {
            take_off();
            Point loc = getLocation();
            move(DroneServer.SharedClasses.Direction.up, 10);
            move(DroneServer.SharedClasses.Direction.left, 30);
            CompletionHanlder ch = MoveByGPS(loc.lat, loc.lng, 10,true);
            System.Threading.Thread.Sleep(3000);
            stop();
            System.Threading.Thread.Sleep(30000);
            Assert.IsNull(ch.response);
            restore();
            move(Direction.backward, 10);
            landing();
        }

        [TestMethod]
        public void stopMove()
        {
            take_off();
            Point loc = getLocation();
            move(DroneServer.SharedClasses.Direction.up, 10);
            CompletionHanlder ch = move(DroneServer.SharedClasses.Direction.forward, 30, true);
            System.Threading.Thread.Sleep(3000);
            stop();
            System.Threading.Thread.Sleep(30000);
            Assert.IsNull(ch.response);
            restore();
            move(Direction.backward, 10);
            landing();
        }


        //[TestMethod]
        //public void stopGoHome()
        //{
        //    take_off();
        //    move(Direction.up, 5);
        //    move(Direction.forward, 20);
        //    CompletionHanlder ch = goHome(true);
        //    System.Threading.Thread.Sleep(3000);
        //    stop();
        //    System.Threading.Thread.Sleep(30000);
        //    Assert.IsNull(ch.response);
        //    restore();
        //    move(Direction.backward, 10);
        //    landing();
        //}
    }
}
