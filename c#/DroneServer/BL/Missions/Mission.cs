using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    abstract class Mission
    {
        protected ComplexMission m_ParentMission;


        public abstract void execute();

        public abstract void done();
        
        public abstract void stop();
        
        
    }
}
