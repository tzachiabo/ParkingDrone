using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class MoveGimbal : LeafMission
    {
        private double horizontal;
        private double vertical;

        public MoveGimbal(double horizontal, double vertical):base()
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "moveGimbal " + m_index + " " + horizontal + " " + vertical;
        }
    }
}
