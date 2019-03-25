using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses.Tests
{
    [TestClass()]
    public class CarTests
    {
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