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
            int cameraOpeningDegree = Int32.Parse(Configuration.getInstance().get("cameraOpeningDegree"));

            double length = 2 * above_ground * Math.Tan(to_rad(cameraOpeningDegree));

            size_of_pixel_h = length / num_of_height_pixels;
            size_of_pixel_w = length / num_of_width_pixels;
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

        public static double convert_height(int pixel)
        {
            int num_of_height_pixels = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels"));
            Assertions.verify(pixel < num_of_height_pixels, "PixelConverter cannot pixel from out of pic");
            Assertions.verify(pixel > 0, "PixelConverter cannot pixel less than zero");

            return pixel * instance.size_of_pixel_h;
        }
    }
}
