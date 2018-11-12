package BL.missions;

import SharedClasses.Assertions;
import SharedClasses.Logger;
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
        Assertions.verify(aircraft != null, "when try to confirm landing got null aircraft");

        FlightController controller = aircraft.getFlightController();
        Assertions.verify(controller != null, "when try to confirm landing got null controller");

        controller.confirmLanding(this.onResult);
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
