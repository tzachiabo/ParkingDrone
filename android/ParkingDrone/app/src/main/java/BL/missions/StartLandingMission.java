package BL.missions;

import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Config;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class StartLandingMission extends Mission {

    public StartLandingMission(int index){
        super("startLanding", index);
    }

    @Override
    public void start() {
        IDrone drone = DroneFactory.getDroneManager();
        drone.startLanding(new Promise() {
            @Override
            protected void onSuccess() {
                Logger.info("finish start landing mission ;");
                onResult.onResult(null);
            }

            @Override
            public void onFailed() {
                Logger.fatal("failed to start landing " + index);
            }
        });
    }

    @Override
    public void stop() {
        IDrone drone = DroneFactory.getDroneManager();
        drone.stopLanding();
    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }

}
