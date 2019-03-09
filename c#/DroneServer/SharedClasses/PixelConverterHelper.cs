using DroneServer.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    class PixelConverterHelper
    {
        private static PixelConverterHelper instance = null;
        double size_of_pixel_h = 0;
        double size_of_pixel_w = 0;


        private PixelConverterHelper(double above_ground)
        {
            int num_of_width_pixels = Int32.Parse(Configuration.getInstance().get("num_of_width_pixels"));
            int num_of_height_pixels = Int32.Parse(Configuration.getInstance().get("num_of_height_pixels"));
            int cameraOpeningDegree = Int32.Parse(Configuration.getInstance().get("cameraOpeningDegree"));

            double length = 2 * above_ground * Math.Tan(cameraOpeningDegree);

            size_of_pixel_h = num_of_height_pixels / length;
            size_of_pixel_w = num_of_width_pixels / length;
        }
        public static PixelConverterHelper getInstance()
        {
            Assertions.verify(instance != null, "PixelConverterHelper intance is null");
            return instance;
        }
        public static void init(double aboveGround)
        {
            Assertions.verify(instance == null, "PixelConverterHelper intance is not null");
            instance = new PixelConverterHelper(aboveGround);
        }

        public static double convert_width(int pixel)
        {
            return pixel * instance.size_of_pixel_w;
        }

        public static double convert_height(int pixel)
        {
            return pixel * instance.size_of_pixel_h;
        }
    }
}
