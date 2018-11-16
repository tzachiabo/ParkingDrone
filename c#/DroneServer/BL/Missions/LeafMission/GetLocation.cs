using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GetLocation : LeafMission
    {
        public GetLocation() { }

        public GetLocation(ComplexMission mission) { m_ParentMission = mission; }

        public override void stop()
        {

        }

        public override string encode()
        {
            return "getLocation " + m_index;
        }

        public override void done(Response response)
        {
            Point p = (Point)response.Data;
            Logger.getInstance().debug("update map location with this params :" + p.y + " " + p.x);
            BLManagger.getInstance().setLocation(p.y, p.x);   
            base.done(response);
        }
    }
}
