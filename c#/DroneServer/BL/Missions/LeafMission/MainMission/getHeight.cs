using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class getHeight : LeafMainMission 
    {
        public getHeight(ComplexMission ParentMission = null) : base(ParentMission)
        {

        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "getHeight " + m_index;
        }
    }
}
