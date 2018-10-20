using System.Text;
using System.Threading.Tasks;

using log4net;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using System.Windows.Forms;
using DroneServer.BL.Comm;
using System.IO;
using System;

namespace DroneServer.BL 
{
    public class BLManagger
    {
        private static BLManagger instance = null;
        private static Logger logger = Logger.getInstance();

        private BLManagger()
        {
            CommManager.getInstance();

            if (File.Exists("./MyTestAppender.log"))
            {
          ///      File.WriteAllLines("./MyTestAppender.log", new string[0]);
            }
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
            throw new NotImplementedException();
        }

        public void stop()
        {
            throw new NotImplementedException();
        }

        public void abort()
        {
            throw new NotImplementedException();
        }

        public void registerToLogs(ListBox list)
        {
            logger.register(new ListObserver(list));
        }

        public void registerToConnection(object o)
        {
            throw new NotImplementedException();
        }

        public void registerToDroneLocation(object o)
        {
            throw new NotImplementedException();
        }



        public void TakeOffForTest()
        {
            TakeOff take_off = new TakeOff();
            take_off.execute();
        }

        public void LandingForTest()
        {
            Landing landing = new Landing();
            landing.execute();
        }
    }
}
