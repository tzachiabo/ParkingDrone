using DroneServer.BL;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class goToCarTest : BaseIntegrationTest
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
        public void simpleGoToCar()
        {            
            Car car = new Car("car", 98, 360, 500, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

        [TestMethod]
        public void goToCarAtTopLeft()
        {
            Car car = new Car("car", 98, 1, 1, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

        [TestMethod]
        public void goToCarAtTopRight()
        {
            Car car = new Car("car", 98, 1279, 1, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

        [TestMethod]
        public void goToCarAtBottomleft()
        {
            Car car = new Car("car", 98, 1, 719, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

        [TestMethod]
        public void goToCarAtBottomRight()
        {
            Car car = new Car("car", 98, 1279, 719, 30, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

        [TestMethod]
        public void goToNarrowCar()
        {
            Car car = new Car("car", 98, 360, 500, 3, 30, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

        [TestMethod]
        public void goToShortCar()
        {
            Car car = new Car("car", 98, 360, 500, 30, 3, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

        [TestMethod]
        public void goTosmallCar()
        {
            Car car = new Car("car", 98, 360, 500, 3, 3, parking.getBasePoint().alt);
            Point curr_position = (Point)verifyLocationMission().m_res.Data;

            Point position = (Point)goToCarMission(curr_position, car).m_res.Data;

            Assert.IsTrue(position.is_close(car.getPointOfCar()));
        }

    }
}
