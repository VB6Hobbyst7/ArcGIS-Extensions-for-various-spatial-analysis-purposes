using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace ROI
{
    /// <summary>
    /// Summary description for GetRectangles.
    /// </summary>
    [Guid("c2e2949c-c146-411b-9653-f06ebbc136cf")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ROI.GetRectangles")]
    public sealed class GetRectangles : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        public GetRectangles()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "ROI Plot Generator"; //localizable text 
            base.m_caption = "ROI Plot Generator";  //localizable text 
            base.m_message = "";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_ArcMapTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".png";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="
        /// hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            Color defaultColor = new Color();
            defaultColor = Color.FromArgb(100, 100, 100);

            frmOptions.colorOfLines = defaultColor;
            frmOptions.colorOfPoints = defaultColor;
            frmOptions.sizeOfLines = 1;
            frmOptions.sizeOfPoints = 5;
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
           

        }

        IActiveView activeView;
        IPoint point;
      
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
        
            try
            {
                if (Button == 2)
                {
                    CreateContextMenu(m_application);
                    return;
                }

                IMap map = GetMapFromArcMap(m_application);
                IGraphicsContainer graphicsContainer = (IGraphicsContainer)map;
                activeView = (IActiveView)map;

                point = GetPointFromMouseClicks(activeView);

                IRgbColor rgbColor = GetColor(frmOptions.colorOfPoints.R, frmOptions.colorOfPoints.B, frmOptions.colorOfPoints.G);
                AddGraphicToMap(map, point, rgbColor, rgbColor);
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

               Variables. s_NumberOfPoints += 1;
              
                Variables.s_LstPoints.Add(point);

                if (Variables.s_LstPoints.Count == 3)
                {
                    Variables.s_polyline = new PolylineClass();
                    IPointCollection pc = (IPointCollection)Variables.s_polyline;
                    for (int i = 0; i < Variables.s_LstPoints.Count; i++)
                    {
                        pc.AddPoint(Variables.s_LstPoints[i]);
                    }
                    Variables.s_polyline.SpatialReference = map.SpatialReference;

                     rgbColor = GetColor( frmOptions.colorOfLines.R, frmOptions.colorOfLines.B, frmOptions.colorOfLines.G);
                    AddGraphicToMap(map, Variables.s_polyline, rgbColor, rgbColor);
                    activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
                //ShowFrom();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"Error");
            }
        }

     

        #region"Create Context Menu"
        // ArcGIS Snippet Title:
        // Create Context Menu
        // 
        // Long Description:
        // Create a context or popup menu dynamically using some default command items.
        // 
        // Add the following references to the project:
        // ESRI.ArcGIS.Framework
        // ESRI.ArcGIS.System
        // ESRI.ArcGIS.SystemUI
        // System
        // System.Drawing
        // System.Windows.Forms
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop (ArcEditor, ArcInfo, ArcView)
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

        ///<summary>Create a context or popup menu dynamically using some default command items.</summary>
        ///  
        ///<param name="application">An IApplication Interface.</param>
        ///   
        ///<remarks>Three default command items are added to the ContextMenu. Change the CLSID or ProgID as necessary for your specific command items.</remarks>
        public void CreateContextMenu(ESRI.ArcGIS.Framework.IApplication application)
        {
            ESRI.ArcGIS.Framework.ICommandBars commandBars = application.Document.CommandBars;
            ESRI.ArcGIS.Framework.ICommandBar commandBar = commandBars.Create("TemporaryContextMenu", ESRI.ArcGIS.SystemUI.esriCmdBarType.esriCmdBarTypeShortcutMenu);

            System.Object optionalIndex = System.Type.Missing;
            ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UIDClass();

            uid.Value = "ROI.ContextMenuItems.ShowForm"; // Can use CLSID or ProgID
            uid.SubType = 0;
            commandBar.Add(uid, ref optionalIndex);

            uid.Value = "ROI.ContextMenuItems.ClearGraphics.ClearGraphics"; // Can use CLSID or ProgID
            uid.SubType = 0;
            commandBar.Add(uid, ref optionalIndex);

            //uid.Value = "{FBF8C3FB-0480-11D2-8D21-080009EE4E51}"; // Can use CLSID or ProgID
            //uid.SubType = 1;
            //commandBar.Add(uid, ref optionalIndex);

            //uid.Value = "{FBF8C3FB-0480-11D2-8D21-080009EE4E51}"; // Can use CLSID or ProgID
            //uid.SubType = 2;
            //commandBar.Add(uid, ref optionalIndex);

            //Show the context menu at the current mouse location
            System.Drawing.Point currentLocation = System.Windows.Forms.Form.MousePosition;
            commandBar.Popup(currentLocation.X, currentLocation.Y);
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

        #region"Get Map from ArcMap"
        // ArcGIS Snippet Title:
        // Get Map from ArcMap
        // 
        // Long Description:
        // Get Map from ArcMap.
        // 
        // Add the following references to the project:
        // ESRI.ArcGIS.ArcMapUI
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Framework
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop (ArcEditor, ArcInfo, ArcView)
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

        ///<summary>Get Map from ArcMap</summary>
        ///  
        ///<param name="application">An IApplication interface that is the ArcMap application.</param>
        ///   
        ///<returns>An IMap interface.</returns>
        ///   
        ///<remarks></remarks>
        public ESRI.ArcGIS.Carto.IMap GetMapFromArcMap(ESRI.ArcGIS.Framework.IApplication application)
        {
            if (application == null)
            {
                return null;
            }
            ESRI.ArcGIS.ArcMapUI.IMxDocument mxDocument = ((ESRI.ArcGIS.ArcMapUI.IMxDocument)(application.Document)); // Explicit Cast
            ESRI.ArcGIS.Carto.IActiveView activeView = mxDocument.ActiveView;
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            return map;

        }
        #endregion


        #region"Get Point From Mouse Clicks"

        public ESRI.ArcGIS.Geometry.IPoint GetPointFromMouseClicks(ESRI.ArcGIS.Carto.IActiveView activeView)
        {

            ESRI.ArcGIS.Display.IScreenDisplay screenDisplay = activeView.ScreenDisplay;

            ESRI.ArcGIS.Display.IRubberBand rubberBand = new ESRI.ArcGIS.Display.RubberPoint();
            ESRI.ArcGIS.Geometry.IGeometry geometry = rubberBand.TrackNew(screenDisplay, null);

            ESRI.ArcGIS.Geometry.IPoint point = (ESRI.ArcGIS.Geometry.IPoint)geometry;

            return point;

        }
        #endregion

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
                simpleMarkerSymbol.Size = frmOptions.sizeOfPoints;
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
                simpleLineSymbol.Width = frmOptions.sizeOfLines;

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

        #region"Draw Polyline"

        public IGeometry DrawPolyline(ESRI.ArcGIS.Carto.IActiveView activeView)
        {

            if (activeView == null)
            {
                return null;
            }
            ESRI.ArcGIS.Display.IScreenDisplay screenDisplay = activeView.ScreenDisplay;

            // Constant
            screenDisplay.StartDrawing(screenDisplay.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache); // Explicit Cast
            ESRI.ArcGIS.Display.IRgbColor rgbColor = new ESRI.ArcGIS.Display.RgbColor();
            rgbColor.Red = 255;

            ESRI.ArcGIS.Display.IColor color = rgbColor; // Implicit Cast
            ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
            simpleLineSymbol.Color = color;
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDot;

            ESRI.ArcGIS.Display.ISymbol symbol = (ESRI.ArcGIS.Display.ISymbol)simpleLineSymbol; // Explicit Cast
            ESRI.ArcGIS.Display.IRubberBand rubberBand = new RubberLine();
            ESRI.ArcGIS.Geometry.IGeometry geometry = rubberBand.TrackNew(screenDisplay, symbol);
            screenDisplay.SetSymbol(symbol);
            screenDisplay.DrawPolyline(geometry);
            screenDisplay.FinishDrawing();
            return geometry;
        }
        #endregion

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add GetRectangles.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add GetRectangles.OnMouseUp implementation
        }
        #endregion
    }
}
