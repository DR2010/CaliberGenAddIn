using System;
using System.Data;
using System.Windows.Forms;
using EA;
using EAStructures;
using File=System.IO.File;

namespace EAAddIn.Windows
{
    public partial class WrapperFileSelection : Form
    {
        public const string EAREP1ID = "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";

        public string caliberCSVfile;
        public DataTable dtcablist;
        public EaCaliberGenEngine EAGen;
        public string initialLoadPackageGUID;
        public SecurityInfo secinfo;
        public DataTable UIdt;
        public string wrapperPathName;

        public WrapperFileSelection()
        {
            InitializeComponent();
            //CurrentRepository = rc;

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            // Get selected packages
            Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            secinfo.packageDestination = p.PackageGUID;
        }

        private void WrapperFileSelection_Load(object sender, EventArgs e)
        {
            showWrapperDetails();
        }

        private void btnLoadWrapperIntoEA_Click(object sender, EventArgs e)
        {
            //
            // If Model selected is not Relase 1 an error message will be displayed
            //
            if (!AddInRepository.Instance.Repository.ConnectionString.Contains(EAREP1ID))
            {
                MessageBox.Show("The repository selected is not Release 1. \n" +
                                "You can proceed with the load.\n" +
                                "The mapping table won't be updated.");
            }

            var result = MessageBox.Show("Are you sure you want to import this structure chart XML? "
                + Environment.NewLine + Environment.NewLine
                + "This may take 5 minutes or so, depending on the size of the chart.",
                                          "Import Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
                EAGen.EADestinationPackage = p.PackageGUID;

                // Create output tab
                //
                AddInRepository.Instance.InitialiseGenResults();

                if (EAGen.EADestinationPackage != null)
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = EAGen.elementSourceDataTable.Rows.Count;

                    EAGen.EaCoolGenSync(wrapperPathName, progressBar1);

                    if (EAGen.dtMessageLog.Messages.Where(x => x.Type == "Error").AsDataView().Count == 0)
                    {
                        AddInRepository.Instance.WriteGenResults(
                            "Structure chart loaded successfully!", 1000);
                        AddInRepository.Instance.WriteGenResults(
                            "New CABs loaded:" +
                            EAGen.countNewLoadedCAB, 1001);
                        AddInRepository.Instance.WriteGenResults(
                            "Existing CABs updated:" +
                            EAGen.countAlreadyLoadedCAB, 1001);

                        if (EAGen.dtMessageLog.Messages.Rows.Count > 0)
                        {
                            AddInRepository.Instance.WriteGenResults("Warnings", 0);
                            AddInRepository.Instance.WriteGenResults("========", 0);

                            foreach (MessagesDataSet.MessagesRow row in EAGen.dtMessageLog.Messages.Rows)
                            {
                                AddInRepository.Instance.WriteGenResults("* " + row.Description, 0);
                            }
                            
                        }
                    }
                    else
                    {
                        AddInRepository.Instance.WriteGenResults("Warnings and Errors", 0);
                        AddInRepository.Instance.WriteGenResults("====================", 0);

                        foreach (MessagesDataSet.MessagesRow row in EAGen.dtMessageLog.Messages.Rows)
                        {
                            AddInRepository.Instance.WriteGenResults("*" + row.Type + "- " + row.Description, 0);
                        }
                        AddInRepository.Instance.WriteGenResults(
                            Environment.NewLine + "Some elements haven't been loaded.",
                            2001);
                        AddInRepository.Instance.WriteGenResults(
                            "Please email this screen to the Solution Architects.",
                            2002);
                    }

                    var el = new DataSourceViewer(EAGen.dtMessageLog.Messages);
                    el.GetColumn(0).Width = 80;
                    el.GetColumn(1).Width = 350;
                    el.ShowDialog();
                }
                AddInRepository.Instance.Repository.EnableUIUpdates = true;
                AddInRepository.Instance.Repository.RefreshModelView(p.PackageID);

                Cursor.Current = Cursors.Arrow;
            }
        }

        private void findInProjectBrowserToolStripMenuItem_Click(
            object sender, EventArgs e)
        {
            TreeNode cabSelected = selectXMLTreeView.SelectedNode;
            var cabInfo = new mtCABMapping();
            var tableInfo = new mtTableMapping();

            cabInfo = (mtCABMapping) cabSelected.Tag; // Cast 

            //
            // Find element in Mapping table
            //
            // if (cabInfo.getGuidByCABName(MappingTableConnection))

