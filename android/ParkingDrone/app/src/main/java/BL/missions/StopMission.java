package BL.missions;

import BL.TaskManager;
import SharedClasses.RemoteLogCat;
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
        TaskManager.getInstance().stopAllTasks();

        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        aircraft.getFlightController().setVirtualStickModeEnabled(false, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError == null){
                    RemoteLogCat.info("set virtual stick off");
                }
                else {
                    RemoteLogCat.fatal("Failed to set virtual stick off");
                }
            }
        });
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + "Done";
    }
}
