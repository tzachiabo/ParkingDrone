package BL.missions;

import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Config;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class TakeoffMission extends Mission {
    public TakeoffMission(int index){
        super("takeOff", index);
    }
    @Override
    public void start() {
        IDrone drone = DroneFactory.getDroneManager();
        drone.takeOff(new Promise() {

            @Override
            public void onSuccess() {
                onResult.onResult(null);
            }

            @Override
            public void onFailed() {
                Logger.fatal("failed to take off");
            }
        });
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }
}
