package BL.Drone.DJIM210;

import java.util.concurrent.CompletableFuture;

import SharedClasses.AssertionViolation;
import SharedClasses.Assertions;
import SharedClasses.Config;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class ControllerManager {
    FlightController m_flight_controller;

    public ControllerManager(Aircraft aircraft){
        m_flight_controller = aircraft.getFlightController();
        Assertions.verify(m_flight_controller != null,
                "FlightController is null when constracting ControllerManager");
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

}
