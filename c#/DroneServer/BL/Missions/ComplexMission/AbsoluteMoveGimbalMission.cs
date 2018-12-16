using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class AbsoluteMoveGimbalMission : ComplexMission
    {
        public AbsoluteMoveGimbalMission(ComplexMission parent_mission, Gimbal gimbal, double roll, double pitch, double yaw) : base(parent_mission)
        {
            m_SubMission.Enqueue(new MoveGimbal(this, gimbal, GimbalMovementType.absolute, 0, 0, 0));
            m_SubMission.Enqueue(new MoveGimbal(this, gimbal, GimbalMovementType.relative, roll, pitch, yaw));
        }

        public override void stop()
        {
        }
    }
}
