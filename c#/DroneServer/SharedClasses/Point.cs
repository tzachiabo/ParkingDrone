using DroneServer.BL;
using System;
using System.Collections.Generic;

namespace DroneServer.SharedClasses
{
    public class Point
    {
        public double lng;//x
        public double lat;//y
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

        public List<KeyValuePair<Direction, double>> get_moves(Point des_point)
        {
            List<KeyValuePair<Direction, double>> res = new List<KeyValuePair<Direction, double>>();

            double curr_x = lng;
            double des_x = des_point.lng;
            double distance_x = Math.Abs(des_x - curr_x);

            if (distance_x > 1)
            {
                if (curr_x < des_x)
                {
                    res.Add(new KeyValuePair<Direction, double>(Direction.right, distance_x));
                }
                else
                {
                    res.Add(new KeyValuePair<Direction, double>(Direction.left, distance_x));
                }
            }

            double curr_y = lat;
            double des_y = des_point.lat;
            double distance_y = Math.Abs(des_y - curr_y);

            if (distance_y > 1)
            {
                if (curr_y < des_y)
                {
                    res.Add(new KeyValuePair<Direction, double>(Direction.backward, distance_y));
                }
                else
                {
                    res.Add(new KeyValuePair<Direction, double>(Direction.forward, distance_y));
                }
            }

            return res;
        }


        public bool is_close(Point other_point, Double acceptable_delta_for_error_in_distance = -1)
        {
            if (acceptable_delta_for_error_in_distance == -1)
            {
                acceptable_delta_for_error_in_distance = Double.Parse(Configuration.getInstance().get("acceptable_delta_for_error_in_distance"));
            }

            double distance = Math.Sqrt(Math.Pow(Math.Abs(lng - other_point.lng), 2) + Math.Pow(Math.Abs(lat - other_point.lat), 2));

            return distance < acceptable_delta_for_error_in_distance;
        }
    }
}
