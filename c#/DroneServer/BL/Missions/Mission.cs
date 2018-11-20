using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DroneServer.BL.Missions
{
    public abstract class Mission
    {
        static int NextIndex = 1;

        protected int m_version;

        public int m_index;

        protected ComplexMission m_ParentMission;

        public Mission(ComplexMission ParentMission)
        {
            m_index = getNextIndex();
            m_ParentMission = ParentMission;
            if (m_ParentMission==null)
                m_version = BLManagger.getInstance().get_version();
            else
                m_version = m_ParentMission.m_version;
        }

        public abstract void execute();

        public abstract void done(Response response);
        
        public abstract void stop();

        protected int getNextIndex()
        {
            return NextIndex++;
        }
        
        
    }
}
