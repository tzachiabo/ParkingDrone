using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class TakePhotoTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void simpleTakePhoto()
        {
            takePicture();
        }

        [TestMethod]
        public void TakeTwoPhoto()
        {
            take_off();
            move(Direction.up, 100);
            MoveGimbal(GimbalMovementType.relative, 0, -90, 0);
            takePicture();
            move(Direction.down, 70);
            takePicture();
            landing();
        }

        [TestMethod]
        public void takePhotoAfterTakeOff()
        {
            take_off();
            takePicture();
            landing();
        }

        [TestMethod]
        public void takePhotoAfterRotateGimbal()
        {
            take_off();
            MoveGimbal(DroneServer.SharedClasses.GimbalMovementType.relative, 0, 0, 90);
            takePicture();
            landing();
        }
    }
}
