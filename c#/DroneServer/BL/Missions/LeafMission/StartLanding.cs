using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class StartLanding : LeafMission
    {
        public StartLanding() { }

        public StartLanding(ComplexMission mission) { m_ParentMission = mission; }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "startLanding " + m_index;
        }
    }
}
