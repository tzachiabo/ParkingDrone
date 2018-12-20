using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public abstract class ComplexMission : Mission
    {
        protected Queue<Mission> m_SubMission;
        protected CompletionHandler compHandler;

        public ComplexMission(ComplexMission ParentMission) : base(ParentMission)
        {
            compHandler = new CompletionHandler(this.m_index,10000);
            m_SubMission = new Queue<Mission>();
        }

        public override CompletionHandler execute()
        {
            Mission mission = m_SubMission.Dequeue();

            mission.execute();
            return compHandler;
        }

        public virtual void notify(Response response)
        {
            if (m_SubMission.Count == 0)
            {
                Response res = new Response(m_index, Status.Ok, MissionType.MainMission, null);
                compHandler.response = res;
                done(res);
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
