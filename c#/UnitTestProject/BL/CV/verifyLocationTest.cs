﻿using System;
using DroneServer.BL.CV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.BL
{
    [TestClass]
    public class VerifyLocationTest
    {
        [TestMethod]
        public void verifyLocation()
        {
            Double ratio = 0.33;
            VerifyLocationTemplateMatching.getLocation("BL\\VerifyLocationTests\\big_pic.jpg", "BL\\VerifyLocationTests\\sub_big_pic.jpg", ratio);
        }

        [TestMethod]
        public void verifyLocationSmall ()
        {
            Double ratio = 0.2;
            DroneServer.SharedClasses.Point point = VerifyLocationTemplateMatching.getLocation("BL\\VerifyLocationTests\\car.jpg", "BL\\VerifyLocationTests\\sub_car.jpg", ratio);
            int i = 5;
        }
    }
}
