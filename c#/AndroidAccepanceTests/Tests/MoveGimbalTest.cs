using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
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
            MoveGimbal(GimbalMovementType.relative, 0, 0, - 90);
        }

        [TestMethod]
        public void moveGimbalRollAbsolute()
        {
            MoveGimbal(GimbalMovementType.absolute, 90, 0, 0);
            MoveGimbal(GimbalMovementType.absolute, -90, 0, 0);
            MoveGimbal(GimbalMovementType.absolute, 0, 0, 0);

        }

        [TestMethod]
        public void moveGimbalPitchAbsolute()
        {
            MoveGimbal(GimbalMovementType.absolute, 0, 90, 0);
            MoveGimbal(GimbalMovementType.absolute, 0, -90, 0);
            MoveGimbal(GimbalMovementType.absolute, 0, 0, 0);
        }

        [TestMethod]
        public void moveGimbalYawAbsolute()
        {
            MoveGimbal(GimbalMovementType.absolute, 0, 0, 90);
            MoveGimbal(GimbalMovementType.absolute, 0, 0, -90);
            MoveGimbal(GimbalMovementType.absolute, 0, 0, 0);
        }
    }
}
