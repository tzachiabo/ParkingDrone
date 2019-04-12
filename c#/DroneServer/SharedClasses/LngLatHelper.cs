using DroneServer.BL;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    public class LngLatHelper
    {
        private const double DegreesToRadians = Math.PI / 180.0;
        private const double RadiansToDegrees = 180.0 / Math.PI;
        private const double EarthRadius = 6378137.0;

        public static Point toMarginFromBasePhoto(Point location)  // not support bearing
        {
            Point base_photo_location = BL.BLManagger.getInstance().get_parking().getBasePoint();

            var sCoord = new GeoCoordinate(location.lat, base_photo_location.lng);
            var eCoord = new GeoCoordinate(location.lat, location.lng);
            double x_distance = sCoord.GetDistanceTo(eCoord);
            if (base_photo_location.lng > location.lng)
                x_distance *= -1;

            sCoord = new GeoCoordinate(base_photo_location.lat, location.lng);
            eCoord = new GeoCoordinate(location.lat, location.lng);
            double y_distance = sCoord.GetDistanceTo(eCoord);

            if (base_photo_location.lat < location.lat)
                y_distance *= -1;

            int mid_width_pixel = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels")) / 2;
            int mid_height_pixel = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels")) / 2;

            Point res = new Point(PixelConverterHelper.convert_width(mid_width_pixel) + x_distance, PixelConverterHelper.convert_height(mid_height_pixel) + y_distance);
            return res;
        }


        public static GeoCoordinate getLocationByBearingAndDistance(GeoCoordinate source, double range, double bearing)
        {
            var latA = source.Latitude * DegreesToRadians;
            var lonA = source.Longitude * DegreesToRadians;
            var angularDistance = range / EarthRadius;
            var trueCourse = bearing * DegreesToRadians;

            var lat = Math.Asin(
                Math.Sin(latA) * Math.Cos(angularDistance) +
                Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            var dlon = Math.Atan2(
                Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
                Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

            var lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            GeoCoordinate res = new GeoCoordinate(lat * RadiansToDegrees, lon * RadiansToDegrees, source.Altitude);
            return res;
        }

        public static double getBearingBetweenLatLngPoints(Point source, Point destination)
        {
            double lat1 = degreeToRad(source.lat);
            double lng1 = degreeToRad(source.lng);

            double lat2 = degreeToRad(source.lat);
            double lng2 = degreeToRad(source.lng);

            double dLon = (lng2 - lng1);
            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            double brng = radToDegree((Math.Atan2(y, x)));
            brng = (360 - ((brng + 360) % 360));

            return brng;
        }

        public static double getBearingBetweenMarginPoints(Point source, Point destination)
        {
            double delta_y = source.lat - destination.lat;
            double delta_x = source.lng - destination.lng;

            if (destination.lat < source.lat && destination.lng > source.lng)
            {
                double degree_res = radToDegree(Math.Atan(delta_x / delta_y));

                return degree_res;
            }
            else if (destination.lat > source.lat && destination.lng > source.lng)
            {
                double degree_res = radToDegree(Math.Atan(delta_x / delta_y));

                return 180 - degree_res;
            }
            else if (destination.lat > source.lat && destination.lng < source.lng)
            {
                double degree_res = radToDegree(Math.Atan(delta_x / delta_y));

                return degree_res + 180;
            }
            else if (destination.lat < source.lat && destination.lng < source.lng)
            {
                double degree_res = radToDegree(Math.Atan(delta_x / delta_y));

                return 360 - degree_res;
            }
            else if (destination.lat == source.lat && destination.lng > source.lng)
            {
                return 0;
            }
            else if (destination.lat == source.lat && destination.lng < source.lng)
            {
                return 180;
            }
            else if (destination.lat > source.lat && destination.lng == source.lng)
            {
                return 90;
            }
            else if (destination.lat < source.lat && destination.lng == source.lng)
            {
                return 270;
            }

            return 0;
        }

        public static double getDistanceBetweenMarginPoints(Point source, Point destination)
        {
            double delta_y = Math.Abs(source.lat - destination.lat);
            double delta_x = Math.Abs(source.lng - destination.lng);

            return Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2));
        }

        public static double radToDegree(double rad)
        {
            return rad * 180 / Math.PI;
        }

        public static double degreeToRad(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}
