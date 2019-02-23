using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.CV
{
    public class VerifyLocation
    {
        public static SharedClasses.Point getLocation(String base_photo_path, String current_photo_path, Double ratio)
        {
            String module_result = run_verify_location_module(base_photo_path, current_photo_path, ratio);

            return parseStringToPoint(module_result);
        }

        private static String run_verify_location_module(String base_photo_path, String current_photo_path, Double ratio)
        {

            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("python");

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.CreateNoWindow = true;

            String myPythonApp = "verify_location.py " + base_photo_path + " " + current_photo_path + " " + ratio;
            myProcessStartInfo.Arguments = myPythonApp;

            Process myProcess = new Process();
            myProcess.StartInfo = myProcessStartInfo;

            // start the process 
            myProcess.Start();
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadToEnd();
            myProcess.WaitForExit();

            return myString;
        }

        private static Point parseStringToPoint(String point_str)
        {
            int start = point_str.IndexOf('(') + 1;
            int end  = point_str.IndexOf(')') - 1;
            String sub_point_str = point_str.Substring(start, end);

            String[] split_point_str = sub_point_str.Split(',');

            return new Point(Int32.Parse(split_point_str[0]), Int32.Parse(split_point_str[1]));
        }
    }
}
