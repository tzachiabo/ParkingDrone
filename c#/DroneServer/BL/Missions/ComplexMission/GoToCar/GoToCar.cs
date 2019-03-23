using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GoToCar : ComplexMission
    {
        public GoToCar(SharedClasses.Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            if (Configuration.getInstance().get("go_to_car_by_gps") == "False")
            {
                GoToCarByRelativeMove mission = new GoToCarByRelativeMove(curr_position, car, this);
                m_SubMission.Enqueue(mission);
            }
            else if (Configuration.getInstance().get("go_to_car_by_gps") == "True")
            {
                GoToCarByGPS mission = new GoToCarByGPS(car, this);
                m_SubMission.Enqueue(mission);
            }
            else
            {
                Assertions.verify(false, "unpredicted path in GoToCar");
            }
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
