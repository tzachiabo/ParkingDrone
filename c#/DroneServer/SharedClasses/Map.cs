using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.SharedClasses
{
    class Map : BaseObservable
    {
        private Point location;

        public Map()
        {
            location = null;
        }

        public override object getData()
        {
            return location;
        }

        public void setLocation(Point location)
        {
            this.location = location;
            Console.WriteLine(location.lng+" "+ location.lat);
            notifyAll();
        }
        
    }
}
