using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class GetToCertainHeight : ComplexMission
    {
        private double m_height_destination;
        private int get_location_index;

        public GetToCertainHeight(double height_destination, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_height_destination = height_destination;
            GetLocation get_location = new GetLocation();

            get_location.register_to_notification(get_location_finished);
            m_SubMission.Enqueue(get_location);
        }

        public override void stop()
        {
        }

        public void get_location_finished(Response response)
        {
            Point location = (Point)response.Data;
            double curr_height = location.alt;

            double distance_to_move = Math.Abs(curr_height - m_height_destination);

            if (distance_to_move > 2)
            {
                MoveMission mission = null;
                if (curr_height > m_height_destination)
                {
                    mission = new MoveMission(Direction.down, distance_to_move);
                }
                else
                {
                    mission = new MoveMission(Direction.up, distance_to_move);
                }

                mission.register_to_notification(move_mission_finished);
                mission.execute();
            }
            else
            {
                done(new Response(m_index, Status.Ok, MissionType.MainMission, response.Data));
            }
        }

        public void move_mission_finished(Response response)
        {
            GetLocation get_location = new GetLocation();

            get_location.register_to_notification(get_location_finished);
            get_location.execute();
        }

        public override void notify(Response response)
        {

        }
    }
}
