package BL.missions;

import BL.TaskManager;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class StopMission extends Mission {
    public StopMission(int index){
        super("stop", index);
    }
    @Override
    public void start() {
        Logger.info("Stop mission started");
        TaskManager.getInstance().stopAllTasks();

        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "when stop mission aircraft is null");

        aircraft.getFlightController().setVirtualStickModeEnabled(false, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError == null){
                    Logger.info("set virtual stick off");
                }
                else {
                    Logger.fatal("Failed to set virtual stick off");

                }
            }
        });

        Logger.info("Stop mission finished");
        onResult.onResult(null);
    }

    @Override
    public void stop() {
        Logger.warn("stop mission number "+ getIndex() + " has requested to stop");
    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " Done";
    }
}
