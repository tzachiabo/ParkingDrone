using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    abstract class ComplexMission : Mission
    {
        protected List<Mission> m_SubMission;

        public abstract Mission notify(Mission m);
        
    }
}
