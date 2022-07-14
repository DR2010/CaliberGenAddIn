using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications;
using EAStructures;
using File = System.IO.File;


namespace EAAddIn.Windows
{
    public partial class AuditHistory : Form
    {
        public Repository CurrentRepository;
        public EaCaliberGenEngine EaEngine;
        public DataTable ElementSourceDataTable;
        public SecurityInfo Secinfo;
        public ArrayList VisitedArray;
        public List<EaCaliberGenEngine.callCompare> ListOfCabs;

        public AuditHistory( )
        {
            InitializeComponent();
            //CurrentRepository = rc;

            //var dbcon = new dbConnections();
            //Secinfo.EARepository = AddInRepository.Instance.ConnectionStringshort;
            //Secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //Secinfo.MigrateToolDB = dbcon.csMigrateTool;

            EaEngine = new EaCaliberGenEngine(Secinfo, "caliber");

            var genCallType = new DataColumn("genCallType", typeof(String));
            var genCabName = new DataColumn("genCABName", typeof(String));
            var genElementId = new DataColumn("genElementID", typeof(String));
            var eaCallType = new DataColumn("eaCallType", typeof(String));
            var eaCabName = new DataColumn("eaCABName", typeof(String));
            var eaElementId = new DataColumn("eaElementID", typeof(String));
            var eaConnectorId = new DataColumn("eaConnectorID", typeof(String));
            var action = new DataColumn("action", typeof(String));
            
            ElementSourceDataTable = new DataTable("ElementSourceDataTable");

            ElementSourceDataTable.Columns.Add(genCallType);
            ElementSourceDataTable.Columns.Add(genCabName);
            ElementSourceDataTable.Columns.Add(genElementId);
            ElementSourceDataTable.Columns.Add(eaCallType);
            ElementSourceDataTable.Columns.Add(eaCabName);
            ElementSourceDataTable.Columns.Add(eaElementId);
            ElementSourceDataTable.Columns.Add(eaConnectorId);
            ElementSourceDataTable.Columns.Add(action);
            
            dgvGENCabLoad.DataSource = ElementSourceDataTable;
        
        }

        private void AuditHistory_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            var aha = new AuditHistoryAccess( );

            Element element;

            // Get selected item in EA
            //
            try
            {
                element = (Element)AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch (Exception)
            {
                return;
            }

            aha.getAuditHistory(element);

        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            var aha = new AuditHistoryAccess();
            string delOnly = "No";

            if (cbxDeleteOnly.Checked)
                delOnly = "Yes";

            string ret = aha.deleteAudit(txtFromDate.Text, txtToDate.Text, delOnly);

            MessageBox.Show(ret);

        }

        private void btnSaveNotes_Click(object sender, EventArgs e)
        {
            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Element)
            {
                // only proceed if an element is selected from the browser
            }
            else
            {
                return;
            }

            var element = (Element)AddInRepository.Instance.Repository.GetTreeSelectedObject();

            EadbUpdate eatoupdate = new EadbUpdate();

            eatoupdate.ElementID = element.ElementID;
            eatoupdate.Note = txtNotes.Text;

            eatoupdate.UpdateEaElementNotes();

        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {

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

            string wrapperPathName = openFileDialog1.FileName;

            if (!File.Exists(wrapperPathName))
            {
                MessageBox.Show("File not found: " + wrapperPathName);
            }

            ElementSourceDataTable.Clear();
            // ListOfCabs.Clear();

            ListOfCabs =
                EaCaliberGenEngine.LoadCABConnections(wrapperPathName);

            foreach (EaCaliberGenEngine.callCompare item in ListOfCabs)
            {

                DataRow elementRow = ElementSourceDataTable.NewRow();
                elementRow["genCallType"] = item.GENcall.callType;
                elementRow["genCABName"] = item.GENcall.loadModuleName;
                elementRow["genElementID"] = item.GENcall.elementID;
                elementRow["eaCallType"] = item.EAcall.callType;
                elementRow["eaCABName"] = item.EAcall.loadModuleName;
                elementRow["eaElementID"] = item.EAcall.elementID;
                elementRow["eaConnectorID"] = item.EAcall.eaConnectorID;
                elementRow["action"] = item.decision;

                ElementSourceDataTable.Rows.Add(elementRow);

            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            EaCaliberGenEngine.updateCABConnections(ListOfCabs);
        }

        private void btnNewGuid_Click(object sender, EventArgs e)
        {

            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Element)
            {
                Element SelectedElement = 
                    (Element)AddInRepository.Instance.Repository.GetTreeSelectedObject();

                // only proceed if an element is selected from the browser

                var eadb = new EadbUpdate();
                var el = new EadbUpdate.SqlTaggedValue();

                el.XElementId = SelectedElement.ElementID;
                el.XNotes = "";
                el.XProperty = txtProperty.Text;
                el.XValue = txtValue.Text;

                if (SelectedElement.ElementID > 0)
                {
                    el = eadb.SqlSaveTaggedValue(el);
                }

                txtea_guid.Text = el.XGuid;
            }
        }

