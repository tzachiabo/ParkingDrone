using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class EndMission : ComplexMission
    {
        stopMission m_stop_mission;
        GoHomeMission m_go_home_mission;

        public EndMission(ComplexMission parent_mission) : base(parent_mission)
        {
            m_stop_mission = new stopMission(this);
            m_SubMission.Enqueue(m_stop_mission);

        }

        public EndMission() : this(null) { }

        public new void notify(Response response)
        {
            if (response.Key == m_stop_mission.m_index)
            {
                m_go_home_mission = new GoHomeMission(this);
                m_go_home_mission.execute();
            }
            else if (response.Key == m_go_home_mission.m_index)
            {
                Landing land = new Landing(this);
                land.execute();
            }
            else
            {
                done(response);
            }
        }


        public override void stop()
        {

        }
    }
}
