﻿using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GetCarPlate : ComplexMission
    {
        public GetCarPlate(ComplexMission ParentMission = null) : base(ParentMission)
        {
        }

        public override void stop()
        {

        }

    }
}
