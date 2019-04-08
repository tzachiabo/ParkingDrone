using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Collections.Generic;
using DroneServer.SharedClasses;
using DroneServer;

namespace UnitTestProject
{
    [TestClass]
    public class DBTest
    {
        static string cs = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName + @"\DroneServer\DL\DroneDB.mdf;";

        [TestInitialize]
        public void TestInitialize()
        {
            SqlConnection con;
            SqlCommand cmd;
            con = new SqlConnection(cs);

            string qry = "DELETE from Parking; " + "DELETE from BorderPoint;" ;

            con.Open();
            cmd = new SqlCommand(qry, con);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("err");
                cmd.Dispose();
                con.Close();
                return;
            }
            cmd.Dispose();
            con.Close();
        }

        [TestMethod]
        public void SimpleAddParking()
        {
            List<Point> points = new List<Point>();
            for(int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 0, points);
            DB.addParking(park);
            List<Parking> parkings = DB.selectAllParkings();
            Assert.IsTrue(parkings.Count==1);
            Assert.IsTrue(parkings[0].name.Equals("aviad park"));
            Assert.IsTrue(parkings[0].lat==0);
            Assert.IsTrue(parkings[0].lng == 0);
            Assert.IsTrue(parkings[0].maxZoom == 0);
            Assert.IsTrue(parkings[0].minZoom == 0);
            Assert.IsTrue(parkings[0].zoom == 0);
            Assert.IsTrue(parkings[0].border.Count == points.Count);
        }
        [TestMethod]
        public void AddParkingWithoutBorder()
        {
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 0, null);
            DB.addParking(park);
            List<Parking> parkings = DB.selectAllParkings();
            Assert.IsTrue(parkings.Count == 0);
        }
        [TestMethod]
        public void AddParkingEmptyName()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("", 0, 0, 0, 0, 0, 37, points);
            
            DB.addParking(park);
            List<Parking> parkings = DB.selectAllParkings();
            Assert.IsTrue(parkings.Count == 0);
        }
        [TestMethod]
        public void simpleExistParkingName()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 0, points);

            DB.addParking(park);
            Assert.IsTrue(DB.existParkingName("aviad park"));
        }
        [TestMethod]
        public void simpleNotExistParkingName()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 0, points);

            DB.addParking(park);
            Assert.IsFalse(DB.existParkingName("bar park"));
        }
        [TestMethod]
        public void emptyDBExistParkingName()
        {
            Assert.IsFalse(DB.existParkingName("bar park"));
        }
        [TestMethod]
        public void emptyNameExistParkingName()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 0, points);

            DB.addParking(park);
            Assert.IsFalse(DB.existParkingName(""));
        }
        [TestMethod]
        public void simpleDeleteParking()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 0, points);

            DB.addParking(park);
            DB.deleteParking("aviad park");
            Assert.IsTrue(DB.selectAllParkings().Count==0);
        }
        [TestMethod]
        public void deleteParkingNotExist()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 0, points);

            DB.addParking(park);
            DB.deleteParking("aviad");
            Assert.IsTrue(DB.selectAllParkings().Count == 1);
        }
        [TestMethod]
        public void emptyDBDeleteParkingNotExist()
        {
            DB.deleteParking("aviad");
            Assert.IsTrue(DB.selectAllParkings().Count == 0);
        }
        [TestMethod]
        public void emptyNameDeleteParking()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 4; i++)
            {
                points.Add(new Point(i, i, i));
            }
            Parking park = new Parking("aviad park", 0, 0, 0, 0, 0, 37, points);

            DB.addParking(park);
            DB.deleteParking("");
            Assert.IsTrue(DB.selectAllParkings().Count == 1);
        }


    }
}
