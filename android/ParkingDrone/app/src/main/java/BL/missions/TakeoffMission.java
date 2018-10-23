package BL.missions;

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
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        if(aircraft != null)
            aircraft.getFlightController().startTakeoff(new CommonCallbacks.CompletionCallback() {
                @Override
                public void onResult(DJIError djiError) {
                    float height = aircraft.getFlightController().getState().getAircraftLocation().getAltitude();
                    while(height < 1.1){
                        height = aircraft.getFlightController().getState().getAircraftLocation().getAltitude();
                    }
                    onResult.onResult(djiError);
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
