using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    internal class GoToCarByRelativeMove : ComplexMission
    {
        private Point m_curr_position;
        private Car m_car;
        private Response response;

        public GoToCarByRelativeMove(Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_curr_position = curr_position;
            m_car = car;

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

            double height_of_drone_when_moving_in_parking = Double.Parse(Configuration.getInstance().get("height_of_drone_when_moving_in_parking"));
            m_SubMission.Enqueue(new GetToCertainHeight(height_of_drone_when_moving_in_parking, this));

            build_moves();

            VerifyLocation vl = new VerifyLocation();
            vl.register_to_notification(verify_location_done);
            m_SubMission.Enqueue(vl);

            Mission m = m_SubMission.Dequeue();
            m.execute();
        }

        private void build_moves()
        {
            Logger.getInstance().info("GoToCar : start build moves");
            Point car_position = m_car.getPointOfCar();  // inside_pic
            Logger.getInstance().info("GoToCar at position margin-left: " + car_position.lng + " margin-top: " + car_position.lat);
            double pic_bearing = BL.BLManagger.getInstance().get_parking().bearing;

            double relative_bearing = LngLatHelper.getBearingBetweenMarginPoints(m_curr_position, car_position);
            double absoulte_rotate_des = (relative_bearing + pic_bearing) % 360;
            m_SubMission.Enqueue(new AbsoulteRotateMission(absoulte_rotate_des, this));

            double distance = LngLatHelper.getDistanceBetweenMarginPoints(m_curr_position, car_position);
            m_SubMission.Enqueue(new MoveMission(this, Direction.forward, distance));
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
                Logger.getInstance().info("GoToCar : got to the car pic at position " + PicTransferServer.getLastPicPath());
                response = res;

                double pic_bearing = BL.BLManagger.getInstance().get_parking().bearing;

                Mission m = new AbsoulteRotateMission(pic_bearing);
                m.register_to_notification(final_mission);
                m.execute();
            }
            else
            {
                Logger.getInstance().info("GoToCar : not close to car try again");
                m_curr_position = curr_position;

                build_moves();

                VerifyLocation vl = new VerifyLocation();
                vl.register_to_notification(verify_location_done);
                m_SubMission.Enqueue(vl);

                Mission mission = m_SubMission.Dequeue();
                mission.execute();
            }
        }

        private void final_mission(Response res)
        {
            done(new Response(m_index, Status.Ok, MissionType.MainMission, response.Data));
        }

    }
}
