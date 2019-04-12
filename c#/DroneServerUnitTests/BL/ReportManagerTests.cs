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
            List<string> cars= new List<string>();
            cars.Add("1296733O1");
            List<string> cars_2 = new List<string>();
            cars_2.Add("1296733O1");
            cars_2.Add("1296734O2");
            cars_2.Add("1296734O3");
            cars_2.Add("1296734O4");
            cars_2.Add("1296734O5");
            cars_2.Add("1296734O6"); 
            cars_2.Add("1296734O7");
            cars_2.Add("1296734O8");
            cars_2.Add("1296734O9");
            cars_2.Add("129673411");
            cars_2.Add("129673421");
            cars_2.Add("129673431");
            cars_2.Add("129673441");
            ReportManager.getInstance().addCarPlate(cars, "1.JPG");
            ReportManager.getInstance().addCarPlate(cars_2, "2.JPG");

            ReportManager.getInstance().make_report("car_report.pdf");
        }
    }
}