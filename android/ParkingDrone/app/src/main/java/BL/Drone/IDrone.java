package BL.Drone;

import SharedClasses.Promise;
import dji.common.flightcontroller.LocationCoordinate3D;

public interface IDrone {
    boolean isInitiated();

    void takeOff(final Promise p);
    void moveByGPS(double x, double y, float z, final Promise p);
    void confirmLanding(final Promise cb);
    void goHome(final Promise cb);
    LocationCoordinate3D getDroneStatus();
}
