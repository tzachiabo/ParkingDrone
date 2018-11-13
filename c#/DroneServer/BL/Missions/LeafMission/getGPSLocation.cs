using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions.LeafMission
{
    class getGPSLocation : LeafMission
    {
        public getGPSLocation() { }

        public getGPSLocation(ComplexMission mission) { m_ParentMission = mission; }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "getLocation " + m_index;
        }
    }
}