        private void xRefresh_Click(object sender, EventArgs e)
        {
            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Element)
            {
                Element SelectedElement =
                    (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();

                txtObjectID.Text = SelectedElement.ElementID.ToString();

            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        //
        // open file dialog and return file path and name
        //

        private string getFilePathName( string fileExtension, string fileFilter )
        {

            string ret = "";
            // fileExtension = "*.xml"
            // fileFilter = "XML Files (*.xml)|*.xml|All files(*.*)|*.*"

            if ( string.IsNullOrEmpty( fileExtension ))
            {
                fileExtension = "*.xml";
            }

            if (string.IsNullOrEmpty(fileFilter))
            {
                fileFilter = "XML Files (*.xml)|*.xml|All files(*.*)|*.*";
            }

            //
            // Display the file dialog
            //
            openFileDialog1.Filter = fileFilter;
            openFileDialog1.FileName = fileExtension;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                Close();
                return "";
            }

            // If no file has been selected
            if (openFileDialog1.FileName == "*.xml")
            {
                Close();
                return "";
            }

            string wrapperPathName = openFileDialog1.FileName;

            if (!File.Exists(wrapperPathName))
            {
                MessageBox.Show("File not found: " + wrapperPathName);
                ret = "";
            }
            else
            {
                ret = wrapperPathName;
            }

            return ret;
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            string fileExtension = "*.jpg";
            string fileFilter = "XML Files (*.xml)|*.xml|All files(*.*)|*.*";

            string fileName = getFilePathName(fileExtension, fileFilter);
    
            if ( String.IsNullOrEmpty(fileName) )
            {
                MessageBox.Show("File not found.");
                return;
            }

            // txtImageFile.Text = fileName;

        }

        private void btnSelectDocument_Click(object sender, EventArgs e)

        {
            string fileExtension = "*.doc";
            string fileFilter = "Word Files (*.doc)|*.doc|All files(*.*)|*.*";

            string fileName = getFilePathName(fileExtension, fileFilter);

            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("File not found.");
                return;
            }

            // txtWordDocument.Text = fileName;

        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            // WordReport.insertPicture(txtImageFile.Text);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            EATools eat = new EATools();
            eat.ApplyProjectTemplate2();
            Cursor.Current = Cursors.Arrow;

            MessageBox.Show("Project Created Successfully.");

        }

        private void btnNewProjectNumbers_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            EATools eat = new EATools();
            eat.ApplyProjectTemplate2();

            Cursor.Current = Cursors.Arrow;

            MessageBox.Show("Project Created Successfully.");

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            //WordReport.PdfCreate();

            
        }

        private void btnShowLastGlobal_Click(object sender, EventArgs e)
        {
            EaAccess eac = new EaAccess();
            var lastTagged = eac.GetLastTaggedValueByStereotype("Load Module", "GEN Global");

            txtLoadModule.Text = lastTagged.Value;
            txtCABName.Text = lastTagged.Name;
            txtGlobalAuthor.Text = lastTagged.Author;

        }

        
        // ------------------------------------------------
        //  This method will clean-up the snapshot folder
        // ------------------------------------------------
        private void btnCleanUp_Click(object sender, EventArgs e)
        {



        }
    }
}