using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace DroneServer
{
    class TextObserver : BaseObserver
    {
        protected Control listbox;
        public TextObserver(Control c) : base()
        {
            listbox = c;    
        }

        public override void update()
        {
            string s;
            try
            {
                s = (string)observable.getData();
            }
            catch (Exception) { return; }

            listbox.Text = s;
        }
    }
}
