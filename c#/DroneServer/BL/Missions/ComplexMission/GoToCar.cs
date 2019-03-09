using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GoToCar : ComplexMission
    {
        private Point m_curr_position;
        private Car m_car;

        public GoToCar(Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_curr_position = curr_position;
            m_car = car;

            build_moves();

            VerifyLocation vl = new VerifyLocation(this);
            vl.register_to_notification(verify_location_done);
            m_SubMission.Enqueue(vl);
        }

        public override void stop()
        {

        }

        private void build_moves()
        {
            Logger.getInstance().debug("start build moves");
            Point car_position = m_car.getPointOfCar();

            int height_of_drone_when_moving_in_parking = Int32.Parse(Configuration.getInstance().get("height_of_drone_when_moving_in_parking"));
            m_SubMission.Enqueue(new GetToCertainHeight(height_of_drone_when_moving_in_parking, this));

            List<KeyValuePair<Direction, double>> moves_direction = m_curr_position.get_moves(car_position);
            foreach (KeyValuePair<Direction, double> single_move in moves_direction)
            {
                Logger.getInstance().info("enqueue move mission direction : " + single_move.Key + " amount : " + single_move.Value);
                m_SubMission.Enqueue(new MoveMission(this, single_move.Key, single_move.Value));
            }

            int height_of_drone_when_get_close_to_car = Int32.Parse(Configuration.getInstance().get("height_of_drone_when_get_close_to_car"));
            m_SubMission.Enqueue(new GetToCertainHeight(height_of_drone_when_get_close_to_car, this));
        }

        public override void notify(Response response)
        {
            if (m_SubMission.Count > 0)
            {
                Mission mission = m_SubMission.Dequeue();

                Logger.getInstance().debug("start mission number : " + mission.m_index);

                mission.execute();
            }
        }

        private void verify_location_done(Response res)
        {
            Logger.getInstance().debug("GoToCar : verify location done");
            Point curr_position = (Point)res.Data;

            if (curr_position.is_close(m_car.getPointOfCar()))
            {
                Logger.getInstance().info("GoToCar : done");
                done(res);
            }
            else
            {
                Logger.getInstance().info("GoToCar : not close to car try again");
                m_curr_position = curr_position;

                build_moves();

                VerifyLocation vl = new VerifyLocation(this);
                vl.register_to_notification(verify_location_done);
                m_SubMission.Enqueue(vl);

                Mission mission = m_SubMission.Dequeue();
                mission.execute();
            }
        }

    }
}
