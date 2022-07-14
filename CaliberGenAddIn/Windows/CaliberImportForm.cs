using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EA;
using EAAddIn.Properties;
using EAStructures;
using Starbase.CaliberRM.Interop;
using IProject=Starbase.CaliberRM.Interop.IProject;
using IRequirement=Starbase.CaliberRM.Interop.IRequirement;
using starbase = Starbase.CaliberRM.Interop;
using TreeNode=System.Windows.Forms.TreeNode;

namespace EAAddIn.Windows
{
    public partial class CaliberImportForm : Form
    {
        public const string EAREP1ID = "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";
        public const string EAREP2ID = "initial catalog=EA_Release2;data source=DRPSQL007\\SQL07";
        public const string EASECDB = "initial catalog=EA_Secure_DB;data source=DRPSQL007\\SQL07";
        public const string EADMLOCALDB = "initial catalog=EA_LOCAL;data source=FNOA0189\\SQL2005";

        private static bool isShown;
        public EaCaliberGenEngine CaliberEA;
        public IProject caliberproject;
        public SecurityInfo secinfo;
        private SelectRequirements selectRequirements;

        public CaliberImportForm()
        {
            InitializeComponent();

            //CurrentRepository = rc;

            var dbcon = new dbConnections();

            if (ConnectedToRelease1)
                secinfo.EARepository = dbcon.CSEARepository;
            else
                secinfo.EARepository = dbcon.CSSecureEARepository;

            secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;

            CaliberEA = new EaCaliberGenEngine(secinfo, "caliber");
            loginToCaliber();
            isShown = true;
        }


        public static bool IsShown
        {
            get { return isShown; }
        }

        public bool ConnectedToRelease1
        {
            get { return AddInRepository.Instance.Repository.ConnectionString.ToUpper().Contains(EAREP1ID.ToUpper()); }
        }

        //bool to check, if user is using different database
        public bool ConnectedToSecureDatabase
        {
            get { return AddInRepository.Instance.Repository.ConnectionString.ToUpper().Contains(EASECDB.ToUpper()); }
        }

        //
        // Login to Caliber
        //
        private void loginToCaliberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginToCaliber();
        }

        //
        // Login to Caliber
        //
        private void loginToCaliber()
        {
            string server = "EPMGT035";
            string username = "";

            Login login;

            login = new Login(server, username);

            if (login.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            // Add list of projects to the combo boxes
                var sourceProjectList = new List<IProject>();
            try
            {
                foreach (IProject project in SessionManager.Object.Session.Projects)
                {
                    sourceProjectList.Add(project);
                }
            }
            catch (Exception exception)
            {
                var message =
                    "An unknown error has occurred.  Please send a screenshot of this message to ES - Solution Design." +
                    Environment.NewLine + Environment.NewLine +
                    exception.Message;

                if (exception.InnerException != null)
                {
                    message += Environment.NewLine + Environment.NewLine +
                               "Inner exception: " +
                               exception.InnerException.Message;
                }
                MessageBox.Show( message, "Caliber Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Sort the list for display
            sourceProjectList.Sort(CaliberUtils.CompareProjects);

            cmbProjectSelect.DataSource = sourceProjectList;
            cmbProjectSelect.DisplayMember = "Name";

            if (!String.IsNullOrEmpty(Settings.Default.LastCaliberProject))
                cmbProjectSelect.Text = Settings.Default.LastCaliberProject;
        }

        //
        // Try to fix the imports
        //
        private void fixAll()
        {
            // Determine (1)
            CaliberEA.DetermineElementTypeForList();

            // Fix data (2)
            CaliberEA.FixCaliberInput();

            // Verify integrity (3)
            CaliberEA.VerifyElementIntegrity();

            // Display tree (4)
            CaliberEA.DisplayEaTreePreview(treeViewEAAddIn);

            // Check again
            if (CaliberEA.VerifyElementIntegrity())
            {
            }
            else
            {
                var el = new DataSourceViewer(CaliberEA.dtMessageLog.Messages);
                Cursor.Current = Cursors.Arrow;
                el.ShowDialog();
            }
            ;
        }


        private void cmbProjectSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //disable EA load button

            //clear tree view if nodes exist before loading next ones
            selectRequirements = null;
            treeViewCaliber.Nodes.Clear();

            //load the Caliber Requirement View once a new project has been selected
            Cursor.Current = Cursors.WaitCursor;
            if (selectRequirements == null)
            {
                caliberproject = (IProject) cmbProjectSelect.SelectedItem;
                selectRequirements =
                    new SelectRequirements(caliberproject, this, SortTreeView);
            }

            Cursor.Current = Cursors.WaitCursor;
            btnSync.Enabled = false;
        }

        private bool SortTreeView
        {
            get { return sortCaliberTreeToolStripMenuItem.Checked; }
        }


        private void treeViewCaliber_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                //if (e.Node.Nodes.Count > 0)
                //{
                CheckChildren(e.Node, e.Node.Checked);
                // }
            }
        }

