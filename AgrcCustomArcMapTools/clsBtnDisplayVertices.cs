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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;

namespace AgrcCustomArcMapTools
{
    /// <summary>
    /// Summary description for clsBtnDisplayVertices.
    /// </summary>
    [Guid("3313d5a7-9aa4-4c6b-8a3a-0dd0c3aebe00")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AgrcCustomArcMapTools.clsBtnDisplayVertices")]
    public sealed class clsBtnDisplayVertices : BaseCommand
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

        IMap pMap;
        IMxDocument pMxDocument;
        IActiveView pActiveView;
        Boolean bolVerticesOn;

        private IApplication m_application;
        public clsBtnDisplayVertices()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "AGRC Tools"; //localizable text
            base.m_caption = "Display Vertices";  //localizable text
            base.m_message = "Display vertices for selected layer";  //localizable text 
            base.m_toolTip = "Display Vertices";  //localizable text 
            base.m_name = "AgrcDisplayVertices";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
            base.m_bitmap = Properties.Resources.button_display_vertices;
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
           	try
			{
				//get access to the document and the active view
				pMxDocument = (IMxDocument)clsAgrcArcMapExtension.m_application.Document;
				pMap = pMxDocument.FocusMap;
				pActiveView = pMxDocument.ActiveView;  //pActiveView = (IActiveView)pMap;

				//get the map's graphics layer
				ICompositeGraphicsLayer2 pComGraphicsLayer = pMap.BasicGraphicsLayer as ICompositeGraphicsLayer2;
				ICompositeLayer pCompositeLayer = pComGraphicsLayer as ICompositeLayer;
				ILayer pLayer;

				//loop through all graphic layers in the map and check for the 'PolyVertices' layer, if found, delete it, in order to start fresh
				for (int i = 0; i < pCompositeLayer.Count; i++)
				{
					pLayer = pCompositeLayer.get_Layer(i);
					if (pLayer.Name == "PolyVertices")
					{
						pComGraphicsLayer.DeleteLayer("PolyVertices");
						break;
					}
				}


				if (bolVerticesOn == false)
				{
					IGraphicsLayer pGraphicsLayer = pComGraphicsLayer.AddLayer("PolyVertices", null);
					pMap.ActiveGraphicsLayer = (ILayer)pGraphicsLayer;
					IGraphicsContainer pGraphicsContainer = pComGraphicsLayer.FindLayer("PolyVertices") as IGraphicsContainer;


				//make sure the user has selected a polygon or polyline layer
				if (pMxDocument.SelectedLayer == null)
				{
					MessageBox.Show("Please select a layer.","Select Layer",MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (!(pMxDocument.SelectedLayer is IFeatureLayer))
				{
					MessageBox.Show("Please select a polygon or line layer.", "Polygon or Line", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				//cast the selected layer as a feature layer
				IGeoFeatureLayer pGFlayer = (IGeoFeatureLayer)pMxDocument.SelectedLayer;

				//check if the feaure layer is a polygon or line layer
				if (pGFlayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon & pGFlayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
				{
					MessageBox.Show("Please select a polygon or line layer.", "Polygon or Line", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}


				//setup marker symbol
				ISimpleMarkerSymbol pSimpleMarker = new SimpleMarkerSymbol();
				ISymbol pSymbolMarker = (ISymbol)pSimpleMarker;
				IRgbColor pRgbColor = new ESRI.ArcGIS.Display.RgbColorClass();
				pRgbColor.Red = 223;
				pRgbColor.Green = 155;
				pRgbColor.Blue = 255;
				pSimpleMarker.Color = pRgbColor;
				pSimpleMarker.Style = esriSimpleMarkerStyle.esriSMSDiamond;
				pSimpleMarker.Size = 8;

				//setup line symbol
				ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbol();
				ISymbol pSymbolLine = (ISymbol)pSimpleLineSymbol;
				pRgbColor = new ESRI.ArcGIS.Display.RgbColor();
				pRgbColor.Red = 0;
				pRgbColor.Green = 255;
				pRgbColor.Blue = 0;
				pSimpleLineSymbol.Color = pRgbColor;
				pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
				pSimpleLineSymbol.Width = 1;

				//setup simplefill symbol
				ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbol();
				ISymbol pSymbolPolygon = (ISymbol)pSimpleFillSymbol;
				pRgbColor = new ESRI.ArcGIS.Display.RgbColor();
				pRgbColor.Red = 0;
				pRgbColor.Green = 0;
				pRgbColor.Blue = 255;
				pSimpleFillSymbol.Color = pRgbColor;
				pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
 
				//get all the polygons in the current map extent ina cursor
				IEnvelope pMapExtent = pActiveView.Extent;
				ISpatialFilter pQFilter = new SpatialFilter();
				pQFilter.GeometryField = "SHAPE";
				pQFilter.Geometry = pMapExtent;
				pQFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
				IFeatureCursor pFCursor = pGFlayer.Search(pQFilter,true);


				//draw each polygon and then each polygon's point collection
				IFeature pFeature = pFCursor.NextFeature();
				IGeometry pGeometry;

				while (pFeature != null)
				{
					pGeometry = pFeature.Shape;
					//draw the polygon
					//draw each vertex on the polygon
					IPointCollection pPointCollection = pGeometry as IPointCollection;
					for (int i = 0; i < pPointCollection.PointCount; i++)
					{
						IGeometry pPtGeom = pPointCollection.get_Point(i);    
						IElement pElement = new MarkerElement();
						pElement.Geometry = pPtGeom;
						IMarkerElement pMarkerElement = pElement as IMarkerElement;
						pMarkerElement.Symbol = pSimpleMarker;
						pGraphicsContainer.AddElement(pElement, 0);
					}
					pFeature = pFCursor.NextFeature();
				}

				bolVerticesOn = true;
					
				}
				else //if (bolVerticesOn == true)
				{
					bolVerticesOn = false;
				}				

				//refresh the map
				pActiveView.Refresh();
				pActiveView.Refresh();

			}
			catch (Exception ex)
			{
				MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
				"Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
				"Error Location:" + Environment.NewLine + ex.StackTrace,
				"AGRC Custom Tools ArcMap Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

        #endregion
    }
}
