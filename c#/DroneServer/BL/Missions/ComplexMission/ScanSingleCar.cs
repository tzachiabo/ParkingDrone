using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class ScanSingleCar : ComplexMission
    {
        private Point curr_position;
        private Point car_position;

        public ScanSingleCar(ComplexMission ParentMission = null) : base(ParentMission)
        {
        }

        public override void stop()
        {

        }

    }
}
