using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidAccepanceTests
{
    public class BaseAcceptanceTest
    {
        private Comm comm = Comm.getInstance();

        protected void take_off()
        {
            TakeOff take_off = new TakeOff();
            CompletionHanlder take_off_mission = comm.sendMission(take_off);
        }

        protected void landing()
        {
            startLanding();
            confirmLanding();
        }

        protected void startLanding()
        {
            DroneServer.BL.Missions.StartLanding start_landing = new DroneServer.BL.Missions.StartLanding();
            CompletionHanlder start_landing_mission = comm.sendMission(start_landing);
        }

        protected void confirmLanding()
        {
            ConfirmLanding conf_landing = new ConfirmLanding();
            CompletionHanlder conf_landing_mission = comm.sendMission(conf_landing);
        }

        protected void move(Direction direction, double distance)
        {
            MoveMission move = new MoveMission(direction, distance);
            CompletionHanlder move_mission = comm.sendMission(move);
        }

        protected void MoveByGPS(double lat, double lng, double alt)
        {
            MoveToGPSPoint move = new MoveToGPSPoint(lat, lng, alt);
            CompletionHanlder move_mission = comm.sendMission(move);
        }

        protected void MoveGimbal(GimbalMovementType movment_type, double roll, double pitch, double yaw)
        {
            MoveGimbal move = new MoveGimbal(Gimbal.left, movment_type, roll, pitch, yaw);
            CompletionHanlder move_mission = comm.sendMission(move);
        }

        protected Point getLocation()
        {
            GetLocation get_loation = new GetLocation();
            CompletionHanlder get_loation_mission = comm.sendMission(get_loation);
            return (Point)get_loation_mission.response.Data;
        }
    }
}
