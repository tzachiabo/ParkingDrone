using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class StartLanding : LeafMission
    {

        public StartLanding(ComplexMission ParentMission=null) : base(ParentMission)
        {

        }
        public override void execute()
        {
            BLManagger.getInstance().setSafeZone(false);
            base.execute();
        }
        public override void stop()
        {

        }

        public override string encode()
        {
            return "startLanding " + m_index;
        }
    }
}
