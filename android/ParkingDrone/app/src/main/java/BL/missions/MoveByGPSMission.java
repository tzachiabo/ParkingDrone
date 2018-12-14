package BL.missions;


import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.mission.waypoint.Waypoint;
import dji.common.mission.waypoint.WaypointMission;
import dji.common.mission.waypoint.WaypointMission.Builder;
import dji.common.mission.waypoint.WaypointMissionFinishedAction;
import dji.common.mission.waypoint.WaypointMissionFlightPathMode;
import dji.common.mission.waypoint.WaypointMissionHeadingMode;
import dji.common.mission.waypoint.WaypointMissionState;
import dji.common.util.CommonCallbacks;
import dji.sdk.mission.MissionControl;
import dji.sdk.mission.waypoint.WaypointMissionOperator;
import dji.sdk.sdkmanager.DJISDKManager;

public class MoveByGPSMission extends Mission {

    private double xLoc;
    private double yLoc;
    private float zLOC;
    private WaypointMissionFinishedAction on_finish = WaypointMissionFinishedAction.NO_ACTION;
    private WaypointMissionHeadingMode headingMode = WaypointMissionHeadingMode.AUTO;


    public MoveByGPSMission(int index, double x, double y, float z){
        super("goToGPS", index);
        xLoc=x;
        yLoc=y;
        zLOC=z;
    }

    @Override
    public void start() {
        IDrone drone = DroneFactory.getDroneManager();
        drone.moveByGPS(xLoc, yLoc, zLOC, new Promise(){
            @Override
            public void onSuccess() {
                Logger.info("Move to gps finished");
                onResult.onResult(null);
            }

            @Override
            public void onFailed() {
                Logger.error("Move to gps failed");
            }
        });

    }

    @Override
    public void stop() {
        Logger.debug("trying to stop go to gps");
        IDrone drone = DroneFactory.getDroneManager();
        drone.stopMoveByGPS();
    }

    @Override
    public String encode() {
        Logger.info("encoding mission move by gps");
        return getName() +" "+ getIndex() + " Done";
    }
}
