using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missionManagerAccepanceTests.Tests
{
    [TestClass]
    public class MoveGimbalTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void moveGimbalRollRelative()
        {
            MoveGimbal(GimbalMovementType.relative, 90, 0, 0);
            MoveGimbal(GimbalMovementType.relative, -90, 0, 0);
        }

        [TestMethod]
        public void moveGimbalPitchRelative()
        {
            MoveGimbal(GimbalMovementType.relative, 0, 90, 0);
            MoveGimbal(GimbalMovementType.relative, 0, -90, 0);
        }

        [TestMethod]
        public void moveGimbalYawRelative()
        {
            MoveGimbal(GimbalMovementType.relative, 0, 0, 90);
            MoveGimbal(GimbalMovementType.relative, 0, 0, -90);
        }

    }
}
