package BL.missions;

import SharedClasses.Config;
import SharedClasses.Assertions;
import SharedClasses.Logger;
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
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "when try to start landing got null aircraft");

        FlightController controller = aircraft.getFlightController();
        Assertions.verify(controller != null, "when try to start landing got null controller");

        controller.startLanding(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if(djiError != null) {
                    Logger.error("after start landing djierror is " + djiError.toString());
                    Assertions.verify(false, "failed to move drone");
                }

                long startTime = System.currentTimeMillis();
                Logger.debug("start time "+ startTime);

                while(!aircraft.getFlightController().getState().isLandingConfirmationNeeded() && !hasStoped){
                    Assertions.verify( System.currentTimeMillis() - startTime < Config.MAX_TIME_WAIT_FOR_LANDING,
                            "Start Landing timeout: wait too much time for landing confirmation");
                }
                if (!hasStoped)
                    onResult.onResult(djiError);
            }
        });
    }

    @Override
    public void stop() {
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Logger.debug("Cancel start landing");
        aircraft.getFlightController().cancelLanding(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError != null)
                    Logger.error("err while cancel landing "+ djiError.toString());
            }
        });
        hasStoped = true;
    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }

}
