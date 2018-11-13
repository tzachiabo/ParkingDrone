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
        private Parking parking;


        public InitParkingMission(Parking parking) : this(null, parking)
        {

        }
        public InitParkingMission(ComplexMission parent_mission, Parking parking) : base()
        {
            m_ParentMission = parent_mission;
            Point basePoint = GetBaseLocation(parking.border);
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
        public static Point GetBaseLocation(List<Point> points)
        {
            List<double> Xlist= new List<double>(); 
            List<double> Ylist= new List<double>(); ;
            List<double> Zlist= new List<double>(); ;
            foreach (Point p in points)
            {
                Xlist.Add(p.x);
                Ylist.Add(p.y);
                Zlist.Add(p.z);
            }
            double minX = Xlist.Min();
            double maxX = Xlist.Max();
            double minY = Ylist.Min();
            double maxY = Ylist.Max();
            double maxZ = Zlist.Max();

            double middleX = (minX + maxX) / 2;
            double middleY = (minY + maxY) / 2;
            Configuration conf = Configuration.getInstance();
            double highet = Math.Sqrt(Math.Pow((maxX - minX), 2) + Math.Pow((maxY - minY), 2)) / conf.getHighetfix()+ maxZ;
            return new Point(middleX, middleY, highet);

        }

    }
}
