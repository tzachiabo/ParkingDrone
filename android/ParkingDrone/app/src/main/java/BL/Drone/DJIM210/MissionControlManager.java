package BL.Drone.DJIM210;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;

import java.util.ArrayList;
import java.util.List;

import SharedClasses.AssertionViolation;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.flightcontroller.LocationCoordinate3D;
import dji.common.mission.waypoint.Waypoint;
import dji.common.mission.waypoint.WaypointAction;
import dji.common.mission.waypoint.WaypointMission;
import dji.common.mission.waypoint.WaypointMissionDownloadEvent;
import dji.common.mission.waypoint.WaypointMissionExecutionEvent;
import dji.common.mission.waypoint.WaypointMissionFinishedAction;
import dji.common.mission.waypoint.WaypointMissionFlightPathMode;
import dji.common.mission.waypoint.WaypointMissionHeadingMode;
import dji.common.mission.waypoint.WaypointMissionState;
import dji.common.mission.waypoint.WaypointMissionUploadEvent;
import dji.common.model.LocationCoordinate2D;
import dji.common.util.CommonCallbacks;
import dji.sdk.mission.MissionControl;
import dji.sdk.mission.waypoint.WaypointMissionOperator;
import dji.sdk.mission.waypoint.WaypointMissionOperatorListener;
import dji.sdk.sdkmanager.DJISDKManager;

public class MissionControlManager {
    MissionControl m_mission_control;

    private WaypointMissionFinishedAction on_finish = WaypointMissionFinishedAction.NO_ACTION;
    private WaypointMissionHeadingMode headingMode = WaypointMissionHeadingMode.AUTO;

    public MissionControlManager() {
        m_mission_control = DJISDKManager.getInstance().getMissionControl();
        Assertions.verify(m_mission_control != null,
                "Mission Control is null when constracting MissionControlManager");
    }

