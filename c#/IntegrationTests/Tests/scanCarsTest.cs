using DroneServer.BL;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Device.Location;

namespace IntegrationTests
{
    [TestClass]
    public class scanCarsTest : BaseIntegrationTest
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
            String Base_photo_path = (String)initParkingMission(parking).m_res.Data;
            PixelConverterHelper.init(parking.getBasePoint().alt);
            BLManagger.getInstance().set_base_photo_path(Base_photo_path);
        }

        [TestMethod]
        public void simpleScanCars()
        {            
            Point position = (Point)scanCarsMission(parking).m_res.Data;
        }

    }
}
