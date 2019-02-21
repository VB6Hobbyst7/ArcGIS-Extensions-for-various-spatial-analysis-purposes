using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;

namespace ROI
{
    class GetFeatureClass
    {

        #region"Get FeatureClass From Shapefile On Disk"
        public ESRI.ArcGIS.Geodatabase.IFeatureClass GetFeatureClassFromShapefileOnDisk(System.String string_ShapefileName , out ESRI.ArcGIS.Geodatabase.IWorkspace workspace)
        {


            //We have a valid directory, proceed

            System.IO.FileInfo fileInfo_check = new System.IO.FileInfo(string_ShapefileName);
            if (fileInfo_check.Exists)
            {

                //We have a valid shapefile, proceed

                ESRI.ArcGIS.Geodatabase.IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactory();
                 workspace = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(string_ShapefileName), 0);
                ESRI.ArcGIS.Geodatabase.IFeatureWorkspace featureWorkspace = (ESRI.ArcGIS.Geodatabase.IFeatureWorkspace)workspace; // Explict Cast
                ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(System.IO.Path.GetFileName(string_ShapefileName));
                return featureClass;
            }
            else
            {
                workspace = null;
                //Not valid shapefile
                return null;
            }



        }
        #endregion

    }
}
