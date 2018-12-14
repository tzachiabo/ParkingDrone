using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class GoGomeTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void simpleGoHome()
        {
            take_off();
            move(Direction.up, 5);
            move(Direction.forward, 20);
            goHome();
            landing();
        }
    }
}
