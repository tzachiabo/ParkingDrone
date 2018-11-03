﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    class MoveToGPSPoint : LeafMission
    {
        private Point location;
        
        public MoveToGPSPoint(Point location):base()
        {
            this.location = location;
        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "goToGPS " + m_index + " " + location.x + " " + location.y+" "+location.z;
        }
    }
}
