using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAStructures;

namespace EAAddIn.Windows
{
    public partial class UIGetCurrentPath : Form
    {

        public EaCaliberGenEngine EAEngine;
        public DataTable elementSourceDataTable;
        public SecurityInfo secinfo;
        public ArrayList visitedArray;


        public UIGetCurrentPath()
        {
            InitializeComponent();

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

        }

        private void GetCurrentPath_Load(object sender, EventArgs e)
        {
            var currPath = EaAccess.getCurrentPath();

            txtGetCurrentPath.Text = currPath;

        }

        private void btnGetCurrent_Click(object sender, EventArgs e)
        {
            var currPath = EaAccess.getCurrentPath();

            txtGetCurrentPath.Text = currPath;

        }

        private void btnLocate_Click(object sender, EventArgs e)
        {

            var currPath = txtGetCurrentPath.Text;
            string pathType = "";
            string itemType = "";
            string eaGUID = "";
            object obj = null;
            int elementID = 0;


            if (currPath.Substring(0, 1) == "#")
                pathType = "INHOUSE";

            if (currPath.Substring(0, 1) == "{")
                pathType = "GUID";

            if (pathType == "")
                pathType = "EATYPE";

            if (pathType == "EATYPE")
            {
                eaGUID = GetGuidFromReferencePath();
            }

            if (pathType == "GUID")
            {
                eaGUID = currPath;
            }

            if (pathType == "EATYPE" || pathType == "GUID")
            {

                if (eaGUID == "")
                    return;

                try
                {
                    obj = (object)AddInRepository.Instance.Repository.GetDiagramByGuid(eaGUID);
                }
                catch (Exception)
                {
                }

                if (obj == null)
                {
                    try
                    {
                        obj = (object)AddInRepository.Instance.Repository.GetPackageByGuid(eaGUID);
                    }
                    catch (Exception)
                    {
                    }
                }

                if (obj == null)
                {
                    try
                    {
                        obj = (object)AddInRepository.Instance.Repository.GetElementByGuid(eaGUID);
                    }
                    catch (Exception)
                    {
                    }
                }


            }
            
            
            
            if (pathType == "INHOUSE")
            {


                try
                {
                    itemType = currPath.Substring(1, 1);
                    elementID = Convert.ToInt32(currPath.Substring(2, 10));

                }
                catch (Exception)
                {
                    return;
                }


                if (itemType == "D")
                {
                    obj = (object)AddInRepository.Instance.Repository.GetDiagramByID(elementID);
                }
                if (itemType == "E")
                {
                    obj = (object)AddInRepository.Instance.Repository.GetElementByID(elementID);
                }
                if (itemType == "P")
                {
                    obj = (object)AddInRepository.Instance.Repository.GetPackageByID(elementID);
                }
            }

            if (obj != null)
            {
                AddInRepository.Instance.Repository.ShowInProjectView(obj);

                if (obj is Diagram)
                {
                    AddInRepository.Instance.Repository.OpenDiagram(((Diagram)obj).DiagramID);
                }
            }

            return;

        }
        private string GetGuidFromReferencePath()
        {
            var guid =  new EaAccess().GetPathGuidForReference(txtGetCurrentPath.Text);

            //check to see if an element
            if (guid == string.Empty)
            {
                guid = new EaAccess().GetElementGuidForReference(txtGetCurrentPath.Text);
            }

            //check to see if a diagram
            if (guid == string.Empty)
            {
                guid = new EaAccess().GetDiagramGuidForReference(txtGetCurrentPath.Text);
            }

            return guid;
        }
    }
}
