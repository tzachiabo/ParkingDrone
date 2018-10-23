﻿using System;
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

using DroneServer.BL;
using DroneServer.SharedClasses;

namespace DroneServer
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        BLManagger bl;

        private void GUI_Load(object sender, EventArgs e)
        {
            Logger.getInstance().debug("Gui Load has started");
            bl = BLManagger.getInstance();
            bl.registerToLogs(logger_home_lst);
            bl.registerToLogs(logger_mission_lst);

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
            BLManagger.getInstance().StartLandingForTest();
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

        private void ConfirmLanding_dummy_btn_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().ConfirmLandingForTest();
        }

        private void parking_mission_dummy_btn_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().ParkingForTest();
        }
    }
}
