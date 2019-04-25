using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class VerifyLocation : ComplexMission
    {
        public VerifyLocation(ComplexMission ParentMission = null, string algo="") : base(ParentMission)
        {
            if (algo == "")
            {
                algo = Configuration.getInstance().get("verify_location_alguritum");
            }

            if (algo == "Template_Matching") { 
                m_SubMission.Enqueue(new VerifyLocationByTemplateMatching(this));
            }
            else if (algo == "SIFT")
            {
                m_SubMission.Enqueue(new VerifyLocationBySift(this));
            }
            else if (algo == "GPS")
            {
                m_SubMission.Enqueue(new VerifyLocationByGPS(this));
            }
            else
            {
                Assertions.verify(false, "unexpected path at Verify location");
            }

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
