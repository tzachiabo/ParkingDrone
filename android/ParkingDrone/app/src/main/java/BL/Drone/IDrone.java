package BL.Drone;

import SharedClasses.Promise;
import dji.common.flightcontroller.FlightControllerState;
import dji.common.flightcontroller.LocationCoordinate3D;

public interface IDrone {
    boolean isInitiated();
    void initAircraft();
    void takeOff(final Promise p);
    void startLanding(final Promise p);
    void stopLanding();
    void moveByGPS(double x, double y, float z, final Promise p);
    void stopMoveByGPS();
    void confirmLanding(final Promise cb);
    void goHome(final Promise cb);
    void stopGoHome();
    FlightControllerState getDroneState();
    float getDroneBearing();
}
