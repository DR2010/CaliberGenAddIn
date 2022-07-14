using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using EAAddIn.Applications;
using EAStructures;

namespace EAAddIn.Windows
{
    public partial class MappingTableMaintenance : Form
    {
        public static SqlConnection EAConnection;
        public static SqlConnection MappingTableConnection;
        private readonly SecurityInfo secInfo;
        public DataTable elementSourceDataTable;

        public MappingTableMaintenance(SecurityInfo sec)

        {
            InitializeComponent();
            secInfo = sec;

            //MappingTableConnection.Open();

            EAConnection = SqlHelpers.EaRelease1DbConnection;
            //EAConnection.Open();
        }

        private void btnGetOrfanRows_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var tm = new mtTableMapping();
            int i = tm.getOrfanRowsCount(MappingTableConnection);

            Cursor.Current = Cursors.Arrow;
        }

        private void btnElementInfoGet_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var eaa = new EaAccess();
            Object selectedObject = AddInRepository.Instance.Repository.GetTreeSelectedObject();
            EaAccess.sEAElement EAElem;

            EAElem = eaa.GetEaElement(MappingTableConnection, selectedObject);

            Cursor.Current = Cursors.Arrow;
        }

        // ----------------------------------------------------
        //              Row selected
        // ----------------------------------------------------
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string tableName = "";
            string EA_GUID = "";
            string AlternateName = "";

            foreach (DataGridViewRow dgvr in dgvTableList.SelectedRows)
            {
                tableName = dgvr.Cells["tableName"].Value.ToString();
                EA_GUID = dgvr.Cells["EA_GUID"].Value.ToString();
                AlternateName = dgvr.Cells["AlternateName"].Value.ToString();
                break;
            }

            txtPhysicalName.Text = tableName.Trim();
            txtLogicalName.Text = AlternateName.Trim();
            txtEAGUID.Text = EA_GUID;

            txtTableName.Text = tableName.Trim();
            txtTableStatus.Text = "";

            locateInEABrowser(txtEAGUID.Text);

            txtPhysicalName.ReadOnly = true;

            retrieveEACurrentElement();
        }

        // ----------------------------------------------------
        //         Get selected element from EA
        // ----------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            retrieveEACurrentElement();
            Cursor.Current = Cursors.Arrow;
        }

        // ----------------------------------------------------
        //          Reload List of Tables
        // ----------------------------------------------------
        private void btnReloadList_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            reloadTableList();
            Cursor.Current = Cursors.Arrow;
        }

        // ----------------------------------------------------
        // Activate Event
        // ----------------------------------------------------
        private void MappingTableMaintenance_Activated(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            retrieveEACurrentElement();
            Cursor.Current = Cursors.Arrow;
        }

        // ----------------------------------------------------
        // Retrieve details of the current element
        // ----------------------------------------------------
        private void retrieveEACurrentElement()
        {
            // 1) Retrieve selected element
            var eaa = new EaAccess();
            Object selectedObject = AddInRepository.Instance.Repository.GetTreeSelectedObject();
            EaAccess.sEAElement EAElem;

            EAElem = eaa.GetEaElement(EAConnection, selectedObject);

            if (EAElem.Type != "Class")
                return;

            if (EAElem.Stereotype != "table")
                return;

            txtEAGUID.Text = EAElem.EA_GUID;
            txtTableName.Text = EAElem.Name;
            txtTableStatus.Text = EAElem.Status;

            // 2) Retrieve information from table
            var tm = new mtTableMapping();
            tm.EA_GUID = EAElem.EA_GUID;

            tm.getTableNamebyGUID(MappingTableConnection);

            txtLogicalName.Text = tm.alternateName.Trim();
            txtPhysicalName.Text = tm.tableName.Trim();

            if (tm.tableName == null || tm.tableName == "")
            {
                txtPhysicalName.ReadOnly = false;
            }

            btnDelete.Enabled = true;
            if (txtPhysicalName.Text == "")
            {
                btnDelete.Enabled = false;
            }
        }

        // ----------------------------------------------------
        //            Update
        // ----------------------------------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update table details
            // AlternateName = GEN Logical Name
            // TableName     = DB2 Physical Name

            if (txtEAGUID.Text == "")
            {
                MessageBox.Show("EA Element not selected.");
                return;
            }

            if (txtPhysicalName.Text == "")
            {
                MessageBox.Show("Physical Name must be entered (DB2 name).");
                return;
            }

            var tableDetails = new mtTableMapping();
            tableDetails.EA_GUID = txtEAGUID.Text;
            tableDetails.alternateName = txtLogicalName.Text;
            tableDetails.tableName = txtPhysicalName.Text;

            string ans = (MessageBox.Show("Are you sure? ",
                                          "Save Table Link", MessageBoxButtons.YesNo).ToString());
            if (ans == "Yes")
            {
                Cursor.Current = Cursors.WaitCursor;

                string ret = tableDetails.save(MappingTableConnection);

                // Reload Table
                reloadTableList();

                MessageBox.Show(ret);

                Cursor.Current = Cursors.Arrow;
            }
        }

        // --------------------------------------------
        // Reload table list
        // --------------------------------------------

        private void reloadTableList()
        {
            // Reload Table
            var tml = new mtTableMappingList();

            tml.getTableList(MappingTableConnection);
            dgvTableList.DataSource = tml.tableMapping;
        }

        // --------------------------------------------
        //  Locate in project browser
        // --------------------------------------------
        private void locateInEABrowser(string EA_GUID)
        {
            var tableInfo = new mtTableMapping();

            // Find element in project browser
            var objectElement = new object();

            try
            {
                objectElement = AddInRepository.Instance.Repository.GetElementByGuid(EA_GUID);
                if (objectElement != null)
                {
                    AddInRepository.Instance.Repository.ShowInProjectView(objectElement);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //MappingTableConnection.Close();
            //EAConnection.Close();

            Close();
        }

        private void MappingTableMaintenance_Load(object sender, EventArgs e)
        {
            var tml = new mtTableMappingList();

            tml.getTableList(MappingTableConnection);
            dgvTableList.DataSource = tml.tableMapping;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Update table details
            // AlternateName = GEN Logical Name
            // TableName     = DB2 Physical Name

            if (txtEAGUID.Text == "")
            {
                MessageBox.Show("EA Element not selected.");
                return;
            }

            if (txtPhysicalName.Text == "")
            {
                MessageBox.Show("Physical Name must be entered (DB2 name).");
                return;
            }

            var tableDetails = new mtTableMapping();
            tableDetails.EA_GUID = txtEAGUID.Text;
            tableDetails.alternateName = txtLogicalName.Text;

            string ans = (MessageBox.Show("Are you sure? ",
                                          "Delete Link", MessageBoxButtons.YesNo).ToString());
            if (ans == "Yes")
            {
                Cursor.Current = Cursors.WaitCursor;

                tableDetails.tableName = txtPhysicalName.Text;
                string ret = tableDetails.delete(MappingTableConnection);
                reloadTableList();

                MessageBox.Show(ret);

                Cursor.Current = Cursors.Arrow;
            }
        }
    }
}