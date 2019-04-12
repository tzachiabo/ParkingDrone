using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.SharedClasses
{
    [TestClass]
    public class CircleMathTest
    {
        [TestMethod]
        public void pick_right_direction()
        {
            Assert.IsTrue(CircleMath.leftOrRight(50, 30) == Direction.rtt_right);
            Assert.IsTrue(CircleMath.leftOrRight(220, 0) == Direction.rtt_left);
            Assert.IsTrue(CircleMath.leftOrRight(-10, 30) == Direction.rtt_left);
            Assert.IsTrue(CircleMath.leftOrRight(120, 30) == Direction.rtt_right);
            Assert.IsTrue(CircleMath.leftOrRight(179, 0) == Direction.rtt_right);
            Assert.IsTrue(CircleMath.leftOrRight(180, 0) == Direction.rtt_right);
            Assert.IsTrue(CircleMath.leftOrRight(181, 0) == Direction.rtt_left);
        }
    }
}
