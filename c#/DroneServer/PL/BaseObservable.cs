using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace DroneServer
{
    class BaseObservable
    {
        List<BaseObserver> l;

        public BaseObservable()
        {
            l = new List<BaseObserver>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void register(BaseObserver obs)
        {
            l.Add(obs);
            obs.setObserable(this);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void notifyAll()
        {
            Assertions.verify(l != null, "tried to notify a null list of objects");
            foreach (BaseObserver obs in l)
            {
                obs.update();
            }
        }

        public virtual object getData()
        {
            return null;
        }

    }
}
