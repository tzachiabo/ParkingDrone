    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class Landing : ComplexMission
    {
<<<<<<< HEAD

        public Landing() : this(null)
=======
        public Landing() : base()
        {

        }

        public override void stop()
>>>>>>> 1bdffa1... add map to gui
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
<<<<<<< HEAD

=======
            return "startLanding " + m_index;
>>>>>>> 1bdffa1... add map to gui
        }

    }
}
