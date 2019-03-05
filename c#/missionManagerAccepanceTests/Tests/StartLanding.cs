using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missionManagerAccepanceTests.Tests
{
    [TestClass]
    public class StartLanding : BaseAcceptanceTest
    {
        [TestMethod]
        public void StartLandingFromMaxHight()
        {
            take_off();
            move(Direction.up, 100);

            landing();
        }
    }
}
