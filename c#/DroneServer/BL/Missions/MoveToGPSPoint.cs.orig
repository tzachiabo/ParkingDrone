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
<<<<<<< HEAD
        private double m_x;
        private double m_y;
        private double m_z;

        public MoveToGPSPoint(double x, double y, double z) : base()
        {
            m_x = x;
            m_y = y;
            m_z = z;
=======
        private Point location;
        
        public MoveToGPSPoint(Point location):base()
        {
            this.location = location;
>>>>>>> 1bdffa1... add map to gui
        }

        public override void stop()
        {

        }

        public override string encode()
        {
<<<<<<< HEAD
            return "goToGPS "+ m_index + " " + m_x + " " + m_y + " " + m_z;
=======
            return "goToGPS " + m_index + " " + location.x + " " + location.y+" "+location.z;
>>>>>>> 1bdffa1... add map to gui
        }
    }
}
