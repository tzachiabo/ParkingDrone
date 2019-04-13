using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class TakePhoto : LeafMainMission
    {
        public TakePhoto(ComplexMission ParentMission=null) : base(ParentMission)
        {

        }

        public override void stop()
        {

        }


        public override void execute()
        {
            Logger.getInstance().info("start take photo");
            base.execute();
        }

        public override string encode()
        {
            return "takePhoto " + m_index;
        }
    }
}
