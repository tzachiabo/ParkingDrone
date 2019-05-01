using System;
using System.Collections.Generic;
using System.IO;
using DroneServer.BL.CV;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class CarPlateDetectorTest
    {

        [TestMethod]
        public void get_car_plate2()
        {
            String car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/car2.JPG");

            Assert.IsTrue(car_plates == "29673301");
        }

        [TestMethod]
        public void get_car_plate3()
        {
            String car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/car3.JPG");

            Assert.IsTrue(car_plates != "");
        }

        [TestMethod]
        public void get_car_plate5()
        {
            String car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/car5.JPG");

            Assert.IsTrue(car_plates != "");
        }

        [TestMethod]
        public void get_car_plate_no_cars()
        {
            String car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/2.JPG");

            Assert.IsTrue(car_plates == "");
        }
    }
}
