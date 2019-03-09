using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class VerifyLocation : ComplexMission 
    {
        public VerifyLocation(ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_SubMission.Enqueue(new AbsoluteMoveGimbalMission(this, Gimbal.left, 0, -90, 0));
            m_SubMission.Enqueue(new TakePhoto(this));
        }

        public override void stop()
        {

        }
        public override void done(Response response)
        {
            String curr_photo_path = PicTransferServer.getLastPicPath();
            String base_photo_path = BLManagger.getInstance().get_base_photo_path();
            Point location = CV.VerifyLocation.getLocation(base_photo_path, curr_photo_path);
            base.done(new Response(m_index, Status.Ok, MissionType.MainMission, location));
        }
    }
}
