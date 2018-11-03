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

<<<<<<< HEAD
        public Move(ComplexMission parent_mission, Direction direction, double distance)
=======
        public Move(Direction direction, double distance):base()
>>>>>>> 1bdffa1... add map to gui
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
<<<<<<< HEAD
            return "move " + m_index + " " + direction + " " + distance;
=======
            
            return "move " + m_index+" "+ direction + " "+ distance;
>>>>>>> 1bdffa1... add map to gui
        }
    }
}
