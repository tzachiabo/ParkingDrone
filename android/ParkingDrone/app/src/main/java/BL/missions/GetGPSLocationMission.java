package BL.missions;

import BL.Drone.DroneFactory;
import BL.Drone.IDrone;
import SharedClasses.Assertions;
import SharedClasses.Logger;
import dji.common.flightcontroller.FlightControllerState;
import dji.common.flightcontroller.LocationCoordinate3D;

public class GetGPSLocationMission extends Mission {
    double Altitude;
    double Latitude;
    double Longitude;

    public GetGPSLocationMission(int index){
        super("getLocation", index);
    }

    @Override
    public void start() {
        Logger.info("start getGPSLocation mission num " + index);
        IDrone drone = DroneFactory.getDroneManager();
        FlightControllerState droneState = null;
        while (droneState == null){
            droneState = drone.getDroneState();
        }
        LocationCoordinate3D lc3d = droneState.getAircraftLocation();
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
