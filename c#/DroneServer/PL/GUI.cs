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

        BLManagger bl = BLManagger.getInstance();

        private void GUI_Load(object sender, EventArgs e)
        {
            Logger.getInstance().debug("Gui Load has started");
            bl.registerToLogs(logger_home_lst);
            bl.registerToLogs(logger_mission_lst);

            initMaps();




        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        //home section
        private void start_home_btn_Click(object sender, EventArgs e)
        {
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //create section
        private void map_create_map_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mea = (MouseEventArgs)e;
            double lat = map_create_map.FromLocalToLatLng(mea.X, mea.Y).Lat;
            double lng = map_create_map.FromLocalToLatLng(mea.X, mea.Y).Lng;
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

        
    }
}
