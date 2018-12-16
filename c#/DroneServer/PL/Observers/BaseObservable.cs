using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace DroneServer
{
    public class BaseObservable
    {
        List<BaseObserver> observers;

        public BaseObservable()
        {
            observers = new List<BaseObserver>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void register(BaseObserver obs)
        {
            observers.Add(obs);
            obs.setObserable(this);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void notifyAll()
        {
            Assertions.verify(observers != null, "tried to notify a null list of objects");
            foreach (BaseObserver obs in observers)
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
