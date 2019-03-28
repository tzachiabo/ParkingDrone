using System;
using System.IO;
using DroneServer.BL.CV;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.BL
{
    [TestClass]
    public class VerifyLocationSiftTest
    {
        //[TestMethod]
        //public void verifyLocation()
        //{
        //    Double ratio = 0.33;
        //    VerifyLocationTemplateMatching.getLocation("BL\\VerifyLocationTests\\big_pic.jpg", "BL\\VerifyLocationTests\\sub_big_pic.jpg", ratio);
        //}

        //[TestMethod]
        //public void verifyLocationSmall ()
        //{
        //    Double ratio = 0.2;
        //    DroneServer.SharedClasses.Point point = VerifyLocationTemplateMatching.getLocation("BL\\VerifyLocationTests\\car.jpg", "BL\\VerifyLocationTests\\sub_car.jpg", ratio);
        //    int i = 5;
        //}

        [TestMethod]
        public void verifyLocation()
        {
            PixelConverterHelper.init(52);
            Point p1 = VerifyLocationSift.getLocation(@"./BL/CV/VerifyLocationTestFiles/b.jpg", @"./BL/CV/VerifyLocationTestFiles/s.jpg", 52.0 / 17);
            Point p2 = new Point(PixelConverterHelper.convert_width(455), PixelConverterHelper.convert_height(317));

            Assert.IsTrue(p1.lat == p2.lat);
            Assert.IsTrue(p1.lng == p2.lng);


        }
    }
}
