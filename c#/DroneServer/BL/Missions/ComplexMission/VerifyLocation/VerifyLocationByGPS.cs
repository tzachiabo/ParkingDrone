using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    class VerifyLocationByGPS : ComplexMission
    {

        public VerifyLocationByGPS(ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_SubMission.Enqueue(new GetLocation(this));
        }

        public override void stop()
        {
        }

        public override void notify(Response response)
        {
            Point location = (Point)response.Data;

            done(new Response(m_index, Status.Ok, MissionType.MainMission, LngLatHelper.toMarginFromBasePhoto(location)));
        }

    }
}
