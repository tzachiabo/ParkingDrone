using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public abstract class LeafMission : Mission
    {
        

        public LeafMission(ComplexMission ParentMission = null) : base(ParentMission)
        {

        }

        public override void done(Response response)
        {
            if(m_ParentMission != null)
            {
                m_ParentMission.notify(response);
            }
        }

        public override void execute()
        {
            Logger.getInstance().debug("start executing a leaf mission");
            CommManager.getInstance().execMission(this);
        }

        public bool validate_version()
        {
            return m_version == BLManagger.getInstance().get_version();
        }

        public abstract string encode();
    }
}
