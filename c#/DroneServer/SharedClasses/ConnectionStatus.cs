using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    class ConnectionStatus : BaseObservable
    {
        DroneStatus ds;
        public ConnectionStatus():base()
        {
            ds = DroneStatus.Disconnected;
        }

        public override object getData()
        {
            return "" + ds.ToString();
        }

        public void setStatus(DroneStatus ds)
        {
            this.ds = ds;
            notifyAll();
        }
    }
}
