using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class ScanSingleCar : ComplexMission
    {
        private Point m_curr_position;
        private int m_go_to_car_index;
        private int m_get_to_certain_height;
        private int m_move_gimbal_index;
        private Response m_res;
        private Car m_car;

        public ScanSingleCar(Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_curr_position = curr_position;
            m_car = car;

            GoToCar go_to_car = new GoToCar(m_curr_position, m_car, this);
            m_go_to_car_index = go_to_car.m_index;
            m_SubMission.Enqueue(go_to_car);
        }

        public override void stop()
        {

        }

        public override void notify(Response response)
        {
            if (response.Key == m_go_to_car_index)
            {
                m_res = response;
                int height = Int32.Parse(Configuration.getInstance().get("height_of_drone_when_get_close_to_car"));
                Mission m = new GetToCertainHeight(height, this);
                m_get_to_certain_height = m.m_index;
                m.execute();
            }
            else if (response.Key == m_get_to_certain_height)
            {
                TakePhoto take_photo = new TakePhoto(this);
                take_photo.execute();
            }
            else
            {
                Logger.getInstance().info("finish scan single car");
                BL.BLManagger.getInstance().num_of_scaned_cars++;
                Assertions.verify(m_res != null, "response should not be null at this point");
                Assertions.verify(m_res.Data != null, "response data should not be null");
                Assertions.verify(m_res.Data is Point, "response data should be point");
                done(new Response(m_index, Status.Ok, MissionType.MainMission, m_res.Data));
            }
        }

    }
}
