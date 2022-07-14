using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAAddIn.Others;
using EAStructures;
using Constraint = EA.Constraint;
using EAAddIn.Others;

namespace EAAddIn.Windows.Controls
{
    public partial class BusinessRulesRealisationUserControl : UserControl
    {

        public EaCaliberGenEngine EAEngine;
        public DataTable elementSourceDataTable;
        public string includeSubPackages;
        public string ignoreDeletedRules;
        public SecurityInfo secinfo;
        public ArrayList visitedArray;

        public BusinessRulesRealisationUserControl()
        {
         
            InitializeComponent();

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            EAEngine = new EaCaliberGenEngine(secinfo, "caliber");

            var packageName = new DataColumn(DTColumnNames.ElementAssociationsScreen.Package, typeof(String));
            var businessRule = new DataColumn(DTColumnNames.ElementAssociationsScreen.Element, typeof(String));
            var BRStatus = new DataColumn(DTColumnNames.ElementAssociationsScreen.Status, typeof(String));
            var linkedTo = new DataColumn(DTColumnNames.ElementAssociationsScreen.RelatedTo, typeof(String));
            var placeddiagrams = new DataColumn(DTColumnNames.ElementAssociationsScreen.Diagrams, typeof(String));
            var brnotes = new DataColumn(DTColumnNames.ElementAssociationsScreen.Notes, typeof(String));
            var elementID = new DataColumn(DTColumnNames.ElementAssociationsScreen.ElementID, typeof(String));
            var author = new DataColumn(DTColumnNames.ElementAssociationsScreen.Author, typeof(String));
            var ea_guid = new DataColumn(DTColumnNames.ElementAssociationsScreen.EAGUID, typeof(String));
            var stereotype = new DataColumn(DTColumnNames.ElementAssociationsScreen.Stereotype, typeof(String));
            var createdDate = new DataColumn(DTColumnNames.ElementAssociationsScreen.CreatedDate, typeof(String));

            elementSourceDataTable = new DataTable("ElementSourceDataTable");

            elementSourceDataTable.Columns.Add(packageName);
            elementSourceDataTable.Columns.Add(businessRule);
            elementSourceDataTable.Columns.Add(BRStatus);
            elementSourceDataTable.Columns.Add(linkedTo);
            elementSourceDataTable.Columns.Add(placeddiagrams);
            elementSourceDataTable.Columns.Add(brnotes);
            elementSourceDataTable.Columns.Add(elementID);
            elementSourceDataTable.Columns.Add(author);
            elementSourceDataTable.Columns.Add(ea_guid);
            elementSourceDataTable.Columns.Add(createdDate);
            elementSourceDataTable.Columns.Add(stereotype);

            dgvRealisation.DataSource = elementSourceDataTable;

        }

        private void BusinessRulesRealisationUserControl_Load(object sender, EventArgs e)
        {
                
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            includeSubPackages = "No";
            ignoreDeletedRules = "No";

            if (cbxRecursive.Checked)
                includeSubPackages = "Yes";

            if (cbxIgnoreDeleted.Checked)
                ignoreDeletedRules = "Yes";

            Cursor.Current = Cursors.WaitCursor;

            Package packageSelected =
                AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (packageSelected != null)
            {
                // listElementsRelated(packageSelected);
                listElementsRelatedDT(packageSelected: packageSelected, 
                                      eltype: cbxType.Text, 
                                      connectedTo: cbxConnectedTo.Text,
                                      includeSubPackages: includeSubPackages, 
                                      ignoreDeletedRules: ignoreDeletedRules);

            }

            Cursor.Current = Cursors.Arrow;
        }

        // ----------------------------------------------
        //    List Related Elements
        // ----------------------------------------------
        private void listElementsRelatedDT(Package packageSelected,
                                           string eltype,
                                           string connectedTo,
                                           string includeSubPackages,
                                           string ignoreDeletedRules)
        {
            if (packageSelected == null)
                return;

            var eainst = new EaAccess();

            elementSourceDataTable.Clear();

            var packsql = new EaAccess.sqlPackage();
            packsql.Package_ID = packageSelected.PackageID;
            packsql.ea_guid = packageSelected.PackageGUID;
            packsql.Name = packageSelected.Name;

            eainst.getElementsFromPackages(packsql, elementSourceDataTable,
                                           eltype, connectedTo, includeSubPackages,
                                           ignoreDeletedRules);

            txtFilter.Text = "";
        }

        private void dgvRealisation_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvRealisation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get selected row
            string elementID = "";
            int ielementID = 0;

            foreach (DataGridViewRow dgvr in dgvRealisation.SelectedRows)
            {
                elementID = dgvr.Cells[DTColumnNames.ElementAssociationsScreen.ElementID].Value.ToString();
                ielementID = Convert.ToInt32(elementID);
                break;
            }

            // Find element in project browser

            if (ielementID == 0)
                return;

            var objElement = new object();
            objElement = AddInRepository.Instance.Repository.GetElementByID(ielementID);

