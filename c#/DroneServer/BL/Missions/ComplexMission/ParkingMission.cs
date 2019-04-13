using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class ParkingMission : ComplexMission
    {
        protected Parking m_parking;

        public ParkingMission(Parking parking):this(null,parking){ }

        public ParkingMission(ComplexMission ParentMission, Parking parking) : base(ParentMission)
        {
            m_parking = parking;
            BLManagger.getInstance().set_parking(parking);

            m_SubMission.Enqueue(new TakeOff(this));

            InitParkingMission init = new InitParkingMission(parking, this);
            init.register_to_notification(init_mission_finished);
            m_SubMission.Enqueue(init);
        }

        public override void stop()
        {

        }
        
        private void init_mission_finished(Response res)
        {
            String Base_photo_path = (String)res.Data;
            BLManagger.getInstance().set_base_photo_path(Base_photo_path);
            PixelConverterHelper.init(m_parking.getBasePoint().alt);
            ScanCars scan_cars = new ScanCars(m_parking, this);
            m_SubMission.Enqueue(scan_cars);
            scan_cars.register_to_notification(scan_cars_finished);
        }

        private void scan_cars_finished(Response res)
        {
            DateTime date = DateTime.Now;
            ReportManager.getInstance().make_report(m_parking.name + ".PDF");
            m_SubMission.Enqueue(new ComplexGoHome(this));
            m_SubMission.Enqueue(new Landing(this));

        }

        public override void done(Response response)
        {
            base.done(new Response(m_index, Status.Ok, MissionType.MainMission, BLManagger.getInstance().num_of_scaned_cars));
        }
    }
}
