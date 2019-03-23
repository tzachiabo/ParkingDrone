using DroneServer.BL;
using System;
using System.Collections.Generic;
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

            return new Point(width, height); 
        }

        public Point getCarLocationByGPS()
        {
            Point car_location_with_margin_to_base_photo = getPointOfCar();
            Point base_photo_point = BL.BLManagger.getInstance().get_parking().getBasePoint();

            int cameraOpeningDegree = Int32.Parse(Configuration.getInstance().get("cameraOpeningDegree"));
            double radius = base_photo_point.alt * Math.Tan(DegreesToRadians(cameraOpeningDegree));

            GeoLocation geo_base_loc = new GeoLocation();
            geo_base_loc.Latitude = base_photo_point.lat;
            geo_base_loc.Longitude = base_photo_point.lng;

            GeoLocation left = FindPointAtDistanceFrom(geo_base_loc, (3 / 2) * Math.PI, radius / 1000);
            GeoLocation top_left = FindPointAtDistanceFrom(left, 0, radius / 1000);

            GeoLocation margin_left_from_top_left = FindPointAtDistanceFrom(top_left, Math.PI / 2, car_location_with_margin_to_base_photo.lng / 1000);
            GeoLocation car_location_geo = FindPointAtDistanceFrom(margin_left_from_top_left, Math.PI, car_location_with_margin_to_base_photo.lat / 1000);

            return new Point(car_location_geo.Longitude, car_location_geo.Latitude);
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

    }
}
