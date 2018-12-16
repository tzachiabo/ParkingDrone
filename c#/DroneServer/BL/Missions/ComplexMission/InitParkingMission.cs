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
            Point base_point = parking.getBasePoint();
            m_SubMission.Enqueue(new MoveToGPSPoint(this, base_point.lat, base_point.lng, base_point.alt));
            m_SubMission.Enqueue(new AbsoluteMoveGimbalMission(this, Gimbal.left, 0, -90, 0));
            m_SubMission.Enqueue(new TakePhoto(this));
        }
        
        public override void stop()
        {

        }

    }
}
