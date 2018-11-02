package BL;

import BL.missions.*;
import SharedClasses.Direction;

public class Decoder {

    public static Mission decode(String mission_string){
        Mission to_return = null;

        String[] dispatched = mission_string.split(" ");
        int index =Integer.parseInt(dispatched[1]);
        String missionName=dispatched[0];

        switch (missionName){
            case "move":
                Direction direction=Direction.valueOf(dispatched[2]);
                double distance =Double.parseDouble(dispatched[3]);
                to_return=new MoveMission(index,direction,distance);
                break;
            case "takeOff":
                 to_return= new TakeoffMission(index);
                break;
            case "startLanding":
                to_return = new StartLandingMission(index);
                break;
            case "confirmLanding":
                to_return = new ConfirmLandingMission(index);
                break;
            case "goHome":
                to_return= new GoHomeMission(index);
                break;
            case "moveGimbal":
                String gimbal = dispatched[2];
                double roll =Double.parseDouble(dispatched[3]);
                double pitch =Double.parseDouble(dispatched[4]);
                double yaw =Double.parseDouble(dispatched[5]);
                to_return= new MoveCameraGimbalMission(index, gimbal, roll, pitch, yaw);
                break;
            case "goToGPS":
                double x=Double.parseDouble(dispatched[2]);
                double y=Double.parseDouble(dispatched[3]);
                double z=Double.parseDouble(dispatched[4]);
                to_return = new MoveByGPSMission(index,x,y,z);
                break;
            case "takePhoto":
                to_return = new TakePictureMission(index);
                break;
            case "stop":
                to_return = new StopMission(index);
                break;
            case "getStatus":
                to_return = new GetDroneStatusMission(index);
                break;
            case "isAlive":
                to_return = new IsAliveMission(index);
                break;
            case "getLocation":
                to_return = new GetGPSLocationMission(index);
                break;
        }

        return to_return;
    }
}
