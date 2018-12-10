using DroneServer.BL.Missions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AndroidAccepanceTests
{
    class Loader
    {
        private static System.Timers.Timer m_get_status_timer;
        private static System.Timers.Timer m_get_location_timer;

        public Loader(double interval) : this(interval*2, interval*2) { }

        public Loader(double status_interval, double location_interval)
        {
            m_get_status_timer = new System.Timers.Timer(status_interval);
            // Hook up the Elapsed event for the timer. 
            m_get_status_timer.Elapsed += on_get_status_Event;
            m_get_status_timer.AutoReset = true;
            m_get_status_timer.Enabled = true;

            m_get_location_timer = new System.Timers.Timer(location_interval);
            // Hook up the Elapsed event for the timer. 
            m_get_status_timer.Elapsed += on_get_location_Event;
            m_get_status_timer.AutoReset = true;
            m_get_status_timer.Enabled = true;
        }


        private static void on_get_status_Event(Object source, ElapsedEventArgs e)
        {
            LeafMission mission = new getDroneStatus();
            Comm.getInstance().sendMission(mission);
        }

        private static void on_get_location_Event(Object source, ElapsedEventArgs e)
        {
            LeafMission mission = new GetLocation();
            Comm.getInstance().sendMission(mission);
        }


    }
}
