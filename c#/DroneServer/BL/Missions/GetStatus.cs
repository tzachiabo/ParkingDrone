using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GetStatus : LeafMission
    {
        public GetStatus():base()
        {

        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "getStatus " + m_index;
        }
    }
}
