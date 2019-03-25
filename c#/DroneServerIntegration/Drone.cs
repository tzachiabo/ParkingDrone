using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DroneServerIntegration
{
    class Drone
    {
        private Process myDroneProcess;
        private bool is_drone_running;

        public Drone()
        {
            is_drone_running = false;
        }

        public void start_drone()
        {
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("python");

            // make sure we can read the output from stdout 
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.CreateNoWindow = true;

            String myPythonApp = "Drone.py";

            myProcessStartInfo.Arguments = myPythonApp;

            myDroneProcess = new Process();
            myDroneProcess.StartInfo = myProcessStartInfo;

            // start the process 
            myDroneProcess.Start();

            is_drone_running = true;
        }

        public void close_drone()
        {
            myDroneProcess.Close();
            is_drone_running = false;
        }
    }
}
