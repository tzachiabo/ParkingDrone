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

        public Move(ComplexMission parent_mission, Direction direction, double distance)
        {
            this.m_ParentMission = parent_mission;
            this.direction = direction;
            this.distance = distance;
        }
        public Move(Direction direction, double distance) : this(null, direction, distance)
        {
        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "move " + m_index + " " + direction + " " + distance;
        }
    }
}
