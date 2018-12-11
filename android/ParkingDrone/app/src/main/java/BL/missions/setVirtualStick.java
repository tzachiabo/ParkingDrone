package BL.missions;

import BL.TaskManager;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import dji.common.error.DJIError;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class setVirtualStick extends Mission {

    public setVirtualStick(int index){
        super("setVirtualStick", index);
    }

    @Override
    public void start() {

        Logger.info("setVirtualStick mission started");
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "when setVirtualStick aircraft is null");

        aircraft.getFlightController().setVirtualStickModeEnabled(true, new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError == null){
                    Logger.info("set virtual stick on");
                    onResult.onResult(null);
                }
                else {
                    Logger.fatal("Failed to set virtual stick on " + djiError.toString());
                }
            }
        });

    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return "setVirtualStick "+ getIndex() + " Done";
    }
}
