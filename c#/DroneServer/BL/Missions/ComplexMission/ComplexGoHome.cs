using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class ComplexGoHome : ComplexMission
    {
        public ComplexGoHome(ComplexMission ParentMission = null) : base(ParentMission)
        {
        }

        public override void execute()
        {
            BLManagger.getInstance().setSafeZone(false);

            int hight = Convert.ToInt32(Configuration.getInstance().get("Home_Location_Hight"));

            Mission mission = new MoveToGPSPoint(this, LocationManager.HomeLocation.lat, LocationManager.HomeLocation.lng, hight);

            mission.execute();
        }

        public override void stop()
        {
        }
    }


}
