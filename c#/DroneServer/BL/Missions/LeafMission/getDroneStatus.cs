using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    class getDroneStatus : LeafMission
    {
        public getDroneStatus() { }

        public getDroneStatus(ComplexMission mission) { m_ParentMission = mission; }
        
        public override string encode()
        {
            return "getStatus" + m_index;
        }

        public override void stop()
        {
            Assertions.verify(false, "get status -stop not implemented yet");
        }

        public override void done(Response response)
        {
            Assertions.verify(false, "get status -done not implemented yet");
        }

        public override void execute()
        {
            Logger.getInstance().debug("start executing a getStatus");
            CommManager comm_manager = CommManager.getInstance();

            if (comm_manager.isSocketInitiated)
            {
                comm_manager.execMission(this);
            }
            else
            {
                done(new Response(m_index, Status.Failed, MissionType.StateMission, DroneStatus.Disconnected));
            }
        }
    }
}
