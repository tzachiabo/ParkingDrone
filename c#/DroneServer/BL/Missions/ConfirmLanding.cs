using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class ConfirmLanding : LeafMission
    {
        public ConfirmLanding() { }

        public ConfirmLanding(ComplexMission mission) { m_ParentMission = mission; }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "confirmLanding " + m_index;
        }
    }
}
