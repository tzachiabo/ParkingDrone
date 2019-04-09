using System;
using System.Collections.Generic;
using DroneServer.BL;
using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using DroneServerIntegration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DroneServerAceptanceTests
{
    [TestClass]
    public class ParkingAcceptanceTests : BaseAcepptanceTest
    {

        [TestMethod]
        public void CreateGoodParking()
        {
            int num_of_parking = BLManagger.getInstance().DBGetAllParkings().Count;
            string parking_name = "parking_acceptanceTests";
            List<Point> border = new List<Point>();
            border.Add(new Point(31.2649332655875, 34.8064202070236));
            border.Add(new Point(31.2647498502423, 34.8063397407532));
            border.Add(new Point(31.264896582547, 34.8066079616547));
            border.Add(new Point(31.2647269232991, 34.8065274953842));
            Parking park = new Parking(parking_name, border);
            BLManagger.getInstance().DBAddParking(park);
            Assert.IsTrue(BLManagger.getInstance().DBExistParkingName(parking_name));

            BLManagger.getInstance().DBDeleteParking(parking_name);
            Assert.AreEqual(num_of_parking, BLManagger.getInstance().DBGetAllParkings().Count);
        }

        [TestMethod]
        public void RemoveParking()
        {
            int num_of_parking = BLManagger.getInstance().DBGetAllParkings().Count;

            string parking_name = "parking_acceptanceTests";
            List<Point> border = new List<Point>();
            border.Add(new Point(31.2649332655875, 34.8064202070236));
            border.Add(new Point(31.2647498502423, 34.8063397407532));
            border.Add(new Point(31.264896582547, 34.8066079616547));
            border.Add(new Point(31.2647269232991, 34.8065274953842));
            Parking park = new Parking(parking_name, border);
            BLManagger.getInstance().DBAddParking(park);
            Assert.IsTrue(BLManagger.getInstance().DBExistParkingName(parking_name));

            BLManagger.getInstance().DBDeleteParking(parking_name);

            Assert.IsFalse(BLManagger.getInstance().DBExistParkingName(parking_name));
            Assert.AreEqual(num_of_parking, BLManagger.getInstance().DBGetAllParkings().Count);

        }

        [TestMethod]
        public void CreateTooBigParking()
        {
            int num_of_parking = BLManagger.getInstance().DBGetAllParkings().Count;
            string parking_name = "parking_acceptanceTests";
            List<Point> border = new List<Point>();
            border.Add(new Point(1, 1));
            border.Add(new Point(31.2647498502423, 34.8063397407532));
            border.Add(new Point(31.264896582547, 34.8066079616547));
            border.Add(new Point(31.2647269232991, 34.8065274953842));
            Parking park = new Parking(parking_name, border);
            Assert.IsFalse(BLManagger.getInstance().DBAddParking(park));
           
            Assert.AreEqual(num_of_parking, BLManagger.getInstance().DBGetAllParkings().Count);
        }




    }
}
