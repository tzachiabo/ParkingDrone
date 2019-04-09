using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass()]
    public class CarTests
    {


        [TestMethod()]
        public void getPointOfCarTest()
        {
            PixelConverterHelper.init(50);
            Car loc = new Car("car", 97, 10, 10, 10, 15, 50);
            Point p = loc.getPointOfCar();

            Assert.AreEqual(0.63867187500000022, p.lng);
            Assert.AreEqual(0.78993055555555558, p.lat);
        }

        [TestMethod()]
        public void FindPointAtDistanceFromForwardTest()
        {
            Car.GeoLocation geo_base_loc = new Car.GeoLocation();
            geo_base_loc.Latitude = 0;
            geo_base_loc.Longitude = 0;

            Car.GeoLocation geo_res = Car.FindPointAtDistanceFrom(geo_base_loc, 0, 10);

            Assert.AreEqual(geo_res.Longitude, 0);
            Assert.AreEqual(geo_res.Latitude, 0.089932019433468666);
        }

        [TestMethod()]
        public void FindPointAtDistanceFromLeftTest()
        {
            Car.GeoLocation geo_base_loc = new Car.GeoLocation();
            geo_base_loc.Latitude = 0;
            geo_base_loc.Longitude = 0;

            Car.GeoLocation geo_res = Car.FindPointAtDistanceFrom(geo_base_loc, Math.PI/2, 10);

            //Assert.AreEqual(geo_res.Longitude, 0);
            //Assert.AreEqual(geo_res.Latitude, 0.089932019433468666);
        }
    }
}