using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class ParkingMission : ComplexMission
    {
        protected Parking m_parking;

        public ParkingMission(Parking parking):this(null,parking)
        {

        }

        public ParkingMission(ComplexMission ParentMission, Parking parking) : base(ParentMission)
        {
            m_SubMission.Enqueue(new TakeOff(this));
            InitParkingMission init = new InitParkingMission(parking, this);
            m_SubMission.Enqueue(init);
            init.register_to_notification(init_mission_finished);
            m_parking = parking;
        }

        public override void stop()
        {

        }
        
        private void init_mission_finished(Response res)
        {
            String Base_photo_path = (String)res.Data;
            m_SubMission.Enqueue(new ScanCars(m_parking, Base_photo_path, this));
            m_SubMission.Enqueue(new ComplexGoHome(this));
            m_SubMission.Enqueue(new Landing(this));
        }

        //public void updateBasePhoto(Photo photo)
        //{

        //}
    }
}
