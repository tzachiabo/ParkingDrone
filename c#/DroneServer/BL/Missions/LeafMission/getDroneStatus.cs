using DroneServer.BL.Comm;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class getDroneStatus : LeafMission
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

        public override CompletionHandler execute()
        {
            if (Configuration.getInstance().get("print_get_status") == "True")
                Logger.getInstance().debug("start executing a getStatus");

            CommManager comm_manager = CommManager.getInstance();

            if (comm_manager.isSocketInitiated)
            {
                return comm_manager.execMission(this);
            }
            else
            {
                Response res = new Response(m_index, Status.Failed, MissionType.StateMission, DroneStatus.Disconnected);
                CompletionHandler compHandler = new CompletionHandler(this.m_index);
                compHandler.response = res;
                done(res);
                return compHandler;
            }
        }
    }
}
