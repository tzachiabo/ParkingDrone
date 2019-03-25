using DroneServer.BL;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    class LngLatHelper
    {
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

        public static Point getLocationByBearingAndDistance(double latitude, double longitude, double distance, double bearing)
        {
            return null;
        }
    }
}
