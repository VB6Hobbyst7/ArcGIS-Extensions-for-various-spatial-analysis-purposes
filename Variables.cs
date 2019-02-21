using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROI
{
   static class Variables
    {
        public static int s_NumberOfPoints;
        public static  List<IPoint> s_LstPoints = new System.Collections.Generic.List<IPoint>();
        public static IPolyline s_polyline;
        
    }
}
