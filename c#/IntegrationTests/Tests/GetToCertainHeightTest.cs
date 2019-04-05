using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class GetToCertainHeightTest : BaseIntegrationTest
    {
        [TestMethod]
        public void simpleGetToCertainHeight()
        {
            takeoff();
            getToCertainHeight(10);
            Point location = (Point)getLocation().m_res.Data;

            //Assert.IsTrue(is_close(location.alt, 10));
        }


        [TestMethod]
        public void simpleGetTolimitHeight()
        {
            takeoff();
            getToCertainHeight(50);
            Point location = (Point)getLocation().m_res.Data;

            //Assert.IsTrue(is_close(location.alt, 50));
        }

        [TestMethod]
        public void simpleGetToLowHeight()
        {
            takeoff();
            getToCertainHeight(50);
            getToCertainHeight(1);
            Point location = (Point)getLocation().m_res.Data;

            //Assert.IsTrue(is_close(location.alt, 1));
        }
    }
}
