package BL.missions;


import dji.common.mission.waypoint.Waypoint;
import dji.common.mission.waypoint.WaypointMission;
import dji.common.mission.waypoint.WaypointMission.Builder;
import dji.common.mission.waypoint.WaypointMissionFinishedAction;
import dji.common.mission.waypoint.WaypointMissionFlightPathMode;
import dji.common.mission.waypoint.WaypointMissionHeadingMode;
import dji.common.mission.waypoint.WaypointMissionState;
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
        while(waypointMissionOperator.getCurrentState()!=WaypointMissionState.READY_TO_UPLOAD);
        waypointMissionOperator.uploadMission(null);
        while(waypointMissionOperator.getCurrentState()!=WaypointMissionState.READY_TO_EXECUTE);
        waypointMissionOperator.startMission(null);
        while (waypointMissionOperator.getCurrentState()!=WaypointMissionState.READY_TO_UPLOAD);
        onResult.onResult(null);

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " Done";
    }
}
