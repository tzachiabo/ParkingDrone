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
            getHeight get_height = new getHeight();

            get_height.register_to_notification(get_height_finished);
            m_SubMission.Enqueue(get_height);
        }

        public override void stop()
        {
        }

        public void get_height_finished(Response response)
        {
            double curr_height = (double)response.Data;

            double distance_to_move = Math.Abs(curr_height - m_height_destination);

            if (distance_to_move > 0.5)
            {
                MoveMission mission = null;
                if (curr_height > m_height_destination)
                {
                    if (distance_to_move > 3)
                        distance_to_move -= 1; // wierd fix
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
            getHeight get_height = new getHeight();

            get_height.register_to_notification(get_height_finished);
            get_height.execute();
        }

        public override void notify(Response response)
        {

        }
    }
}
