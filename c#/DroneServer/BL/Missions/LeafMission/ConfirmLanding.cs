﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class ConfirmLanding : LeafMission
    {

        public ConfirmLanding(ComplexMission ParentMission = null) :base(ParentMission)
        {

        }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "confirmLanding " + m_index;
        }
    }
}
