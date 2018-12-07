package BL.Drone.DJIM210;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;

import SharedClasses.AssertionViolation;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.mission.waypoint.Waypoint;
import dji.common.mission.waypoint.WaypointMission;
import dji.common.mission.waypoint.WaypointMissionDownloadEvent;
import dji.common.mission.waypoint.WaypointMissionExecutionEvent;
import dji.common.mission.waypoint.WaypointMissionFinishedAction;
import dji.common.mission.waypoint.WaypointMissionFlightPathMode;
import dji.common.mission.waypoint.WaypointMissionHeadingMode;
import dji.common.mission.waypoint.WaypointMissionState;
import dji.common.mission.waypoint.WaypointMissionUploadEvent;
import dji.common.util.CommonCallbacks;
import dji.sdk.mission.MissionControl;
import dji.sdk.mission.waypoint.WaypointMissionOperator;
import dji.sdk.mission.waypoint.WaypointMissionOperatorListener;
import dji.sdk.sdkmanager.DJISDKManager;

public class MissionControlManager {
    MissionControl m_mission_control;

    private WaypointMissionFinishedAction on_finish = WaypointMissionFinishedAction.NO_ACTION;
    private WaypointMissionHeadingMode headingMode = WaypointMissionHeadingMode.AUTO;

    public MissionControlManager(){
        m_mission_control = DJISDKManager.getInstance().getMissionControl();
        Assertions.verify(m_mission_control != null,
                "Mission Control is null when constracting MissionControlManager");
    }

    public void moveByGPS(double x, double y, float z, final Promise p) {
        Logger.info("start move by GPS mission "+ x+ " "+ y + " "+ z);
        try {
            WaypointMission.Builder mission_builder = new WaypointMission.Builder();
            mission_builder
                    .headingMode(headingMode)
                    .finishedAction(on_finish)
                    .autoFlightSpeed(2.0f)
                    .maxFlightSpeed(3.0f)
                    .flightPathMode(WaypointMissionFlightPathMode.NORMAL)
                    .addWaypoint(new Waypoint(x, y + 0.00005, z))
                    .addWaypoint(new Waypoint(x, y, z))
                    .repeatTimes(0)
                    .waypointCount(mission_builder.getWaypointList().size());

            final WaypointMission waypointMission = mission_builder.build();

            final WaypointMissionOperator waypointMissionOperator = m_mission_control.getWaypointMissionOperator();

            waypointMissionOperator.addListener(new WaypointMissionOperatorListener(){
                @Override
                public void onDownloadUpdate(@NonNull WaypointMissionDownloadEvent waypointMissionDownloadEvent) {
                    Logger.info("onDownloadUpdate");
                }

                @Override
                public void onUploadUpdate(@NonNull WaypointMissionUploadEvent waypointMissionUploadEvent) {
                    Logger.info("onUploadUpdate");
                }

                @Override
                public void onExecutionUpdate(@NonNull WaypointMissionExecutionEvent waypointMissionExecutionEvent) {
                    Logger.info("onExecutionUpdate "+ waypointMissionExecutionEvent.getCurrentState() +
                            " msg : "+ waypointMissionExecutionEvent.toString());

                }

                @Override
                public void onExecutionStart() {
                    Logger.info("onExecutionStart");
                }

                @Override
                public void onExecutionFinish(@Nullable DJIError djiError) {
                    Logger.info("onExecutionFinish");
                    p.success();
                }
            });


            load_mission(waypointMissionOperator, waypointMission);

            waypointMissionOperator.startMission(new CommonCallbacks.CompletionCallback() {
                @Override
                public void onResult(DJIError djiError) {
                    try {
                        if (djiError != null) {
                            Assertions.verify(false,
                                    "failed to start mission with err :" + djiError.toString());
                            p.failed();
                        }
                    }
                    catch(AssertionViolation e){
                        p.failed();
                    }
                }
            });


        }
        catch (AssertionViolation e){
            p.failed();
        }


    }

    private void load_mission(WaypointMissionOperator waypointMissionOperator,
                              WaypointMission waypointMission)
    {
        waypointMissionOperator.loadMission(waypointMission);
        long startTime = System.currentTimeMillis();
        while(waypointMissionOperator.getCurrentState()!= WaypointMissionState.READY_TO_UPLOAD){
            Assertions.verify( System.currentTimeMillis() - startTime < Config.MAX_TIME_FOR_SETP_IN_GO_TO_GPS,
                    "waypoint mission operator wasn't ready to upload");
        }

        Logger.info("Finished first waypoint mission");

        waypointMissionOperator.uploadMission(null);
        startTime = System.currentTimeMillis();
        while(waypointMissionOperator.getCurrentState()!=WaypointMissionState.READY_TO_EXECUTE){
            Assertions.verify( System.currentTimeMillis() - startTime < Config.MAX_TIME_FOR_SETP_IN_GO_TO_GPS,
                    "waypoint mission operator wasn't ready to execute");
        }

        Logger.info("Finished loading waypoint missions");
    }


}
