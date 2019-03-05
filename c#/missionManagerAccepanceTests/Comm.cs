using DroneServer.BL;
using DroneServer.BL.Comm;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace missionManagerAccepanceTests
{
    class Comm
    {
        private static Comm instance;

        private Comm()
        {
            BLManagger.getInstance().initComm();
            while (!CommManager.getInstance().isSocketInitiated);

            init();
        }

        public static Comm getInstance()
        {
            if (instance == null)
            {
                instance = new Comm();
            }
            return instance;
        }

        private void init()
        {
            getDroneStatus get_status = new getDroneStatus();
            CompletionHandler ch = get_status.execute();

            ch.wait();

            while ((DroneStatus)ch.response.Data != DroneStatus.Connected)
            {
                get_status = new getDroneStatus();
                ch = get_status.execute();
                ch.wait();

            }
        }

    }
}
