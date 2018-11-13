using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class MoveGimbal : LeafMission
    {
        private Gimbal m_gimbal;
        private GimbalMovementType m_gimbal_movement_type;
        private double m_roll;
        private double m_pitch;
        private double m_yaw;

        public MoveGimbal(Gimbal gimbal, GimbalMovementType gimbal_movement_type, double roll, double pitch, double yaw) : base()
        {
            m_gimbal = gimbal;
            m_gimbal_movement_type = gimbal_movement_type;
            m_roll = roll;
            m_pitch = pitch;
            m_yaw = yaw;
        }


        public override string encode()
        {
            return "moveGimbal " + m_index + " " + m_gimbal + " " + m_gimbal_movement_type + " " + m_roll + " " + m_pitch + " " + m_yaw;
        }

        public override void stop()
        {
            throw new NotImplementedException();
        }
    }
}
