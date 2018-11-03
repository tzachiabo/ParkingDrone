using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    class Move : LeafMission
    {
        private double distance;
        private Direction direction;

        public Move(Direction direction, double distance):base()
        {
            this.direction = direction;
            this.distance = distance;
        }

        public override void stop()
        {

        }

        public override string encode()
        {
            
            return "move " + m_index+" "+ direction + " "+ distance;
        }
    }
}
