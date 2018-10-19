using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using log4net;

namespace DroneServer.BL
{
    public class BLManagger
    {
        private static BLManagger instance = null;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private BLManagger()
        {
            if (File.Exists("./MyTestAppender.log"))
            {
                File.WriteAllLines("./MyTestAppender.log", new string[0]);
            }
            Log.Debug("sasdasdasddf");
        }

        public static BLManagger getInstance()
        {
            if (instance == null)
                instance = new BLManagger();
            return instance;
        }

        //public bool createParkingSpot(ParkingSpot p)
        //{

            //return false;
        //}

        //public void startMission(ParkingSpot p)
        //{

        //}

        //public List<ParkingSpot> getAllParkingSpots()
        //{

        //}

        public void endMission()
        {

        }

        public void stop()
        {

        }

        public void abort()
        {

        }

        public void registerToLogs(object o)
        {

        }

        public void registerToConnection(object o)
        {
            
        }

        public void registerToDroneLocation(object o)
        {
            
        }
    }
}
