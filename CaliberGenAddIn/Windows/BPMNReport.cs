using System;
using System.Data;
using System.Windows.Forms;
using EA;
using EAStructures;

namespace EAAddIn.Windows
{
    public partial class BPMNReport : Form
    {
        public const string EAREP1ID = "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";

        public DataTable dtBPMNUsers;
        public DataTable dtPackageList;
        public string initialLoadPackageGUID;
        public SecurityInfo secinfo;

        public BPMNReport()
        {
            InitializeComponent();

            //CurrentRepository = rc;

            //var dbcon = new dbConnections();

            // secinfo.EARepository = dbcon.CSEARepository;

            //secinfo.EARepository = AddInRepository.Instance.ConnectionStringshort;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            // Get selected packages
            Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            secinfo.packageDestination = p.PackageGUID;

            // Data table Package List
            dtPackageList = new DataTable();
            var PackageName = new DataColumn("PackageName", typeof (String));
            var DiagramName = new DataColumn("DiagramName", typeof (String));
            var DiagramVersion = new DataColumn("DiagramVersion", typeof (String));
            var DiagramStereotype = new DataColumn("DiagramStereotype", typeof (String));
            var DiagramNotes = new DataColumn("DiagramNotes", typeof (String));
            var DiagramModifiedDate = new DataColumn("DiagramModifiedDate", typeof (String));
            var PackageEAGUID = new DataColumn("PackageEAGUID", typeof (String));
            var DiagramParentID = new DataColumn("DiagramParentID", typeof (String));
            var DiagramPackageID = new DataColumn("DiagramPackageID", typeof (String));

            dtPackageList.Columns.Add(PackageName);
            dtPackageList.Columns.Add(DiagramName);
            dtPackageList.Columns.Add(DiagramVersion);
            dtPackageList.Columns.Add(DiagramStereotype);
            dtPackageList.Columns.Add(DiagramNotes);
            dtPackageList.Columns.Add(DiagramModifiedDate);
            dtPackageList.Columns.Add(PackageEAGUID);
            dtPackageList.Columns.Add(DiagramParentID);
            dtPackageList.Columns.Add(DiagramPackageID);

            // Data table BPMN Users
            dtBPMNUsers = new DataTable();
            var UserLogin = new DataColumn("UserLogin", typeof (String));
            var FirstName = new DataColumn("FirstName", typeof (String));
            var Surname = new DataColumn("Surname", typeof (String));
            var GroupName = new DataColumn("GroupName", typeof (String));

            dtBPMNUsers.Columns.Add(UserLogin);
            dtBPMNUsers.Columns.Add(FirstName);
            dtBPMNUsers.Columns.Add(Surname);
            dtBPMNUsers.Columns.Add(GroupName);
        }

        private void btnShowBPMNUsers_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Get selected packages
            Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            secinfo.packageDestination = p.PackageGUID;

            // Show User list in BPMNdataGridView
            var eabpmn = new EABPMN();
            eabpmn.BPMNUserList(dtBPMNUsers);
            BPMNdataGridView.DataSource = dtBPMNUsers;

            Cursor.Current = Cursors.Arrow;
        }

        private void btnDiagramReport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Get selected packages
            Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            var bpmn = new EABPMN();
            bpmn.report(p, dtPackageList);
            BPMNdataGridView.DataSource = dtPackageList;
            Cursor.Current = Cursors.Arrow;
        }
    }
}