package BL.Drone;

import SharedClasses.Promise;

public interface IDrone {
    void takeOff(final Promise p);
    void moveByGPS(double x, double y, float z, final Promise p);
    void confirmLanding(final Promise cb);
    void goHome(final Promise cb);
}
