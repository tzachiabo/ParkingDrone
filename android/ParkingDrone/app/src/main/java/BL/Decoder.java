package BL;

import BL.missions.GetDroneStatusMission;
import BL.missions.GetGPSLocationMission;
import BL.missions.GoHomeMission;
import BL.missions.IsAliveMission;
import BL.missions.LandingMission;
import BL.missions.Mission;
import BL.missions.MoveByGPSMission;
import BL.missions.MoveCameraGimbalMission;
import BL.missions.MoveMission;
import BL.missions.StopMission;
import BL.missions.TakePictureMission;
import BL.missions.TakeoffMission;
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
            case "landing":
                to_return = new LandingMission(index);
                break;
            case "goHome":
                to_return= new GoHomeMission(index);
                break;
            case "moveGimbal":
                double VerDegree =Double.parseDouble(dispatched[2]);
                double HorDegree =Double.parseDouble(dispatched[3]);
                to_return= new MoveCameraGimbalMission(index,VerDegree,HorDegree);
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
