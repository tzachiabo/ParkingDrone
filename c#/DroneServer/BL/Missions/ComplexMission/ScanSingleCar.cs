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
        private Car m_car;

        public ScanSingleCar(Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_curr_position = curr_position;
            m_car = car;

            m_SubMission.Enqueue(new GoToCar(m_curr_position, m_car, this));
        }

        public override void stop()
        {

        }

        public override void notify(Response response)
        {
            Logger.getInstance().info("finish scan single car");
            done(response);
        }

    }
}
