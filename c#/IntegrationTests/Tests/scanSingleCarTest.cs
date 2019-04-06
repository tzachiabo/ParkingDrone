using DroneServer.BL;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class scanSingleCarTest : BaseIntegrationTest
    {
        Parking parking;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            takeoff();
            Point location = (Point)getLocation().m_res.Data;
            parking = genRandomParking(location);
            BLManagger.getInstance().set_parking(parking);
            initParkingMission(parking);
            PixelConverterHelper.init(parking.getBasePoint().alt);
            absoluteRotateMission(0);
        }

        [TestMethod]
        public void simpleScanCar()
        {            
            Car car = new Car("car", 98, 360, 500, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

        [TestMethod]
        public void scanCarAtTopLeft()
        {
            Car car = new Car("car", 98, 150, 150, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

        [TestMethod]
        public void scanCarAtTopRight()
        {
            Car car = new Car("car", 98, 1000, 150, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

        [TestMethod]
        public void scanCarAtBottomleft()
        {
            Car car = new Car("car", 98, 150, 500, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

        [TestMethod]
        public void scanCarAtBottomRight()
        {
            Car car = new Car("car", 98, 1000, 500, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

        [TestMethod]
        public void scanNarrowCar()
        {
            Car car = new Car("car", 98, 360, 500, 3, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

        [TestMethod]
        public void scanToShortCar()
        {
            Car car = new Car("car", 98, 360, 500, 30, 3, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

        [TestMethod]
        public void scansmallCar()
        {
            Car car = new Car("car", 98, 360, 500, 3, 3, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)scanSingleCarMission(curr_position, car).m_res.Data;
        }

    }
}
