package BL.missions;

import dji.sdk.flightcontroller.FlightController;
import dji.sdk.mission.timeline.actions.AircraftYawAction;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class StartLandingMission extends Mission {

    public StartLandingMission(int index){
        super("startLanding", index);
    }

    @Override
    public void start() {
        Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        if(aircraft != null) {
            FlightController controller = aircraft.getFlightController();
            if (controller != null) {
                controller.startLanding(this.onResult);
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
