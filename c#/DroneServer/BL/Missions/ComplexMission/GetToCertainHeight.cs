using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GetToCertainHeight : ComplexMission
    {
        private double m_height_destination;

        public GetToCertainHeight(int height_destination, ComplexMission ParentMission = null) : base(ParentMission)
        {
            Point curr_position = LocationManager.current_position;
            
            m_SubMission.Enqueue(new MoveToGPSPoint(curr_position.lng, curr_position.lng, height_destination));
        }

        public override void stop()
        {
        }
    }
}
