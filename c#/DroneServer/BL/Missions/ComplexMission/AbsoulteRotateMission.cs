using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class AbsoulteRotateMission : ComplexMission
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

            double dgree_to_rotate = destinated_bearing-curr_bearing;
            Logger.getInstance().info("absoulote-rotate: current bearing: " + curr_bearing + " destination bearing: " + destinated_bearing);
  
            if (Math.Abs(destinated_bearing - curr_bearing) > 5)
            {
                MoveMission mission = null;
                if (destinated_bearing > curr_bearing)
                {
                    double rotating_amount = destinated_bearing - curr_bearing;
                    Assertions.verify(rotating_amount < 360, "it is not make sense to rotate more than 360 degree");
                    mission = new MoveMission(Direction.rotate, rotating_amount);
                }
                else
                {
                    double rotating_amount = 360 + destinated_bearing - curr_bearing - rnd.Next(0, 20);
                    Assertions.verify(rotating_amount < 360, "it is not make sense to rotate more than 360 degree");
                    mission = new MoveMission(Direction.rotate, rotating_amount);
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
