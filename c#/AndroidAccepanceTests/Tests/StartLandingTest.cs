using System;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class StartLanding : BaseAcceptanceTest
    {
        [TestMethod]
        public void simpleStartLanding()
        {
            take_off();
            landing();
        }

        [TestMethod]
        public void StartLandingFromMaxHight()
        {
            take_off();
            for (int i = 0; i< 50; i++) {
                move(Direction.up, 2);
                System.Threading.Thread.Sleep(200);
            }

            for(int i = 0; i < 600; i++) {
                move(Direction.forward, 1);
                System.Threading.Thread.Sleep(200);
            }
            //move(Direction.forward, 300);
            landing();
        }
    }
}
