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
using System.Net;
using DroneServerIntegration;

namespace DroneServer.BL 
{
    public class BLManagger
    {
        private static BLManagger instance = null;
        private static Logger logger;
        private static Map map;
        private static ConnectionStatus status;
        private static int Version;
        private static bool stayInSafeZone = false;
        private String m_base_photo_path = null;
        private Parking m_parking = null;
        public int num_of_scaned_cars;

        private BLManagger()
        {
            status = new ConnectionStatus();
            logger = Logger.getInstance();
            logger.debug("Initiate BL");
            map = new Map();
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

        public void set_base_photo_path(String base_photo_path)
        {
            Assertions.verify(m_base_photo_path == null, "cannot set base photo twice");
            m_base_photo_path = base_photo_path;
        }

        public String get_base_photo_path()
        {
            Assertions.verify(m_base_photo_path != null, "base photo is null");
            return m_base_photo_path;
        }

        public void set_parking(Parking parking)
        {
            Assertions.verify(m_parking == null, "cannot set parking twice");
            m_parking = parking;
        }

        public Parking get_parking()
        {
            Assertions.verify(m_parking != null, "parking is null");
            return m_parking;
        }

        public void initComm()
        {
            CommManager.getInstance();
            statusManager.init();
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

        public bool DBExistParkingName(string name)
        {
            return DB.existParkingName(name);
        }

        public void DBDeleteParking(string name)
        {        
            DB.deleteParking(name);
            logger.debug("The Parking " + name + " has deleted from DB");
        }


        public void endMission()
        {
            EndMission end_mission = new EndMission();
            end_mission.execute();
        }

        public void stop()
        {
            stopMission stop_mission = new stopMission();
            stop_mission.execute();
        }

        public void abort()
        {
            AbortMission abort_mission = new AbortMission();
            abort_mission.execute();
        }

        public void registerToLogs(ListBox list)
        {
            logger.register(new ListObserver(list));
            logger.debug("The ListBox "+list.Name+" has registered");
        }

        public void registerToConnection(Control text)
        {
            status.register(new TextObserver(text));    
            logger.debug("The Control " + text.Name + " has registered");
        }

        public void registerToMap(GMapControl Gmap)
        {
            map.register(new MapObserver(Gmap));
            logger.debug("The Gmap " + Gmap.Name + " has registered");
        }

        public void setLocation(double lat,double lng)
        {
            if (map!=null)
                map.setLocation(new Point(lng, lat));
        }

        public void setStatus(DroneStatus ds)
        {
            if (status != null)
                status.setStatus(ds);
        }

        public MissionWraper startMission(Parking parking)
        {
            num_of_scaned_cars = 0;
            LocationManager.init(parking);

            ParkingMission start_mission = new ParkingMission(parking);

            return new MissionWraper(start_mission);
        }

        public void shutdown()
        {
            CommManager.getInstance().shutDown();
            LocationManager.shutDown();
        }


      
        public async void updateAndroidLog(ListBox androidLogger_home_lst, ListBox androidLogger_mission_lst)
        {
            Task<string[]> t = new Task<string[]>(()=> {
                string Url = "https://floating-fjord-95063.herokuapp.com/log";
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                myRequest.Method = "GET";
                try
                {
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                    string result = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();

                    result = result.Replace("<br/>", "#");
                    string[] s = result.Split('#');
                    androidLogger_home_lst.Items.Clear();
                    androidLogger_mission_lst.Items.Clear();
                    for (int i = 0; i <= 500; i++)
                    {
                        if (s[i] != "")
                        {
                            androidLogger_home_lst.Items.Add(s[i]);
                            androidLogger_mission_lst.Items.Add(s[i]);
                        }
                    }   

                }
                catch (Exception)
                {

                }
                return null;
            });
            t.Start();
            await t;

            
        }

        public void clearLogs()
        {
            logger.clearData();

            try
            {
                string Url = "https://floating-fjord-95063.herokuapp.com/empty";
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                myRequest.Method = "GET";
                WebResponse myResponse = myRequest.GetResponse();
                myResponse.Close();
            }
            catch (Exception)
            {
            }

        }

        public bool validateParkingHeight(Parking p)
        {
            //if (p.getBasePoint().alt > Convert.ToInt32(Configuration.getInstance().get("max_parking_height")))
            //    return false;
            // TODO zabow
            return true;
        }
        
        public void setSafeZone(bool safeZone)
        {
            stayInSafeZone = safeZone;
        }
        public bool getSafeZone()
        {
            return stayInSafeZone;
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
            //ParkingMission parkingMission = new ParkingMission();
            //parkingMission.execute();
        }

        public void MoveForTest(int amount_to_move, Direction d)
        {
            MoveMission parkingMission = new MoveMission(d, amount_to_move);
            parkingMission.execute();
        }

        public void GoToGpsTest()
        {
            MoveToGPSPoint mtgp = new MoveToGPSPoint(22.54281,113.95890,5);
            mtgp.execute();
        }

        public void goHomeForTest()
        {
            new ComplexGoHome().execute();
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
