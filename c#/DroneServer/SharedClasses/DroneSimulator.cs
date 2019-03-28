using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DroneServerIntegration
{
    public class DroneSimulator
    {
        private Process myDroneProcess;
        private bool is_drone_running;

        public DroneSimulator()
        {
            is_drone_running = false;
        }

        public void start_drone(string override_params="")
        {
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("python");

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.CreateNoWindow = true;

            String myPythonApp = "Drone.py "+ override_params;

            myProcessStartInfo.Arguments = myPythonApp;

            myDroneProcess = new Process();
            myDroneProcess.StartInfo = myProcessStartInfo;

            // start the process 
            myDroneProcess.Start();
            Thread.Sleep(2000);

            is_drone_running = true;
        }

        public void close_drone()
        {
            myDroneProcess.Close();
            is_drone_running = false;
        }

        public bool is_drone_runing()
        {
            return is_drone_running;
        }

        ~DroneSimulator()
        {
            if (is_drone_runing())
            {
                close_drone();
            }
        }
    }
}
