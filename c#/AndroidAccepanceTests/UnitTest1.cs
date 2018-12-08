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
            take_off_mission.wait();

            StartLanding start_landing = new StartLanding();
            CompletionHanlder start_landing_mission = comm.sendMission(start_landing);
            start_landing_mission.wait();

            ConfirmLanding conf_landing = new ConfirmLanding();
            CompletionHanlder conf_landing_mission = comm.sendMission(conf_landing);
            conf_landing_mission.wait();
        }
    }
}
