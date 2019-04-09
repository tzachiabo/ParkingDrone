using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using DroneServer.BL;
using GMap.NET.WindowsForms;
using System.Device;
using System.Device.Location;

namespace DroneServer.SharedClasses
{
    public class Parking
    {
        public string name;
        public double lat;
        public double lng;
        public double zoom;
        public double maxZoom;
        public double minZoom;
        public double bearing;
        public List<Point> border;
        public Point basePossition;

        public Parking()
        {
            this.name = null;
            this.lat = 0;
            this.lng = 0;
            this.zoom = 0;
            this.maxZoom = 0;
            this.minZoom = 0;
            this.border = null;
            this.bearing = 404;
        }
        public Parking(string name, double lat, double lng, double zoom, double maxZoom, double minZoom, double bearing, List<Point> border)
        {
            this.name = name;
            this.lat = lat;
            this.lng = lng;
            this.zoom = zoom;
            this.maxZoom = maxZoom;
            this.minZoom = minZoom;
            this.border = border;
            this.bearing = bearing;
        }
        public Parking(string name, List<Point> border)
        {
            this.name = name;
            this.border = border;
            bearing = 0;
        }

        public Parking(TextBox parkingName ,GMapControl map,ListBox points)
        {
            name = parkingName.Text;
            lat = map.Position.Lat;
            lng = map.Position.Lng;
            zoom = map.Zoom;
            maxZoom = map.MaxZoom;
            minZoom = map.MinZoom;
            border = new List<Point>();
            foreach (string item in points.Items)
                border.Add(new SharedClasses.Point(Convert.ToDouble(item.Split(' ')[0]), Convert.ToDouble(item.Split(' ')[1])));
        }
        public Point getBasePoint()
        {
            if (basePossition == null)
            {
                var XList = new List<double>();
                var YList = new List<double>();
                var ZList = new List<double>();

                foreach (Point point in border)
                {
                    XList.Add(point.lng);
                    YList.Add(point.lat);
                    ZList.Add(point.alt);
                }

                double minX = XList.Min();
                double maxX = XList.Max();
                double minY = YList.Min();
                double maxY = YList.Max();
                double maxZ = ZList.Max();

                Configuration conf = Configuration.getInstance();
                double middleX = (minX + maxX) / 2;
                double middleY = (minY + maxY) / 2;
                var sCoord = new GeoCoordinate(minX, minY);
                var eCoord = new GeoCoordinate(maxX, maxY);

                double radius = eCoord.GetDistanceTo(sCoord) / 2;
                double degree = double.Parse(conf.get("cameraOpeningDegree"));
                double rad = ConvertToRadians(degree);

                double hight = maxZ + (radius / Math.Tan(rad));

                basePossition = new Point(middleX, middleY, hight);

            }
            return basePossition;
        }

        public Point getMidleEdgePoint()
        {
            var XList = new List<double>();
            var YList = new List<double>();

            foreach (Point point in border)
            {
                XList.Add(point.lng);
                YList.Add(point.lat);
            }

            double minX = XList.Min();
            double maxX = XList.Max();
            double minY = YList.Min();

            return new Point((minX + maxX) / 2, minY, getBasePoint().alt);

        }

        public Point getBasePointInMeters()
        {  
            int num_of_width_pixels = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels"));
            int num_of_height_pixels = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels"));
            return new Point(PixelConverterHelper.convert_width(num_of_width_pixels / 2), PixelConverterHelper.convert_height(num_of_height_pixels / 2));
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
