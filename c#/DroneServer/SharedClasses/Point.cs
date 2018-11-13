using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    class Point
    {
        public double x;//lng
        public double y;//lat
        public double z;

        public Point()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }
        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
