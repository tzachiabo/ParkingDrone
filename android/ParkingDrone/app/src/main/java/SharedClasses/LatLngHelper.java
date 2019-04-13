package SharedClasses;

import dji.common.flightcontroller.LocationCoordinate3D;

public class LatLngHelper {

    public static double getDistanceBetweenTwoPoints(LocationCoordinate3D point_a, LocationCoordinate3D point_b)
    {
        return distance(point_a.getLatitude(), point_a.getLongitude(),
                point_b.getLatitude(), point_b.getLongitude());
    }

    private static double distance(double lat1, double lon1, double lat2, double lon2) {
        double R = 6371; // km
        double dLat = deg2rad(lat2-lat1);
        double dLon = deg2rad(lon2-lon1);
        double a = Math.sin(dLat/2) * Math.sin(dLat/2) +
                Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
                        Math.sin(dLon/2) * Math.sin(dLon/2);
        double c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
        double d = R * c;
        return d * 1000;
    }

    /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
    /*::  This function converts decimal degrees to radians             :*/
    /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
    private static double deg2rad(double deg) {
        return (deg * Math.PI / 180.0);
    }

    /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
    /*::  This function converts radians to decimal degrees             :*/
    /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
    private static double rad2deg(double rad) {
        return (rad * 180.0 / Math.PI);
    }
}
