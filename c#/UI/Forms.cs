using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOCIAL_APP_BUNIFU_FRAMEWORK_DEMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool DrawerOpen = true;

        private void btnToggleDrawer_Click(object sender, EventArgs e)
        {
            DrawerOpen = !DrawerOpen;
            pnlDrawer.Visible = false;

            if(DrawerOpen)
            {
                //animated Drawer Open
                pnlDrawer.Width = 233;
                bunifuTransition1.ShowSync(pnlDrawer);
            }
            else
            {
                //Aminated Drawer close
                pnlDrawer.Width = 56;
                bunifuTransition1.ShowSync(pnlDrawer);
            }


          
 
        }

        private void bunifuFlatButton1_MouseUp(object sender, EventArgs e)
        {
            tabProfile1.BringToFront();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //tabDashboard1.BringToFront();
            tabMessages1.BringToFront();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
           
            tabDashboard1.BringToFront();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            tabSupport1.BringToFront();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabProfile1_Load(object sender, EventArgs e)
        {

        }
    }
}
