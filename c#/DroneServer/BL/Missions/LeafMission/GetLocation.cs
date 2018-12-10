using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class GetLocation : LeafMission
    {


        public GetLocation(ComplexMission ParentMission = null) :base(ParentMission)
        {
        }

        public GetLocation()
        {
        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "getLocation " + m_index;
        }
    }
}
