﻿using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.CV
{
    public class CarPlateDetector
    {
        public static List<String> getCarPlates(String image_path)
        {
            String[] res = run_car_plate_detector_module(image_path);
            
            if (res[0] == "No license plates found.\r")
            {
                return new List<string>();
            }
            return parse_result(res);
        }

        private static List<String> parse_result(String[] output_of_car_plate_module)
        {
            List<String> res = new List<String>();
            String car_plate = output_of_car_plate_module[1];
            res.Add(car_plate.Substring(6, car_plate.IndexOf('\t') - 6));

            return res;
        }


        private static String[] run_car_plate_detector_module(String image_path)
        {

            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("python");

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.CreateNoWindow = true;

            String myPythonApp = "number_plate_detector.py " + image_path;
            Logger.getInstance().info("CV CarPlate params: " + myPythonApp);
            myProcessStartInfo.Arguments = myPythonApp;

            Process myProcess = new Process();
            myProcess.StartInfo = myProcessStartInfo;

            // start the process 
            myProcess.Start();
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadToEnd();
            myProcess.WaitForExit();
            Logger.getInstance().info("CV car_plate_detector result: " + myString);

            return myString.Split('\n');
        }
    }
}
