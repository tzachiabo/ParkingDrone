package BL.Drone.DJIM210;

import android.support.annotation.NonNull;

import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.AssertionViolation;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.flightcontroller.FlightControllerState;
import dji.common.flightcontroller.virtualstick.FlightCoordinateSystem;
import dji.common.flightcontroller.virtualstick.RollPitchControlMode;
import dji.common.flightcontroller.virtualstick.VerticalControlMode;
import dji.common.flightcontroller.virtualstick.YawControlMode;
import dji.common.flightcontroller.LocationCoordinate3D;
import dji.common.model.LocationCoordinate2D;
import dji.common.util.CommonCallbacks;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class ControllerManager {
    FlightController m_flight_controller;
    FlightControllerState flight_controller_state;
    private boolean isInitiated;
    private boolean hasStoped;

    public ControllerManager(Aircraft aircraft){
        m_flight_controller = aircraft.getFlightController();
        Assertions.verify(m_flight_controller != null,
                "FlightController is null when constracting ControllerManager");

        isInitiated = false;
        flight_controller_state = m_flight_controller.getState();
        m_flight_controller.setStateCallback(new FlightControllerState.Callback() {
            @Override
            public void onUpdate(@NonNull FlightControllerState flightControllerState) {
                flight_controller_state = flightControllerState;
            }
        });
        initFlightController();
    }

    public void takeOff(final Promise cb) {
        m_flight_controller.startTakeoff(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                try {
                    if (djiError != null) {
                        Logger.error("after takeoff djierror is " + djiError.toString());
                        Assertions.verify(false,
                                "failed to take off drone returned " + djiError.toString());
                    }

                    float height;
                    long startTime = System.currentTimeMillis();
                    do {
                        height = m_flight_controller.getState().getAircraftLocation().getAltitude();
                        Assertions.verify(System.currentTimeMillis() - startTime < Config.MAX_TIME_WAIT_FOR_TAKEOFF,
                                "Takeoff timeout: wait too much time for takeoff");
                    }
                    while (height < 1.1);

                    m_flight_controller.setHomeLocationUsingAircraftCurrentLocation(null);

                    cb.success();
                }
                catch (AssertionViolation e){
                    cb.failed();
                }
            }
        });
    }

    public void confirmLanding(final Promise cb){
        m_flight_controller.confirmLanding(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError == null){
                    while (m_flight_controller.getState().areMotorsOn());
                    cb.success();
                }
                else{
                    Logger.error("confirm landing failed with err : " + djiError.toString());
                    cb.failed();
                }
            }
        });
    }

    public void goHome(final Promise cb){
        hasStoped = false;
        Logger.debug("hasStoped=false");

        try{
            m_flight_controller.startGoHome(new CommonCallbacks.CompletionCallback() {
                @Override
                public void onResult(DJIError djiError) {
                    if (djiError != null) {
                        Logger.error("start go home resulted in dji error " + djiError.toString());
                        Assertions.verify(
                                false,
                                "failed to set GOHome on GoHomeMission.start");
                    } else {
                        try {
                            Logger.info("Drone is Going Home!");

                            Thread.sleep(500);

                            m_flight_controller.getHomeLocation(new CommonCallbacks.CompletionCallbackWith<LocationCoordinate2D>() {
                                @Override
                                public void onSuccess(LocationCoordinate2D locationCoordinate2D) {
                                    double distance;
                                    do {
                                        LocationCoordinate3D curr_loc = m_flight_controller.getState().getAircraftLocation();
                                        distance = gps2m(locationCoordinate2D.getLatitude(),
                                                locationCoordinate2D.getLongitude(),
                                                curr_loc.getLatitude(),
                                                curr_loc.getLongitude());
                                    }
                                    while (distance > 3 && !hasStoped);

                                    if (!hasStoped)
                                        cb.success();
                                }

                                @Override
                                public void onFailure(DJIError djiError) {
                                    cb.failed();
                                }
                            });
                        }
                        catch (InterruptedException e) {
                            Logger.fatal("got interupted exception when go home");
                        }
                    }
                }
            });
        }
        catch(AssertionViolation e){
            cb.failed();
        }
    }

    private double gps2m(double lat_a, double lng_a, double lat_b, double lng_b) {
        double pk = 180/3.14169;

        double a1 = lat_a / pk;
        double a2 = lng_a / pk;
        double b1 = lat_b / pk;
        double b2 = lng_b / pk;

        double t1 = Math.cos(a1)*Math.cos(a2)*Math.cos(b1)*Math.cos(b2);
        double t2 = Math.cos(a1)*Math.sin(a2)*Math.cos(b1)*Math.sin(b2);
        double t3 = Math.sin(a1)*Math.sin(b1);
        double tt = Math.acos(t1 + t2 + t3);

        return 6366000*tt;
    }

    public void stopGoHome(){
        hasStoped = true;
        Logger.debug("hasStoped=true");
        m_flight_controller.cancelGoHome(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {

                if (djiError!= null){
                    Logger.error("stop go home resulted in dji error " + djiError.toString());
                }
                else{
                    Logger.info("stop go home finished");

                }
            }
        });
    }

    private void initFlightController() {
        m_flight_controller.setESCBeepEnabled(false,null);
        m_flight_controller.setRollPitchControlMode(RollPitchControlMode.VELOCITY);
        m_flight_controller.setYawControlMode(YawControlMode.ANGULAR_VELOCITY);
        m_flight_controller.setVerticalControlMode(VerticalControlMode.VELOCITY);
        m_flight_controller.setRollPitchCoordinateSystem(FlightCoordinateSystem.BODY);

        m_flight_controller.setVirtualStickModeEnabled(true, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError != null) {
                    Logger.error("Setting virtual stick mode resulted " + djiError.toString());
                    try {
                        Thread.sleep(2000);
                    } catch (InterruptedException e) {
                    }
                    initFlightController();
                } else {
                    Logger.info("init FlightController has been done");
                    isInitiated = true;
                }
            }
        });
    }

    public boolean isInitiated() {
        if (!isInitiated){
            Logger.info("flight controller is not initiated yet");
        }
        return isInitiated;
    }

    public FlightControllerState  getDroneState(){
        return flight_controller_state;
    }

    public void startLanding(final Promise p){
        hasStoped = false;
        m_flight_controller.startLanding(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError != null) {
                    Logger.error("after start landing djierror is " + djiError.toString());
                    if (djiError == DJIError.COMMON_TIMEOUT){
                        try {
                            Thread.sleep(3000);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                        startLanding(p);
                        return;
                    }
                    else {
                        p.failed();
                        return;
                    }
                }

                Logger.info("start start landing");
                long startTime = System.currentTimeMillis();
                while (!isFinishedLanding() && !hasStoped) {
                    if (System.currentTimeMillis() - startTime > Config.MAX_TIME_WAIT_FOR_LANDING)
                    {
                        p.failed();
                        return;
                    }
                    try {
                        Thread.sleep(500);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }

                Logger.info("finish start landing hasStop="+ hasStoped);
                if (!hasStoped)
                    p.success();
                else
                    p.failed();
            }
        });

    }

    private boolean isFinishedLanding(){
        return getDroneState().isLandingConfirmationNeeded();
    }

    public void stopLanding() {
        hasStoped = true;
        m_flight_controller.cancelLanding(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError != null)
                    Logger.error("err while cancel landing "+ djiError.toString());
            }
        });
    }

}
