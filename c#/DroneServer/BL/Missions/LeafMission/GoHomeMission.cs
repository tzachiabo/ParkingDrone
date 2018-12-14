using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class GoHomeMission : LeafMission
    {

        public GoHomeMission(ComplexMission ParentMission = null) : base(ParentMission)
        {

        }

        public override string encode()
        {
            return "goHome " + m_index;
        }

        public override void stop()
        {
           
        }
    }
}
