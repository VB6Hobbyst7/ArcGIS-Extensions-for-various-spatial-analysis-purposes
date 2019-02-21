using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROI
{
    public partial class frmGetRectangles : Form
    {
        IApplication application;
        private IActiveViewEvents_Event activeview_Event;
        frmOptions options;
        About.AboutBox about;
        List<ILayer> lstLayers;

        public IActiveView GetActiveView { get; set; }
        ESRI.ArcGIS.Carto.IGraphicsContainer graphicsContainer;
        ISegment widthSegment;
        ISegment heightSegment;

        public frmGetRectangles()
        {
            InitializeComponent();
        }

        private void frmGetRectangles_Load(object sender, EventArgs e)
        {
            lstLayers = new List<ILayer>();
            activeview_Event = (IActiveViewEvents_Event)GetActiveView;
            LoopThroughLayersOfSpecificUID((IMap)GetActiveView, "{40A9E885-5533-11D0-98BE-00805F7CED21}");
            activeview_Event.ItemAdded += Activeview_Event_ItemAdded;
            activeview_Event.ItemDeleted += Activeview_Event_ItemDeleted;
        }

        private void Activeview_Event_ItemDeleted(object Item)
        {
            if (Item is IFeatureLayer)
                LoopThroughLayersOfSpecificUID((IMap)GetActiveView, "{40A9E885-5533-11D0-98BE-00805F7CED21}");
        }

        private void Activeview_Event_ItemAdded(object Item)
        {
            if (Item is IFeatureLayer)
                LoopThroughLayersOfSpecificUID((IMap)GetActiveView, "{40A9E885-5533-11D0-98BE-00805F7CED21}");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnClearPath_Click(object sender, EventArgs e)
        {
            IMap map = (IMap)GetActiveView;
            graphicsContainer = (ESRI.ArcGIS.Carto.IGraphicsContainer)map; // Explicit Cast
            graphicsContainer.DeleteAllElements();
            Variables.s_NumberOfPoints = 0;
            Variables.s_LstPoints.Clear();
            Variables.s_polyline = null;
            GetActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void btnGetRectangles_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variables.s_polyline == null)
                {
                    labelStatus.Text = "Please draw width and heigth.";
                    return;
                }
                if (!IsValidate()) return;

                labelStatus.Text = "In processing ...";
                Application.DoEvents();

                ConstructPointParallelToLine(Variables.s_polyline);

                Variables.s_NumberOfPoints = 0;
                Variables.s_LstPoints.Clear();
                Variables.s_polyline = null;
            }
            catch (Exception ex)
            {

            }

        }


        private bool IsValidate()
        {
            bool result = true;
            double buffer = 0 ;

            try
            {
                if (!chkBuffer.Checked)
                {
                    result = double.TryParse(txtHeight.Text, out Heigthofrects);
                    if (!result)
                    {
                        labelStatus.Text = "The heigth should be double.";
                        return false;
                    }
                    result = double.TryParse(txtWidth.Text, out Widthofrects);
                    if (!result)
                    {
                        labelStatus.Text = "The width should be double.";
                        return false;
                    }
                }
                else
                {
                    double HeigthofrectsWithoutBuffer;
                    double WidthofrectsWithoutBuffer;

                    result = double.TryParse(txtBuffer.Text, out buffer);
                    if (!result)
                    {
                        labelStatus.Text = "The buffer should be double.";
                        return false;
                    }
                    result = double.TryParse(txtHeight.Text, out HeigthofrectsWithoutBuffer);
                    if (!result)
                    {
                        labelStatus.Text = "The heigth should be double.";
                        return false;
                    }
                    result = double.TryParse(txtWidth.Text, out WidthofrectsWithoutBuffer);
                    if (!result)
                    {
                        labelStatus.Text = "The width should be double.";
                        return false;
                    }

                    Heigthofrects = HeigthofrectsWithoutBuffer - buffer;
                    Widthofrects = WidthofrectsWithoutBuffer - buffer;
                }

                result = double.TryParse(txtNumRectHeight.Text, out NumberOfRectsinHeigth);
                if (!result)
                {
                    labelStatus.Text = "The number of rects in heigth should be double.";
                    return false;
                }
                result = double.TryParse(txtNumRectWidth.Text, out NumberOfRectsinWidth);
                if (!result)
                {
                    labelStatus.Text = "The number of rects in width should be double.";
                    return false;
                }
                if (!chkBuffer.Checked)
                {
                    result = double.TryParse(txtDisHeight.Text, out distanceAlongHeigth);
                    if (!result)
                    {
                        labelStatus.Text = "The distance of rects in heigth should be double.";
                        return false;
                    }
                    result = double.TryParse(txtDisWidth.Text, out distanceAlongWidth);
                    if (!result)
                    {
                        labelStatus.Text = "The distance of rects in width should be double.";
                        return false;
                    }
                }
                else
                {
                    double distanceAlongHeigthWithoutBuffer;
                    double distanceAlongWidthWithoutBuffer;
                
                    result = double.TryParse(txtDisHeight.Text, out distanceAlongHeigthWithoutBuffer);
                    if (!result)
                    {
                        labelStatus.Text = "The distance of rects in heigth should be double.";
                        return false;
                    }
                    result = double.TryParse(txtDisWidth.Text, out distanceAlongWidthWithoutBuffer);
                    if (!result)
                    {
                        labelStatus.Text = "The distance of rects in width should be double.";
                        return false;
                    }

                    distanceAlongHeigth = distanceAlongHeigthWithoutBuffer + 2 * buffer;
                    distanceAlongWidth = distanceAlongWidthWithoutBuffer + 2 * buffer;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        double Heigthofrects;
        double Widthofrects;
        double NumberOfRectsinHeigth;
        double NumberOfRectsinWidth;
        double distanceAlongHeigth;
        double distanceAlongWidth;
        List<IPoint> widthPointsMain;
        List<IPoint> widthPoints;
        List<IPoint> heigthPoints;

        GetFeatureClass getFeatureClass = new GetFeatureClass();
        CreatefFeatures createRects = new CreatefFeatures();
        bool firstLine;
        public void ConstructPointParallelToLine(IPolyline Path)
        {
            IWorkspaceEdit WorkspaceEdit = null;
            try
            {

                if (Path != null)
                {
                    if (Path.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        firstLine = false;
                        widthPointsMain = new List<IPoint>();
                        heigthPoints = new List<IPoint>();
                        widthPoints = new List<IPoint>();

                        IFeatureClass rectsFeatureClass = ((IFeatureLayer)lstLayers[cboLayers.SelectedIndex]).FeatureClass;
                        IWorkspace workspace = ((IDataset)rectsFeatureClass).Workspace;
                        WorkspaceEdit = (IWorkspaceEdit)workspace;

                        if (!WorkspaceEdit.IsBeingEdited())
                            WorkspaceEdit.StartEditing(true);
                        WorkspaceEdit.StartEditOperation();

                        IConstructPoint constructionPoint = (IConstructPoint)new ESRI.ArcGIS.Geometry.Point();

                        IGeometryCollection geometryCollection = Path as IGeometryCollection;
                        ISegmentCollection segmentCollection = geometryCollection.get_Geometry(0) as ISegmentCollection;
                        widthSegment = segmentCollection.get_Segment(0);
                        heightSegment = segmentCollection.get_Segment(1);

                        IPoint fromPoint = new ESRI.ArcGIS.Geometry.Point();
                        heightSegment.QueryFromPoint(fromPoint);
                        widthPointsMain.Add(fromPoint);

                        IRgbColor rgbColor = GetColor(255, 0, 0);
                        //AddGraphicToMap((IMap)GetActiveView, fromPoint, rgbColor, rgbColor);
                        //GetActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

                        widthSegment.ReverseOrientation();


                        double distanceAlongWidthBunderiesPoints = 0;
                        double distanceAlongHeightBunderiesPoints = 0;

                        for (int i = 0; i < NumberOfRectsinWidth; i++)
                        {
                            IPoint qpWidthSegment = GetQPWidthSegment(distanceAlongWidthBunderiesPoints + Widthofrects);
                            if (qpWidthSegment == null) return;
                            widthPointsMain.Add(qpWidthSegment);

                            //AddGraphicToMap((IMap)GetActiveView, qpWidthSegment, rgbColor, rgbColor);

                            if (i != NumberOfRectsinWidth - 1)
                            {
                                IPoint qpWidthSegmentWithDistance = GetQPWidthSegment(distanceAlongWidthBunderiesPoints + Widthofrects + distanceAlongWidth);
                                //AddGraphicToMap((IMap)GetActiveView, qpWidthSegmentWithDistance, rgbColor, rgbColor);
                                widthPointsMain.Add(qpWidthSegmentWithDistance);
                            }
                            distanceAlongWidthBunderiesPoints = (Widthofrects + distanceAlongWidth) * (i + 1);

                        }

                        for (int i = 0; i < NumberOfRectsinHeigth; i++)
                        {
                            IPoint qpHeightSegment = GetQPHeightSegment(distanceAlongHeightBunderiesPoints + Heigthofrects);
                            if (qpHeightSegment == null) return;
                            heigthPoints.Add(qpHeightSegment);

                            //AddGraphicToMap((IMap)GetActiveView, qpHeightSegment, rgbColor, rgbColor);

                            if (i != NumberOfRectsinHeigth - 1)
                            {
                                IPoint qpHeightSegmentWithDistance = GetQPHeightSegment(distanceAlongHeightBunderiesPoints + Heigthofrects + distanceAlongHeigth);
                                //AddGraphicToMap((IMap)GetActiveView, qpHeightSegmentWithDistance, rgbColor, rgbColor);
                                heigthPoints.Add(qpHeightSegmentWithDistance);
                            }
                            distanceAlongHeightBunderiesPoints = (Heigthofrects + distanceAlongHeigth) * (i + 1);
                        }

                        for (int j = 0; j < heigthPoints.Count; j++)
                        {

                            constructionPoint.ConstructParallel(widthSegment, esriSegmentExtension.esriExtendAtTo, heigthPoints[j], Widthofrects);
                            IPoint outPutPoint = constructionPoint as IPoint;

                            IPath newPath = (IPath)new ESRI.ArcGIS.Geometry.Path();
                            newPath.FromPoint = heigthPoints[j];
                            newPath.ToPoint = outPutPoint;

                            if (!firstLine) widthPoints.Add(heigthPoints[j]);
                            else widthPointsMain.Add(heigthPoints[j]);

                            distanceAlongWidthBunderiesPoints = 0;

                            for (int i = 0; i < NumberOfRectsinWidth; i++)
                            {
                                IPoint qpWidthSegment = GetQPWidthNewSegment(newPath, distanceAlongWidthBunderiesPoints + Widthofrects);
                                if (qpWidthSegment == null) return;

                                if (!firstLine) widthPoints.Add(qpWidthSegment);
                                else widthPointsMain.Add(qpWidthSegment);

                                //AddGraphicToMap((IMap)GetActiveView, qpWidthSegment, rgbColor, rgbColor);

                                if (i != NumberOfRectsinWidth - 1)
                                {
                                    IPoint qpWidthSegmentWithDistance = GetQPWidthNewSegment(newPath, distanceAlongWidthBunderiesPoints + Widthofrects + distanceAlongWidth);
                                    //AddGraphicToMap((IMap)GetActiveView, qpWidthSegmentWithDistance, rgbColor, rgbColor);
                                    if (!firstLine) widthPoints.Add(qpWidthSegmentWithDistance);
                                    else widthPointsMain.Add(qpWidthSegmentWithDistance);
                                }

                                //widthPoints.Add(qpWidthSegment);
                                //widthPoints.Add(qpWidthSegmentWithDistance);
                                distanceAlongWidthBunderiesPoints = (Widthofrects + distanceAlongWidth) * (i + 1);

                            }

                            //IRgbColor rgbColor1 = GetColor(0, 255, 0);

                            if (widthPoints.Count > 0 && widthPointsMain.Count > 0)
                            {

                              

                                //IFeatureClass rectsFeatureClass = getFeatureClass.GetFeatureClassFromShapefileOnDisk(shapefileLocation, out workspace);
                                if (rectsFeatureClass == null) return;

                                createRects.FeatureClass = rectsFeatureClass;
                                createRects.SpatialReference = ((IGeoDataset)rectsFeatureClass).SpatialReference;
                                createRects.WidthPoints = widthPoints;
                                createRects.WidthPointsMain = widthPointsMain;
                                createRects.CreateFeatures();
                                widthPoints.Clear();
                                widthPointsMain.Clear();
                                //if(!ShapefileIsAdded()) AddShapefile(GetActiveView, shapefileLocation);

                            }
                            firstLine = !firstLine;

                            //AddGraphicToMap((IMap)GetActiveView, outPutPoint, rgbColor1, rgbColor1);
                        }

                        labelStatus.Text = "It's done.";

                        WorkspaceEdit.StopEditOperation();
                        WorkspaceEdit.StopEditing(true);

                        GetActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                        GetActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Path = null;
                graphicsContainer.DeleteAllElements();
                labelStatus.Text = ex.Message;
                if (WorkspaceEdit != null)
                {
                    if (WorkspaceEdit.IsBeingEdited())
                    {
                        WorkspaceEdit.AbortEditOperation();
                        WorkspaceEdit.StopEditing(false);
                    }
                }
                GetActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }


        #region"Loop Through Layers of Specific UID"
        // ArcGIS Snippet Title:
        // Loop Through Layers of Specific UID
        // 
        // Long Description:
        // Stub code to loop through layers in a map where a specific UID is supplied.
        // 
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.System
        // System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop (ArcEditor, ArcInfo, ArcView)
        // ArcGIS Engine
        // ArcGIS Server
        // 
        // Applicable ArcGIS Product Versions:
        // 9.2
        // 9.3
        // 9.3.1
        // 10.0
        // 
        // Required ArcGIS Extensions:
        // (NONE)
        // 
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        // 

        ///<summary>Stub code to loop through layers in a map where a specific UID is supplied.</summary>
        ///
        ///<param name="map">An IMap interface in which the layers reside.</param>
        ///<param name="layerCLSID">A System.String that is the layer GUID type. For example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" is the IGeoFeatureLayer.</param>
        /// 
        ///<remarks>In order of the code to be useful the user needs to write their own code to use the layer in the TODO section.
        ///
        /// The different layer GUID's and Interface's are:
        /// "{AD88322D-533D-4E36-A5C9-1B109AF7A346}" = IACFeatureLayer
        /// "{74E45211-DFE6-11D3-9FF7-00C04F6BC6A5}" = IACLayer
        /// "{495C0E2C-D51D-4ED4-9FC1-FA04AB93568D}" = IACImageLayer
        /// "{65BD02AC-1CAD-462A-A524-3F17E9D85432}" = IACAcetateLayer
        /// "{4AEDC069-B599-424B-A374-49602ABAD308}" = IAnnotationLayer
        /// "{DBCA59AC-6771-4408-8F48-C7D53389440C}" = IAnnotationSublayer
        /// "{E299ADBC-A5C3-11D2-9B10-00C04FA33299}" = ICadLayer
        /// "{7F1AB670-5CA9-44D1-B42D-12AA868FC757}" = ICadastralFabricLayer
        /// "{BA119BC4-939A-11D2-A2F4-080009B6F22B}" = ICompositeLayer
        /// "{9646BB82-9512-11D2-A2F6-080009B6F22B}" = ICompositeGraphicsLayer
        /// "{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E}" = ICoverageAnnotationLayer
        /// "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}" = IDataLayer
        /// "{0737082E-958E-11D4-80ED-00C04F601565}" = IDimensionLayer
        /// "{48E56B3F-EC3A-11D2-9F5C-00C04F6BC6A5}" = IFDOGraphicsLayer
        /// "{40A9E885-5533-11D0-98BE-00805F7CED21}" = IFeatureLayer
        /// "{605BC37A-15E9-40A0-90FB-DE4CC376838C}" = IGdbRasterCatalogLayer
        /// "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer
        /// "{34B2EF81-F4AC-11D1-A245-080009B6F22B}" = IGraphicsLayer
        /// "{EDAD6644-1810-11D1-86AE-0000F8751720}" = IGroupLayer
        /// "{D090AA89-C2F1-11D3-9FEF-00C04F6BC6A5}" = IIMSSubLayer
        /// "{DC8505FF-D521-11D3-9FF4-00C04F6BC6A5}" = IIMAMapLayer
        /// "{34C20002-4D3C-11D0-92D8-00805F7C28B0}" = ILayer
        /// "{E9B56157-7EB7-4DB3-9958-AFBF3B5E1470}" = IMapServerLayer
        /// "{B059B902-5C7A-4287-982E-EF0BC77C6AAB}" = IMapServerSublayer
        /// "{82870538-E09E-42C0-9228-CBCB244B91BA}" = INetworkLayer
        /// "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}" = IRasterLayer
        /// "{AF9930F0-F61E-11D3-8D6C-00C04F5B87B2}" = IRasterCatalogLayer
        /// "{FCEFF094-8E6A-4972-9BB4-429C71B07289}" = ITemporaryLayer
        /// "{5A0F220D-614F-4C72-AFF2-7EA0BE2C8513}" = ITerrainLayer
        /// "{FE308F36-BDCA-11D1-A523-0000F8774F0F}" = ITinLayer
        /// "{FB6337E3-610A-4BC2-9142-760D954C22EB}" = ITopologyLayer
        /// "{005F592A-327B-44A4-AEEB-409D2F866F47}" = IWMSLayer
        /// "{D43D9A73-FF6C-4A19-B36A-D7ECBE61962A}" = IWMSGroupLayer
        /// "{8C19B114-1168-41A3-9E14-FC30CA5A4E9D}" = IWMSMapLayer
        ///</remarks>
        public void LoopThroughLayersOfSpecificUID(ESRI.ArcGIS.Carto.IMap map, System.String layerCLSID)
        {
            if (map == null || layerCLSID == null)
            {
                return;
            }
            ESRI.ArcGIS.esriSystem.IUID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = layerCLSID; // Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer
            try
            {
                ESRI.ArcGIS.Carto.IEnumLayer enumLayer = map.get_Layers(((ESRI.ArcGIS.esriSystem.UID)(uid)), true); // Explicit Cast 
                enumLayer.Reset();
                ESRI.ArcGIS.Carto.ILayer layer = enumLayer.Next();
                cboLayers.Items.Clear();
                lstLayers.Clear();
                while (!(layer == null))
                {
                    IFeatureLayer featureLayer = (IFeatureLayer)layer;
                    if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        cboLayers.Items.Add(layer.Name);
                        lstLayers.Add(layer);
                    }
                    layer = enumLayer.Next();
                }
                if (cboLayers.Items.Count > 0)
                    cboLayers.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("No layers of type: " + uid.Value.ToString);
            }
        }
        #endregion


        private IRgbColor GetColor(int red, int blue, int green)
        {
            IRgbColor rgbColor = new RgbColor();
            rgbColor.Red = red;
            rgbColor.Blue = blue;
            rgbColor.Green = green;

            return rgbColor;
        }

        public void AddGraphicToMap(ESRI.ArcGIS.Carto.IMap map, ESRI.ArcGIS.Geometry.IGeometry geometry, ESRI.ArcGIS.Display.IRgbColor rgbColor, ESRI.ArcGIS.Display.IRgbColor outlineRgbColor)
        {
            ESRI.ArcGIS.Carto.IGraphicsContainer graphicsContainer = (ESRI.ArcGIS.Carto.IGraphicsContainer)map; // Explicit Cast
            ESRI.ArcGIS.Carto.IElement element = null;
            if ((geometry.GeometryType) == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
            {
                // Marker symbols
                ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbol();
                simpleMarkerSymbol.Color = rgbColor;
                simpleMarkerSymbol.Outline = true;
                simpleMarkerSymbol.OutlineColor = outlineRgbColor;
                simpleMarkerSymbol.Size = 10;
                simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;

                ESRI.ArcGIS.Carto.IMarkerElement markerElement = (IMarkerElement)new ESRI.ArcGIS.Carto.MarkerElement();
                markerElement.Symbol = simpleMarkerSymbol;
                element = (ESRI.ArcGIS.Carto.IElement)markerElement; // Explicit Cast
            }
            else if ((geometry.GeometryType) == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
            {
                //  Line elements
                ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbol();
                simpleLineSymbol.Color = rgbColor;
                simpleLineSymbol.Style = ESRI.ArcGIS.Display.esriSimpleLineStyle.esriSLSSolid;
                simpleLineSymbol.Width = 2;

                ESRI.ArcGIS.Carto.ILineElement lineElement = (ILineElement)new ESRI.ArcGIS.Carto.LineElement();
                lineElement.Symbol = simpleLineSymbol;
                element = (ESRI.ArcGIS.Carto.IElement)lineElement; // Explicit Cast
            }
            else if ((geometry.GeometryType) == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
            {
                // Polygon elements
                ESRI.ArcGIS.Display.ISimpleFillSymbol simpleFillSymbol = new ESRI.ArcGIS.Display.SimpleFillSymbol();
                simpleFillSymbol.Color = rgbColor;
                simpleFillSymbol.Style = ESRI.ArcGIS.Display.esriSimpleFillStyle.esriSFSForwardDiagonal;
                ESRI.ArcGIS.Carto.IFillShapeElement fillShapeElement = (IFillShapeElement)new ESRI.ArcGIS.Carto.PolygonElement();
                fillShapeElement.Symbol = simpleFillSymbol;
                element = (ESRI.ArcGIS.Carto.IElement)fillShapeElement; // Explicit Cast
            }
            if (!(element == null))
            {
                element.Geometry = geometry;
                graphicsContainer.AddElement(element, 0);
            }
        }

        private IPoint GetQPHeightSegment(double distance)
        {
            ICurve curve = (ICurve)heightSegment;
            IPoint outPoint = new ESRI.ArcGIS.Geometry.Point();
            curve.QueryPoint(esriSegmentExtension.esriExtendAtTo, distance, false, outPoint);
            return outPoint;
        }

        private IPoint GetQPWidthSegment(double distance)
        {
            ICurve curve = (ICurve)widthSegment;
            IPoint outPoint = new ESRI.ArcGIS.Geometry.Point();
            curve.QueryPoint(esriSegmentExtension.esriExtendAtTo, distance, false, outPoint);
            return outPoint;
        }

        private IPoint GetQPWidthNewSegment(ICurve curve, double distance)
        {
            IPoint outPoint = new ESRI.ArcGIS.Geometry.Point();
            curve.QueryPoint(esriSegmentExtension.esriExtendAtTo, distance, false, outPoint);
            return outPoint;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            options = new frmOptions();
            options.ShowDialog();
        }

        private void frmGetRectangles_FormClosing(object sender, FormClosingEventArgs e)
        {
            activeview_Event = null;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            about = new About.AboutBox();
            about.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkBuffer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBuffer.Checked)
                txtBuffer.Enabled = true;
            else
                txtBuffer.Enabled = false;
        }
    }
}
