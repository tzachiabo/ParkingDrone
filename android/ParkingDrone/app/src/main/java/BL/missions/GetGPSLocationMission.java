package BL.missions;

import SharedClasses.Assertions;
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
        final Aircraft aircraft = (Aircraft) DJISDKManager.getInstance().getProduct();
        Assertions.verify(aircraft != null, "when get Location mission aircraft is null");

        LocationCoordinate3D lc3d = aircraft.getFlightController().getState().getAircraftLocation();

        Altitude = lc3d.getAltitude();
        Latitude = lc3d.getLatitude();
        Longitude = lc3d.getLongitude();
    }

    @Override
    public void stop() { }

    @Override
    public String encode() {
        return getName() +" "+ getIndex()+ " " + Altitude + " " + Latitude + " " + Longitude;
    }
}
