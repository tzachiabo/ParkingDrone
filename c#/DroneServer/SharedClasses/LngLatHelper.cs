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

        public static Point toMarginFromBasePhoto(Point location)
        {
            Point base_photo_location = BL.BLManagger.getInstance().get_parking().getBasePoint();

            int mid_width_pixel = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels")) / 2;
            int mid_height_pixel = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels")) / 2;
            double margin_top_base_location = PixelConverterHelper.convert_height(mid_height_pixel);
            double margin_left_base_location = PixelConverterHelper.convert_width(mid_width_pixel);

            var sCoord = new GeoCoordinate(base_photo_location.lat, base_photo_location.lng);
            var eCoord = new GeoCoordinate(location.lat, location.lng);
            double distance = sCoord.GetDistanceTo(eCoord);

            double bearing = DegreeBearing(base_photo_location.lat, base_photo_location.lng, 
                                           location.lat, location.lng);

            double pic_bearing = bearing - BL.BLManagger.getInstance().get_parking().bearing;

            double distance_x = distance * Math.Sin(ToRad(pic_bearing));
            double distance_y = distance * Math.Cos(ToRad(pic_bearing));

            Point res = new Point(margin_left_base_location + distance_x, margin_top_base_location - distance_y);
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

        public static double DegreeBearing(double lat1, double lon1, double lat2, double lon2)
        {
            var dLon = ToRad(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        public static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double ToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) + 360) % 360;
        }

        public static double getBearingBetweenMarginPoints(Point source, Point destination)
        {
            double delta_y = Math.Abs(source.lat - destination.lat);
            double delta_x = Math.Abs(source.lng - destination.lng);

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
