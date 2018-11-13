using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DroneServer.SharedClasses;
namespace DroneServer.PL
{
    class ParkingList : BaseObservable
    {
        List<Parking> list;

        public ParkingList()
        {
            list = new List<Parking>();
        }

        public override object getData()
        {
            List<string> tmp = new List<string>();
            foreach (Parking p in list)
            {
                tmp.Add(p.name);
            }
            return tmp;
        }

        public void add(Parking p)
        {
            list.Add(p);
            notifyAll();
        }
        public void delete(string name)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (name==list[i].name)
                {
                    list.RemoveAt(i);
                    break;
                }
            }

            notifyAll();
        }
        public Parking getParking(int index)
        {
            if (list.Count > index)
                return list[index];
            return null;
        }
    }
}
