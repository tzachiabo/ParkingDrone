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
        private int m_take_photo;
        private int tries = 0;
        private ReportManager report_mannager;

        public GetCarPlate(ComplexMission ParentMission = null) : base(ParentMission)
        {
            Tuple<double,double,double> gimbal_rotaion = getGimbalRotation();
            AbsoluteMoveGimbalMission rotate_mission = new AbsoluteMoveGimbalMission(this, Gimbal.left, gimbal_rotaion.Item1, gimbal_rotaion.Item2, gimbal_rotaion.Item3);
            TakePhoto take_photo = new TakePhoto(this);
            report_mannager = ReportManager.getInstance();
            m_take_photo = take_photo.m_index;
            m_SubMission.Enqueue(rotate_mission);
            m_SubMission.Enqueue(take_photo);
        }

        public override void stop()
        {

        }


        private Tuple<double, double, double> getGimbalRotation()
        {
            return Tuple.Create(0.0, 0.0, 0.0);
        }

        public override void notify(Response response)
        {
            if (response.Key == m_take_photo)
            {
                String car_plate_photo_path = (String)response.Data;
                List<string> cars = CarPlateDetector.getCarPlates(car_plate_photo_path);
                if (cars.Count > 0)
                {
                    report_mannager.addCarPlate(cars, car_plate_photo_path);
                    done(new Response(m_index, Status.Ok, MissionType.MainMission, response.Data));
                }
                else
                {

                }

            }

        }
    }
}
