using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class InitParkingMission : ComplexMission
    {
        public InitParkingMission() : this(null) { }

        public InitParkingMission(ComplexMission parent_mission) : base()
        {
            m_ParentMission = parent_mission;
            m_SubMission.Enqueue(new Move(this, Direction.up, 10));
            m_SubMission.Enqueue(new Move(this, Direction.forward, 10));
            m_SubMission.Enqueue(new Move(this, Direction.left, 10));
            m_SubMission.Enqueue(new Move(this, Direction.backward, 10));
            m_SubMission.Enqueue(new Move(this, Direction.right, 10));
        }

        public override void done(Response response)
        {
            if (m_ParentMission != null)
                m_ParentMission.notify(response);
        }
        public override void stop()
        {

        }

    }
}
