using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer
{
    class SpecificObservable : BaseObservable
    {
        public override object getData()
        {
            return "Asd";
        }
    }
}
