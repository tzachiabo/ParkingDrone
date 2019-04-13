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
            List<String> car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/car2.JPG");

            Assert.IsTrue(car_plates[0] == "296733O1");
        }

        [TestMethod]
        public void get_car_plate3()
        {
            List<String> car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/car3.JPG");

            Assert.IsTrue(car_plates[0] == "5954237");
        }

        [TestMethod]
        public void get_car_plate5()
        {
            List<String> car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/car5.JPG");

            Assert.IsTrue(car_plates[0] == "8954478");
        }

        [TestMethod]
        public void get_car_plate_no_cars()
        {
            List<String> car_plates = CarPlateDetector.getCarPlates(@"./BL/CV/CarPlateDetectorTestFiles/2.JPG");

            Assert.IsTrue(car_plates.Count == 0);
        }
    }
}
