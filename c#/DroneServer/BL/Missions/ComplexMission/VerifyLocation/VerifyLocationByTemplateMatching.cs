using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    internal class VerifyLocationByTemplateMatching : ComplexMission 
    {
        Point start_location;
        Response res;


        public VerifyLocationByTemplateMatching(ComplexMission ParentMission = null) : base(ParentMission)
        {
            AbsoluteMoveGimbalMission absolute_move_gimbal = new AbsoluteMoveGimbalMission(null, Gimbal.left, 0, -90, 0);
            absolute_move_gimbal.register_to_notification(absolute_move_gimbal_finished);
            m_SubMission.Enqueue(absolute_move_gimbal);
        }

        public override void stop()
        {

        }

        public override void execute()
        {
            Logger.getInstance().info("Start verify location");

            base.execute();
        }

        public void absolute_move_gimbal_finished(Response response)
        {
            GetLocation get_location = new GetLocation();
            get_location.register_to_notification(get_location_finished);

            get_location.execute();
        }

        public void get_location_finished(Response response)
        {
            start_location = (Point)response.Data;
            int height_of_drone_when_get_close_to_car = Int32.Parse(Configuration.getInstance().get("height_of_drone_when_get_close_to_car"));
            GetToCertainHeight get_to_ceratin_height = new GetToCertainHeight(height_of_drone_when_get_close_to_car);
            get_to_ceratin_height.register_to_notification(move_to_low_height_finished);
            get_to_ceratin_height.execute();
        }

        public void move_to_low_height_finished(Response response)
        {
            TakePhoto take_photo = new TakePhoto();
            take_photo.register_to_notification(take_photo_finished);
            take_photo.execute();
        }

        public void take_photo_finished(Response response)
        {
            String curr_photo_path = PicTransferServer.getLastPicPath();
            String base_photo_path = BLManagger.getInstance().get_base_photo_path();
            Point locationFromCV = CV.VerifyLocation.getLocation(base_photo_path, curr_photo_path);
            res = new Response(m_index, Status.Ok, MissionType.MainMission, locationFromCV);

            GetToCertainHeight back_to_the_real_height = new GetToCertainHeight(start_location.alt, this);

            back_to_the_real_height.execute();
        }

        public override void notify(Response response)
        {
            done(res);
        }

        public override void done(Response response)
        {
            Point p = (Point)response.Data;

            Logger.getInstance().info("After verify location drone location is margin-left: " + p.lng + " margin-top " + p.lat);
            base.done(response);
        }
    }
}
