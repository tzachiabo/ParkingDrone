using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missionManagerAccepanceTests.Tests
{
    [TestClass]
    public class MoveToGPSTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void simpleMoveByGPS()
        {
            take_off();
            Point loc = (Point)getLocation().response.Data;
            move(DroneServer.SharedClasses.Direction.left, 10);
            //move(DroneServer.SharedClasses.Direction.up, 20);
            MoveByGPS(loc.lat, loc.lng, 10);
            loc = (Point)getLocation().response.Data;

            landing();
        }

        [TestMethod]
        public void moveToLimit()
        {
            take_off();
            Point loc = (Point)getLocation().response.Data;
            move(DroneServer.SharedClasses.Direction.right, 30);
            move(DroneServer.SharedClasses.Direction.up, 95);
            MoveByGPS(loc.lat, loc.lng, 10);
            loc = (Point)getLocation().response.Data;
            landing();
        }

        [TestMethod]
        public void moveToShortLocation()
        {
            take_off();
            Point loc = (Point)getLocation().response.Data;
            move(DroneServer.SharedClasses.Direction.backward, 2);
            move(DroneServer.SharedClasses.Direction.up, 6);
            MoveByGPS(loc.lat, loc.lng, 10);
            loc = (Point)getLocation().response.Data;
            landing();
        }

        [TestMethod]
        public void moveAfterMoveToGps()
        {
            take_off();
            Point loc = (Point)getLocation().response.Data;
            move(DroneServer.SharedClasses.Direction.backward, 2);
            move(DroneServer.SharedClasses.Direction.up, 6);
            MoveByGPS(loc.lat, loc.lng, 10);
            move(DroneServer.SharedClasses.Direction.right, 15);
            loc = (Point)getLocation().response.Data;
            landing();
        }

    }
}
