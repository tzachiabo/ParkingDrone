using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class GoToCar : ComplexMission
    {
        public GoToCar(SharedClasses.Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            GoToCarByRelativeMove mission = new GoToCarByRelativeMove(curr_position, car, this);
            m_SubMission.Enqueue(mission);
        }

        public override void stop()
        {

        }

        public override void notify(Response response)
        {
            done(new Response(m_index, Status.Ok, MissionType.MainMission, response.Data));
        }
    }
}
