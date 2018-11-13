using System.Text;
using System.Threading.Tasks;

using log4net;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using System.Windows.Forms;
using DroneServer.BL.Comm;
using System.IO;
using System;
using System.Threading;
using System.Runtime.CompilerServices;

using GMap.NET.WindowsForms;
using DroneServer.PL;
namespace DroneServer.BL 
{
    public class BLManagger
    {
        private static BLManagger instance = null;
        private static Logger logger = Logger.getInstance();
        private static ParkingList parkingList = new ParkingList();
        private static int Version;

        private BLManagger()
        {
            logger.debug("Initiate BL");
            CommManager.getInstance();

        }

        public static BLManagger getInstance()
        {
            if (instance == null)
                instance = new BLManagger();
            return instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void increment_version()
        {
            Version++;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int get_version()
        {
            return Version;
        }

<<<<<<< HEAD

        //public bool createParkingSpot(ParkingSpot p)
        //{
=======
        public void restoreData()
        {
            foreach (Parking p in DB.selectAllParkings())
            {
                parkingList.add(p);
            }
            
             
        }
>>>>>>> add parking object to gui and db

        public void createParking(TextBox name,GMapControl map, ListBox border)
        {
            Parking p = new Parking(name, map, border);
            DB.addParking(p);
            logger.debug("The Parking " + name.Text + " has added to DB");
            parkingList.add(p);
            logger.debug("The Parking " + name.Text + " has added to the ListBox: "+ border.Name);
        }

        public void deleteParking(string name)
        {
            
            DB.deleteParking(name);
            logger.debug("The Parking " + name + " has deleted from DB");
            parkingList.delete(name);
            logger.debug("The Parking " + name + " has deleted from the ListBox");
        }

        //public void startMission(ParkingSpot p)
        //{

        //}



        public void endMission()
        {
            throw new NotImplementedException();
        }

        public void stop()
        {
            stopMission stop_mission = new stopMission();
            stop_mission.execute();
        }

        public void abort()
        {
            throw new NotImplementedException();
        }

        public void registerToLogs(ListBox list)
        {
            logger.register(new ListObserver(list));
            logger.debug("The ListBox "+list.Name+" has registered");
        }

        public void registerToParkings(ListBox list)
        {
            parkingList.register(new ListObserver(list));
            logger.debug("The ListBox " + list.Name + " has registered");
        }

        public void registerToConnection(object o)
        {
            throw new NotImplementedException();
        }

        public void registerToDroneLocation(object o)
        {
            throw new NotImplementedException();
        }


        //----------------------------------tests-------------------------------//

        public void TakeOffForTest()
        {
            TakeOff take_off = new TakeOff();
            take_off.execute();
        }

        public void StartLandingForTest()
        {
            StartLanding startLanding = new StartLanding();
            startLanding.execute();
        }

        public void ConfirmLandingForTest()
        {
            ConfirmLanding confirmLanding = new ConfirmLanding();
            confirmLanding.execute();
        }

        public void LandingForTest()
        {
            Landing landingMission = new Landing();
            landingMission.execute();
        }

        public void ParkingForTest()
        {
            ParkingMission parkingMission = new ParkingMission();
            parkingMission.execute();
        }

        public void MoveForTest(int amount_to_move, Direction d)
        {
            Move parkingMission = new Move(d, amount_to_move);
            parkingMission.execute();
        }

        public void GoToGpsTest()
        {
            MoveToGPSPoint mtgp = new MoveToGPSPoint(22.54281,113.95890,5);
            mtgp.execute();
        }

        public void MoveGimbalTest(Gimbal gimbal, double roll, double pitch, double yaw)
        {
            MoveGimbal mg = new MoveGimbal(gimbal, GimbalMovementType.relative, roll, pitch, yaw);
            mg.execute();
        }

        public void TakePhoto()
        {
            TakePhoto tp = new TakePhoto();
            tp.execute();
        }

    }
}
