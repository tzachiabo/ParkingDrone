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
        private int left_margin;
        private int top_margin;
        private int width;
        private int height;

        public Car(String type, int precent, int left_margin, int top_margin, int width, int height, double base_photo_height)
        {
            this.type = type;
            this.precent = precent;
            this.left_margin = left_margin;
            this.top_margin = top_margin;
            this.width = width;
            this.height = height;
            // TODO aviad zabow decode to meter
        }


    }
}
