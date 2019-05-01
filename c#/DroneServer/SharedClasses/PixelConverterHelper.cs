using DroneServer.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    public class PixelConverterHelper
    {
        private static PixelConverterHelper instance = null;
        double size_of_pixel_h = 0;
        double size_of_pixel_w = 0;
        
        private double to_rad(double degree)
        {
            return degree * Math.PI / 180;
        }

        private PixelConverterHelper(double above_ground)
        {
            int num_of_width_pixels = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels"));
            int num_of_height_pixels = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels"));

            double width_ratio = Double.Parse(Configuration.getInstance().get("width_pixels_ratio"));
            double height_ratio = Double.Parse(Configuration.getInstance().get("height_pixels_ratio"));

            size_of_pixel_h = height_ratio * above_ground / num_of_height_pixels;
            size_of_pixel_w = width_ratio * above_ground / num_of_width_pixels;
        }
        public static PixelConverterHelper getInstance()
        {
            Assertions.verify(instance != null, "PixelConverterHelper intance is null");
            return instance;
        }
        public static void init(double aboveGround)
        {
            //Deleted for tests
            //Assertions.verify(instance == null, "PixelConverterHelper intance is not null");
            instance = new PixelConverterHelper(aboveGround);
        }

        public static double convert_width(int pixel)
        {
            int num_of_width_pixels = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels"));
            Assertions.verify(pixel < num_of_width_pixels, "PixelConverter cannot pixel from out of pic");
            Assertions.verify(pixel > 0, "PixelConverter cannot pixel less than zero");

            return pixel * instance.size_of_pixel_w;
        }

        public static int re_convert_width(double margin)
        {
            int num_of_width_pixels = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels"));

            return (int)(margin / instance.size_of_pixel_w);
        }

        public static double convert_height(int pixel)
        {
            int num_of_height_pixels = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels"));
            Assertions.verify(pixel < num_of_height_pixels, "PixelConverter cannot pixel from out of pic");
            Assertions.verify(pixel > 0, "PixelConverter cannot pixel less than zero");

            return pixel * instance.size_of_pixel_h;
        }

        public static int re_convert_height(double margin)
        {
            int num_of_height_pixels = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels"));

            return (int)(margin / instance.size_of_pixel_h);
        }
    }
}
