using DroneServer.BL.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    abstract class LeafMission : Mission
    {
        public LeafMission():base()
        {

        }

        public override void done()
        {
            if(m_ParentMission != null)
            {
                m_ParentMission.notify(this);
            }
        }

        public override void execute()
        {
            //CommManager.getInstance().execMission(this);
        }

        public abstract string encode();
    }
}
