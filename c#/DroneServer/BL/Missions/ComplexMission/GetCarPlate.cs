using DroneServer.BL.Comm;
using DroneServer.BL.CV;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class GetCarPlate : ComplexMission
    {
        private int m_tries = 0;
        private ReportManager report_mannager;

        public GetCarPlate(ComplexMission ParentMission = null) : base(ParentMission)
        {
            List<Tuple<double,double,double>> gimbal_rotaions = getGimbalRotation();

            double yaw = 0;
            double pitch = 0;
            MoveGimbal move_gimbal = new MoveGimbal(this, Gimbal.left, GimbalMovementType.absolute, 0,0,0);
            m_SubMission.Enqueue(move_gimbal);

            foreach (Tuple<double, double, double> gimbal_rotaion in gimbal_rotaions)
            {
                double destination_yaw = gimbal_rotaion.Item3;
                double destination_pitch = gimbal_rotaion.Item2;
                MoveGimbal move_gimbal_pos = new MoveGimbal(this, Gimbal.left, GimbalMovementType.relative, 0, destination_pitch - pitch, destination_yaw - yaw);
                m_SubMission.Enqueue(move_gimbal_pos);

                yaw = destination_yaw;
                pitch = destination_pitch;

                TakePhoto take_photo = new TakePhoto();
                take_photo.register_to_notification(after_take_photo);
                m_SubMission.Enqueue(take_photo);
            }
            report_mannager = ReportManager.getInstance();

        }

        public override void stop()
        {

        }


        private List<Tuple<double, double, double>> getGimbalRotation()
        {
            List<Tuple<double, double, double>> res = new List<Tuple<double, double, double>>();

            for (int yaw = -10; yaw < 20; yaw += 10)
            {
                //for (int pitch = -10; pitch < 20; pitch += 4)
                //{
                    res.Add(new Tuple<double, double, double>(0, 10, yaw));
                //}
            }

            return res;
        }

        private void after_take_photo(Response response)
        {
            String car_plate_photo_path = PicTransferServer.getLastPicPath();
            string car_plate = CarPlateDetector.getCarPlates(car_plate_photo_path);
            if (car_plate != "" && car_plate != " ")
            {
                report_mannager.addCarPlate(car_plate, car_plate_photo_path);
                done(new Response(m_index, Status.Ok, MissionType.MainMission, "succes"));
            }
            else
            {
                if (m_SubMission.Count > 0)
                {
                    m_SubMission.Dequeue().execute();
                }
                else
                {
                    done(new Response(m_index, Status.Ok, MissionType.MainMission, "fail"));
                }
            }
        }

        private void move_finished(Response response)
        {
            TakePhoto take_pic = new TakePhoto();

            take_pic.register_to_notification(after_take_photo);
            take_pic.execute();
        }
    }
}
