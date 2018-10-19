using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace DroneServer
{
    abstract class BaseObserver
    {
        protected BaseObservable observable;

        public BaseObserver()
        {
            observable = null;
        }
        public void setObserable(BaseObservable o)
        {
            observable = o;
        }

        public abstract void update();
    }
}
