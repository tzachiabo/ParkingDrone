package BL.missions;

import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class TakeoffMission extends Mission {
    public TakeoffMission(int index){
        super("takeOff", index);
    }
    @Override
    public void start() {
        Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        if(aircraft != null)
            aircraft.getFlightController().startTakeoff(this.onResult);
    }

    @Override
    public void stop() {

    }

    @Override
    public String encode() {
        return getName() +" "+ getIndex() + " " + "Done";
    }
}
