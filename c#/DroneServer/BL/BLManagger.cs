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
using DroneServer.PL.Observers;
using System.Collections.Generic;

namespace DroneServer.BL 
{
    public class BLManagger
    {
        private static BLManagger instance = null;
        private static Logger logger = Logger.getInstance();
        private static Map map= new Map();
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

        public List<Parking> DBGetAllParkings()
        {
            return DB.selectAllParkings();
        }

        public void DBAddParking(Parking p)
        {
            DB.addParking(p);
            logger.debug("The Parking " +p.name + " has added to DB");
        }

        public void DBDeleteParking(string name)
        {        
            DB.deleteParking(name);
            logger.debug("The Parking " + name + " has deleted from DB");
        }


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

        public void registerToConnection(object o)
        {
            throw new NotImplementedException();
        }

        public void registerToMap(GMapControl Gmap)
        {
            map.register(new MapObserver(Gmap));
            logger.debug("The Gmap " + Gmap.Name + " has registered");
            LocationManager.init();
        }

        public void setLocation(double lat,double lng)
        {
            if (map!=null)
                map.setLocation(new Point(lng, lat));
        }

        public void startMission(Parking parking)
        {

        }

        public void shutdown()
        {
            CommManager.getInstance().shutDown();
            LocationManager.shutDown();
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
