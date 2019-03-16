using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class ScanSingleCar : ComplexMission
    {
        private Point m_curr_position;
        private int m_go_to_car_index;
        private Response m_res;
        private Car m_car;

        public ScanSingleCar(Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_curr_position = curr_position;
            m_car = car;

            GoToCar go_to_car = new GoToCar(m_curr_position, m_car, this);
            m_go_to_car_index = go_to_car.m_index;
            m_SubMission.Enqueue(go_to_car);
            m_SubMission.Enqueue(new GetToCertainHeight(3, this));
        }

        public override void stop()
        {

        }

        public override void notify(Response response)
        {
            if (response.Key == m_go_to_car_index)
            {
                m_res = response;

                Mission m = m_SubMission.Dequeue();
                m.execute();
            }
            else
            {
                Logger.getInstance().info("finish scan single car");
                done(response);
            }
        }

    }
}
