using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AgrcCustomArcMapTools
{
    public partial class frmCompareVertices : Form
    {
        public frmCompareVertices()
        {
            InitializeComponent();
        }

        //get access to the document (the current mxd), and the active view (data view or layout view), and the focus map (the data frame with focus, aka: the visible map)
        IMxDocument pMxDocument = (IMxDocument)clsAgrcArcMapExtension.m_application.Document;
        IActiveView pActiveView;
        IMap pMap;

        private void frmCompareVertices_Load(object sender, EventArgs e)
        {
            try
            {
                //show busy mouse
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                //clear the comboboxs
                cboLayer1.Items.Clear();
                cboLayer2.Items.Clear();

                pActiveView = pMxDocument.ActiveView;
                pMap = pActiveView.FocusMap;


                //loop through all the layers in the map and add only the polygon layers to the combobox
                IEnumLayer pEnumLayer;
                ILayer pLayer;
                IFeatureLayer pFeatureLayer;
                IFeatureClass pFeatureClass;

                //get access to all the map's layers
                pMap = pMxDocument.FocusMap;
                pEnumLayer = pMap.Layers;

                if (pMap.LayerCount != 0)
                {
                    //loop through the map's layers
                    while ((pLayer = pEnumLayer.Next()) != null)
                    {
                        //make sure the layer is a feature layer
                        if (pLayer is FeatureLayer)
                        {
                            //get access to the feature layer's feature class properties
                            pFeatureLayer = pLayer as IFeatureLayer;
                            pFeatureClass = pFeatureLayer.FeatureClass;

                            //check if the layer is a polygon layer, if so, add it to both comboboxes
                            if (pFeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                            {
                                cboLayer1.Items.Add(pLayer.Name);
                                cboLayer2.Items.Add(pLayer.Name);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please add at least one polygon layer to the map", "Add Layer to Map", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "AGRC Custom Tools ArcMap Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        //this method is called when the user clicks the "Compare Vertices" button at the bottom of the form
        private void cmdCompare_Click(object sender, EventArgs e)
        {

            try
            {
                if (cboLayer1.SelectedItem.ToString() == "" || cboLayer2.SelectedItem.ToString() == "" || txtOID1.Text == "" || txtOID2.Text == "") //the double pipe is an or operator that "short circuts", meaning that it will bail out early if the condition is true, whereas the single pipe evaluates all conditions
                {
                    MessageBox.Show("Choose polygon layer, or specify OBJECTID.", "Verify Selections...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //show busy mouse
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                IFeatureLayer pFeatureLayer1 = null;
                IFeatureLayer pFeatureLayer2 = null;

                IPointCollection pPtnCollMissingVertices = new MultipointClass(); //point collection for missing vertices and used for graphics layer
                IPoint pPointMissing = new PointClass(); //used for graphics layer

                //get access to polygon layer 1
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    if (pMap.get_Layer(i) is IFeatureLayer)
                    {
                        if (pMap.get_Layer(i).Name.ToString() == cboLayer1.SelectedItem.ToString())
                        {
                            pFeatureLayer1 = pMap.get_Layer(i) as IFeatureLayer;
                            break;
                        }
                    }
                }

                //get access to polygon layer 2
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    if (pMap.get_Layer(i) is IFeatureLayer)
                    {
                        if (pMap.get_Layer(i).Name.ToString() == cboLayer2.SelectedItem.ToString())
                        {
                            pFeatureLayer2 = pMap.get_Layer(i) as IFeatureLayer;
                            break;
                        }
                    }
                }

                //clear the x,y report list box
                lstMissingVertices.Items.Clear();


                //get vertices for feature 1
                //set up query filter for feature 1
                IQueryFilter pQueryFilter = new QueryFilter();
                pQueryFilter.WhereClause = "OBJECTID = '" + txtOID1.Text + "'";

                //set up feature cursor for feature 1
                IFeatureCursor pFeatureCursor = pFeatureLayer1.Search(pQueryFilter, false);

                //get feature 1
                IFeature pFeature1 = pFeatureCursor.NextFeature();

                //if no objectid is found
                if (pFeature1 == null)
                {
                    MessageBox.Show("The provided OBJECTID was not found for layer: " + cboLayer1.SelectedItem, "ObjectID Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //get feature 1 as a polygon
                IPolygon pPolygon1 = pFeature1.Shape as IPolygon;

                //get the point collection for feature 1
                IPointCollection pPointColl1 = pPolygon1 as IPointCollection;


                //get vertices for feature 2
                //set up query filter for feature 2
                pQueryFilter = new QueryFilter();
                pQueryFilter.WhereClause = "OBJECTID = '" + txtOID2.Text + "'";

                //set up feature cursor for feature 2
                pFeatureCursor = null; //reuse the feature cursor from above
                pFeatureCursor = pFeatureLayer2.Search(pQueryFilter, false);

                //get feature 2
                IFeature pFeature2 = pFeatureCursor.NextFeature();

                //if no objectid is found
                if (pFeature2 == null)
                {
                    MessageBox.Show("The provided OBJECTID was not found for layer: " + cboLayer1.SelectedItem, "ObjectID Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //get feature 1 as a polygon
                IPolygon pPolygon2 = pFeature2.Shape as IPolygon;

                //get the point collection for feature 1
                IPointCollection pPointColl2 = pPolygon2 as IPointCollection;


                //loop through point collection and check for missing vertices
                IPoint pPoint1;
                IPoint pPoint2;
                Boolean blnFound;


                //loop through each point in the first collection and check for missing
                for (int i = 0; i < pPointColl1.PointCount; i++)
                {
                    pPoint1 = pPointColl1.get_Point(i);

                    //loop through all the points in the second collection and check for the point from the above collection
                    blnFound = false;
                    for (int j = 0; j < pPointColl2.PointCount; j++)
                    {
                        pPoint2 = pPointColl2.get_Point(j);

                        //check for overlapping points
                        if (pPoint1.X == pPoint2.X && pPoint1.Y == pPoint2.Y)
                        {
                            blnFound = true;
                        }
                    }

                    //if no overlapping points were found, report the missing x,y location to the listbox
                    if (blnFound == false)
                    {
                        lstMissingVertices.Items.Add(pPoint1.X + " " + pPoint1.Y + " " + " Feet" + " (missing from polygon 2)");

                        //add the missing point to a new collection for display on map
                        pPtnCollMissingVertices.AddPoint(pPoint1);
                    }
                }


                //loop through each point in the first collection and check for missing
                for (int i = 0; i < pPointColl2.PointCount; i++)
                {
                    pPoint2 = pPointColl2.get_Point(i);

                    //loop through all the points in the second collection and check for the point from the above collection
                    blnFound = false;
                    for (int j = 0; j < pPointColl1.PointCount; j++)
                    {
                        pPoint1 = pPointColl1.get_Point(j);

                        //check for overlapping points
                        if (pPoint2.X == pPoint1.X && pPoint2.Y == pPoint1.Y)
                        {
                            blnFound = true;
                        }
                    }

                    //if no overlapping points were found, report the missing x,y location to the listbox
                    if (blnFound == false)
                    {
                        lstMissingVertices.Items.Add(pPoint2.X + " " + pPoint2.Y + " " + " Feet" + " (missing from polygon 1)");

                        //add the missing point to a new collection for display on map
                        pPtnCollMissingVertices.AddPoint(pPoint2);
                    }
                }


                //if no missing vertices were found, inform the user
                if (lstMissingVertices.Items.Count < 1)
                {
                    lstMissingVertices.Items.Add("No Missing Vertices Found");
                }


                //display vertices if checkbox is checked
                if (chkDisplayVertices.Checked == true)
                {
                    //show busy mouse
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                    //set up the graphics layer for the display of vertices
                    ICompositeGraphicsLayer2 pCompositeGraphicLayer = pMap.BasicGraphicsLayer as ICompositeGraphicsLayer2;
                    ICompositeLayer pCompositeLayer = pCompositeGraphicLayer as ICompositeLayer;
                    ILayer pLayer;
                    IGraphicsLayer pGraphicsLayer;
                    IGraphicsContainer pGraphicsContainer;

                    //loop through the graphics layer and check for the missing vertices layer.  if found, delete it
                    for (int i = 0; i < pCompositeLayer.Count; i++)
                    {
                        pLayer = pCompositeLayer.Layer[i];
                        if (pLayer.Name == "MissingVertices")
                        {
                            pCompositeGraphicLayer.DeleteLayer("MissingVertices");
                            break;
                        }
                    }


                    //set up a new 'missing vertices' graphics layer
                    pGraphicsLayer = pCompositeGraphicLayer.AddLayer("MissingVertices", null);
                    pMap.ActiveGraphicsLayer = pGraphicsLayer as ILayer;
                    pGraphicsContainer = pCompositeGraphicLayer.FindLayer("MissingVertices") as IGraphicsContainer;

                    //define the color
                    ESRI.ArcGIS.Display.IRgbColor rgbColorCls = new ESRI.ArcGIS.Display.RgbColorClass();
                    rgbColorCls.Red = 250;
                    rgbColorCls.Green = 0;
                    rgbColorCls.Blue = 0;

                    //define the font
                    stdole.IFontDisp stdFontCls = new stdole.StdFontClass() as stdole.IFontDisp;
                    stdFontCls.Name = "ESRI Surveyor Marker"; //this one has a thin-line hollow circle
                    stdFontCls.Name = "ESRI Surveyor"; //this one has a bold-line hollow circle
                    stdFontCls.Size = 16;

                    //set the character marker symbol's properties
                    ESRI.ArcGIS.Display.ICharacterMarkerSymbol charMarkerSymb = new ESRI.ArcGIS.Display.CharacterMarkerSymbolClass();
                    charMarkerSymb.Angle = 0;
                    charMarkerSymb.CharacterIndex = 47;
                    charMarkerSymb.Color = rgbColorCls;
                    charMarkerSymb.Font = stdFontCls;
                    charMarkerSymb.Size = 16;
                    charMarkerSymb.XOffset = 0;
                    charMarkerSymb.YOffset = 0;

                    //place the graphics on the map
                    for (int i = 0; i < pPtnCollMissingVertices.PointCount; i++)
                    {
                        pPointMissing = pPtnCollMissingVertices.Point[i];
                        IElement pElement = new MarkerElement();
                        pElement.Geometry = pPointMissing;
                        IMarkerElement pMarkerElement = pElement as IMarkerElement;
                        pMarkerElement.Symbol = charMarkerSymb;
                        pGraphicsContainer.AddElement(pElement, 0);
                    }

                    //refresh the map, in order to see the newly added graphics
                    pActiveView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "AGRC Custom Tools ArcMap Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        //this method zooms the map to the selected location, after the user double-clicks the point locaction in the listbox 
        private void lstMissingVertices_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                IPoint pPointXY = new PointClass();
                double X;
                double Y;
                object[] XY;
                IEnvelope pEnvelope;

                XY = lstMissingVertices.SelectedItem.ToString().Split(' ');
                X = Convert.ToDouble(XY.GetValue(0));
                Y = Convert.ToDouble(XY.GetValue(1));

                //set the point's xy values
                pPointXY.X = X;
                pPointXY.Y = Y;

                pEnvelope = pActiveView.Extent;
                pEnvelope.CenterAt(pPointXY);
                pActiveView.Extent = pEnvelope;

                pMap.MapScale = 100;

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



        //select all the text in the textbox when the user enters the textbox 
        private void txtOID1_Enter(object sender, EventArgs e)
        {
            txtOID1.SelectAll();
        }
        private void txtOID2_Enter(object sender, EventArgs e)
        {
            txtOID2.SelectAll();
        }



        //this method is executed when the user wants to zoom to the user-defined object id in textbox 1
        private void btnZoomOID1_Click(object sender, EventArgs e)
        {
            try
            {
                IFeatureLayer pFeatureLayer = null;

                //clear the selected features - in case other layers are selected
                pMap.ClearSelection();

                //get access to the layer associated with the objectid the user wants to zoom to
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    //check to see if the layer is an event layer (ex: VoterXY)
                    if (pMap.get_Layer(i) is FeatureLayer)
                    {
                        pFeatureLayer = pMap.get_Layer(i) as IFeatureLayer;
                        if (!(pFeatureLayer is IEventSource))
                        {
                            if (pFeatureLayer.Name == cboLayer1.Text)
                            {
                                pFeatureLayer.Visible = true;
                                break;
                            }
                        }
                    }
                }

                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;  //QI
                pFeatureSelection.Clear();

                //set up query filter
                IQueryFilter pQueryFilter = new QueryFilter();
                pQueryFilter.WhereClause = "OBJECTID = " + txtOID1.Text;

                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                pFeatureSelection.SelectionChanged();

                //refresh the maps - allow the selection anchor to display
                ISelectionEvents pSelectionEvents = pMap as ISelectionEvents;
                pSelectionEvents.SelectionChanged();

                //zoom to selected features
                UID pUID = new UID();
                pUID.Value = "{AB073B49-DE5E-11D1-AA80-00C04FA37860}";
                clsAgrcArcMapExtension.m_application.Document.CommandBars.Find(pUID).Execute();

                //refresh map
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pMxDocument.UpdateContents();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "AGRC Custom Tools ArcMap Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        //this method is executed when the user wants to zoom to the user-defined object id in textbox 2
        private void btnZoomOID2_Click(object sender, EventArgs e)
        {
            try
            {
                IFeatureLayer pFeatureLayer = null;

                //clear the selected features - in case other layers are selected
                pMap.ClearSelection();

                //get access to the layer associated with the objectid the user wants to zoom to
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    //check to see if the layer is an event layer (ex: VoterXY)
                    if (pMap.get_Layer(i) is FeatureLayer)
                    {
                        pFeatureLayer = pMap.get_Layer(i) as IFeatureLayer;
                        if (!(pFeatureLayer is IEventSource))
                        {
                            if (pFeatureLayer.Name == cboLayer2.Text)
                            {
                                pFeatureLayer.Visible = true;
                                break;
                            }
                        }
                    }
                }

                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;  //QI
                pFeatureSelection.Clear();

                //set up query filter
                IQueryFilter pQueryFilter = new QueryFilter();
                pQueryFilter.WhereClause = "OBJECTID = " + txtOID2.Text;

                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                pFeatureSelection.SelectionChanged();

                //refresh the maps - allow the selection anchor to display
                ISelectionEvents pSelectionEvents = pMap as ISelectionEvents;
                pSelectionEvents.SelectionChanged();

                //zoom to selected features
                UID pUID = new UID();
                pUID.Value = "{AB073B49-DE5E-11D1-AA80-00C04FA37860}";
                clsAgrcArcMapExtension.m_application.Document.CommandBars.Find(pUID).Execute();

                //refresh map
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pMxDocument.UpdateContents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "AGRC Custom Tools ArcMap Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        //this method is executed when the user clicks the clear vertices button
        private void btnClearVertices_Click(object sender, EventArgs e)
        {
            try
            {
                //show busy mouse
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                //get access to the graphics layer, in order to delete the vertices
                ICompositeGraphicsLayer2 pCompositeGraphicsLayer2 = pMap.BasicGraphicsLayer as ICompositeGraphicsLayer2;
                ICompositeLayer pCompositeLayer = pCompositeGraphicsLayer2 as ICompositeLayer;
                ILayer pLayer = null;

                for (int i = 0; i < pCompositeLayer.Count; i++)
                {
                    pLayer = pCompositeLayer.get_Layer(i);
                    if (pLayer.Name == "MissingVertices")
                    {
                        pCompositeGraphicsLayer2.DeleteLayer("MissingVertices");
                        break;
                    }
                }

                //refresh the map
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "AGRC Custom Tools ArcMap Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }





    }
}
