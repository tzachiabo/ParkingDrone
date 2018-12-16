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

        public AbortMission(ComplexMission parent_mission = null) : base(parent_mission)
        {
            m_stop_mission = new stopMission(this);
            m_SubMission.Enqueue(m_stop_mission);
        }

        public override void notify(Response response)
        {
            Landing land = new Landing();
            land.register_to_notification(done);
            land.execute();
        }

        public override void stop()
        {
           
        }
    }
}
