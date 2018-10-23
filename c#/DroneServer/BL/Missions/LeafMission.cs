using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    abstract class LeafMission : Mission
    {
        public override void done()
        {
            if(m_ParentMission != null)
            {
                m_ParentMission.notify();
            }
        }

        public override void execute()
        {
            Logger.getInstance().debug("start executing a leaf mission");
            CommManager.getInstance().execMission(this);
        }

        public abstract string encode();
    }
}
