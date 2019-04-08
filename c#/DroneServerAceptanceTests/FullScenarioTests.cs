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
    public class FullScenarioTests : BaseAcepptanceTest
    {
        [TestMethod]
        public void NoCar()
        {
            DroneSimulator drone = new DroneSimulator();
            drone.start_drone("drone.camera.images_path=../c#/DroneServerAceptanceTests/BasePhotoImages drone.camera.base_photo_location=3.JPG");
            while (!CommManager.getInstance().isSocketInitiated) ;

            Point location = (Point)getLocation().m_res.Data;

            Parking park = genRandomParking(location);

            MissionWraper mission = BLManagger.getInstance().startMission(park);

            Assert.IsTrue(mission.Wait(60 * 5));

            int num_of_scaned_car = (int)mission.m_res.Data;
            Assert.AreEqual(num_of_scaned_car, 0);

            drone.close_drone();
        }

        [TestMethod]
        public void FewCar()
        {
            DroneSimulator drone = new DroneSimulator();
            drone.start_drone("drone.camera.images_path=../c#/DroneServerAceptanceTests/BasePhotoImages drone.camera.base_photo_location=1.JPG");
            while (!CommManager.getInstance().isSocketInitiated) ;

            Point location = (Point)getLocation().m_res.Data;

            Parking park = genRandomParking(location);

            MissionWraper mission = BLManagger.getInstance().startMission(park);

            Assert.IsTrue(mission.Wait(60 * 10));

            int num_of_scaned_car = (int)mission.m_res.Data;
            Assert.AreEqual(num_of_scaned_car, 2);

            drone.close_drone();
        }

    }
}
