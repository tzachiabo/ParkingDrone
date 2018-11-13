using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DroneServer.BL.Comm
{
    class Publisher
    {
        System.Timers.Timer publisher;
        bool is_running;

        internal Publisher()
        {
            is_running = false;
        }

        internal void stopPublish()
        {
            Assertions.verify(is_running, "trying to stop publisher when it is not runing");
            publisher.Stop();
            is_running = false;
        }

        internal void startPublish()
        {
            Assertions.verify(!is_running, "trying to restart publisher when it is allready runing");
            startTimer();
        }

        private void startTimer()
        {
            publisher = new System.Timers.Timer();
            publisher.Elapsed += new ElapsedEventHandler(publishIPViaUdp);
            publisher.Interval = 5 * 1000; // interval of one hour
            publisher.Enabled = true;
            is_running = true;
        }

        private void publishIPViaUdp(object source, ElapsedEventArgs e)
        {
            Logger.getInstance().debug("publish ip via publisher");
            UdpClient client = new UdpClient();
            int port = Int32.Parse(Configuration.getInstance().get("brodcust_port"));
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, port);

            byte[] bytes = Encoding.ASCII.GetBytes("DroneServer");
            
            client.Send(bytes, bytes.Length, ip);
            client.Close();
        }



    }
}
