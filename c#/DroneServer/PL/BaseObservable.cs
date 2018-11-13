using System;
using System.Collections.Generic;
using System.Linq;
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
        public void register(BaseObserver obs)
        {
            l.Add(obs);
            obs.setObserable(this);
        }
        public void notifyAll()
        {
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
