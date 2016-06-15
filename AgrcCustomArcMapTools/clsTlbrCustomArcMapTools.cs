using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AgrcCustomArcMapTools
{
    /// <summary>
    /// Summary description for clsTlbrCustomArcMapTools.
    /// </summary>
    [Guid("24f80a7d-5a0a-455a-8ab4-02193d31488b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AgrcCustomArcMapTools.clsTlbrCustomArcMapTools")]
    public sealed class clsTlbrCustomArcMapTools : BaseToolbar
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
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public clsTlbrCustomArcMapTools()
        {
            //
            // TODO: Define your toolbar here by adding items
            //
            //AddItem("esriArcMapUI.ZoomInTool");
            //BeginGroup(); //Separator
            //AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            //AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command
            AddItem("{b882f2a0-c8e8-4aa1-ad6b-76efbd3d4eba}"); //compare vertices
            AddItem("{3313d5a7-9aa4-4c6b-8a3a-0dd0c3aebe00}"); //display vertices
            AddItem("{56010cd9-a205-4cce-9c1c-50a7a0fc268d}"); //google streetview dude

        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "Utah AGRC Tools";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "clsTlbrCustomArcMapTools";
            }
        }
    }
}