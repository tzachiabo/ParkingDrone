package BL.missions;



import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.common.error.DJIError;
import dji.common.flightcontroller.GoHomeExecutionState;
import dji.common.model.LocationCoordinate2D;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class GoHomeMission extends Mission {
    LocationCoordinate2D home;

    public GoHomeMission(int index) {
        super("goHome", index);
    }

    @Override
    public void start() {
        IDrone drone = DroneFactory.getDroneManager();
        drone.goHome(new Promise() {
            @Override
            public void onSuccess() {
                onResult.onResult(null);
            }

            @Override
            public void onFailed() {
                Logger.fatal("mission go home failed");
            }
        });
    }

    @Override
    public void stop() {
        //todo: there is a built in method for this function
    }

    @Override
    public String encode() {
        return getName() + " " + getIndex() + " " + "Done";
    }
}
