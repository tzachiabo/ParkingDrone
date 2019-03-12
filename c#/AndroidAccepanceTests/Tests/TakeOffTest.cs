using System;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class TakeOffTest : BaseAcceptanceTest
    {
        private Comm comm = Comm.getInstance();

        private void take_off()
        { 
            TakeOff take_off = new TakeOff();
            CompletionHanlder take_off_mission = comm.sendMission(take_off);
        }

        public void landing()
        {
            DroneServer.BL.Missions.StartLanding start_landing = new DroneServer.BL.Missions.StartLanding();
            CompletionHanlder start_landing_mission = comm.sendMission(start_landing, true);
            start_landing_mission.wait();

            //ConfirmLanding conf_landing = new ConfirmLanding();
            //CompletionHanlder conf_landing_mission = comm.sendMission(conf_landing);
        }

        [TestMethod]
        public void simpleTakeOff()
        {
            take_off();

            GetLocation get_loation = new GetLocation();
            CompletionHanlder get_loation_mission = comm.sendMission(get_loation);
            Point loc = (Point)get_loation_mission.response.Data;
            Assert.IsTrue(loc.alt > 0.5);

            landing();
        }

        [TestMethod]
        public void takeOffAfterLanding()
        {
            take_off();
            landing();
            take_off();

            GetLocation get_loation = new GetLocation();
            CompletionHanlder get_loation_mission = comm.sendMission(get_loation);
            Point loc = (Point)get_loation_mission.response.Data;
            Assert.IsTrue(loc.alt > 0.5);
            landing();
        }

         

    }
}
