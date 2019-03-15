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
        private int get_location_index;

        public GetToCertainHeight(int height_destination, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_height_destination = height_destination;
            GetLocation get_location = new GetLocation(this);
            get_location_index = get_location.m_index;
            m_SubMission.Enqueue(get_location);


            //m_SubMission.Enqueue(new MoveToGPSPoint(this, curr_position.lat, curr_position.lng, height_destination));
        }

        public override void stop()
        {
        }

        public override void notify(Response response)
        {
            if (response.Key == get_location_index)
            {
                Point location = (Point)response.Data;
                MoveToGPSPoint move_to_gps_point = new MoveToGPSPoint(this, location.lat, location.lng, m_height_destination);
                move_to_gps_point.execute();
            }
            else
            {
                done(response);
            }
        }
    }
}
