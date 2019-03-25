using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneServerIntegration
{
    public class MissionWraper
    {
        private Mission m_mission;
        private bool m_is_finished;
        public Response m_res;

        public MissionWraper(Mission mission)
        {
            m_mission = mission;
            m_is_finished = false;
            m_res = null;

            m_mission.register_to_notification(Done);
            m_mission.execute();
        }

        public void Wait()
        {
            while (!m_is_finished);
        }

        private void Done(Response res)
        {
            m_is_finished = true;
            m_res = res;
        }

    }
}
