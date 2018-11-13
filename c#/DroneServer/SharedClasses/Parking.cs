using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using GMap.NET.WindowsForms;


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
        public List<Point> border;

        public Parking()
        {
            this.name = null;
            this.lat = 0;
            this.lng = 0;
            this.zoom = 0;
            this.maxZoom = 0;
            this.minZoom = 0;
            this.border = null;
        }
        public Parking(string name, double lat, double lng, double zoom, double maxZoom, double minZoom, List<Point> border)
        {
            this.name = name;
            this.lat = lat;
            this.lng = lng;
            this.zoom = zoom;
            this.maxZoom = maxZoom;
            this.minZoom = minZoom;
            this.border = border;
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
                border.Add(new SharedClasses.Point(Convert.ToDouble(item.Split(' ')[1]), Convert.ToDouble(item.Split(' ')[0])));
        }
    }
}
