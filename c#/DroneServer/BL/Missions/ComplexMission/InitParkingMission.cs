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
        public InitParkingMission(ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_ParentMission = ParentMission;
            m_SubMission.Enqueue(new Move(this, Direction.up, 10));
            m_SubMission.Enqueue(new Move(this, Direction.forward, 100));
            m_SubMission.Enqueue(new Move(this, Direction.left, 100));
            m_SubMission.Enqueue(new Move(this, Direction.backward, 100));
            m_SubMission.Enqueue(new Move(this, Direction.right, 100));
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
