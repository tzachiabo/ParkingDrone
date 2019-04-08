using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class InitParkingMission : ComplexMission
    {
        public InitParkingMission(Parking parking, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_ParentMission = ParentMission;
            Point edge_point = parking.getMidleEdgePoint();
            Point base_point = parking.getBasePoint();

            m_SubMission.Enqueue(new AbsoluteMoveGimbalMission(this, Gimbal.left, 0, -90, 0));
            // m_SubMission.Enqueue(new MoveToGPSPoint(this, edge_point.lat, edge_point.lng, edge_point.alt));
            m_SubMission.Enqueue(new MoveToGPSPoint(this, base_point.lat, base_point.lng, base_point.alt));
            m_SubMission.Enqueue(new AbsoulteRotateMission(parking.bearing, this));
            m_SubMission.Enqueue(new TakePhoto(this));
        }

        public override void stop()
        {

        }

        public override void execute()
        {
            Logger.getInstance().info("init mission started");
            base.execute();
        }

        public override void done(Response response) 
        {
            Response res = new Response(m_index, Status.Ok, MissionType.MainMission, PicTransferServer.getLastPicPath());
            base.done(res);
        }
    }
}
