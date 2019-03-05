using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missionManagerAccepanceTests.Tests
{
    [TestClass]
    public class TakeOffTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void simpleTakeOff()
        {
            take_off();
            CompletionHandler comphandler = getLocation();
            Point loc = (Point)comphandler.response.Data;
            Assert.IsTrue(loc.alt > 0.5);
            landing();
        }

        [TestMethod]
        public void takeOffAfterLanding()
        {
            take_off();
            landing();
            take_off();

            CompletionHandler comphandler = getLocation();
            Point loc = (Point)comphandler.response.Data;
            Assert.IsTrue(loc.alt > 0.5);
            landing();
        }

    }
}
