using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class stopMission : LeafMission
    {
        public stopMission(ComplexMission ParentMission=null) : base(ParentMission)
        {

        }

        public override string encode()
        {
            return "stop " + m_index;
        }

        public override void stop()
        {
            throw new NotImplementedException();
        }
    }
}
