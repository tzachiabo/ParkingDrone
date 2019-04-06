using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class TakeOff : LeafMainMission
    {
        public TakeOff(ComplexMission ParentMission=null) : base(ParentMission)
        {

        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "takeOff " + m_index;
        }

    }
}