            try
            {
                if (objElement != null)
                {
                    AddInRepository.Instance.Repository.ShowInProjectView(objElement);
                }
                else
                {
                    MessageBox.Show("Element not found in EA.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPlaceInDiagram_Click(object sender, EventArgs e)
        {
            // Get selected row
            string elementID = "";
            int ielementID = 0;

            foreach (DataGridViewRow dgvr in dgvRealisation.SelectedRows)
            {
                elementID = dgvr.Cells[DTColumnNames.ElementAssociationsScreen.ElementID].Value.ToString();
                ielementID = Convert.ToInt32(elementID);

                if (ielementID > 0)
                {
                    EaAccess.placeElementInDiagram(ielementID);
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            elementSourceDataTable.Clear();
            txtFilter.Text = "";

            // Get selected element

            Element element = EaAccess.GetSelectedElement();

            if (element == null)
                return;

            txtFilter.Text = element.Name;

            // Get the rules
            //
            var eainst = new EaAccess();

            var listOfRelatedItems =
                eainst.getLinkedElementsSQL(element,
                                            "",
                                            "",
                                            true,
                                            true,
                                            new List<string>(),
                                            null, null,
                                            0);


            foreach (object item in listOfRelatedItems)
            {
                var connList = new EaAccess.ConnectorList();
                connList = (EaAccess.ConnectorList) item;

                DataRow elementRow = elementSourceDataTable.NewRow();

                elementRow[DTColumnNames.ElementAssociationsScreen.Element] = connList.ObjectName;
                elementRow[DTColumnNames.ElementAssociationsScreen.RelatedTo] = " ";
                elementRow[DTColumnNames.ElementAssociationsScreen.Notes] = connList.ObjectNote;
                elementRow[DTColumnNames.ElementAssociationsScreen.ElementID] = connList.ObjectElementId;

                elementSourceDataTable.Rows.Add(elementRow);
            }
        }


        // ----------------------------------------------
        //    List Related Elements (DEPRECATED)
        // ----------------------------------------------
        private void listElementsRelated(Package packageSelected)
        {
            if (packageSelected == null)
                return;

            var eainst = new EaAccess();

            ArrayList listOfRelatedItems =
                eainst.getElementsFromPackage(packageSelected);

            elementSourceDataTable.Clear();
            txtFilter.Text = "";

            foreach (object item in listOfRelatedItems)
            {
                var britem = new EaAccess.brItem();
                britem = (EaAccess.brItem) item;

                DataRow elementRow = elementSourceDataTable.NewRow();

                elementRow[DTColumnNames.ElementAssociationsScreen.Element] = britem.name;
                elementRow[DTColumnNames.ElementAssociationsScreen.Notes] = britem.BRStatus;
                elementRow[DTColumnNames.ElementAssociationsScreen.RelatedTo] = britem.linkNames;
                elementRow[DTColumnNames.ElementAssociationsScreen.Notes] = britem.brnotes;
                elementRow[DTColumnNames.ElementAssociationsScreen.ElementID] = britem.elementID;

                elementSourceDataTable.Rows.Add(elementRow);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BusinessRulesRealisation_Load(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void locateInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {


            foreach (DataGridViewRow dr in dgvRealisation.SelectedRows)
            {
                int elementID = Convert.ToInt32(dr.Cells[DTColumnNames.ElementAssociationsScreen.ElementID].Value);

                var objElement = new object();
                objElement = AddInRepository.Instance.Repository.GetElementByID(elementID);

                // Find element in project browser

                try
                {
                    if (objElement != null)
                    {
                        AddInRepository.Instance.Repository.ShowInProjectView(objElement);
                    }
                    else
                    {
                        MessageBox.Show("Element not found in EA.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                break;

            }
        }

        private void moveToTBDPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AddInRepository.Instance.InitialiseEADeleteResults();

            Cursor.Current = Cursors.WaitCursor;
            Package currentPackage = null;
            Package package = null;

            // Create package
            //
            foreach (DataGridViewRow dr in dgvRealisation.SelectedRows)
            {
                int elementID = Convert.ToInt32(dr.Cells[DTColumnNames.ElementAssociationsScreen.ElementID].Value);
                Element element = AddInRepository.Instance.Repository.GetElementByID(elementID);

                currentPackage =
                    AddInRepository.Instance.Repository.GetPackageByID(element.PackageID);

                if (currentPackage.PackageID > 0)
                {
                    package = EaCaliberGenEngine.CreateEaPackage(
                        currentPackage, "TBD");
                }
                break;

            }


            if (package == null)
                return;

            foreach (DataGridViewRow dr in dgvRealisation.SelectedRows)
            {
                int elementID = Convert.ToInt32(dr.Cells[DTColumnNames.ElementAssociationsScreen.ElementID].Value);

                Element element = AddInRepository.Instance.Repository.GetElementByID(elementID);
                element.PackageID = package.PackageID;
                element.Update();
                element.Refresh();

                string results = 
                    "Moved to TBD folder " + element.ElementID + " " + element.Name + " " + element.Stereotype;
                AddInRepository.Instance.WriteEADeleteResults(results, 0);

            }
            package.Elements.Refresh();

            Cursor.Current = Cursors.Arrow;

        }
    }
}
