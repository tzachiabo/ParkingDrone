package BL.missions;

import dji.sdk.flightcontroller.FlightController;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class ConfirmLandingMission extends Mission{

    public ConfirmLandingMission(int index){
        super("confirmLanding", index);
    }

    @Override
    public void start() {
        Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        if(aircraft != null) {
            FlightController controller = aircraft.getFlightController();
            if (controller != null) {
                controller.confirmLanding(this.onResult);
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
