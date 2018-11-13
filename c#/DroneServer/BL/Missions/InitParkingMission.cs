using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneServer.SharedClasses;


namespace DroneServer.BL.Missions
{
    public class InitParkingMission : ComplexMission
    {
        private GPSPoint gps1;
        private GPSPoint gps2;
        private GPSPoint gps3;
        private GPSPoint gps4;
        
        public InitParkingMission(GPSPoint gps1, GPSPoint gps2, GPSPoint gps3, GPSPoint gps4) : this(null, gps1, gps2, gps3, gps4)
        {

        }
        public InitParkingMission(ComplexMission parent_mission, GPSPoint gps1, GPSPoint gps2, GPSPoint gps3, GPSPoint gps4) : base()
        {
            m_ParentMission = parent_mission;
            GPSPoint basePoint = getBaseLocation(gps1, gps2, gps3, gps4);
            m_SubMission.Enqueue(new MoveToGPSPoint(basePoint.x, basePoint.y, basePoint.z));
            
        }
        public override void execute()
        {
            Mission mission = m_SubMission.Dequeue();

            mission.execute();
        }
        public override void done()
        {

        }
        public override void stop()
        {

        }
        public static GPSPoint getBaseLocation(GPSPoint gps1, GPSPoint gps2, GPSPoint gps3, GPSPoint gps4)
        {
            var Xlist = new[] { gps1.x, gps2.x, gps3.x, gps4.x };
            var Ylist = new[] { gps1.y, gps2.y, gps3.y, gps4.y };
            var Zlist = new[] { gps1.z, gps2.z, gps3.z, gps4.z };
            double minX = Xlist.Min();
            double maxX = Xlist.Max();
            double minY = Ylist.Min();
            double maxY = Ylist.Max();
            double maxZ = Zlist.Max();

            double middleX = (minX + maxX) / 2;
            double middleY = (minY + maxY) / 2;
            Configuration conf = Configuration.getInstance();
            double highet = Math.Sqrt(Math.Pow((maxX - minX), 2) + Math.Pow((maxY - minY), 2)) / conf.getHighetfix()+ maxZ;
            return new GPSPoint(middleX, middleY, highet);

        }

    }
}
