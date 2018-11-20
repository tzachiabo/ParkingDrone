package BL.missions;



import SharedClasses.Assertions;
import SharedClasses.Logger;
import dji.common.error.DJIError;
import dji.common.model.LocationCoordinate2D;
import dji.common.util.CommonCallbacks;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class GoHomeMission extends Mission {
    LocationCoordinate2D home;

    public GoHomeMission(int index) {
        super("goHome", index);
    }

    @Override
    public void start() {
        Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "aircraft is null on GoHomeMission.start");
        aircraft.getFlightController().startGoHome(new CommonCallbacks.CompletionCallback() {
            @Override
            public void onResult(DJIError djiError) {
                if (djiError != null) {
                    Logger.error("start go home resulted in dji error " + djiError.toString());
                    Assertions.verify(
                            false,
                            "failed to set GOHome on GoHomeMission.start");
                } else {
                    Logger.info("Drone is Going Home!");
                }
            }
        });
    }

    @Override
    public void stop() {
        //todo: there is a built in method for this function
    }

    @Override
    public String encode() {
        return getName() + " " + getIndex() + " " + "Done";
    }
}
