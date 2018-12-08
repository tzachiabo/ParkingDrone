using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    public class AbortMission : ComplexMission
    {
        stopMission m_stop_mission;

        public AbortMission(ComplexMission parent_mission): base(parent_mission)
        {
            m_stop_mission = new stopMission(this);
            m_SubMission.Enqueue(m_stop_mission);
        }

        public AbortMission() : this(null) { }

        public new void notify(Response response)
        {
            if (response.Key == m_stop_mission.m_index)
            {
                Landing land = new Landing(this);
                land.execute();
            }
            else
            {
                done(response);
            }
        }

        public override void done(Response response)
        {
            
        }

        public override void stop()
        {
           
        }
    }
}
