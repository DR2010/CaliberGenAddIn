using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications.Reports;
using EAAddIn.Windows.Dialog;

namespace EAAddIn.Windows
{
    public partial class EaReports : Form
    {
        private DataTable elementSourceDataTable;
        private string currentTag;

        readonly DesignSignOff _designSignOff = new DesignSignOff();

        private List<string> _releaseList;
        private List<IDualElement> _projectList;

        private PleaseWaitDialog pleaseWaitDialog = new PleaseWaitDialog();

        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        private Action runReport;


        public EaReports()
        {
            InitializeComponent();

            InitialiseDesignSignOff();
        }

        #region Design Sign off report

        private void InitialiseDesignSignOff()
        {
            dtpManagerFrom.Value = DateTime.Today.AddDays(-7);
            dtpManagerTo.Value = DateTime.Today;

            PopualateReleaseList();
        }

        private void PopualateReleaseList()
        {
            _releaseList = _designSignOff.PopulateReleaseList();
            foreach (var releaseItem in _releaseList)
            {
                cbReleases.Items.Add(releaseItem);
            }
        }

        private void PopulateProjectList()
        {
            if (_projectList != null) _projectList.Clear();

            object selectedItem = cbReleases.SelectedItem;

            if (selectedItem != null)

           // if (!string.IsNullOrEmpty(cbReleases.SelectedItem.ToString()))

            {
                _projectList = _designSignOff.PopulateProjectList(cbReleases.SelectedItem.ToString(),
                                                                  cbIncludeArchivedProjects.Checked);
                cbProjects.DataSource = _projectList;
                cbProjects.DisplayMember = "Name";
            }
        }

        private void btnCreateFinaldDesignSignOff_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _designSignOff.CreateDesignSignOffReport((Element)cbProjects.SelectedItem);
            Cursor.Current = Cursors.Default;
        }

        private void cbReleaes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PopulateProjectList();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        private void MenuDupLoadModule_Click(object sender, EventArgs e)
        {
            currentTag = "Load Module";
            duplicateTaggedTree(currentTag);
        }

