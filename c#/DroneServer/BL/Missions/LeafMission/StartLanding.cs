using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class StartLanding : LeafMission
    {

        public StartLanding(ComplexMission ParentMission=null) : base(ParentMission)
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
