namespace DroneServer.SharedClasses
{
    public class Point
    {
        public double lng;//lng
        public double lat;//lat
        public double alt;

        public Point()
        {
            this.lng = 0;
            this.lat = 0;
            this.alt = 0;
        }
        public Point(double lng, double lat)
        {
            this.lng = lng;
            this.lat = lat;
            this.alt = 0;
        }
        public Point(double lng, double lat, double alt)
        {
            this.lng = lng;
            this.lat = lat;
            this.alt = alt;
        }
    }
}
