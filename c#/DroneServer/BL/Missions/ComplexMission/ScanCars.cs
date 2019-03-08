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

        public ScanCars(Parking parking,String BasePhoto, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_ParentMission = ParentMission;
            Point base_point = parking.getBasePoint();
            cars = new Queue<Car>(CarDetector.getCarsFromBasePhoto(BasePhoto, base_point.alt));
            if (cars.Count > 0)
            {
                Car first = cars.Dequeue();
                ScanSingleCar scan_car = new ScanSingleCar(this);
                m_SubMission.Enqueue(scan_car);
            }
            else
            {

            }            
        }

        public override void stop()
        {

        }

        public override void notify(Response response)
        {
            if (cars.Count > 0)
            {
                Car car = cars.Dequeue();
                ScanSingleCar scan_car = new ScanSingleCar(this);
                scan_car.execute();
            }
            else
            {
                done(new Response(m_index, Status.Ok, MissionType.MainMission, null));
            }
        }

    }
}
