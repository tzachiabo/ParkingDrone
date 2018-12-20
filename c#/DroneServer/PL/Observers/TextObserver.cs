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
                    if (s == "Connected")
                        control.ForeColor = System.Drawing.Color.LimeGreen;
                    else if (s.ToLower() == "not ready")
                        control.ForeColor = System.Drawing.Color.Yellow;
                    else if (s == "Disconnected")
                        control.ForeColor = System.Drawing.Color.Red;
                    control.Text = s;
                }));
            }
            catch (Exception) { return; }

        }
    }
}
