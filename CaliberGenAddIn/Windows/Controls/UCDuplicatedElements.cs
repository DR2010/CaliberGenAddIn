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

namespace EAAddIn.Windows.Controls
{
    public partial class UCDuplicatedElements : UserControl
    {
        public DataTable elementSourceDataTable;
        public string currentTag;

        public UCDuplicatedElements()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

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


            var tagValue = new DataColumn("tagValue", typeof(String));
            var tagCount = new DataColumn("tagCount", typeof(String));

            elementSourceDataTable = new DataTable("ElementSourceDataTable");

            elementSourceDataTable.Columns.Add(tagValue);
            elementSourceDataTable.Columns.Add(tagCount);

            var duplTagValue = new ArrayList();

            duplTagValue = new EaAccess().GetDuplicateTaggedValues(tag);

            foreach (object obj in duplTagValue)
            {
                var td = (DEEWREAReports.taggedDuplicate)obj;

                DataRow elementRow = elementSourceDataTable.NewRow();
                elementRow["tagValue"] = td.tagValue;
                elementRow["tagCount"] = td.tagCount;

                elementSourceDataTable.Rows.Add(elementRow);
            }

        }

        //
        // Duplicate tagged values
        //
        private void duplicateTaggedTree(string tag)
        {

            Cursor.Current = Cursors.WaitCursor;

            int i = 0;
            int max = 20;

            var duplTagValue = new ArrayList();

            ImageList imageList = EaCaliberGenEngine.GetImageList();
            tvReport.ImageList = imageList;

            duplTagValue = new EaAccess().GetDuplicateTaggedValues(tag);

            int image = 2;

            var root = new TreeNode(" Listing " + max + " of " + duplTagValue.Count + " duplicated tag for " + tag, image, image);
            // root.Text = tag;

            tvReport.Nodes.Clear();

            tvReport.Nodes.Add(root);
            
            // List only first 10
            //

            foreach (object obj in duplTagValue)
            {
                i++;
                if (i > max) break;

                var td = (DEEWREAReports.taggedDuplicate)obj;

                var node = new TreeNode("TagValue: " + td.tagValue + "; Count: " + td.tagCount, 2, 2);
                // node.Text = td.tagValue;
                node.Tag = td;

                root.Nodes.Add(node);

                // get related items
                var duplTagList = new ArrayList();
                duplTagList = new EaAccess().GetDuplicateElementInfoForTagAndValue(tag, td.tagValue);

                foreach (object objTag in duplTagList)
                {
                    var ot = (DEEWREAReports.objectTag)objTag;

                    image = EaCaliberGenEngine.GetImageIdUsingEaType(ot.objectType);

                    var subnode = new TreeNode(ot.objectName, image, image);
                    subnode.Text = "EA Element ID: " + ot.objectID + " > " + ot.objectName + "; Link Table Caliber ID: " + ot.CaliberID;
                    subnode.Tag = ot;

                    node.Nodes.Add(subnode);
                }
            }

            Cursor.Current = Cursors.Arrow;
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

            // Get selected node
            TreeNode tnSelected = tvReport.SelectedNode;

            if (tnSelected.Tag.GetType().Name == "taggedDuplicate") // Package
            {
                // Ignore
                return;
            }

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
            Element el = (Element)obj;

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
                objElement = null;
            }

            return objElement;
        }

        private void mergeElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Get selected node
            TreeNode tnSelected = tvReport.SelectedNode;

            if (tnSelected.Tag.GetType().Name == "objectTag") // Item
            {
                // ignore
                return;
            }

            EA.Element elementFrom = null;
            EA.Element elementTo = null;


            if (tnSelected.Tag.GetType().Name == "taggedDuplicate") // Package
            {
                int i = 1;
                foreach (TreeNode tn in tnSelected.Nodes)
                {
                    var node = (DEEWREAReports.objectTag)tn.Tag;

                    // First one is from
                    if (i == 1)
                    {
                        // Set document from
                        elementFrom = AddInRepository.Instance.Repository.GetElementByGuid(node.objectEAGUID);
                    }

                    // Second one is  to
                    if (i == 2)
                    {
                        // Set document to
                        elementTo = AddInRepository.Instance.Repository.GetElementByGuid(node.objectEAGUID);
                        break;
                    }

                    i++;
                }

                var promoteWindow = new PromoteElement(elementFrom, elementTo);
                promoteWindow.ShowDialog();
            
            }

            return;

        }

    }
}