            if (cabInfo.EAElementType == "Table")
            {
                tableInfo.alternateName = cabInfo.CABName;
                tableInfo.getTableByTaggedValue();

                // Find element in project browser
                var o = new object();

                if (tableInfo != null && tableInfo.EA_GUID != null)
                {
                    try
                    {
                        o = AddInRepository.Instance.Repository.GetElementByGuid(tableInfo.EA_GUID);
                        if (o != null)
                        {
                            AddInRepository.Instance.Repository.ShowInProjectView(o);
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
            else
            {
                cabInfo.getGuidByCABName_EA();
                Element el = AddInRepository.Instance.Repository.GetElementByGuid(cabInfo.EA_GUID);

                if (el != null)
                {
                    var o = new object();
                    if (cabInfo != null && cabInfo.EA_GUID != null)
                    {
                        try
                        {
                            // Find element in project browser
                            o = AddInRepository.Instance.Repository.GetElementByGuid(cabInfo.EA_GUID);

                            if (o != null)
                            {
                                AddInRepository.Instance.Repository.ShowInProjectView(o);
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
            }
        }


        //
        // Load Wrapper
        //
        private void showWrapperDetails()
        {
            // Identify read only actions
            if (!AddInRepository.Instance.Repository.ConnectionString.Contains(EAREP1ID))
            {
                Text = "TRAINING VERSION - TEST ONLY - " + Text;
            }


            // Display the file dialog
            //
            openFileDialog1.Filter = "XML Files (*.xml)|*.xml|All files(*.*)|*.*";
            openFileDialog1.FileName = "*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                Close();
                return;
            }

            // If no file has been selected
            if (openFileDialog1.FileName == "*.xml")
            {
                Close();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            // Initial Load Package GUID for Release1
            initialLoadPackageGUID = "{956B96BC-56C3-4044-AAE9-6C455136014B}";

            UIdt = new DataTable("UIDataTable");

            secinfo.packageDestination = initialLoadPackageGUID;

            // Data table with cab to be implemented list
            dtcablist = new DataTable();
            var EA_GUID = new DataColumn("EA_GUID", typeof (String));
            var CABName = new DataColumn("CABName", typeof (String));
            var Author = new DataColumn("Author", typeof (String));

            dtcablist.Columns.Add(EA_GUID);
            dtcablist.Columns.Add(CABName);
            dtcablist.Columns.Add(Author);

            //
            // Instantiate EA Gen object
            //

            EAGen = new EaCaliberGenEngine(secinfo, "coolgen");
            fileSelectorGridView.DataSource = EAGen.elementSourceDataTable;

            Cursor.Current = Cursors.WaitCursor;

            wrapperPathName = openFileDialog1.FileName;

            if (!File.Exists(wrapperPathName))
            {
                MessageBox.Show("File not found: " + wrapperPathName);
            }
            else
            {
                // Get selected packages
                Package p = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
                secinfo.packageDestination = p.PackageGUID;

                if (p == null)
                {
                    MessageBox.Show("Package has not been selected.\n" +
                                    "Please select a package in EA to load the xml.");
                    return;
                }


                // Reset grid view
                EAGen.ClearElementSourceDataTable();
                selectXMLTreeView.Nodes.Clear();

                // Root
                var cm = new mtCABMapping();
                cm.CAB = "COOLGEN";
                var rootNode = new TreeNode("Coolgen", 1, 0);
                rootNode.Tag = cm;

                selectXMLTreeView.Nodes.Add(rootNode);

                // Load wrappers
                EAGen.LoadWrapper(wrapperPathName, selectXMLTreeView);

                //if (EAGen.ElementSourceDataTable.Rows.Count > 0)
                //{
                //    btnSyncEAwithGen.Enabled = true;
                //}
            }

            // Image list
            //
            var imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.ImageWhite); // image 0 
            imageList.Images.Add(Properties.Resources.ImageCABImplemented); // image 1 
            imageList.Images.Add(Properties.Resources.ImageCABNotFound); // image 2 
            imageList.Images.Add(Properties.Resources.ImageCABProposed); // image 3 
            imageList.Images.Add(Properties.Resources.ImageQuestionMark); // image 4 
            imageList.Images.Add(Properties.Resources.ImageCABUnderDevelopment); // image 5 
            imageList.Images.Add(Properties.Resources.ImageCABChangesRequired); // image 6 
            imageList.Images.Add(Properties.Resources.ImageCABApproved); // image 7 
            imageList.Images.Add(Properties.Resources.ImageCABToBeReviewed); // image 8
            imageList.Images.Add(Properties.Resources.ImageTable); // image 9

            selectXMLTreeView.ImageList = imageList;

            Cursor.Current = Cursors.Arrow;
        }

        private void btnGetNewWrapper_Click(object sender, EventArgs e)
        {
            showWrapperDetails();
        }

        private void unlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}