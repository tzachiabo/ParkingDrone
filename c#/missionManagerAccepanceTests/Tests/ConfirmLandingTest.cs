using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missionManagerAccepanceTests.Tests
{
    [TestClass]
    public class ConfirmLandingTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void ConfirmLanding()
        {
            take_off();
            landing();
            CompletionHandler completionHandler= getLocation();
            Point point = (Point)completionHandler.response.Data;

            Assert.AreEqual(point.alt, 0);
        }
    }
}
