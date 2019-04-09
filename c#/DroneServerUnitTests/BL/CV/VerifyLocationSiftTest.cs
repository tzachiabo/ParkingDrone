using System;
using System.IO;
using DroneServer.BL.CV;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class VerifyLocationSiftTest
    {
       

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
