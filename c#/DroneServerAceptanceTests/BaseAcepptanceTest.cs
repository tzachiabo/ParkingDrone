using DroneServer.BL;
using DroneServer.BL.Comm;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DroneServerIntegration
{
    [TestClass]
    public class BaseIntegrationTest
    {
        DroneSimulator drone = null;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            BLManagger bl = BLManagger.getInstance();
            bl.init();
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            CommManager.getInstance().shutDown();
        }
    }
}
