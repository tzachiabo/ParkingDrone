using System;
using DroneServer.BL.Missions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            Comm comm = Comm.getInstance();

            TakeOff take_off = new TakeOff();
            CompletionHanlder take_off_mission = comm.sendMission(take_off);

            DroneServer.BL.Missions.StartLanding start_landing = new DroneServer.BL.Missions.StartLanding();
            CompletionHanlder start_landing_mission = comm.sendMission(start_landing, true);
            start_landing_mission.wait();

            ConfirmLanding conf_landing = new ConfirmLanding();
            CompletionHanlder conf_landing_mission = comm.sendMission(conf_landing);
        }
    }
}
