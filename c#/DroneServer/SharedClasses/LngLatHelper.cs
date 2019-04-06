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
    }
}
