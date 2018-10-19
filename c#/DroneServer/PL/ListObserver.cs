using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace DroneServer
{
    class ListObserver : BaseObserver
    {
        protected ListBox listbox;
        public ListObserver(ListBox l) : base()
        {
            listbox = l;
        }

        public override void update()
        {
            List<string> l;
            try
            {
                l = (List<string>)observable.getData();
            }
            catch (Exception){return;}

            listbox.Items.Clear();
            foreach (string s in l)
                listbox.Items.Add(s);
        }
    }
}
