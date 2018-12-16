using System;
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
