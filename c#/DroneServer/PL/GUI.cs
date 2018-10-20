using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using log4net;
using DroneServer.BL;

namespace DroneServer
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void GUI_Load(object sender, EventArgs e)
        {
            Log.Debug("sasdasdasddf");
            
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        //home section
        private void start_home_btn_Click(object sender, EventArgs e)
        {
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //dummy section
        private void move_dummy_btn_Click(object sender, EventArgs e)
        {
         
        }

        private void takeOff_dummy_btn_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().TakeOffForTest();
        }

        private void Landing_dummy_btn_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().LandingForTest();
        }

        private void goHome_dummy_btn_Click(object sender, EventArgs e)
        {

        }

        private void moveGimbal_dummy_btn_Click(object sender, EventArgs e)
        {

        }

        private void goToGPS_dummy_btn_Click(object sender, EventArgs e)
        {

        }

        private void takePhoto_dummy_btn_Click(object sender, EventArgs e)
        {

        }

    }
}
