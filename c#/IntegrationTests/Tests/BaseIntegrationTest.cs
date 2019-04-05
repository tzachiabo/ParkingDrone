using DroneServer.BL;
using DroneServer.BL.Comm;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using DroneServerIntegration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class BaseIntegrationTest
    {
        protected DroneSimulator drone = null;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            BLManagger bl = BLManagger.getInstance();
            bl.init();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            drone = new DroneSimulator();
            drone.start_drone();
        }

        [TestCleanup]
        public void testClean()
        {
            drone.close_drone();
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            CommManager.getInstance().shutDown();
        }

        protected bool is_close(double a, double b, double delta=3)
        {
            return System.Math.Abs(a - b) < delta;
        }

        protected MissionWraper takeoff(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new TakeOff());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper landing(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new Landing());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper move(Direction direction, double amount, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new MoveMission(direction, amount));
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper goHome(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new ComplexGoHome());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper getLocation(bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new GetLocation());
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

        protected MissionWraper getToCertainHeight(double height_destination, bool is_async = false)
        {
            MissionWraper mission = new MissionWraper(new GetToCertainHeight(height_destination));
            if (!is_async)
            {
                mission.Wait();
            }
            return mission;
        }

       
    }
}
