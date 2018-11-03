    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class Landing : LeafMission
    {
        public Landing() : base()
        {

        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "startLanding " + m_index;
        }
    }
}
