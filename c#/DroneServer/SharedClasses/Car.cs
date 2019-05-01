using DroneServer.BL;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    public class Car
    {
        private String type;
        private int precent;
        private double m_left_margin;
        private double m_top_margin;
        private double m_width_of_car;
        private double m_height_of_car;

        public Car(String type, int precent, int left_margin, int top_margin, int width, int height, double base_photo_height)
        {
            this.type = type;
            this.precent = precent;

            this.m_left_margin = PixelConverterHelper.convert_width(left_margin);
            this.m_top_margin = PixelConverterHelper.convert_height(top_margin);

            this.m_width_of_car = PixelConverterHelper.convert_width(width);
            this.m_height_of_car = PixelConverterHelper.convert_height(height);

            Logger.getInstance().info("Car ctor m_left_margin: " + m_left_margin + " m_top_margin: " + m_top_margin +
                                      " m_width_of_car " + m_width_of_car + " m_height_of_car " + m_height_of_car);
        }


        public Point getPointOfCar()
        {
            double width = m_left_margin + m_width_of_car / 2;
            double height = m_top_margin + m_height_of_car / 2;
            if (width > 2)
            {

                Logger.getInstance().info("adjusting car width by -1");
                width -= 1;
            }

            return new Point(width, height); 
        }

        public Point getCarLocationByGPS()
        {
            Point car_location_with_margin_to_base_photo = getPointOfCar();
            Point base_photo_point = BL.BLManagger.getInstance().get_parking().getBasePoint();

            int max_width = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels"));
            int max_height = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels"));
            Point base_photo_point_margin = new Point(max_width/2, max_height/2);

            double parking_bearing = BLManagger.getInstance().get_parking().bearing;
            double car_relative_bearing = LngLatHelper.DegreeBearing(base_photo_point_margin.lat, base_photo_point_margin.lng,
                                           car_location_with_margin_to_base_photo.lat, car_location_with_margin_to_base_photo.lng);

            double absolute_bearing = parking_bearing + car_relative_bearing;
            double distance = LngLatHelper.getDistanceBetweenMarginPoints(base_photo_point_margin, car_location_with_margin_to_base_photo);

            GeoCoordinate middle = new GeoCoordinate();
            middle.Latitude = base_photo_point.lat;
            middle.Longitude = base_photo_point.lng;

            GeoCoordinate car_location = LngLatHelper.getLocationByBearingAndDistance(middle, distance, absolute_bearing);

            return new Point(car_location.Longitude, car_location.Latitude);
        }

        public static GeoLocation FindPointAtDistanceFrom(GeoLocation startPoint, double initialBearingRadians, double distanceKilometres)
        {
            const double radiusEarthKilometres = 6371.01;
            var distRatio = distanceKilometres / radiusEarthKilometres;
            var distRatioSine = Math.Sin(distRatio);
            var distRatioCosine = Math.Cos(distRatio);

            var startLatRad = DegreesToRadians(startPoint.Latitude);
            var startLonRad = DegreesToRadians(startPoint.Longitude);

            var startLatCos = Math.Cos(startLatRad);
            var startLatSin = Math.Sin(startLatRad);

            var endLatRads = Math.Asin((startLatSin * distRatioCosine) + (startLatCos * distRatioSine * Math.Cos(initialBearingRadians)));

            var endLonRads = startLonRad
                + Math.Atan2(
                    Math.Sin(initialBearingRadians) * distRatioSine * startLatCos,
                    distRatioCosine - startLatSin * Math.Sin(endLatRads));

            return new GeoLocation
            {
                Latitude = RadiansToDegrees(endLatRads),
                Longitude = RadiansToDegrees(endLonRads)
            };
        }

        public struct GeoLocation
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public double getCarHeight()
        {
            return m_height_of_car;
        }

        public static double DegreesToRadians(double degrees)
        {
            const double degToRadFactor = Math.PI / 180;
            return degrees * degToRadFactor;
        }

        public static double RadiansToDegrees(double radians)
        {
            const double radToDegFactor = 180 / Math.PI;
            return radians * radToDegFactor;
        }

        public Rectangle GetRectangle()
        {
            int x = PixelConverterHelper.re_convert_width(this.m_left_margin);
            int y = PixelConverterHelper.re_convert_height(this.m_top_margin);

            int width = PixelConverterHelper.re_convert_width(this.m_width_of_car);
            int height = PixelConverterHelper.re_convert_height(this.m_height_of_car);

            return new Rectangle(x, y, width, height);
        }

    }
}
