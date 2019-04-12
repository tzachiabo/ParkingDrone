using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class AbsoulteRotateMission : ComplexMission
    {
        private double destinated_bearing;
        Random rnd = new Random();

        public AbsoulteRotateMission(double destinated_bearing, ComplexMission ParentMission = null) : base(ParentMission)
        {
            this.destinated_bearing = destinated_bearing;
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
            double curr_bearing = location.bearing;
            double dgree_to_rotate = CircleMath.angular_distance(destinated_bearing, curr_bearing);
            Logger.getInstance().info("absoulote-rotate: current bearing: " + curr_bearing + " destination bearing: " + destinated_bearing);

            if (!is_bearing_close(destinated_bearing, curr_bearing, 5))
            {
                Direction direction = CircleMath.leftOrRight(destinated_bearing, curr_bearing);

                MoveMission mission = new MoveMission(direction, dgree_to_rotate);

                mission.register_to_notification(move_mission_finished);
                mission.execute();
            }
            else
            {
                done(new Response(m_index, Status.Ok, MissionType.MainMission, response.Data));
            }
        }

        public static bool is_bearing_close(double dest_bearing, double curr_bearing, double delta)
        {

            //double max_value_of_bearing_1 = (bearing_1 + delta) % 360;
            //double min_value_of_bearing_1 = bearing_1 - delta;

            //if (min_value_of_bearing_1 < 0)
            //{
            //    min_value_of_bearing_1 += 360;
            //}

            //if (min_value_of_bearing_1 < max_value_of_bearing_1)
            //    return min_value_of_bearing_1 <= bearing_2 && bearing_2 <= max_value_of_bearing_1;
            //else
            //    return min_value_of_bearing_1 <= bearing_2 || bearing_2 <= max_value_of_bearing_1;
            return CircleMath.is_close_angle(dest_bearing, curr_bearing, delta);

        }

        public void move_mission_finished(Response response)
        {
            System.Threading.Thread.Sleep(1500);
            GetLocation get_location = new GetLocation();

            get_location.register_to_notification(get_location_finished);
            get_location.execute();
        }

        public override void notify(Response response)
        {

        }
    }
}
