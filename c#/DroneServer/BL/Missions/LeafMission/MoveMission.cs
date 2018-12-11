using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    public class MoveMission : LeafMission
    {
        private double distance;
        private Direction direction;

        public MoveMission(Direction direction, double distance) : this(null, direction, distance)
        {
        }

        public MoveMission(ComplexMission ParentMission, Direction direction, double distance):base(ParentMission)
        {
            this.direction = direction;
            this.distance = distance;
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
