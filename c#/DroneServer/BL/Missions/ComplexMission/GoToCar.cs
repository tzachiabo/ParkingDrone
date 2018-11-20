using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GoToCar : ComplexMission
    {
        public GoToCar(ComplexMission ParentMission = null) : base(ParentMission)
        {
        }

        public override void done(Response respone)
        {

        }
        public override void stop()
        {

        }

    }
}
