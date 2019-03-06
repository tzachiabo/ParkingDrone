using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class quickPhoto : LeafMission
    {
        public quickPhoto(ComplexMission ParentMission = null) : base(ParentMission)
        {

        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "takeQuickPhoto " + m_index;
        }
    }
}
