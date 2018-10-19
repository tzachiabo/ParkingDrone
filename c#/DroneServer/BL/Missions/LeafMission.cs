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
            m_ParentMission.notify(this);
        }

        public abstract string encode();
    }
}
