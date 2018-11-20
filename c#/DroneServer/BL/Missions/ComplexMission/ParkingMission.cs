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
            m_SubMission.Enqueue(new InitParkingMission(this));
            m_SubMission.Enqueue(new Landing(this));
            m_parking = parking;
        }

        public override void done(Response response)
        {
            if (m_ParentMission != null)
                m_ParentMission.notify(response);
        }
        public override void stop()
        {

        }

        //public void updateBasePhoto(Photo photo)
        //{

        //}
    }
}