        private void CheckChildren(TreeNode node, bool checkstate)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = checkstate;
                // if(child.Nodes.Count >0 )
                //{ 
                CheckChildren(child, checkstate);

                //}
            }
        }

        private void btnLoadCaliber_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (CaliberEA != null)
                CaliberEA.elementSourceDataTable.Clear();
            else
            {
                //CaliberEA = new EaCaliberGenEngine(Secinfo, "caliber");
                //CaliberEAGridView.DataSource = CaliberEA.ElementSourceDataTable;
                MessageBox.Show("Please connect to EA before you continue");
                return;
            }

            var selector = new SelectorSerialTag();

            foreach (TreeNode node in treeViewCaliber.Nodes)
            {
                selectRequirements.AddTreeNodeToSelector(node, selector, CaliberEA.elementSourceDataTable);
            }

            if (SelectRequirements.isBranched)
            {
                MessageBox.Show(
                    "This project has been branched in Caliber. " + Environment.NewLine + Environment.NewLine +

                    "Please ensure the original destination in the 'Master Functional Areas' in EA is used (not the 'Project Releases').",
                    "Caliber Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (SelectRequirements.isMapped)
            {
                MessageBox.Show(
                    "This project includes mapped items in Caliber. Mapped items will not be included in the datatable.");
            }

            if (!SelectRequirements.screenselected)
            {
                MessageBox.Show(
                    "You are about to load the UI Objects. No screen has been selected. Please select a screen.");
            }

            dataGridViewTableForAddIn.DataSource = CaliberEA.elementSourceDataTable;

            // Display tree (4)
            CaliberEA.DisplayEaTreePreview(treeViewEAAddIn);

            CaliberEA.MappingCaliberloadEaGuid();
            fixAll();

            Cursor.Current = Cursors.Arrow;

            //enable synchronise button between caliber and ea
            if (ConnectedToRelease1)
                btnSync.Enabled = true;


            if (ConnectedToSecureDatabase)
            {
                if (cmbProjectSelect.Text.Contains("0809.0235.01") || cmbProjectSelect.Text.Contains("0910.0300.01"))
                    btnSync.Enabled = true;
                else
                    btnSync.Enabled = false;
            }

            //Reset flags
            SelectRequirements.isBranched = false;
            SelectRequirements.screenselected = false;
            SelectRequirements.isMapped = false;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            //
            // If Model selected is not Relase 1 an error message will be displayed
            //
            //if (!ConnectedToRelease1) 
            //{
            //    MessageBox.Show("The repository selected is not Release 1. \n" +
            //               "The Caliber EA mapping links won't be created.");
            //}

            // Get selected packages
            //
            Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (p == null || p.PackageGUID == "" || p.PackageGUID == null)
            {
                MessageBox.Show("Please select an EA package to sync.");
                return;
            }

            CaliberEA.EADestinationPackage = p.PackageGUID;

            if (p == null)
            {
                MessageBox.Show("Package has not been selected." + Environment.NewLine +
                                "Please select a package in EA to load the xml.");
                return;
            }

            DialogResult response = MessageBox.Show("Are you sure?", "Sync with EA", MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

            if (response == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;

                // sort the datatable by hierarchy
                CaliberEA.elementSourceDataTable.DefaultView.Sort =
                    CaliberEA.elementSourceDataTable.Columns["CaliberHierarchy"] + " asc";


                // Disable UI updates
                AddInRepository.Instance.Repository.EnableUIUpdates = false;

                // Create output tab
                //
                AddInRepository.Instance.InitialiseCaliberResults();

                // Syncronise
                string msg = CaliberEA.CaliberEaSync();
                MessageBox.Show(msg);

                // Reload GUID's
                CaliberEA.MappingCaliberloadEaGuid();

                // Refresh UI
                AddInRepository.Instance.Repository.EnableUIUpdates = true;

                Package package = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
                AddInRepository.Instance.Repository.RefreshModelView(package.PackageID);

                Cursor.Current = Cursors.Arrow;
            }
        }

        // -----------------------------------------------------------
        //                          Unlink element
        // -----------------------------------------------------------
        private void unlinkElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unlink();
        }

        private void linkElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            link();
        }

        private void linkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            link();
        }

        private void unlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unlink();
        }


        // -----------------------------------------------
        //     Link elements
        // -----------------------------------------------
        private void link()
        {
            //
            // If Model selected is not Relase 1 or the secure database an error message will be displayed
            //
            if (ConnectedToRelease1 || ConnectedToSecureDatabase)
            {
                // Ok to update.
            }
            else
            {
                MessageBox.Show("The repository selected is not Release 1. \n" +
                                "The updates cannot be performed.");
                return;
            }

            Element element;

            // Get selected packages
            //
            try
            {
                element = (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch (Exception)
            {
                MessageBox.Show("Element select in EA is not valid to link.");
                return;
            }


            // Get Caliber Information
            //
            int caliberID = 0;
            CaliberObjectID caliberObject;

            // Check mapping by Caliber ID
            //
            TreeNode tnCaliber = treeViewCaliber.SelectedNode;
            if (tnCaliber == null)
            {
                MessageBox.Show("Caliber Element is not selected.");
                return;
            }

            if (tnCaliber != null && element != null)
            {
                // The node is selected in the Caliber View
                //
                caliberObject = (CaliberObjectID) tnCaliber.Tag; // Cast to 

                if (caliberObject != null)
                {
                    caliberID = caliberObject.IDNumber;
                }

                // Retrieve Caliber Info - Source ID if necessary
                //
                var ir =
                    (IRequirement) caliberproject.Session.get((CaliberObjectID) tnCaliber.Tag);

                var exReq = new ExtendedRequirement(ir);
                if (exReq.SourceRequirementID > 0)
                {
                    caliberID = exReq.SourceRequirementID;
                }


                //
                // Link Selected elements
                //
                var eaInfo = new mtCaliberMapping();
                eaInfo.CaliberID = caliberID;
                eaInfo.CaliberName = tnCaliber.Text;
                eaInfo.EA_GUID = element.ElementGUID;
                eaInfo.EAElementType = element.Type;
                eaInfo.CaliberHierarchy = " ";
                eaInfo.EAElementID = element.ElementID;
                eaInfo.EAParentGUID = " ";
                eaInfo.CaliberFullDescription = tnCaliber.Text;

                // Check if Caliber element is already linked
                //
                var checkMapping = new mtCaliberMapping();
                checkMapping.getDetails(eaInfo.CaliberID);
                //checkMapping.getDetails(eaInfo.CaliberID, EaCaliberGenEngine.MappingTableConnection);

                if (checkMapping.EA_GUID == null)
                {
                    // Check if EA element is already linked
                    checkMapping.getDetails(eaInfo.EA_GUID);
                    if (checkMapping.EA_GUID == null)
                    {
                        string answer =
                            MessageBox.Show("Adding EA Link - Are you sure?", "Warning", MessageBoxButtons.YesNo).
                                ToString();

                        if (answer == "Yes")
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            string msg = eaInfo.add();
                            MessageBox.Show(msg);
                            Cursor.Current = Cursors.Arrow;
                        }
                    }
                    else
                    {
                        MessageBox.Show("EA Element already linked to \n" +
                                        " Caliber ID: " +
                                        checkMapping.CaliberID);
                    }
                }
                else
                {
                    MessageBox.Show("Caliber Element already linked.");
                }
            }
        }

        // -----------------------------------------
        //      Unlink
        // -----------------------------------------
        private void unlink()
        {
            //
            // If Model selected is not Relase 1 an error message will be displayed
            //
            if (!ConnectedToRelease1)
            {
                MessageBox.Show("The repository selected is not Release 1. \n" +
                                "The updates cannot be performed.");
                return;
            }


            // Get Caliber Information
            //
            int caliberID = 0;
            CaliberObjectID caliberObject;

            // Check mapping by Caliber ID
            //
            TreeNode tnCaliber = treeViewCaliber.SelectedNode;

            if (tnCaliber != null)
            {
                // The node is selected in the Caliber View
                //
                caliberObject = (CaliberObjectID) tnCaliber.Tag; // Cast to 

                if (caliberObject != null)
                {
                    caliberID = caliberObject.IDNumber;
                }

                // Retrieve Caliber Info - Source ID if necessary
                //
                var ir =
                    (IRequirement) caliberproject.Session.get((CaliberObjectID) tnCaliber.Tag);

                var exReq = new ExtendedRequirement(ir);
                if (exReq.SourceRequirementID > 0)
                {
                    caliberID = exReq.SourceRequirementID;
                }

                //
                // Unlink Selected elements
                //
                var eaInfo = new mtCaliberMapping();
                eaInfo.CaliberID = caliberID;
                eaInfo.CaliberName = tnCaliber.Text;
                eaInfo.EAParentGUID = " ";
                eaInfo.CaliberFullDescription = tnCaliber.Text;


                // Check if element is not linked
                //
                var checkMapping = new mtCaliberMapping();
                checkMapping.getDetails(eaInfo.CaliberID);

                if (checkMapping.EA_GUID != null)
                {
                    string answer =
                        MessageBox.Show("Removing EA Link - Are you sure?", "Warning", MessageBoxButtons.YesNo).ToString
                            ();

                    if (answer == "Yes")
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        string msg = eaInfo.delete(EaCaliberGenEngine.MappingTableConnection);
                        MessageBox.Show(msg);
                        Cursor.Current = Cursors.Arrow;
                    }
                }
                else
                {
                    MessageBox.Show("Element is not linked.");
                }
            }
        }

        //
        // Loading form
        //
        private void CaliberImportForm_Load(object sender, EventArgs e)
        {
            if (!ConnectedToRelease1 && !ConnectedToSecureDatabase)
            {
                Text = "TRAINING VERSION - TEST ONLY - " + Text;
            }
            else if (ConnectedToSecureDatabase)
            {
                Text = "Secure DB connection - TMS ONLY - " + Text;
            }

        }

        private void locateInEAProjectBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get Caliber Information
            //
            int caliberID = 0;
            CaliberObjectID caliberObject;

            // Check mapping by Caliber ID
            //
            TreeNode tnCaliber = treeViewCaliber.SelectedNode;
            if (tnCaliber == null)
            {
                MessageBox.Show("Caliber Element is not selected.");
                return;
            }

            // The node is selected in the Caliber View
            //
            caliberObject = (CaliberObjectID) tnCaliber.Tag; // Cast to 

            if (caliberObject != null)
            {
                caliberID = caliberObject.IDNumber;
            }

            // Retrieve Caliber Info
            if (tnCaliber.Tag == null)
                return;

            var ir = (IRequirement) caliberproject.Session.get((CaliberObjectID) tnCaliber.Tag);

            var exReq = new ExtendedRequirement(ir);
            if (exReq.SourceRequirementID > 0)
            {
                caliberID = exReq.SourceRequirementID;
            }

            //
            // Find element in Mapping table
            //
            var eaInfo = new mtCaliberMapping();
            eaInfo.CaliberID = caliberID;
            eaInfo.CaliberName = tnCaliber.Text;
            eaInfo.EAParentGUID = " ";
            eaInfo.CaliberFullDescription = tnCaliber.Text;

            var checkMapping = new mtCaliberMapping();
            checkMapping.getDetails(eaInfo.CaliberID);

            if (checkMapping != null && checkMapping.EA_GUID != null)
            {
                try
                {
                    // Find element in project browser
                    var o = new object();

                    if (o != null)
                    {
                        o = AddInRepository.Instance.Repository.GetElementByGuid(checkMapping.EA_GUID);
                    }

                    if (o != null)
                    {
                        AddInRepository.Instance.Repository.ShowInProjectView(o);
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
            else
            {
                MessageBox.Show("Element not found.");
            }
        }

        private void treeViewCaliber_AfterSelect(object sender, TreeViewEventArgs e)
        {
            findRow();
        }

        private void findRow()
        {
            // Get Caliber Information
            //
            int caliberID = 0;
            CaliberObjectID caliberObject;

            // Check mapping by Caliber ID
            //
            TreeNode tnCaliber = treeViewCaliber.SelectedNode;
            if (tnCaliber == null)
            {
                MessageBox.Show("Caliber Element is not selected.");
                return;
            }

            // The node is selected in the Caliber View
            //
            caliberObject = (CaliberObjectID) tnCaliber.Tag; // Cast to 

            if (caliberObject != null)
            {
                caliberID = caliberObject.IDNumber;
            }

            if (caliberID <= 0)
            {
                return;
            }

            // Retrieve Caliber Info
            try
            {
                var ir =
                    (IRequirement) caliberproject.Session.get((CaliberObjectID) tnCaliber.Tag);

                var exReq = new ExtendedRequirement(ir);
                if (exReq.SourceRequirementID > 0)
                {
                    caliberID = exReq.SourceRequirementID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            // Position line in datatable
            // CaliberEA.ElementSourceDataTable.Rows.Find(caliberID);

            DataRow dr = CaliberEA.elementSourceDataTable.Rows.Find(caliberID);
            int cr = CaliberEA.elementSourceDataTable.Rows.IndexOf(dr);

            if (cr > 0)
            {
                // int cr = dataGridViewTableForAddIn.CurrentRow.Index;

                dataGridViewTableForAddIn.CurrentCell =
                    dataGridViewTableForAddIn.Rows[cr].Cells[0];

                dataGridViewTableForAddIn.FirstDisplayedCell =
                    dataGridViewTableForAddIn.CurrentCell;

                // still need to position the row...
            }
        }

        private void CaliberImportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.LastCaliberProject = cmbProjectSelect.Text;
            Settings.Default.Save();
        }

    }
}
