using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    class MoveToGPSPoint : LeafMission
    {
        private double m_x;
        private double m_y;
        private double m_z;

        public MoveToGPSPoint(double x, double y, double z) : base()
        {
            m_x = x;
            m_y = y;
            m_z = z;
        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "goToGPS "+ m_index + " " + m_x + " " + m_y + " " + m_z;
        }
    }
}
