using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GoToCarByGPS : ComplexMission
    {
        private Car m_car;

        public GoToCarByGPS(Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_car = car;

            Point car_loc = car.getCarLocationByGPS();
            double alt = Double.Parse(Configuration.getInstance().get("height_of_drone_when_get_close_to_car"));
            m_SubMission.Enqueue(new MoveToGPSPoint(this, car_loc.lat, car_loc.lng, alt));
        }

        public override void stop()
        {

        }
    }
}
