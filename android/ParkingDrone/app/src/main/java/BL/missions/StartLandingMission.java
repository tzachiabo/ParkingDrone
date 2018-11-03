package BL.missions;


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
        if(aircraft != null) {
            FlightController controller = aircraft.getFlightController();
            if (controller != null) {
                controller.startLanding(new CommonCallbacks.CompletionCallback() {
                    @Override
                    public void onResult(DJIError djiError) {
                        while(!aircraft.getFlightController().getState().isLandingConfirmationNeeded());
                        onResult.onResult(djiError);
                    }
                });
            }
        }
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }

}
