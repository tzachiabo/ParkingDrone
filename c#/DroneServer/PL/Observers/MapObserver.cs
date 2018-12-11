using System;
using System.Windows.Forms;
using DroneServer.SharedClasses;

using GMap.NET.WindowsForms;
using GMap.NET;

namespace DroneServer.PL.Observers
{
    class MapObserver : BaseObserver
    {
        GMapControl map;

        public MapObserver(GMapControl map)
        {
            this.map = map;
        }

        public override void update()
        {
            try
            {
                Point p = (Point)observable.getData();
                map.BeginInvoke((Action)(() =>
                {
                    map.Position = new PointLatLng(p.lat, p.lng);
                }));
                
               
            }
            catch (Exception e) { MessageBox.Show(e.Message); return; }
        }
    }
}
