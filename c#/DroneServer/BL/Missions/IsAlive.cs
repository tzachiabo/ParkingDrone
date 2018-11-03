using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class IsAlive : LeafMission
    {
        public IsAlive():base()
        {

        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "isAlive " + m_index;
        }
    }
}
