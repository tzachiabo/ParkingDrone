using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class stopMission : LeafMission
    {
        public stopMission(ComplexMission ParentMission=null) : base(ParentMission)
        {

        }

        public override void execute()
        {
            BLManagger.getInstance().increment_version();
            m_version = BLManagger.getInstance().get_version();
            base.execute();
        }

        public override string encode()
        {
            return "stop " + m_index;
        }

        public override void stop()
        {

        }

        public override void done(Response response)
        {
            foreach (notify_finished n_f in want_to_be_notified)
            {
                n_f(response);
            }

            if (m_ParentMission != null)
            {
                m_ParentMission.notify(response);
            }
        }
    }
}
