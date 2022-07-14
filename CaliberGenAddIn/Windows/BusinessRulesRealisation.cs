using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EA;
using EAStructures;

namespace EAAddIn.Windows
{
    public partial class BusinessRulesRealisation : Form
    {
        public EaCaliberGenEngine EAEngine;
        public DataTable elementSourceDataTable;
        public string includeSubPackages;
        public string ignoreDeletedRules;
        public SecurityInfo secinfo;
        public ArrayList visitedArray;

        public BusinessRulesRealisation()
        {
            InitializeComponent();

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            EAEngine = new EaCaliberGenEngine(secinfo, "caliber");

            var packageName = new DataColumn("packageName", typeof (String));
            var businessRule = new DataColumn("businessRule", typeof (String));
            var BRStatus = new DataColumn("BRStatus", typeof (String));
            var linkedTo = new DataColumn("linkedTo", typeof (String));
            var placeddiagrams = new DataColumn("placeddiagrams", typeof (String));
            var brnotes = new DataColumn("brnotes", typeof (String));
            var elementID = new DataColumn("elementID", typeof (String));
            var author = new DataColumn("author", typeof(String));
            var ea_guid = new DataColumn("ea_guid", typeof(String));
            var stereotype = new DataColumn("stereotype", typeof(String));
            var createdDate = new DataColumn("createdDate", typeof(String));

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
                listElementsRelatedDT(packageSelected, cbxType.Text, cbxConnectedTo.Text,
                                      includeSubPackages, ignoreDeletedRules);
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
                elementID = dgvr.Cells["ElementID"].Value.ToString();
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
                elementID = dgvr.Cells["ElementID"].Value.ToString();
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

                elementRow["businessRule"] = connList.ObjectName;
                elementRow["linkedTo"] = " ";
                elementRow["brnotes"] = connList.ObjectNote;
                elementRow["elementID"] = connList.ObjectElementId;

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

                elementRow["businessRule"] = britem.name;
                elementRow["BRStatus"] = britem.BRStatus;
                elementRow["linkedTo"] = britem.linkNames;
                elementRow["brnotes"] = britem.brnotes;
                elementRow["elementID"] = britem.elementID;

                elementSourceDataTable.Rows.Add(elementRow);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BusinessRulesRealisation_Load(object sender, EventArgs e)
        {

        }
    }
}