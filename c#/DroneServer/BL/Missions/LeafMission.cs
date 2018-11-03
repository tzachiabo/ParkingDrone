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
<<<<<<< HEAD
        int m_version;

        public LeafMission() : base()
        {
            m_version = BLManagger.getInstance().get_version();
=======
        public LeafMission():base()
        {

>>>>>>> 1bdffa1... add map to gui
        }

        public override void done()
        {
            if(m_ParentMission != null)
            {
                m_ParentMission.notify();
            }
        }

        public override void execute()
        {
<<<<<<< HEAD
            Logger.getInstance().debug("start executing a leaf mission");
            CommManager.getInstance().execMission(this);
=======
            //CommManager.getInstance().execMission(this);
>>>>>>> 1bdffa1... add map to gui
        }

        public bool validate_version()
        {
            return m_version == BLManagger.getInstance().get_version();
        }

        public abstract string encode();
    }
}
