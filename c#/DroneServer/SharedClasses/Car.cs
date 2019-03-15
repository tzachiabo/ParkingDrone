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


    }
}
