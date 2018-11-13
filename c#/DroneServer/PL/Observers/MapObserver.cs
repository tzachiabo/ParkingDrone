using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                map.Position = new PointLatLng(p.y, p.x);
            }
            catch (Exception e) { MessageBox.Show(e.Message); return; }
        }
    }
}
