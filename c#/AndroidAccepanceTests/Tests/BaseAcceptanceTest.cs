using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndroidAccepanceTests
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

        protected void take_off()
        {
            TakeOff take_off = new TakeOff();
            CompletionHandler take_off_mission = comm.sendMission(take_off);
        }

        protected void landing()
        {
            startLanding();
            confirmLanding();
        }

        protected CompletionHandler startLanding(bool isAsync=false)
        {
            DroneServer.BL.Missions.StartLanding start_landing = new DroneServer.BL.Missions.StartLanding();
            return comm.sendMission(start_landing, isAsync);
        }

        protected void confirmLanding()
        {
            ConfirmLanding conf_landing = new ConfirmLanding();
            CompletionHandler conf_landing_mission = comm.sendMission(conf_landing);
        }

        protected CompletionHandler move(Direction direction, double distance, bool isAsync = false)
        {
            MoveMission move = new MoveMission(direction, distance);
            return comm.sendMission(move, isAsync);
        }

        protected CompletionHandler MoveByGPS(double lat, double lng, double alt, bool isAsync = false)
        {
            MoveToGPSPoint move = new MoveToGPSPoint(lat, lng, alt);
            return comm.sendMission(move, isAsync);
        }

        protected void MoveGimbal(GimbalMovementType movment_type, double roll, double pitch, double yaw)
        {
            MoveGimbal move = new MoveGimbal(Gimbal.left, movment_type, roll, pitch, yaw);
            CompletionHandler move_mission = comm.sendMission(move);
        }

        protected void takePicture()
        {
            TakePhoto photo = new TakePhoto();
            CompletionHandler photo_mission = comm.sendMission(photo);
        }

        protected Point getLocation()
        {
            GetLocation get_loation = new GetLocation();
            CompletionHandler get_loation_mission = comm.sendMission(get_loation);
            return (Point)get_loation_mission.response.Data;
        }
        
        protected CompletionHandler stop()
        {
            stopMission stop = new stopMission();
            return comm.sendMission(stop);
        }

        protected void restore()
        {
            comm.sendString(1,"setVirtualStick 1");
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
