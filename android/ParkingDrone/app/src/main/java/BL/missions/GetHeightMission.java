package BL.missions;

import android.os.IBinder;
import android.util.Log;

import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Logger;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class GetHeightMission extends Mission {
    float height;

    public GetHeightMission(int index) {
        super("getHeight", index);
    }

    @Override
    public void start() {
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        FlightController flightController = aircraft.getFlightController();
        float altBySensor = -99;
        if (flightController.getState().isUltrasonicBeingUsed()) {
            altBySensor = flightController.getState().getUltrasonicHeightInMeters();
        }
        float altByGps = flightController.getState().getAircraftLocation().getAltitude();
        if (altByGps < 7 && altBySensor != -99 ) {
            Logger.info("gps lower then 6m determine height by sensor");
            height = altBySensor;
        }
        else {
            Logger.info("gps above 6m determine height by gps");
            height = altByGps;
        }
        onResult.onResult(null);
    }

    @Override
    public void stop() {
    }

    @Override
    public String encode() {
        return getName() + " " + getIndex() + " Done " + height;
    }
}
