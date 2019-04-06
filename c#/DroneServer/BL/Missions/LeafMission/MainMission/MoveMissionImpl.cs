using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    public class MoveMissionImpl : LeafMainMission
    {
        private double distance;
        private Direction direction;

        public MoveMissionImpl(Direction direction, double distance) : this(null, direction, distance)
        {
        }

        public MoveMissionImpl(ComplexMission ParentMission, Direction direction, double distance):base(ParentMission)
        {
            this.direction = direction;
            this.distance = distance;
        }

        public override void stop()
        {

        }

        public override void execute()
        {
            Logger.getInstance().info("move " + direction + " distance: " + distance);
            base.execute();
        }

        public override string encode()
        {
            return "move " + m_index + " " + direction + " " + distance;
        }
    }
}
