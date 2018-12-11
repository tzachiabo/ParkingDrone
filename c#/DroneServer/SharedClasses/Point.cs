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
        public Point(double x, double y)
        {
            this.lng = x;
            this.lat = y;
            this.alt = 0;
        }
        public Point(double x, double y, double z)
        {
            this.lng = x;
            this.lat = y;
            this.alt = z;
        }
    }
}
