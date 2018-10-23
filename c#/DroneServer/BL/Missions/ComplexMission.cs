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

        public void notify()
        {
            if (m_SubMission.Count == 0)
            {
                done();
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
