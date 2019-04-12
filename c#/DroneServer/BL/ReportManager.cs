using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL
{
    class ReportManager
    {
        private static ReportManager instance = null;

        private ReportManager()
        {
        }

        public static ReportManager getInstance()
        {
            if (instance == null)
            {
                instance = new ReportManager();
            }

            return instance;
        }

        public void addCarPlate(string car_plate, string path_to_car_image)
        {

        }

        public void make_report(string path)
        {

        }
    }
}
