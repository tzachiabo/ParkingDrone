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
            move(Direction.up, 100);

            landing();
        }
    }
}