        private void duplicateCaliberIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTag = "CaliberID";
            duplicateTaggedTree(currentTag);
        }

        //
        // Duplicate tagged values
        //
        private void duplicateTagged(string tag)
        {
            var tagValue = new DataColumn("tagValue", typeof (String));
            var tagCount = new DataColumn("tagCount", typeof (String));

            elementSourceDataTable = new DataTable("ElementSourceDataTable");

            elementSourceDataTable.Columns.Add(tagValue);
            elementSourceDataTable.Columns.Add(tagCount);

            var duplTagValue = new ArrayList();

            duplTagValue = new EaAccess().GetDuplicateTaggedValues(tag);

            foreach (object obj in duplTagValue)
            {
                var td = (DEEWREAReports.taggedDuplicate) obj;

                DataRow elementRow = elementSourceDataTable.NewRow();
                elementRow["tagValue"] = td.tagValue;
                elementRow["tagCount"] = td.tagCount;

                elementSourceDataTable.Rows.Add(elementRow);
            }

            dgvReportContents.DataSource = elementSourceDataTable;
        }

        //
        // Duplicate tagged values
        //
        private void duplicateTaggedTree(string tag)
        {
            var duplTagValue = new ArrayList();

            duplTagValue = new EaAccess().GetDuplicateTaggedValues(tag);

            var root = new TreeNode();
            root.Text = tag;

            tvReport.Nodes.Clear();
            tvReport.Nodes.Add(root);

            foreach (object obj in duplTagValue)
            {
                var td = (DEEWREAReports.taggedDuplicate) obj;

                var node = new TreeNode();
                node.Text = td.tagValue;
                node.Tag = td;

                root.Nodes.Add(node);

                // get related items
                var duplTagList = new ArrayList();
                duplTagList = new EaAccess().GetDuplicateElementInfoForTagAndValue(tag, td.tagValue);

                foreach (object objTag in duplTagList)
                {
                    var ot = (DEEWREAReports.objectTag) objTag;

                    var subnode = new TreeNode();
                    subnode.Text = "EA Element ID: " + ot.objectID + " > " + ot.objectName;
                    subnode.Tag = ot;

                    node.Nodes.Add(subnode);
                }
            }
        }

        private void locateInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocateInBrowser(sender, e);
        }

       

 



       


        private void duplicateTableTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTag = "GenLogicalName";
            duplicateTaggedTree(currentTag);
        }

        private void tvReport_DoubleClick(object sender, EventArgs e)
        {
            LocateInBrowser(sender, e);
        }

        private void LocateInBrowser(object sender, EventArgs e)
        {
            object objElement = GetSelectedElement();

            if (objElement != null)
            {
                AddInRepository.Instance.Repository.ShowInProjectView(objElement);
            }
            else
            {
                MessageBox.Show("Element not found in EA.");
            }

        }

        // -----------------------------------------------------
        // Delete tag from element
        // -----------------------------------------------------
        private void deleteTaggedValueToolStripMenuItem_Click(object sender, EventArgs e)
        {

            object obj = GetSelectedElement();
            Element el = (Element) obj;

            if (el.ElementID > 0)
            {
                EaAccess.deleteTaggedValue(el, currentTag);
            }

        }

        // -----------------------------------------------------
        // Retrieve element selected in the tree
        // -----------------------------------------------------
        private object GetSelectedElement()
        {

            // Get selected node
            TreeNode tnSelected = tvReport.SelectedNode;

            var objElement = new object();

            // Cast element
            try
            {
                var connList =
                    (DEEWREAReports.objectTag)tnSelected.Tag;

                try
                {
                    //
                    // Find element in project browser
                    //

                    if (connList.objectEAGUID != null)
                    {
                        objElement = AddInRepository.Instance.Repository.GetElementByGuid(connList.objectEAGUID);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception)
            {
            }

            return objElement;
        }

        private void EA_Reports_Load(object sender, EventArgs e)
        {

        }


        private void ProjectTrackingReport(Action report)
        {
            runReport = report;
            // Go to management package
            if (GetDesignStatusPackage() != null)
            {
                pleaseWaitDialog = new PleaseWaitDialog();

                // Start Process 
                Thread lengthyProcessThread = new Thread(new ThreadStart(report));

                // Display the Please Wait Dialog here 
               
                pleaseWaitDialog.RunProcess(lengthyProcessThread);
            }
        }


        public void PrintDetailedManagementReport()
        {
            Package designStatusPackage = GetDesignStatusPackage();

            if (designStatusPackage != null)
            {
                new WordReport().TrafficLightManagementReport(designStatusPackage, dtpManagerFrom.Value, dtpManagerTo.Value, pleaseWaitDialog.UpdateStatus, true);
            }
        }
        public void PrintSingleTrafficLightManagementReport()
        {
            Package designStatusPackage = GetDesignStatusPackage();

            if (designStatusPackage != null)
            {
                new WordReport().TrafficLightManagementReport(designStatusPackage, dtpManagerFrom.Value, dtpManagerTo.Value, pleaseWaitDialog.UpdateStatus, false);
            }
        }



        private Package GetDesignStatusPackage()
        {
            string designstatspk = "{1A15FA82-C947-4203-963C-DC7B19123050}";

            return AddInRepository.Instance.Repository.GetPackageByGuid(designstatspk);
        }

        private void buttonDetailedProjectTrackingReport_Click(object sender, EventArgs e)
        {
            ProjectTrackingReport(PrintDetailedManagementReport);
        }

        private void buttonSingleTrafficLightReport_Click(object sender, EventArgs e)
        {
            ProjectTrackingReport(PrintSingleTrafficLightManagementReport);
        }
    }
}