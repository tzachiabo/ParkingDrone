using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneServer.BL.Missions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.BL;
using DroneServer.SharedClasses;

namespace UnitTestProject
{
    [TestClass()]
    public class ReportManagerTests
    {
        [TestMethod()]
        public void simple_make_report()
        {
            string cars = "1296733O1";
            string cars_2 = "1296733O1";
            PixelConverterHelper.init(50);

            Car car_1 = new Car("car", 97, 10, 10, 10, 15, 50);
            Car car_2 = new Car("car", 500, 510, 10, 10, 15, 50);

            ReportManager.getInstance().init("BL//reportManagerImages//1.JPG");
            ReportManager.getInstance().addCarPlate(cars, "BL//reportManagerImages//4.JPG", car_1);
            ReportManager.getInstance().addCarPlate(cars_2, "BL//reportManagerImages//7.JPG", car_2);

            ReportManager.getInstance().make_report("car_report.pdf");
        }

        [TestMethod()]
        public void make_report_with_lot_of_cars()
        {
            PixelConverterHelper.init(50);
            ReportManager.getInstance().init("BL//reportManagerImages//1.JPG");

            for (int i = 10; i < 25; i++)
            {
                string car = "1296733" + i;
                Car car_1 = new Car("car", i * 5, 10, 10, 10, 15, 50);
                ReportManager.getInstance().addCarPlate(car, "BL//reportManagerImages//4.JPG", car_1);

            }

            ReportManager.getInstance().make_report("car_report.pdf");
        }

        [TestMethod()]
        public void make_report_with_no_cars()
        {
            PixelConverterHelper.init(50);
            ReportManager.getInstance().init("BL//reportManagerImages//1.JPG");

            ReportManager.getInstance().make_report("car_report.pdf");
        }
    }
}