using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    abstract class ComplexMission : Mission
    {
        protected Queue<Mission> m_SubMission;

        public ComplexMission() : base()
        {
            m_SubMission = new Queue<Mission>();
        }

        public override void execute()
        {
            Mission mission = m_SubMission.Dequeue();

            mission.execute();
        }

        public void notify(Response response)
        {
            if (m_SubMission.Count == 0)
            {
                done(new Response(m_index, Status.Ok, MissionType.MainMission, null));
            }
            else
            {
                Mission mission = m_SubMission.Dequeue();

                Logger.getInstance().debug("start mission number : " + mission.m_index);

                mission.execute();
            }
        }

    }
}
