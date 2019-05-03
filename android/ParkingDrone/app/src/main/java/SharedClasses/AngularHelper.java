package SharedClasses;

public class AngularHelper {

    private static double toRads(double degrees)
    {
        return degrees * Math.PI / 180;
    }
    private static double toDegrees(double rads)
    {
        return rads * 180 / Math.PI;
    }
    public static double angularDistance(double destBearing, double currBearing)
    {
        destBearing = toRads(destBearing);
        currBearing = toRads(currBearing);
        return Math.abs(toDegrees(Math.atan2(Math.sin(destBearing - currBearing), Math.cos(destBearing - currBearing))));
    }
}
