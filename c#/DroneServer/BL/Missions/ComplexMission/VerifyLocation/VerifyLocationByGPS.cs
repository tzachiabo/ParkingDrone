using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public override void execute()
        {
            Thread.Sleep(2000);
            base.execute();
        }

        public override void notify(Response response)
        {
            Point location = (Point)response.Data;
            Point location_in_matgins = LngLatHelper.toMarginFromBasePhoto(location);

            Logger.getInstance().info("location is lng: " + location.lng + " lat: " + location.lat + " alt: " + location.alt);
            Logger.getInstance().info("location in margin x: " + location_in_matgins.lng + " y: " + location_in_matgins.lat);

            done(new Response(m_index, Status.Ok, MissionType.MainMission, location_in_matgins));
        }

    }
}
