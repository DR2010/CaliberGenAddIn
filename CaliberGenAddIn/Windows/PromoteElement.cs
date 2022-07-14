using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using EA;
using EAStructures;
using System.Linq;

namespace EAAddIn.Windows
{
    public partial class PromoteElement : Form
    {
        public const string EAREP1ID = "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";
        public EaCaliberGenEngine EAGen;
        public Element elementDestination;
        public Element elementSource;
        public SecurityInfo secinfo;
        public BindingSource MessagesBindingSource
        {
            get;
            set;
        }
        List<Message> messages = new List<Message>();

        public PromoteElement()
        {
            InitializeComponent();

            //CurrentRepository = rc;

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            MessagesBindingSource = new BindingSource();

            GetFromElement();
        }

        public PromoteElement(EA.Element elementSource, EA.Element elementDestination)
        {
            InitializeComponent();

            //CurrentRepository = rc;

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            MessagesBindingSource = new BindingSource();

            // Source
            //
            var elFrom =  AddInRepository.Instance.Repository.GetElementByGuid(elementSource.ElementGUID);
            AddInRepository.Instance.Repository.ShowInProjectView(elFrom);
            GetFromElement();

            // Destination
            //
            var elTo = AddInRepository.Instance.Repository.GetElementByGuid(elementDestination.ElementGUID);
            AddInRepository.Instance.Repository.ShowInProjectView(elTo);
            GetToElement();

        }

        private void btnFrom_Click(object sender, EventArgs e)
        {
            // Get selected item in EA
            //

            GetFromElement();
        }

        private void GetFromElement()
        {
            try
            {
                elementSource = GetElement();

                if (elementSource == null)
                {
                    MessageBox.Show("Item selected in EA is not valid to link. ", 
                        "Select From Element", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);

                     return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item selected in EA is not valid to link. " + ex.ToString());
                return;
            }
            if (elementSource.ElementGUID == txtToGuid.Text)
            {
                MessageBox.Show("From element cannot be the same as the To element.  Select a different To element in the project browser.",
                                "Select From Element", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFromGuid.Text = elementSource.ElementGUID;
            txtFromName.Text = elementSource.Name;
            txtFromType.Text = elementSource.Type;
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
            // Get selected item in EA
            //

            GetToElement();
        }

        private void GetToElement()
        {
            try
            {
                elementDestination = GetElement();

                if (elementSource == null)
                {
                    MessageBox.Show("Item selected in EA is not valid to link. ",
                                    "Select To Element",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Element selected in EA is not valid to link. "+ex.ToString());
                return;
            }

            if (elementDestination.ElementGUID == txtFromGuid.Text)
            {
                MessageBox.Show("To element cannot be the same as the from element.  Select a different To element in the project browser.",
                                "Select To Element", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtToGuid.Text = elementDestination.ElementGUID;
            txtToName.Text = elementDestination.Name;
            txtToType.Text = elementDestination.Type;
        }

        private void btnPromote_Click(object sender, EventArgs e)
        {
            Promote(false);
        }

        private void Promote(bool verifyOnly)
        {
            messages = new List<Message>();

            if ((! cbxDuplicateConnectors.Checked) && (cbxReplaceInDiagrams.Checked))
            {
                MessageBox.Show("You can't replace in diagrams if you don't duplicate connectors.");
                return;
            }

            var replaceInDiagrams = cbxReplaceInDiagrams.Checked;
            var duplicateConnectors = cbxDuplicateConnectors.Checked;
            var addAttributes = cbxAddAttributes.Checked;
            var addMethods = cbxAddMethods.Checked;


            if (!verifyOnly)
            {
                if (MessageBox.Show("Are you sure?",
                                    "Promote Object",
                                    MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            Cursor.Current = Cursors.WaitCursor;

            var eac = new EaAccess();
            messages =
                eac.promoteClass(elementSource, elementDestination,
                                 replaceInDiagrams,
                                 addAttributes,
                                 addMethods,
                                 duplicateConnectors, verifyOnly);

            Cursor.Current = Cursors.Arrow;
            BindMessages();

            if (verifyOnly) return;

            if (messages.Any(x => x.Type == MessageType.Error))
            {
                MessageBox.Show(
                    "One of more errors occurred promoting element.  Please check the messages for more information",
                    "Promote Element", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    "Promote successful." + Environment.NewLine + "Don't forget to manually remove " + elementSource.Name + " after verification.",
                    "Promote Element", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    

        public void BindMessages()
        {
            MessagesBindingSource.DataSource = messages;
            MessagesDataGridView.DataSource = MessagesBindingSource;
            MessagesDataGridView.Columns[1].Width = 600;
        }

        private void VerifyPromotionButton_Click(object sender, EventArgs e)
        {
            Promote(true);

        }

        private Element GetElement()
        {
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag != null && diag.SelectedObjects.Count == 1)
            {
                foreach (DiagramObject diagObj in diag.SelectedObjects)
                {
                    //objectSelected = diagobj;
                    return AddInRepository.Instance.Repository.GetElementByID(diagObj.ElementID);
                    //break;
                }
            }

            // Get selected elements from project browser
            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Element)
            {
                return (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            return null;
        }

        private void btnMergeTables_Click(object sender, EventArgs e)
        {
            MergeTables();

        }


        // ---------------------------------------------
        //          Merge tables into the latter
        // ---------------------------------------------
        private void MergeTables()
        {
            messages = new List<Message>();


            if (MessageBox.Show("Are you sure?",
                                "Merge Tables",
                                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            var eac = new EaAccess();
            messages =
                eac.MergeTable(elementSource, elementDestination);

            Cursor.Current = Cursors.Arrow;
            BindMessages();

            if (messages.Any(x => x.Type == MessageType.Error))
            {
                MessageBox.Show(
                    "One of more errors occurred promoting element.  Please check the messages for more information",
                    "Merge Tables", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(
                    "Promote successful.",
                    "Merge Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PromoteElement_Load(object sender, EventArgs e)
        {

        }

    }
}