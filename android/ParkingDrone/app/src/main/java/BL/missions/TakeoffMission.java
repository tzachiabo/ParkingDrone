package BL.missions;

import BL.Config;
import SharedClasses.Assertions;
import SharedClasses.Logger;
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
        Assertions.verify(aircraft != null, "when take off mission aircraft is null");

        aircraft.getFlightController().startTakeoff(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError != null) {
                    Logger.error("after takeoff djierror is " + djiError.toString());
                    Assertions.verify(false, "failed to move drone");
                }

                float height;
                long startTime = System.currentTimeMillis();
                do {
                    height = aircraft.getFlightController().getState().getAircraftLocation().getAltitude();
                    Assertions.verify( System.currentTimeMillis() - startTime > Config.MAX_TIME_WAIT_FOR_TAKEOFF,
                            "Takeoff timeout: wait too much time for takeoff");
                }
                while(height < 1.1);

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
