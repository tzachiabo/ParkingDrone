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
        //protected ParkingLotManager m_PLM;

        public ParkingMission() : base()
        {
            m_SubMission.Enqueue(new TakeOff(this));
            m_SubMission.Enqueue(new InitParkingMission(this));
            m_SubMission.Enqueue(new Landing(this));
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
