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
        private Comm comm = Comm.getInstance();

        [TestInitialize]
        public void TestInitialize()
        {
            //emptyHerukuLogs();
        }
        
        protected CompletionHandler initParkMission(Parking park, bool isAsync = false)
        {
            InitParkingMission initParkMission = new InitParkingMission(park);
            CompletionHandler ch = initParkMission.execute();

         q   if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler take_off(bool isAsync = false)
        {
            TakeOff take_off = new TakeOff();
            CompletionHandler ch = take_off.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler landing(bool isAsync = false)
        {
            Landing landing = new Landing();
            CompletionHandler ch = landing.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler startLanding(bool isAsync = false)
        {
            DroneServer.BL.Missions.StartLanding start_landing = new DroneServer.BL.Missions.StartLanding();
            CompletionHandler ch = start_landing.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler confirmLanding(bool isAsync = false)
        {
            ConfirmLanding conf_landing = new ConfirmLanding();
            CompletionHandler ch = conf_landing.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler move(Direction direction, double distance, bool isAsync = false)
        {
            MoveMission move = new MoveMission(direction, distance);
            CompletionHandler ch = move.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler MoveByGPS(double lat, double lng, double alt, bool isAsync = false)
        {
            MoveToGPSPoint move = new MoveToGPSPoint(lat, lng, alt);
            CompletionHandler ch = move.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler MoveGimbal(GimbalMovementType movment_type, double roll, double pitch, double yaw, bool isAsync = false)
        {
            MoveGimbal move = new MoveGimbal(Gimbal.left, movment_type, roll, pitch, yaw);
            CompletionHandler ch = move.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler takePicture(bool isAsync = false)
        {
            TakePhoto photo = new TakePhoto();
            CompletionHandler ch = photo.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
        }

        protected CompletionHandler getLocation(bool isAsync = false)
        {
            GetLocation get_loation = new GetLocation();
            CompletionHandler ch = get_loation.execute();
            if (!isAsync)
            {
                ch.wait();
            }
            return ch;
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
