using DroneServer.SharedClasses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class ScanCars : ComplexMission
    {
        private Queue<Car> cars;

        public ScanCars(Parking parking, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_ParentMission = ParentMission;

            Point base_point = parking.getBasePoint();

            cars = new Queue<Car>(CarDetector.getCarsFromBasePhoto(BLManagger.getInstance().get_base_photo_path(), base_point.alt));

            Logger.getInstance().info("number of cars in parking = " + cars.Count);
            if (cars.Count > 0)
            {
                VerifyLocation vl = new VerifyLocation();
                vl.register_to_notification(veridy_location_finished);
                m_SubMission.Enqueue(vl);
            }
            
        }

        public override void stop()
        {

        }

        public void veridy_location_finished(Response response)
        {
            Logger.getInstance().info("ScanCars : start scanning cars");
            Car first = cars.Dequeue();
            ScanSingleCar scan_car = new ScanSingleCar((Point) response.Data, first, this);
            scan_car.execute();
        }

        public override void notify(Response response)
        {
            if (cars.Count > 0)
            {
                Logger.getInstance().info("ScanCars :scaning next car");
                Point curr_point = (Point)response.Data;
                Car car = cars.Dequeue();
                ScanSingleCar scan_car = new ScanSingleCar(curr_point, car, this);
                scan_car.execute();
            }
            else
            {
                Logger.getInstance().info("ScanCars : finish scanning all cars");

                done(new Response(m_index, Status.Ok, MissionType.MainMission, null));
            }
        }

    }
}
