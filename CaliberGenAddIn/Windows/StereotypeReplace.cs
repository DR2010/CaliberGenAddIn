using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using EA;
using EAStructures;

namespace EAAddIn.Windows
{
    public partial class StereotypeReplace : Form
    {
        //public EA.Repository CurrentRepository;
        public EaCaliberGenEngine EAEngine;
        public DataTable elementSourceDataTable;
        public SecurityInfo secinfo;

        public StereotypeReplace()
        {
            InitializeComponent();

            //CurrentRepository = rc;

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            EAEngine = new EaCaliberGenEngine(secinfo, "caliber");

            var Name = new DataColumn("Name", typeof (String));
            var Type = new DataColumn("Type", typeof (String));
            var ElementID = new DataColumn("ElementID", typeof (String));
            var Stereotype = new DataColumn("Stereotype", typeof(String));
            var Status = new DataColumn("Status", typeof(String));

            elementSourceDataTable = new DataTable("ElementSourceDataTable");

            elementSourceDataTable.Columns.Add(Name);
            elementSourceDataTable.Columns.Add(Type);
            elementSourceDataTable.Columns.Add(ElementID);
            elementSourceDataTable.Columns.Add(Stereotype);
            elementSourceDataTable.Columns.Add(Status);

            dgvElementList.DataSource = elementSourceDataTable;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listElements();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Package package = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            if (package != null)
            {
                string selectedStereotype = "";
                if (cboStereotype.SelectedValue != null)
                {
                    selectedStereotype = cboStereotype.SelectedValue.ToString();
                }

                EaCaliberGenEngine.UpdateEaStereotype(package, selectedStereotype);
                package.Update();
                package.Elements.Refresh();
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void StereotypeReplace_Activated(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            listElements();
            Cursor.Current = Cursors.Arrow;
        }

        private void listElements()
        {
            var elements = new ArrayList();
            elementSourceDataTable.Clear();
            string appliesTo = "";

            Package package = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            if (package != null)
            {
                foreach (Element element in package.Elements)
                {
                    DataRow elementRow = elementSourceDataTable.NewRow();

                    elementRow["Name"] = element.Name;
                    elementRow["Type"] = element.Type;
                    elementRow["ElementID"] = element.ElementID;
                    elementRow["Stereotype"] = element.Stereotype;
                    elementRow["Status"] = element.Status;

                    elementSourceDataTable.Rows.Add(elementRow);

                    appliesTo = element.Type;
                }

                cboAppliesTo.Text = appliesTo;

            }
        }

        private void StereotypeReplace_Load(object sender, EventArgs e)
        {
            var appliesToList = new ArrayList();
            var eaSQL = new EaAccess();
            appliesToList = eaSQL.GetAppliesToList();

            cboAppliesTo.DataSource = appliesToList;

            fillStereotypeList();
            FillStatusList();
        }

        private void cboAppliesTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            fillStereotypeList();
            Cursor.Current = Cursors.Arrow;
        }


        
        private void fillStereotypeList()
        {
            string appliesTo = "Class";
            if (cboAppliesTo.SelectedValue != null)
            {
                appliesTo = cboAppliesTo.SelectedValue.ToString();
            }

            var eaSQL = new EaAccess();
            var stereotypeList = new ArrayList();
            stereotypeList = eaSQL.GetStereotypeList(appliesTo);

            cboStereotype.DataSource = stereotypeList;
        }


        private void FillStatusList()
        {
            var eaSQL = new EaAccess();
            var statusList = new ArrayList();
            statusList = eaSQL.GetStatusList();

            cbxStatusList.DataSource = statusList;
        }

        private void btnStatusReplace_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Package package = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (package != null)
            {
                string selectedStatus = "";
                if (cbxStatusList.SelectedValue != null)
                {
                    selectedStatus = cbxStatusList.SelectedValue.ToString();
                }

                EaCaliberGenEngine.UpdateEAStatus(package, selectedStatus);
                package.Update();
                package.Elements.Refresh();
            }

            listElements();

            Cursor.Current = Cursors.Arrow;

        }

    }
}