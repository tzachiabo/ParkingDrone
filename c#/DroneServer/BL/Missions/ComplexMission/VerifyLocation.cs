using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class VerifyLocation : ComplexMission 
    {
        public VerifyLocation(ComplexMission ParentMission = null) : base(ParentMission)
        {
        }

        public override void stop()
        {

        }

    }
}
