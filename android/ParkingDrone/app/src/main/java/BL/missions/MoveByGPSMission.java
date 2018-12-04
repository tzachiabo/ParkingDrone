package BL.missions;


import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
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
        Builder mission_builder = new WaypointMission.Builder();
        mission_builder
                .headingMode(headingMode)
                .finishedAction(on_finish)
                .autoFlightSpeed(2.0f)
                .maxFlightSpeed(3.0f)
                .flightPathMode(WaypointMissionFlightPathMode.NORMAL)
                .addWaypoint(new Waypoint(xLoc,yLoc+0.00005,zLOC))
                .addWaypoint(new Waypoint(xLoc,yLoc,zLOC))
                .repeatTimes(0)
                .waypointCount(mission_builder.getWaypointList().size());

        WaypointMission waypointMission = mission_builder.build();
        MissionControl missionControl = DJISDKManager.getInstance().getMissionControl();
        WaypointMissionOperator waypointMissionOperator = missionControl.getWaypointMissionOperator();
        waypointMissionOperator.loadMission(waypointMission);
        long startTime = System.currentTimeMillis();
        while(waypointMissionOperator.getCurrentState()!=WaypointMissionState.READY_TO_UPLOAD){
            Assertions.verify( System.currentTimeMillis() - startTime < Config.MAX_TIME_FOR_SETP_IN_GO_TO_GPS,
                    "waypoint mission operator wasn't ready to upload"); }
        waypointMissionOperator.uploadMission(null);
        startTime = System.currentTimeMillis();
        while(waypointMissionOperator.getCurrentState()!=WaypointMissionState.READY_TO_EXECUTE){
            Assertions.verify( System.currentTimeMillis() - startTime < Config.MAX_TIME_FOR_SETP_IN_GO_TO_GPS,
                    "waypoint mission operator wasn't ready to execute");
        }
        waypointMissionOperator.startMission(null);
        while (waypointMissionOperator.getCurrentState()!=WaypointMissionState.READY_TO_UPLOAD){
        }
        Logger.debug("Finished waypoint missions, sending result");
        onResult.onResult(null);

    }

    @Override
    public void stop() {
        Logger.debug("trying to stop go to gps");
        MissionControl missionControl = DJISDKManager.getInstance().getMissionControl();
        WaypointMissionOperator waypointMissionOperator = missionControl.getWaypointMissionOperator();
        waypointMissionOperator.stopMission(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError !=null){
                    Logger.error("when stopping GoToGps mission dji error :"+djiError.toString());
                }
                else{
                    Logger.info("go to gps has stopped");
                }
            }
        });
    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " Done";
    }
}
