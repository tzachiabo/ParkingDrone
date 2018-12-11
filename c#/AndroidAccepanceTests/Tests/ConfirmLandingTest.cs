using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class ConfirmLandingTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void ConfirmLanding()
        {
            take_off();
            landing();
            Point point = getLocation();

            Assert.AreEqual(point.z, 0);
        }
    }
}
