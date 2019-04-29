using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.DataViz.WinForms;
using DroneServer.BL;
using GMap.NET.MapProviders;
using GMap.NET;

namespace SOCIAL_APP_BUNIFU_FRAMEWORK_DEMO
{
    public partial class tabDashboard : UserControl
    {
        Form1 main;

        public tabDashboard(Form1 main)
        {
            if(Program.IsInDesignMode())
            {
                return;
            }
            this.main = main;

            InitializeComponent();
        }

        private void chartDelay_Tick(object sender, EventArgs e)
        {
            //delay for our charts to render.
            chartDelay.Stop();
            renderCharts();
        }

        private void renderCharts()
        {
            //lets get a canvas to paint graphs on
            Canvas Charts = new Canvas();

            //graph 1 dataPoint
            DataPoint graph1 = new DataPoint(BunifuDataViz._type.Bunifu_spline);

            //graph 2 datapoint
            DataPoint graph2 = new DataPoint(BunifuDataViz._type.Bunifu_column);

            //sample data for datapoint 1
            graph1.addLabely("SUN", 1500);
            graph1.addLabely("MON", 1700);
            graph1.addLabely("TUE", 2300);
            graph1.addLabely("WED", 1700);
            graph1.addLabely("THU", 1900);
            graph1.addLabely("FRI", 1600);
            graph1.addLabely("SAT", 1800);

            //sample data for datapoint 2
            graph2.addLabely("SUN", 1000);
            graph2.addLabely("MON", 1200);
            graph2.addLabely("TUE", 2000);
            graph2.addLabely("WED", 1400);
            graph2.addLabely("THU", 1500);
            graph2.addLabely("FRI", 1100);
            graph2.addLabely("SAT", 1200);

            //add datapoints to one canvas
            Charts.addData(graph1);
            Charts.addData(graph2);

            //render canvas through bunifu dataviz component.

        }

        private void progressBarUpdate_Tick(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabDashboard_Load(object sender, EventArgs e)
        {
            BLManagger.getInstance().registerToMap(map_mission_map);
            initMaps();
        }

        public void initMaps()
        {
            double lat = 31.2646168738942;
            double lng = 34.8061412572861;
            try
            {
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
