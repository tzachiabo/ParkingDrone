using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Threading;

namespace DroneServer.SharedClasses
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
            KillProcessAndChildrens(myDroneProcess.Id);
            is_drone_running = false;
        }

        private static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }

            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }
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
