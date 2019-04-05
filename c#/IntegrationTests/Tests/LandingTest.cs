using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class LandingTest : BaseIntegrationTest
    {
        [TestMethod]
        public void simpleLanding()
        {
            takeoff();
            landing();

            Point location = (Point)getLocation().m_res.Data;
            Assert.IsTrue(is_close(location.alt, 0));
        }

        [TestMethod]
        public void landingFromHeight()
        {
            takeoff();
            move(Direction.up, 100);
            landing();

            Point location = (Point)getLocation().m_res.Data;
            Assert.IsTrue(is_close(location.alt, 0));
        }
    }
}
