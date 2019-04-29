using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneServer.SharedClasses;
using DroneServer.BL;

namespace SOCIAL_APP_BUNIFU_FRAMEWORK_DEMO
{
    public partial class tabMessages : UserControl
    {
        List<btnMailItem> all_parkings;
        Parking active_parking;
        Dictionary<String, Parking> parking_map;
        Form1 main;

        public tabMessages(Form1 main)
        {
            if(Program.IsInDesignMode())
            {
                return;
            }
            this.main = main;
            InitializeComponent();
            
        }

        private void btnEditPhoto_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().startMission(active_parking);
            main.tabDashboard1.BringToFront();
            //tabDashboard1.BringToFront();

        }

        private void btnMailItem1_Click(object sender, EventArgs e)
        {
            resetIndicator();

            btnMailItem item = ((btnMailItem)sender);
            item.Active = true;
            parking_map.TryGetValue(item.FirstName, out active_parking);
        }

        void resetIndicator()
        {  

            foreach (Control item in panel1.Controls)
            {
                btnMailItem curitem = ((btnMailItem)item);
                curitem.Active = false;
            }

        }

        private void tabMessages_Load(object sender, EventArgs e)
        {
            List<Parking> parkings = BLManagger.getInstance().DBGetAllParkings();
            this.panel1.Controls.Clear();
            all_parkings = new List<btnMailItem>();
            parking_map = new Dictionary<string, Parking>();

            foreach (Parking parking in parkings)
            {
                btnMailItem btnMailItem = new SOCIAL_APP_BUNIFU_FRAMEWORK_DEMO.btnMailItem();
                all_parkings.Add(btnMailItem);
                parking_map.Add(parking.name, parking); 

                this.panel1.Controls.Add(btnMailItem);
                btnMailItem.Active = false;
                if (panel1.Controls.Count == 0)
                {
                    this.bunifuTransition1.SetDecoration(btnMailItem, BunifuAnimatorNS.DecorationType.BottomMirror);
                }

                if (panel1.Controls.Count % 2 == 0)
                {
                    btnMailItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(36)))));
                }
                else
                {
                    btnMailItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(49)))));
                }
                
                btnMailItem.Body = parking.name;
                btnMailItem.Cursor = System.Windows.Forms.Cursors.Hand;
                btnMailItem.Dock = System.Windows.Forms.DockStyle.Top;
                btnMailItem.Email = "";
                btnMailItem.FirstName = parking.name;
                btnMailItem.Location = new System.Drawing.Point(0, 0);
                btnMailItem.Margin = new System.Windows.Forms.Padding(5);
                btnMailItem.Name = "aviv";
                btnMailItem.Size = new System.Drawing.Size(280, 85);
                btnMailItem.Subject = "What is Lorem Ipsum?";
                btnMailItem.TabIndex = 0;
                btnMailItem.Load += new System.EventHandler(this.btnMailItem1_Load);
                btnMailItem.Click += new System.EventHandler(this.btnMailItem1_Click);
            }
            all_parkings[all_parkings.Count-1].Active = true;
            parking_map.TryGetValue(all_parkings[all_parkings.Count - 1].FirstName, out active_parking);
        }

        private void btnMailItem1_Load(object sender, EventArgs e)
        {

        }
    }
}
