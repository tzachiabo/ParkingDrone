package BL.Drone.DJIM210;

import java.util.concurrent.CompletableFuture;

import SharedClasses.AssertionViolation;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.flightcontroller.virtualstick.FlightCoordinateSystem;
import dji.common.flightcontroller.virtualstick.RollPitchControlMode;
import dji.common.flightcontroller.virtualstick.VerticalControlMode;
import dji.common.flightcontroller.virtualstick.YawControlMode;
import dji.common.flightcontroller.LocationCoordinate3D;
import dji.common.util.CommonCallbacks;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class ControllerManager {
    FlightController m_flight_controller;
    private boolean isInitiated;

    public ControllerManager(Aircraft aircraft){
        m_flight_controller = aircraft.getFlightController();
        Assertions.verify(m_flight_controller != null,
                "FlightController is null when constracting ControllerManager");

        isInitiated = false;
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
                        Logger.info("Drone is Going Home!");

                        while (m_flight_controller.getState().isGoingHome());

                        cb.success();
                    }
                }
            });
        }
        catch(AssertionViolation e){
            cb.failed();
        }
    }

    private void initFlightController() {
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
                        Thread.sleep(500);
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

    public LocationCoordinate3D  getDroneStatus(){
        Logger.debug("start getGPSLocation mission");
        LocationCoordinate3D lc3d = m_flight_controller.getState().getAircraftLocation();
        return lc3d;
    }


}
