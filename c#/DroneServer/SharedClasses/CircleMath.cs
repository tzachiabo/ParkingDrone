using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    public class CircleMath
    {
        public static bool is_close_angle(double dest_bearing, double curr_bearing, double delta)
        {
            double ans =angular_distance(dest_bearing, curr_bearing);
            if (ans <= delta && ans >= 0)
            {
                return true;
            }
            return false;
        }
        
        public static double angular_distance(double dest_bearing, double curr_bearing)
        {
            dest_bearing = to_rads(dest_bearing);
            curr_bearing = to_rads(curr_bearing);
            return Math.Abs(to_degrees(Math.Atan2(Math.Sin(dest_bearing - curr_bearing), Math.Cos(dest_bearing - curr_bearing))));
        }

        public static Direction leftOrRight(double dest_bearing, double curr_bearing)
        {
            dest_bearing = to_rads(dest_bearing);
            curr_bearing = to_rads(curr_bearing);
            if(to_degrees(Math.Atan2(Math.Sin(dest_bearing - curr_bearing), Math.Cos(dest_bearing - curr_bearing))) > 0)
            {
                return Direction.rtt_right;
            }
            return Direction.rtt_left;
        }

        private static double to_rads(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        private static double to_degrees(double rads)
        {
            return rads * 180 / Math.PI;
        }

    }
}
