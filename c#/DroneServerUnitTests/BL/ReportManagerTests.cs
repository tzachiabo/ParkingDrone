using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneServer.BL.Missions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.BL;

namespace UnitTestProject
{
    [TestClass()]
    public class ReportManagerTests
    {
        [TestMethod()]
        public void simple_make_report()
        {
            string cars= "1296733O1";
            string cars_2 = "1296733O1";

            ReportManager.getInstance().addCarPlate(cars, "1.JPG");
            ReportManager.getInstance().addCarPlate(cars_2, "2.JPG");

            ReportManager.getInstance().make_report("car_report.pdf");
        }
    }
}