using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROI
{
    class BoundingPolygons
    {
        public IPolygon BoundingPolygon( IList<IPoint> pList , ISpatialReference spatialrefrence)
        {
            try
            {
                IGeometryBridge2 pGeoBrg = new GeometryEnvironment() as IGeometryBridge2;
                IPointCollection4 pPointColl = (IPointCollection4) new Multipoint(); // edited here

                int numPoints = pList.Count;
                WKSPoint[] aWKSPointBuffer = new WKSPoint[numPoints];
                for (int i = 0; i < pList.Count; i++)
                {
                    WKSPoint A = new WKSPoint();
                    A.X = pList[i].X;
                    A.Y = pList[i].Y;
                    aWKSPointBuffer[i] = A;
                }
                pGeoBrg.SetWKSPoints(pPointColl, ref aWKSPointBuffer);

                // edits here
                IGeometry pGeom = (IMultipoint)pPointColl;
                pGeom.SpatialReference = spatialrefrence;
                ITopologicalOperator pTopOp = (ITopologicalOperator)pGeom;
                IPolygon pPointColl2 = (IPolygon)pTopOp.ConvexHull();

                pPointColl2.SpatialReference = spatialrefrence;
                // OutputPolygon = pPointColl2; maybe you don't need this line as the object is not used
                return pPointColl2;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
