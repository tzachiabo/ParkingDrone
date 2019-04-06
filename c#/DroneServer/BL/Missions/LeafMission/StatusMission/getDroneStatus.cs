using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class getDroneStatus : LeafStatusMission
    {
        public getDroneStatus(ComplexMission ParentMission=null) :base(ParentMission)
        {
        }
        
        public override string encode()
        {
            return "getStatus " + m_index;
        }

        public override void stop()
        {
            Assertions.verify(false, "get status -stop not implemented yet");
        }

        public override void execute()
        {
            if (Configuration.getInstance().get("print_get_status") == "True")
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
