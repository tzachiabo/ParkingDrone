using System;
using DroneServer.BL;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class ComplexGoHomeTest : BaseIntegrationTest
    {
        [TestMethod]
        public void simpleGoHome()
        {
            takeoff();
            Point start_location = (Point)getLocation().m_res.Data;
            move(Direction.forward, 10);

            goHome();
            Point location = (Point)getLocation().m_res.Data;

            double go_home_height = Double.Parse(Configuration.getInstance().get("Home_Location_Hight"));
            Assert.IsTrue(is_close(location.alt, go_home_height));
            Assert.IsTrue(is_close(start_location.lat, location.lat));
            Assert.IsTrue(is_close(start_location.lng, location.lng));
        }

        [TestMethod]
        public void GoHomeFromFarDistance()
        {
            takeoff();
            Point start_location = (Point)getLocation().m_res.Data;
            move(Direction.forward, 100);

            goHome();
            Point location = (Point)getLocation().m_res.Data;

            double go_home_height = Double.Parse(Configuration.getInstance().get("Home_Location_Hight"));
            Assert.IsTrue(is_close(location.alt, go_home_height));
            Assert.IsTrue(is_close(start_location.lat, location.lat));
            Assert.IsTrue(is_close(start_location.lng, location.lng));
        }

        [TestMethod]
        public void GoHomeWhenAllreadyThere()
        {
            takeoff();
            Point start_location = (Point)getLocation().m_res.Data;

            goHome();
            Point location = (Point)getLocation().m_res.Data;

            double go_home_height = Double.Parse(Configuration.getInstance().get("Home_Location_Hight"));
            Assert.IsTrue(is_close(location.alt, go_home_height));
            Assert.IsTrue(is_close(start_location.lat, location.lat));
            Assert.IsTrue(is_close(start_location.lng, location.lng));
        }
    }
}
