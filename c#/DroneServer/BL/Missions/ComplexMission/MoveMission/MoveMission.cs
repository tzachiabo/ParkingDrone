using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class MoveMission : ComplexMission
    {
        public MoveMission(Direction direction, double distance) : this(null, direction, distance)
        {
        }

        public MoveMission(ComplexMission ParentMission, Direction direction, double distance) : base(ParentMission)
        {
            Assertions.verify(distance > 0, "cannot move with negative distance");
            m_SubMission.Enqueue(new MoveMissionImpl(this, direction, distance));
        }


        public override void stop()
        {
            
        }

        public override void notify(Response response)
        {
            done(new Response(m_index, Status.Ok, MissionType.MainMission, response.Data));
        }
    }
}
