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

using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;

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
            confige();
            bl = BLManagger.getInstance();
            bl.registerToLogs(logger_home_lst);
            bl.registerToLogs(logger_mission_lst);
            bl.registerToParkings(parkings_home_lst);
            bl.registerToMap(map_mission_map);
            bl.restoreData();
            initMaps();
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            bl.shutdown();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        //home section
        private void start_home_btn_Click(object sender, EventArgs e)
        {
            if (parkings_home_lst.SelectedIndex == -1)
            {
                MessageBox.Show("Please select parking");
                return;
            }

            tabControl.SelectedIndex = 2;//change tab, has to be replace with panels
            bl.startMission(map_mission_map,parkings_home_lst.SelectedIndex);

        }

        private void delete_home_btn_Click(object sender, EventArgs e)
        {
            if (parkings_home_lst.SelectedIndex == -1)
            {
                MessageBox.Show("Please select parking");
                return;
            }
               
           
            bl.deleteParking(parkings_home_lst.Items[parkings_home_lst.SelectedIndex].ToString());
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //create section
        //private void map_create_map_DoubleClick(object sender, EventArgs e)
        //{

        //}
        private void map_create_map_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double lat = map_create_map.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = map_create_map.FromLocalToLatLng(e.X, e.Y).Lng;
            GMapOverlay markerOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.green_pushpin);
            markerOverlay.Markers.Add(marker);
            map_create_map.Overlays.Add(markerOverlay);
            map_create_map.Position = new PointLatLng(map_create_map.Position.Lat, map_create_map.Position.Lng);

            points_create_lst.Items.Add(lat + " " + lng);
        }

        private void finish_create_btn_Click(object sender, EventArgs e)
        {
            if (parkName_create_txt.Text == "")
            {
                MessageBox.Show("Please enter parking name");
                return;
            }
            if (points_create_lst.Items.Count<3)
            {
                MessageBox.Show("Please mark at least 3 points");
                return;
            }


            bl.createParking(parkName_create_txt, map_create_map, points_create_lst);

            //clear page and move to Home page
            parkName_create_txt.Text = "Parking name";
            points_create_lst.Items.Clear();
            map_create_map.Overlays.Clear();
            initMaps();
            tabControl.SelectedIndex = 0;


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //create section
        private void end_mission_btn_Click(object sender, EventArgs e)
        {
            double lat = 31.2656169738942;
            double lng = 34.8071413572861;
            bl.setLocation(lat, lng);
        }

        private void stop_mission_btn_Click(object sender, EventArgs e)
        {

        }

        private void abort_mission_btn_Click(object sender, EventArgs e)
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
            int roll = Convert.ToInt32(Roll.Value);
            int pitch = Convert.ToInt32(Pitch.Value);
            int yaw = Convert.ToInt32(Yaw.Value);
            BLManagger.getInstance().MoveGimbalTest(Gimbal.left, roll, pitch, yaw);
        }

        private void goToGPS_dummy_btn_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().GoToGpsTest();
        }

        private void takePhoto_dummy_btn_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().TakePhoto();
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

        private void stop_dummy_btn_Click(object sender, EventArgs e)
        {
            BLManagger.getInstance().stop();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //functions section
        public void initMaps()
        {
            double lat = 31.2646168738942;
            double lng = 34.8061412572861;
            try
            {
                map_create_map.MapProvider = GMapProviders.GoogleSatelliteMap;//GMapProviders.GoogleMap
                map_create_map.Position = new PointLatLng(lat, lng);
                map_create_map.MinZoom = 5;
                map_create_map.MaxZoom = 10000;
                map_create_map.Zoom = 18;
                map_create_map.DragButton = MouseButtons.Left;


                map_mission_map.MapProvider = GMapProviders.GoogleSatelliteMap;//GMapProviders.GoogleMap
                map_mission_map.Position = new PointLatLng(lat, lng);
                map_mission_map.MinZoom = 5;
                map_mission_map.MaxZoom = 10000;
                map_mission_map.Zoom = 18;
                map_mission_map.DragButton = MouseButtons.Left;
                map_mission_map.CanDragMap = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Cant show map, no internet connection");
            }
            
        }

        public void confige()
        {
            if (Configuration.getInstance().get("debugMode")=="false")
            {
                tabControl.TabPages.Remove(dummyTab);
            }

        }

        
    }
}
