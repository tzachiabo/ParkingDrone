using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    public class RotateMission : LeafMission
    {
        private double degrees;
        private bool right;


        public RotateMission(double degrees) : this(null, true, degrees)
        {
        }
        public RotateMission(bool right, double degrees) : this(null, right, degrees)
        {
        }
        public RotateMission(ComplexMission ParentMission, bool right, double degrees) :base(ParentMission)
        {
            this.right = right;
            this.degrees = degrees;
        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "rotate " + m_index + " " + right + " " + degrees;
        }
    }
}