    public void moveByGPS(double x, double y, float z, final Promise p) {
        Logger.info("start move by GPS mission " + x + " " + y + " " + z);

        try {
            WaypointMission.Builder mission_builder = new WaypointMission.Builder();
            mission_builder
                    .headingMode(headingMode)
                    .finishedAction(on_finish)
                    .autoFlightSpeed(2.0f)
                    .maxFlightSpeed(3.0f)
                    .flightPathMode(WaypointMissionFlightPathMode.NORMAL);
            WaypointMission.Builder mod_builder = buildWithSubWaypoints(x, y, z, mission_builder);

            mod_builder.repeatTimes(0)
                    .waypointCount(mod_builder.getWaypointList().size());
            Logger.info("Toal way points in queue : " + mission_builder.getWaypointList().size());

            final WaypointMission waypointMission = mod_builder.build();

            final WaypointMissionOperator waypointMissionOperator = m_mission_control.getWaypointMissionOperator();

            waypointMissionOperator.addListener(new WaypointMissionOperatorListener() {
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
                    Logger.info("onExecutionUpdate " + waypointMissionExecutionEvent.getCurrentState() +
                            " msg : " + waypointMissionExecutionEvent.toString());

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
                    } catch (AssertionViolation e) {
                        p.failed();
                    }
                }
            });


        } catch (AssertionViolation e) {
            p.failed();
        }


    }

    private void load_mission(WaypointMissionOperator waypointMissionOperator,
                              WaypointMission waypointMission) {
        waypointMissionOperator.loadMission(waypointMission);
        long startTime = System.currentTimeMillis();
        while (waypointMissionOperator.getCurrentState() != WaypointMissionState.READY_TO_UPLOAD) {
            Assertions.verify(System.currentTimeMillis() - startTime < Config.MAX_TIME_FOR_SETP_IN_GO_TO_GPS,
                    "waypoint mission operator wasn't ready to upload");
        }

        Logger.info("Finished first waypoint mission");

        waypointMissionOperator.uploadMission(null);
        startTime = System.currentTimeMillis();
        while (waypointMissionOperator.getCurrentState() != WaypointMissionState.READY_TO_EXECUTE) {
            Assertions.verify(System.currentTimeMillis() - startTime < Config.MAX_TIME_FOR_SETP_IN_GO_TO_GPS,
                    "waypoint mission operator wasn't ready to execute");
        }

        Logger.info("Finished loading waypoint missions");
    }


    private List<LocationCoordinate3D> genPoints(List<LocationCoordinate3D> points) {
        List<LocationCoordinate3D> newpoints = new ArrayList<>();
        List<LocationCoordinate3D> mixedpoints = new ArrayList<>();
        if (Math.abs(points.get(0).getLatitude() - points.get(1).getLatitude()) < 10
                && Math.abs(points.get(0).getLongitude() - points.get(1).getLongitude()) < 10) {
            return points;
        }
        for (int i = 0; i < points.size() - 1; i++) {
            double newx =
                    Math.abs(points.get(i).getLatitude() -
                            points.get(i + 1).getLatitude()) > 10 ?
                            (points.get(i).getLatitude() + points.get(i + 1).getLatitude()) / 2 :
                            points.get(i).getLatitude();
            double newy =
                    Math.abs(points.get(i).getLongitude() -
                            points.get(i + 1).getLongitude()) > 10 ?
                            (points.get(i).getLongitude() + points.get(i + 1).getLongitude()) / 2 :
                            points.get(i).getLongitude();
            newpoints.add(new LocationCoordinate3D(newx, newy, points.get(i).getAltitude()));
        }
        int times_to_run = newpoints.size() + points.size();
        for (int i = 0; i < times_to_run ; i++) {
            if (i % 2 == 0) {
                mixedpoints.add(points.get(0));
                points.remove(0);
            } else {
                mixedpoints.add(newpoints.get(0));
                newpoints.remove(0);
            }
        }
        return genPoints(mixedpoints);
    }

    private List<LocationCoordinate3D> genZPoints(List<LocationCoordinate3D> points) {
        List<LocationCoordinate3D> newpoints = new ArrayList<>();
        List<LocationCoordinate3D> mixedpoints = new ArrayList<>();
        if (Math.abs(points.get(0).getAltitude() - points.get(1).getAltitude()) < 10) {
            return points;
        }
        for (int i = 0; i < points.size() - 1; i++) {
            float newz = (points.get(0).getAltitude() + points.get(1).getAltitude()) / 2;
            newpoints.add(new LocationCoordinate3D(points.get(0).getLatitude(),points.get(0).getLongitude()
                    , newz));
        }
        int times_to_run = newpoints.size() + points.size();
        for (int i = 0; i < times_to_run ; i++) {
            if (i % 2 == 0) {
                mixedpoints.add(points.get(0));
                points.remove(0);
            } else {
                mixedpoints.add(newpoints.get(0));
                newpoints.remove(0);
            }
        }
        return genPoints(mixedpoints);
    }

    private WaypointMission.Builder buildWithSubWaypoints(double x, double y, float z, WaypointMission.Builder wpm) {
        LocationCoordinate3D currentloc = M210Manager.getInstance().getDroneStatus();
        List<LocationCoordinate3D> points = new ArrayList<>();
        points.add(currentloc);
        points.add(new LocationCoordinate3D(x, y, z));
        List<LocationCoordinate3D> locationCoordinate3DS = genPoints(genZPoints(points));
        if (locationCoordinate3DS.size() == 2) {
            wpm.addWaypoint(new Waypoint(x, y + 0.00005, z));
            wpm.addWaypoint(new Waypoint(x, y, z));
        } else {
            for (LocationCoordinate3D loc : locationCoordinate3DS) {
                wpm.addWaypoint(new Waypoint(loc.getLatitude(), loc.getLongitude(), loc.getAltitude()));
            }
        }

        return wpm;
    }
}
