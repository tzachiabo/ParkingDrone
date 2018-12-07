package BL.missions;

import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import SharedClasses.Promise;
import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class ConfirmLandingMission extends Mission{

    public ConfirmLandingMission(int index){
        super("confirmLanding", index);
    }

    @Override
    public void start() {
        IDrone drone = DroneFactory.getDroneManager();
        drone.confirmLanding(new Promise() {

            @Override
            public void onSuccess() {
                onResult.onResult(null);
            }

            @Override
            public void onFailed() {
                Logger.fatal("mission confirm landing : failed to land");
            }
        });

    }

    @Override
    public void stop() {
        Logger.info("Confirm landing got stop request - did nothing");
    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }

}
