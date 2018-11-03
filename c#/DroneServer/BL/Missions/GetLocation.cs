using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GetLocation : LeafMission
    {
        public GetLocation():base()
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
