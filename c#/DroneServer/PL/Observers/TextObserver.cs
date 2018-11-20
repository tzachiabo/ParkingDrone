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
        protected Control control;
        public TextObserver(Control c) : base()
        {
            control = c;    
        }

        public override void update()
        {
            string s;
            try
            {
                s = (string)observable.getData();
                control.BeginInvoke((Action)(() =>
                {
                    control.Text = s;
                }));
            }
            catch (Exception) { return; }

        }
    }
}
