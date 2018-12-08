package BL.missions;

import BL.Drone.DJIM210.M210Manager;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import dji.common.flightcontroller.LocationCoordinate3D;
import dji.sdk.products.Aircraft;
import dji.sdk.sdkmanager.DJISDKManager;

public class GetGPSLocationMission extends Mission {
    double Altitude;
    double Latitude;
    double Longitude;

    public GetGPSLocationMission(int index){
        super("getLocation", index);
    }

    @Override
    public void start() {
        Logger.debug("start getGPSLocation mission");
        LocationCoordinate3D lc3d = M210Manager.getInstance().getDroneStatus();
        Altitude = lc3d.getAltitude();
        Latitude = lc3d.getLatitude();
        Longitude = lc3d.getLongitude();

        onResult.onResult(null);
    }

    @Override
    public void stop() { }

    @Override
    public String encode() {
        return getName() +" "+ getIndex()+ " Done " + Altitude + " " + Latitude + " " + Longitude;
    }
}
