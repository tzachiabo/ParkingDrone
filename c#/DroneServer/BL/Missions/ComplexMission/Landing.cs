    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class Landing : ComplexMission
    {

        public Landing() : this(null)
        {

        }

        public Landing(ComplexMission parent_mission) : base()
        {
            m_ParentMission = parent_mission;
            m_SubMission.Enqueue(new StartLanding(this));
            m_SubMission.Enqueue(new ConfirmLanding(this));
        }

        public override void execute()
        {
            Mission mission = m_SubMission.Dequeue();

            mission.execute();
        }
        public override void done()
        {
            if (m_ParentMission != null)
                m_ParentMission.notify();
        }
        public override void stop()
        {
        }

    }
}
