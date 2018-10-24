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
            int move_amount = Convert.ToInt32(MoveAmount.Value);

            Logger.getInstance().debug("gui action to move forward distance : " + move_amount);

            BLManagger.getInstance().MoveForTest(move_amount, Direction.forward);
            
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

        private void Landing_dummy_btn_Click_1(object sender, EventArgs e)
        {
            BLManagger.getInstance().LandingForTest();
        }

        private void moveBackward_dummy_btn_Click(object sender, EventArgs e)
        {
            int move_amount = Convert.ToInt32(MoveAmount.Value);

            Logger.getInstance().debug("gui action to move backward distance : " + move_amount);

            BLManagger.getInstance().MoveForTest(move_amount, Direction.backward);
        }

        private void moveLeft_dummy_btn_Click(object sender, EventArgs e)
        {
            int move_amount = Convert.ToInt32(MoveAmount.Value);

            Logger.getInstance().debug("gui action to move left distance : " + move_amount);

            BLManagger.getInstance().MoveForTest(move_amount, Direction.left);
        }

        private void moveRight_dummy_btn_Click(object sender, EventArgs e)
        {
            int move_amount = Convert.ToInt32(MoveAmount.Value);

            Logger.getInstance().debug("gui action to move right distance : " + move_amount);

            BLManagger.getInstance().MoveForTest(move_amount, Direction.right);
        }

        private void moveDown_dummy_btn_Click(object sender, EventArgs e)
        {
            int move_amount = Convert.ToInt32(MoveAmount.Value);

            Logger.getInstance().debug("gui action to move down distance : " + move_amount);

            BLManagger.getInstance().MoveForTest(move_amount, Direction.down);
        }

        private void moveUp_dummy_btn_Click(object sender, EventArgs e)
        {
            int move_amount = Convert.ToInt32(MoveAmount.Value);

            Logger.getInstance().debug("gui action to move up distance : " + move_amount);

            BLManagger.getInstance().MoveForTest(move_amount, Direction.up);
        }
    }
}
