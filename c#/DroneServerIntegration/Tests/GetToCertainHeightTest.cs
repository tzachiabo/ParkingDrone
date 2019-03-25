using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DroneServerIntegration
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

            Assert.AreEqual(location.alt, 10);
        }


        [TestMethod]
        public void simpleGetTolimitHeight()
        {
            takeoff();
            getToCertainHeight(10);
            Point location = (Point)getLocation().m_res.Data;

            Assert.AreEqual(location.alt, 10);
        }
    }
}
