using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.SharedClasses;

namespace DroneServer.BL.Missions
{
    public class MoveToGPSPoint : LeafMission
    {
        private double m_lat;
        private double m_lng;
        private double m_alt;

        public MoveToGPSPoint(double x, double y, double z) : this(null,x,y,z)
        {
        }

        public MoveToGPSPoint(ComplexMission ParentMission,double x, double y, double z) : base(ParentMission)
        {
            m_lat = x;
            m_lng = y;
            m_alt = z;
        }

        public override void stop()
        {

        }

        public override void done(Response response)
        {
            BLManagger.getInstance().setSafeZone(true);
            base.done(response);
        }

        public override void execute()
        {
            BLManagger.getInstance().setSafeZone(false);
            Logger.getInstance().info("move to gps execute");
            base.execute();
        }

        public override string encode()
        {
            return "goToGPS "+ m_index + " " + m_lat + " " + m_lng + " " + m_alt;
        }
    }
}
