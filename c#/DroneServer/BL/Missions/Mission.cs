using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DroneServer.BL.Missions
{
    public delegate void notify_finished(Response res);

    public abstract class Mission
    {
        static int NextIndex = 1;
        protected int m_version;
        public int m_index;

        protected LinkedList<notify_finished> want_to_be_notified;
        protected ComplexMission m_ParentMission;

        public Mission(ComplexMission ParentMission)
        {
            m_index = getNextIndex();
            m_ParentMission = ParentMission;
            if (m_ParentMission==null)
                m_version = BLManagger.getInstance().get_version();
            else
                m_version = m_ParentMission.m_version;

            want_to_be_notified = new LinkedList<notify_finished>();
        }

        public void register_to_notification(notify_finished func)
        {
            want_to_be_notified.AddLast(func);
        }

        public abstract void execute();

        public virtual void done(Response response) {
            foreach (notify_finished n_f in want_to_be_notified)
            {
                n_f(response);
            }
        }
        
        public abstract void stop();

        protected int getNextIndex()
        {
            return NextIndex++;
        }
        
        
    }
}
