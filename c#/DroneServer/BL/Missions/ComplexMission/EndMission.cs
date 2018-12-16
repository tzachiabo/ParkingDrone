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

        public EndMission(ComplexMission parent_mission) : base(parent_mission)
        {
            stopMission m_stop_mission = new stopMission(this);
            m_SubMission.Enqueue(m_stop_mission);
        }

        public EndMission() : this(null) { }

        public override void notify(Response response)
        {
            ComplexGoHome m_go_home_mission = new ComplexGoHome();
            m_go_home_mission.register_to_notification(go_home_done);
            m_go_home_mission.execute();
        }


        public void go_home_done(Response response)
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
