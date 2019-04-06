using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public abstract class LeafMainMission : LeafMission
    {
        public LeafMainMission(ComplexMission ParentMission = null) : base(ParentMission)
        {

        }

        public override bool isMainMission()
        {
            return true;
        }
    }
}
