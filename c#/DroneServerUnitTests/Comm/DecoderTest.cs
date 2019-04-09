using System;
using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class DecoderTest
    {

        [TestMethod]
        public void DecodeMoveTest()
        {
            Response r = Decoder.decode("move 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeTakeOffTest()
        {
            Response r = Decoder.decode("takeOff 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeStartLandingTest()
        {
            Response r = Decoder.decode("startLanding 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeConfirmLandingTest()
        {
            Response r = Decoder.decode("confirmLanding 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeGoHomeTest()
        {
            Response r = Decoder.decode("goHome 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeMoveGimbalTest()
        {
            Response r = Decoder.decode("moveGimbal 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeGoToGPSTest()
        {
            Response r = Decoder.decode("goToGPS 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeTakePhotoTest()
        {
            Response r = Decoder.decode("takePhoto 1234 Done 5678");
            Assert.IsTrue(r.Key == 1234);
            Assert.IsTrue(r.Status == Status.Ok);
            Assert.IsTrue(r.Type == MissionType.MainMission);
            Assert.IsTrue(r.Data is string);
            Assert.IsTrue((string)r.Data == "5678");
        }
        [TestMethod]
        public void DecodeStopTest()
        {
            Response r = Decoder.decode("stop 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeGetStatusTest()
        {
            Response r = Decoder.decode("getStatus 1234 Done Connected");
            Assert.IsTrue(r.Key == 1234);
            Assert.IsTrue(r.Status == Status.Ok);
            Assert.IsTrue(r.Type == MissionType.StateMission);
            Assert.IsTrue(((DroneStatus)r.Data) == (DroneStatus.Connected));
        }
        [TestMethod]
        public void DecodeGetLocationTest()
        {
            Response r = Decoder.decode("getLocation 1234 Done 1 2 3 5");
            Assert.IsTrue(r.Key == 1234);
            Assert.IsTrue(r.Status == Status.Ok);
            Assert.IsTrue(r.Type == MissionType.StateMission);
            Assert.IsTrue(r.Data is Point);
            Point p = (Point)r.Data;
            Assert.IsTrue(p.alt == 1);
            Assert.IsTrue(p.lat == 2);
            Assert.IsTrue(p.lng == 3);
        }
        [TestMethod]
        public void DecodeSetVirtualStickTest()
        {
            Response r = Decoder.decode("setVirtualStick 1234 Done");
            SimpleVerify(r);
        }
        [TestMethod]
        public void DecodeEmptyTest()
        {
            Response r = Decoder.decode("");
            Assert.IsTrue(r.Key == 0);
            Assert.IsTrue(r.Status == Status.Ok);
            Assert.IsTrue(r.Type == MissionType.EndOfSocket);
            Assert.IsNull(r.Data);
        }


        private void SimpleVerify(Response r)
        {
            Assert.IsTrue(r.Key == 1234);
            Assert.IsTrue(r.Status == Status.Ok);
            Assert.IsTrue(r.Type == MissionType.MainMission);
            Assert.IsNull(r.Data);
        }
    }
}


