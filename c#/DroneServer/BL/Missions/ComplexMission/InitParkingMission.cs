using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class InitParkingMission : ComplexMission
    {
        public InitParkingMission(Parking parking, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_ParentMission = ParentMission;
            //m_SubMission.Enqueue(new Move(this, Direction.up, 10));
            Point base_point = parking.getBasePoint();
            m_SubMission.Enqueue(new MoveToGPSPoint(this, base_point.x, base_point.y, base_point.z));
            m_SubMission.Enqueue(new MoveGimbal(this, Gimbal.left, GimbalMovementType.relative, 0, -90, 0));
            m_SubMission.Enqueue(new TakePhoto(this));
        }

        public override void done(Response response)
        {
            if (m_ParentMission != null)
                m_ParentMission.notify(response);

            base.done(response);
        }
        public override void stop()
        {

        }

    }
}
