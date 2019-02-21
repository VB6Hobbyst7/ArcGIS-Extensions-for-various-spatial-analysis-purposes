using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROI
{
    class CreatefFeatures
    {
        BoundingPolygons getPolygons = new BoundingPolygons();

        public IFeatureClass FeatureClass { get; set; }
        public List<IPoint> WidthPointsMain { get; set; }
        public List<IPoint> WidthPoints { get; set; }
        public ISpatialReference SpatialReference { get; set; }
        public void CreateFeatures()
        {
            try
            {

                IFeatureBuffer feature = FeatureClass.CreateFeatureBuffer();
                IFeatureCursor cursor = FeatureClass.Insert(true);
                for (int i = 0; i < WidthPoints.Count; i= i +2)
                {
                    IPolygon polygon = getPolygons.BoundingPolygon(new List<IPoint> { WidthPointsMain[i], WidthPointsMain[i+1], WidthPoints[i], WidthPoints[i+1] }, SpatialReference);

                    feature.Shape = polygon;
                    cursor.InsertFeature(feature);
                }
               
              
            }
            catch (Exception ex)
            {
             
            }

        }
    }
}
