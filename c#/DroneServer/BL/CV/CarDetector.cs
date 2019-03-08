using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL
{
    public class CarDetector
    {
        public static List<Car> getCarsFromBasePhoto(String base_photo_path, double base_photo_height)
        {
            String[] module_result = run_car_detector_module(base_photo_path);

            return generate_cars_objects(module_result, base_photo_height);
        }

        private static String[] run_car_detector_module(String base_photo_path)
        {

            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("python");

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.CreateNoWindow = true;

            String myPythonApp = "car_detector.py "+ base_photo_path;
            myProcessStartInfo.Arguments = myPythonApp;

            Process myProcess = new Process();
            myProcess.StartInfo = myProcessStartInfo;

            // start the process 
            myProcess.Start();
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadToEnd();
            myProcess.WaitForExit();

            return myString.Split('\n');
        }


        private static List<Car> generate_cars_objects(String[] module_result, double base_photo_height)
        {
            List<Car> res = new List<Car>();

            foreach (String line in module_result)
            {
                
                String[] colums = line.Split('\t');
                if (colums.Length == 6)
                {
                    String type = colums[0];
                    int precent = Int32.Parse(colums[1].Substring(0, colums[1].Length - 1));
                    int margin_left = Int32.Parse(colums[2]);
                    int margin_top = Int32.Parse(colums[3]);
                    int width = Int32.Parse(colums[4]);
                    int height = Int32.Parse(colums[5]);
                    Car car = new Car(type, precent, margin_left, margin_top, width, height, base_photo_height);
                    res.Add(car);
                }
            }

            return res;
        }

    }
}
