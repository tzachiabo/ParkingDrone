using DroneServer.BL.Missions;
using DroneServer.BL;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace missionManagerAccepanceTests
{
    [TestClass]
    public class BaseAcceptanceTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            //emptyHerukuLogs();
        }
        protected CompletionHandler initParkMission(Parking park,bool isAsync = false)
        {

            InitParkingMission initParkMission = new InitParkingMission(park);
            return initParkMission.execute();
        }
        protected CompletionHandler take_off()
        {
            TakeOff take_off = new TakeOff();
            return take_off.execute();
        }

        protected CompletionHandler landing()
        {
            startLanding();
            return confirmLanding();
        }

        protected CompletionHandler startLanding(bool isAsync = false)
        {
            DroneServer.BL.Missions.StartLanding start_landing = new DroneServer.BL.Missions.StartLanding();
            return start_landing.execute();
        }

        protected CompletionHandler confirmLanding()
        {
            ConfirmLanding conf_landing = new ConfirmLanding();
            return conf_landing.execute();
        }

        protected CompletionHandler move(Direction direction, double distance, bool isAsync = false)
        {
            MoveMission move = new MoveMission(direction, distance);
            return move.execute();
        }

        protected CompletionHandler MoveByGPS(double lat, double lng, double alt, bool isAsync = false)
        {
            MoveToGPSPoint move = new MoveToGPSPoint(lat, lng, alt);
            return move.execute();
        }

        protected CompletionHandler MoveGimbal(GimbalMovementType movment_type, double roll, double pitch, double yaw)
        {
            MoveGimbal move = new MoveGimbal(Gimbal.left, movment_type, roll, pitch, yaw);
            return move.execute();
        }

        protected CompletionHandler takePicture()
        {
            TakePhoto photo = new TakePhoto();
            return photo.execute();
        }

        protected CompletionHandler getLocation()
        {
            GetLocation get_loation = new GetLocation();
            return get_loation.execute();
        }

        protected CompletionHandler stop()
        {
            stopMission stop = new stopMission();
            return stop.execute();
        }
        private void emptyHerukuLogs()
        {
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
    }
}
