using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EA;
using EAAddIn.Properties;
using EAStructures;
using Attribute = EA.Attribute;

namespace EAAddIn.Windows
{
    public partial class EAExplorer : Form
    {
        //public EA.Repository CurrentRepository;
        private readonly SortedList<int, ListViewItem> attributesToMove = new SortedList<int, ListViewItem>();
        private List<string> connectorTypes = new List<string> { "All" };
        private string[] stereotypes;
        private string[] includedStereotypes;
        private string[] excludedStereotypes;
        public EaCaliberGenEngine EAEngine;
        public DataTable elementSourceDataTable;
        private string lastException;
        private bool moveMode;
        private bool PerformOperation;
        public SecurityInfo secinfo;

        public EAExplorer()
        {
            InitializeComponent();

            //CurrentRepository = rc;

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            cbxDisplayElementInfo_CheckedChanged(null, null);
        }


        public bool NoChildElementsView
        {
            get { return ViewComboBox.SelectedIndex == 1; }
        }

        public bool ChildElementsView
        {
            get { return ViewComboBox.SelectedIndex == 1; }
        }

        public bool ChildAttributesView
        {
            get { return ViewComboBox.SelectedIndex == 2; }
        }

        public bool ChildMethodsView
        {
            get { return ViewComboBox.SelectedIndex == 3; }
        }

        private void DEEWREATreeView_Load(object sender, EventArgs e)
        {
            var eaGen = new EaCaliberGenEngine(secinfo, "coolgen");

            ImageList imageList = EaCaliberGenEngine.GetImageList();

            // Binding
            tvEARelated.ImageList = imageList;
            lvRelated.SmallImageList = imageList;
            lvRelated.LargeImageList = imageList;
            cbxCalledBy.Checked = true;
            cbxCalls.Checked = true;
            cbxShowDiagrams.Checked = true;

            dateTimePickerChangedBefore.Value = DateTime.Now;
            dateTimePickerChangedAfter.Value = DateTime.Now.AddDays(-7);

            ViewComboBox.Text = Settings.Default.HierarchyChildView;

            ConnectorTypesTextBox.Text = BuildStringFromList(connectorTypes);

            if (!(AddInRepository.Instance.Repository.GetTreeSelectedObject() is Package))
            {
                refreshHierarchy();
            }
        }

        // ----------------------------------------------------------
        //             Refresh hierarchy
        // ----------------------------------------------------------
        private void refreshHierarchy()
        {
            Cursor.Current = Cursors.WaitCursor;
            CancelToolStripButton.Visible = true;
            PerformOperation = true;
            Application.DoEvents();
            tvEARelated.Nodes.Clear();
            lvRelated.Items.Clear();
            lastException = string.Empty;
            LastExceptionLabel.Visible = false;

            try
            {
                // Get selected elements from current diagram
                Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

                if (diag != null && diag.SelectedObjects.Count > 0)
                {
                    foreach (DiagramObject diagobj in diag.SelectedObjects)
                    {
                        //objectSelected = diagobj;
                        AddDiagramElements(diagobj);
                        //break;
                    }
                    return;
                }

                // Get selected elements from project browser
                if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Element)
                {
                    AddElement((Element)AddInRepository.Instance.Repository.GetTreeSelectedObject(), null);
                }

                    // Get elements from selected package in project browser
                else if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Package)
                {
                    var package = (Package)AddInRepository.Instance.Repository.GetTreeSelectedObject();
                    AddPackage(package, null, null);

                    if (tvEARelated.SelectedNode != null)
                    {
                        tvEARelated.SelectedNode.Expand();
                    }
                }
            }
            catch (Exception ex)
            {
                lastException = ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
                CancelToolStripButton.Visible = false;
                LastExceptionLabel.Visible = (lastException != string.Empty);
            }

            if (tvEARelated.Nodes.Count == 0)
            {
                MessageBox.Show("No items found.", "Add In", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddPackage(Package package, TreeNode parent, bool? recurse)
        {
            int imagenum = EaCaliberGenEngine.GetImageIdUsingEaType("Package");
            var packageNode = new TreeNode(package.Name, imagenum, 0);

            if (!recurse.HasValue)
            {
                recurse = LoadHierarchyCheckBox.Checked;
            }

            packageNode.Tag = EaAccess.GetConnectorListForPackage(package);

            if (parent == null)
            {
                tvEARelated.Nodes.Add(packageNode);
                tvEARelated.ExpandAll();
            }
            else
            {
                parent.Nodes.Add(packageNode);
            }

            if (recurse.Value || (tvEARelated.Nodes.Count == 1 && (tvEARelated.Nodes[0].Nodes.Count == 0)))
            {
                AddPackageContents(package, packageNode, recurse.Value);

                if (packageNode.Nodes.Count == 0)
                {
                    if (parent == null)
                    {
                        tvEARelated.Nodes.Remove(packageNode);
                    }
                    else
                    {
                        parent.Nodes.Remove(packageNode);
                    }
                }
            }
        }

        private void AddPackageContents(Package package, TreeNode packageNode, bool recurse)
        {
            // always recurse if this package is the only one in the tree view

            if (recurse ||
                packageNode.Nodes.Count == 0 || // packages won't have children loaded (unless full hierarchy checked)
                (tvEARelated.Nodes.Count == 1 && (tvEARelated.Nodes[0].Nodes.Count == 0)))
            // always go down a level if only the first package is in the tree view
            {
                foreach (Package childPackage in package.Packages)
                {
                    AddPackage(childPackage, packageNode, recurse);
                }
            }

            UsingProgressBar(package.Elements.Count, () =>
                                                         {
                                                             foreach (Element element in package.Elements)
                                                             {
                                                                 AddElement(element, packageNode);
                                                                 toolStripProgressBar.PerformStep();
                                                             }
                                                         });
        }

        private void ApplyToPackageElements(Action<Element> elementMethod)
        {
            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (tnSelected != null)
            {
                if (tnSelected.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)tnSelected.Tag;

                    if (connector.ObjectType == "Package")
                    {
                        Package package = connector.ObjectPackage;

                        UsingProgressBar(package.Elements.Count, () =>
                                                                     {
                                                                         foreach (Element element in package.Elements)
                                                                         {
                                                                             elementMethod(element);
                                                                             element.Update();
                                                                             toolStripProgressBar.PerformStep();
                                                                         }
                                                                         package.Update();
                                                                         package.Elements.Refresh();
                                                                         refreshHierarchy();
                                                                     });
                    }
                }
            }
        }

        private void BuildTreeViewContextMenu()
        {
            //var menu = new ContextMenuStrip();

            //var locateInBrowserToolStripMenuItem = new ToolStripMenuItem("Locate in Browser", null,
            //                                                             LocateInBrowserTreeView) { ShortcutKeys = Keys.Alt | Keys.G };
            //var placeInDiagramToolStripMenuItem = new ToolStripMenuItem("Place in Diagram", null,
            //                                                            placeInDiagramTreeView);
            //var openDiagramToolStripMenuItem = new ToolStripMenuItem("Open Diagram", null,
            //                                                         openDiagramToolStripMenuItem_Click);
            //var expandOneLevelToolStripMenuItem = new ToolStripMenuItem("Expand one level", null,
            //                                                          expandOneLevelToolStripMenuItem_Click);

            treeViewContextMenuStrip.Items[0].Visible = false;
            treeViewContextMenuStrip.Items[1].Visible = false;
            treeViewContextMenuStrip.Items[2].Visible = false;
            treeViewContextMenuStrip.Items[3].Visible = false;
            treeViewContextMenuStrip.Items[4].Visible = false;
            treeViewContextMenuStrip.Items[5].Visible = false;
            treeViewContextMenuStrip.Items[6].Visible = false;
            treeViewContextMenuStrip.Items[7].Visible = false;
            treeViewContextMenuStrip.Items[8].Visible = false;
            treeViewContextMenuStrip.Items[9].Visible = false;
            treeViewContextMenuStrip.Items[10].Visible = false;
            treeViewContextMenuStrip.Items[11].Visible = false;

            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (tnSelected != null)
            {
                if (tnSelected.Tag is EaAccess.DiagramStruct)
                {
                    treeViewContextMenuStrip.Items[0].Visible = true;
                    treeViewContextMenuStrip.Items[2].Visible = true;
                    //menu.Items.AddRange(new ToolStripItem[]
                    //                {
                    //                    openDiagramToolStripMenuItem,
                    //                    locateInBrowserToolStripMenuItem
                    //                });

                }

                treeViewContextMenuStrip.Items[3].Visible = true;

                //menu.Items.Add(expandOneLevelToolStripMenuItem);

                if (tnSelected.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)tnSelected.Tag;

                    if (connector.ObjectType == "Package")
                    {
                        //menu.Items.Add(locateInBrowserToolStripMenuItem);
                        treeViewContextMenuStrip.Items[0].Visible = true;
                        treeViewContextMenuStrip.Items[4].Visible = true;
                        treeViewContextMenuStrip.Items[5].Visible = true;

                        //menu.Items.Add(new ToolStripMenuItem("Update Stereotype", null,
                        //                                     new EventHandler(UpdateStereotype)));
                        //menu.Items.Add(new ToolStripMenuItem("Update Status", null, new EventHandler(UpdateStatus)));

                    }
                    else
                    {
                        treeViewContextMenuStrip.Items[0].Visible = true;
                        treeViewContextMenuStrip.Items[1].Visible = true;

                        //menu.Items.Add(locateInBrowserToolStripMenuItem);
                        //menu.Items.Add(placeInDiagramToolStripMenuItem);

                        if (connector.ObjectType == "Class")
                        {
                            treeViewContextMenuStrip.Items[6].Visible = true;
                            treeViewContextMenuStrip.Items[7].Visible = true;
                            treeViewContextMenuStrip.Items[8].Visible = true;

                            //menu.Items.Add(new ToolStripMenuItem("Clean Class", null, new EventHandler(CleanClass)));
                            //menu.Items.Add(new ToolStripMenuItem("Remove Multiple Attribute Stereotypes", null, new EventHandler(RemoveAttributeStereotypes)));
                            //menu.Items.Add(new ToolStripMenuItem("Remove ALL Attribute Stereotypes", null, new EventHandler(RemoveAllAttributeStereotypes)));

                            if (connector.ObjectStereotype == "table")
                            {
                                treeViewContextMenuStrip.Items[9].Visible = true;
                                //menu.Items.Add(new ToolStripMenuItem("Reset Column Stereotypes", null, new EventHandler(ResetColumnStereotypes)));
                            }
                        }

                        treeViewContextMenuStrip.Items[10].Visible = true;
                        //menu.Items.Add(new ToolStripMenuItem("Delete", null, new EventHandler(DeleteElement)));
                    }
                }
            }

            if (comboBoxProfile.Text == "Structure Chart"
                && tnSelected.Tag is EaAccess.ConnectorList
                && ((EaAccess.ConnectorList)tnSelected.Tag).ObjectStereotype.Contains("GEN"))
            {
                treeViewContextMenuStrip.Items[11].Visible = true;
                //menu.Items.Add(new ToolStripMenuItem("Build Structure Chart", null,
                //                                     new EventHandler(BuildStructureChart)));

            }



            //return menu;
        }

        private void BuildStructureChart(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to include modified tables in the structure chart?",
                                                  "Confirm Include Tables", MessageBoxButtons.YesNoCancel,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Cancel) return;

            if (tvEARelated.SelectedNode.Tag is EaAccess.ConnectorList)
            {
                Element element = ((EaAccess.ConnectorList)tvEARelated.SelectedNode.Tag).ObjectElement;

                MessageLabel.Text = string.Empty;
                MessageLabel.Visible = true;

                var maxDepthReached = AddChildElementsToStructureChart(element, result == DialogResult.Yes, 0);

                MessageLabel.Text = "Saving diagram...";
                Application.DoEvents();

                SaveCurrentDiagram();
                MessageLabel.Visible = false;

                if (maxDepthReached)
                {
                    MessageBox.Show("No items after a depth of 10 were added.", "Depth of Chart Exceeded Maximum", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }

                MessageBox.Show("Now use the menu Diagram, Layout Diagram to finish your structure chart",
                                "Creation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveCurrentDiagram()
        {
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag == null)
                return;

            diag.DiagramObjects.Refresh();
            diag.Update();
            AddInRepository.Instance.Repository.SaveDiagram(diag.DiagramID);
            AddInRepository.Instance.Repository.ReloadDiagram(diag.DiagramID);

        }

        private bool AddChildElementsToStructureChart(Element element, bool addModifiedTables, int depth)
        {
            var maxDepthReached = depth > 10;

            List<EaAccess.ConnectorList> listOfRelatedGENItems =
                new EaAccess().getLinkedElementsSQL(element, "GEN", "",
                                                    true, false,
                                                    new List<string> { "Association" },
                                                    null, null,
                                                    0);

            if (listOfRelatedGENItems != null)
            {
                foreach (EaAccess.ConnectorList connectorList in listOfRelatedGENItems)
                {
                    if (connectorList.ObjectName == "G_SYS_INITIALISE"
                        || connectorList.ObjectName == "G_SYS_FINALISE"
                        || connectorList.ObjectName == "G_SYS_SYSTEM_INFO_GET_EAB"
                        || connectorList.ObjectName == "C_WRAP_SYSTEM_INFO_0840"
                        || connectorList.ObjectName == "C_WRAP_SET_NULL_DEF_1412"
                        || connectorList.ObjectName == "C_WRAP_MANDATORY_INP_1953"
                        || connectorList.ObjectName.StartsWith("XX"))
                    {
                        continue;
                    }
                    MessageLabel.Text = "Adding " + connectorList.ObjectName;
                    Application.DoEvents();

                    placeInDiagram(new List<object> { connectorList }, false);

                    if (
                        new[] { "GEN Service", "GEN T-Public", "GEN Orchestrator", "GEN Public", "GEN Private" }.Contains(
                            connectorList.ObjectStereotype))
                    {
                        Element objElement =
                            AddInRepository.Instance.Repository.GetElementByGuid(connectorList.ObjectGuid);

                        if (!maxDepthReached)
                        {
                            AddChildElementsToStructureChart(objElement, addModifiedTables, depth + 1);
                        }
                    }
                }
            }

            if (addModifiedTables)
            {
                List<EaAccess.ConnectorList> listOfRelatedTables =
                    new EaAccess().getLinkedElementsSQL(element, "table", "",
                                                        true, false,
                                                        new List<string> { "Association" },
                                                        null, null,
                                                        0);


                if (listOfRelatedTables != null)
                {
                    foreach (EaAccess.ConnectorList connectorList in listOfRelatedTables)
                    {
                        if (
                            new[] { "-Create", "-Update", "-Delete" }.Contains(connectorList.ConnectorName))
                        {
                            placeInDiagram(new List<object> { connectorList }, false);
                        }
                    }
                }
            }
            return maxDepthReached;
        }

        private ContextMenuStrip BuildRelatedItemsContextMenu()
        {
            var menu = new ContextMenuStrip();

            if (lvRelated.SelectedIndices.Count == 1)
            {
                var locateInBrowserToolStripMenuItem = new ToolStripMenuItem("Locate in Browser", null,
                                                                             LocateInBrowserListView) { ShortcutKeys = Keys.Alt | Keys.G };

                menu.Items.AddRange(new ToolStripItem[]
                                        {
                                            locateInBrowserToolStripMenuItem
                                        });
            }
            if (lvRelated.SelectedIndices.Count > 0)
            {
                var placeInDiagramToolStripMenuItem = new ToolStripMenuItem("Place in Diagram", null,
                                                                            placeInDiagramListView);

                var deleteItemsToolStripMenuItem = new ToolStripMenuItem("Delete", null,
                                                                         deleteListViewItemsFromEA);


                menu.Items.AddRange(new ToolStripItem[]
                                        {
                                            placeInDiagramToolStripMenuItem,
                                            deleteItemsToolStripMenuItem
                                        });

                bool itemAdded = false;
                var separator = new ToolStripSeparator();

                menu.Items.Add(separator);

                if (lvRelated.SelectedIndices.Count == 1)
                {
                    ListViewItem selectedItem = lvRelated.SelectedItems[0];

                    if (selectedItem.Tag is EaAccess.ConnectorList)
                    {
                        var connector = (EaAccess.ConnectorList)selectedItem.Tag;

                        if (connector.ObjectType == "Class")
                        {
                            menu.Items.Add(new ToolStripMenuItem("Clean Class", null, new EventHandler(CleanClass)));

                            itemAdded = true;
                        }
                        if (connector.ObjectType == "Package")
                        {
                            menu.Items.Add(new ToolStripMenuItem("Update Stereotype", null,
                                                                 new EventHandler(UpdateStereotype)));
                            menu.Items.Add(new ToolStripMenuItem("Update Status", null, new EventHandler(UpdateStatus)));

                            itemAdded = true;
                        }
                    }
                }

                if (!itemAdded)
                    menu.Items.Remove(separator);
            }
            return menu;
        }

        private void DeleteElement(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Are you sure you want to delete this item?  This cannot be undone.", "Confirm Delete",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (deleteFromEA(new List<object>() { tvEARelated.SelectedNode.Tag }))
                {
                    tvEARelated.SelectedNode.Remove();
                }
            }
        }

        private void CleanClass(object obj, EventArgs e)
        {
            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (tnSelected != null)
            {
                if (tnSelected.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)tnSelected.Tag;

                    Element element =
                        AddInRepository.Instance.Repository.GetElementByID(
                            connector.ObjectElementId);

                    EaCaliberGenEngine.CleanClass(element);
                }
            }
        }

        private void UpdateStatus(object obj, EventArgs e)
        {
            txtObjectStatus.BackColor = Color.LightPink;
            ApplyStatusChangeButton.Visible = true;
            MessageBox.Show(
                "Please update if necessary the status text box, and click the apply button next to the field");
        }

        private void UpdateStereotype(object obj, EventArgs e)
        {
            txtObjectStereotype.BackColor = Color.LightPink;
            ApplyStereotypeChangeButton.Visible = true;
            MessageBox.Show(
                "Please update if necessary the stereotype text box, and click the apply button next to the field");
        }
        private void RemoveAttributeStereotypes(object obj, EventArgs e)
        {
            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (tnSelected != null)
            {
                if (tnSelected.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)tnSelected.Tag;

                    if (connector.ObjectElement != null)
                    {
                        var elementId = connector.ObjectElementId;

                        new EaAccess().RemoveAttributeMultipleStereotypes(elementId);
                    }
                }
            }
        }
        private void RemoveAllAttributeStereotypes(object obj, EventArgs e)
        {
            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (tnSelected != null)
            {
                if (tnSelected.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)tnSelected.Tag;

                    if (connector.ObjectElement != null)
                    {
                        var element = connector.ObjectElement;

                        foreach (Attribute attribute in element.Attributes)
                        {
                            attribute.Stereotype = string.Empty;
                            attribute.StereotypeEx = string.Empty;
                            attribute.Update();
                        }
                    }
                }
            }
        }

        private void ResetColumnStereotypes(object obj, EventArgs e)
        {
            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (tnSelected != null)
            {
                if (tnSelected.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)tnSelected.Tag;

                    if (connector.ObjectElement != null)
                    {
                        short deleted = 0;

                        var element = connector.ObjectElement;
                        var deleteIndexes = new List<short>();

                        short i = 0;
                        foreach (Attribute attribute in element.Attributes)
                        {
                            if (attribute.Stereotype != "column" ||
                                attribute.StereotypeEx != "column")
                            {
                                if (attribute.Stereotype.Contains("dropped") ||
                                    attribute.Stereotype.Contains("deleted") ||
                                    attribute.StereotypeEx.Contains("dropped") ||
                                    attribute.StereotypeEx.Contains("deleted"))
                                {
                                    deleteIndexes.Add(i);
                                }
                                else
                                {
                                    attribute.Stereotype = "column";
                                    attribute.StereotypeEx = "column";
                                    attribute.Update();
                                }
                            }
                            i++;
                        }
                        if (deleteIndexes.Count > 0)
                        {
                            foreach (var indexToDelete in deleteIndexes)
                            {
                                var actualIndex = (short)(indexToDelete - deleted);
                                if (
                                    MessageBox.Show(
                                        "Delete " + ((Attribute)element.Attributes.GetAt(actualIndex)).Name + "?",
                                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                                    System.Windows.Forms.DialogResult.Yes)
                                {
                                    element.Attributes.DeleteAt(actualIndex, true);
                                    deleted++;
                                }
                            }
                        }

                        element.Update();

                        var message = "Columns reset to <<column>> stereotype";
                        if (deleted > 0)
                        {
                            message += " and ";
                            message += (deleted == 1 ? "an attribute has " : deleted.ToString() + " attributes have ");
                            message += "been deleted";
                        }

                        message += ".";

                        MessageBox.Show(message, "Columns Reset", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ApplyUpdateStatus()
        {
            ApplyToPackageElements(element => { element.Status = txtObjectStatus.Text; });

            txtObjectStatus.BackColor = Color.FromKnownColor(KnownColor.Window);
            ApplyStatusChangeButton.Visible = false;
        }

        private void ApplyUpdateStereotype()
        {
            ApplyToPackageElements(element =>
                                       {
                                           element.StereotypeEx = "";
                                           element.Stereotype = txtObjectStereotype.Text;
                                       });
            txtObjectStereotype.BackColor = Color.FromKnownColor(KnownColor.Window);
            ApplyStereotypeChangeButton.Visible = false;
        }


        private void AddElement(Element element, TreeNode treeNode = null, int parentID = 0)
        {
            Application.DoEvents();
            if (PerformOperation && element.ParentID == parentID)
            {
                bool addElement = true;

                #region Conditions for when not to add an element

                if (ChangedAfterCheckBox.Checked
                    && element.Modified < dateTimePickerChangedAfter.Value.Date)
                {
                    addElement = false;
                }

                if (ChangedBeforeCheckBox.Checked
                    && element.Modified > dateTimePickerChangedBefore.Value.Date.AddDays(1))
                {
                    addElement = false;
                }

                if (StatusCheckBox.Checked && element.Status != StatusComboBox.Text)
                {
                    addElement = false;
                }
                if (UnlinkedCheckBox.Checked)
                {
                    if (element.Type == "Class"
                        ||
                        (element.Type == "Requirement" &&
                         (element.Name.StartsWith("BR") || element.Name.StartsWith("DR")))
                        || element.Type == "UseCase"
                        || element.Type == "Screen"
                        )
                    {
                        List<EaAccess.ConnectorList> listOfRelatedItems =
                            new EaAccess().getLinkedElementsSQL(element, "", "",
                                                                cbxCalls.Checked, cbxCalledBy.Checked,
                                                                connectorTypes, includedStereotypes, excludedStereotypes,
                                                                0);

                        addElement = listOfRelatedItems.Count <= 0;
                    }
                    else
                    {
                        addElement = false;
                    }
                }
                if (addElement &&
                    NotUsedInDiagramsCheckBox.Checked)
                {
                    List<EaAccess.DiagramStruct> diagrams = new EaAccess().getDiagramList(element.ElementID);
                    addElement = (diagrams.Count == 0);
                }

                var elementNode = new TreeNode(element.Name, 40, 0);

                #endregion

                if (addElement)
                {
                    EaAccess.ConnectorList connList = EaAccess.GetConnectorListForElement(element);

                    if (element.Type == "Object"
                    && element.ClassifierID > 0)
                    {
                        connList.ObjectName = new EaAccess().GetElementNameByObjectID(element.ClassifierID);
                    }
                    int imagenum = EaCaliberGenEngine.GetImageIdUsingEaType(connList.ObjectType);

                    elementNode = new TreeNode(connList.ObjectName, imagenum, 0) { Tag = connList };

                    //AddLinks(element, connList, elementNode);
                }

                if (treeNode == null)
                {
                    tvEARelated.Nodes.Add(elementNode);
                }
                else
                {
                    treeNode.Nodes.Add(elementNode);
                }

                //if (LoadHierarchyCheckBox.Checked)
                //{
                foreach (Element childElement in element.Elements)
                {
                    AddElement(childElement, elementNode, element.ElementID);
                }
                //}
                if (!addElement && elementNode.Nodes.Count == 0)
                {
                    elementNode.Remove();
                }
            }
        }

        private void AddLinks(Element element, EaAccess.ConnectorList connList, TreeNode elementNode)
        {
            if (ShowLinksCheckBox.Checked)
            {
                TreeNode diagramNode;

                if (cbxShowDiagrams.Checked)
                {
                    var connList2 = new EaAccess.ConnectorList
                                        {
                                            ObjectName = "Diagrams",
                                            ObjectStereotype = "",
                                            ObjectType = ""
                                        };

                    diagramNode = new TreeNode(connList2.ObjectName, 28, 0) { Tag = connList };
                    addRelatedElementsToTreeView(element, elementNode, diagramNode);

                    if (diagramNode.Nodes.Count > 0)
                    {
                        elementNode.Nodes.Add(diagramNode);
                    }
                }
                else
                {
                    addRelatedElementsToTreeView(element, elementNode, null);
                }
            }
        }

        private void AddDiagramElements(DiagramObject diagramObject)
        {
            if (diagramObject.ObjectType.ToString() != "Package")
            {
                Element element =
                    AddInRepository.Instance.Repository.GetElementByID(
                        diagramObject.ElementID);

                if (element == null || element.ElementID <= 0 || element.Type == "Package")
                    return;

                AddElement(element);
            }
        }


        // ----------------------------------------------
        //    List Related Elements
        // ----------------------------------------------
        private void addRelatedElementsToTreeView(Element elementSelect, TreeNode elementNode, TreeNode diagramNode)
        {
            bool calls = cbxCalls.Checked;
            bool calledby = cbxCalledBy.Checked;

           // Stop at tables if calls is selected
            if (cbxCalls.Checked && (!cbxCalledBy.Checked))
            {
                if (elementSelect.Stereotype == "table")
                    return;
            }

            if (((EaAccess.ConnectorList)elementNode.Tag).ObjectType != "Package")
            {
                var eainst = new EaAccess();


                if (diagramNode != null)
                {
                    //
                    // Get Diagrams
                    //
                    List<EaAccess.DiagramStruct> listOfDiagrams =
                        eainst.getDiagramList(elementSelect.ElementID);

                    foreach (EaAccess.DiagramStruct item in listOfDiagrams)
                    {
                        //var diagList = new EaAccess.DiagramStruct();
                        //diagList = item;

                        int imagenum = EaCaliberGenEngine.GetDiagramImageId(item.DiagramType);

                        var tnDiag = new TreeNode(item.DiagramName, imagenum, 0) { Tag = item };

                        diagramNode.Nodes.Add(tnDiag);
                    }
                }


                //
                //  Get elements
                //
                List<EaAccess.ConnectorList> listOfRelatedItems =
                    eainst.getLinkedElementsSQL(elementSelect,
                                                "",
                                                "",
                                                calls,
                                                calledby,
                                                connectorTypes,
                                                includedStereotypes,
                                                excludedStereotypes,
                                                0);

                // Prevent node reload (Allow for Diagram node)
                if (NodeContainsLinksOrDiagrams(elementNode)) return;

                UsingProgressBar(listOfRelatedItems.Count,
                                 () =>
                                 {
                                     foreach (EaAccess.ConnectorList item in listOfRelatedItems)
                                     {
                                         EaAccess.ConnectorList connList = item;

                                         // If notes is not checked, ignore it.
                                         if (connList.ObjectType == "Note" && !cbxLinkeNotes.Checked)
                                             continue;

                                         if (connList.ObjectType == "Package")
                                             connList.ObjectPackage =
                                                 AddInRepository.Instance.Repository.GetPackageByGuid(
                                                     connList.ObjectGuid);
                                         else if (connList.ObjectType != "Diagram")
                                             connList.ObjectElement =
                                                 AddInRepository.Instance.Repository.GetElementByGuid(
                                                     connList.ObjectGuid);

                                         int imagenum = EaCaliberGenEngine.GetImageIdUsingEaType(connList.ObjectType);

                                         string nodecontents;


                                         if (cbxDisplayDetails.Checked)
                                         {
                                             if (connList.To_ConnectorFeatureType == "")
                                             {
                                                 nodecontents = connList.UsesUsedBy + " <" +
                                                                connList.ObjectName +
                                                                "> " + connList.ConnectorName +
                                                                "; " + connList.ObjectStereotype +
                                                                "; " + connList.ObjectNote.Substring(0,
                                                                                                     (connList.
                                                                                                          ObjectNote
                                                                                                          .Length >
                                                                                                      50
                                                                                                          ? 50
                                                                                                          :
                                                                                                              connList
                                                                                                                  .
                                                                                                                  ObjectNote
                                                                                                                  .
                                                                                                                  Length));
                                             }
                                             else
                                             {
                                                 nodecontents = connList.UsesUsedBy + "<F>" + " <" +
                                                                connList.ObjectName + "." +
                                                                connList.To_ConnectorFeature +
                                                                "> " + connList.ConnectorName +
                                                                "; " + connList.ObjectStereotype +
                                                                "; " + connList.ObjectNote.Substring(0,
                                                                                                     (connList.
                                                                                                          ObjectNote
                                                                                                          .Length >
                                                                                                      50
                                                                                                          ? 50
                                                                                                          :
                                                                                                              connList
                                                                                                                  .
                                                                                                                  ObjectNote
                                                                                                                  .
                                                                                                                  Length));
                                             }
                                         }
                                         else
                                         {
                                             string from = connList.FromConnectorFeature ?? "";

                                             string to = connList.To_ConnectorFeature ?? "";

                                             if (from == "" && to == "")
                                             {
                                                 nodecontents = connList.UsesUsedBy +
                                                                connList.ObjectName + " " + connList.ConnectorName +
                                                                " (" + connList.ConnectorType + ")";
                                             }
                                             else
                                             {
                                                 nodecontents = (connList.UsesUsedBy +
                                                                 (from == ""
                                                                      ? elementSelect.Name + " "
                                                                      :
                                                                          connList.FromConnectorFeatureType + "." +
                                                                          connList.FromConnectorFeature)
                                                                 + " <----> " + connList.OtherEndObjectName +
                                                                 (to == ""
                                                                      ? " "
                                                                      :
                                                                          "." + connList.To_ConnectorFeature));
                                                 if (to != "")
                                                 {
                                                     imagenum =
                                                         EaCaliberGenEngine.GetImageIdUsingEaType(
                                                             connList.To_ConnectorFeatureType);
                                                 }
                                             }
                                         }

                                         var tn = new TreeNode(nodecontents, imagenum, 0) { Tag = connList };

                                         elementNode.Nodes.Add(tn);

                                         toolStripProgressBar.PerformStep();
                                     }
                                 });
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshHierarchy();
        }

        private void UpdateChildItemsListView()
        {
            if (tvEARelated.SelectedNode == null) return;

            var connList = (EaAccess.ConnectorList)tvEARelated.SelectedNode.Tag;

            lvRelated.Items.Clear();
            MoveReleatedItemsButton.Visible = false;

            if (LoadHierarchyCheckBox.Checked)
            {
                UsingProgressBar(tvEARelated.SelectedNode.Nodes.Count,
                                 () =>
                                 {
                                     foreach (TreeNode node in tvEARelated.SelectedNode.Nodes)
                                     {
                                         if (node.Tag is EaAccess.ConnectorList)
                                         {
                                             var list = (EaAccess.ConnectorList)node.Tag;
                                             Element element = list.ObjectElement;

                                             if (list.ObjectType == "Package") continue;

                                             int imagenum =
                                                 EaCaliberGenEngine.GetImageIdUsingEaType(element.Type);
                                             var lviElement =
                                                 new ListViewItem(new[]
                                                                          {
                                                                              element.Name
                                                                          },
                                                                  imagenum)
                                                     {
                                                         Tag = list
                                                     };
                                             lvRelated.Items.Add(lviElement);
                                         }
                                         toolStripProgressBar.PerformStep();
                                     }
                                 }
                    );
            }
            else
            {
                #region Add Diagram Elements

                if (connList.ObjectType == "Diagram")
                {
                    if (ChildElementsView) // elements view
                    {
                        //
                        // Get Diagrams
                        //
                        List<EaAccess.DiagramStruct> listOfDiagrams =
                            new EaAccess().getDiagramList(connList.ObjectElementId);

                        UsingProgressBar(listOfDiagrams.Count,
                                         () =>
                                         {
                                             foreach (object item in listOfDiagrams)
                                             {
                                                 //var diagList = new EaAccess.diagramStruct();
                                                 var diagList = (EaAccess.DiagramStruct)item;

                                                 int imagenum =
                                                     EaCaliberGenEngine.GetImageIdUsingEaType(
                                                         diagList.DiagramType);
                                                 var lviElement = new ListViewItem(new[]
                                                                                           {
                                                                                               diagList.DiagramName
                                                                                           }, imagenum);

                                                 lviElement.Tag = item;
                                                 lvRelated.Items.Add(lviElement);
                                                 toolStripProgressBar.PerformStep();
                                             }
                                         });
                    }
                }
                #endregion

                #region Add Package Elements

                else if (connList.ObjectType == "Package")
                {
                    if (ChildElementsView) // elements view
                    {
                        UsingProgressBar(connList.ObjectPackage.Elements.Count,
                                         () =>
                                         {
                                             foreach (Element element in connList.ObjectPackage.Elements)
                                             {
                                                 int imagenum =
                                                     EaCaliberGenEngine.GetImageIdUsingEaType(element.Type);
                                                 var lviElement =
                                                     new ListViewItem(new[]
                                                                              {
                                                                                  element.Name
                                                                              },
                                                                      imagenum)
                                                         {
                                                             Tag = EaAccess.GetConnectorListForElement(element)
                                                         };
                                                 lvRelated.Items.Add(lviElement);
                                                 toolStripProgressBar.PerformStep();
                                             }
                                         });
                    }
                }
                #endregion

                #region Add Element items

                else if (connList.ObjectElement != null)
                {
                    var newItems = new SortedList<string, EaAccess.ConnectorList>(new StringComparer());
                    var newMethods = new SortedList<string, Method>(new NumberComparer());
                    var newAttributes = new SortedList<string, Attribute>(new NumberComparer());


                    int imagenum = 0;
                    var lviElement = new ListViewItem();

                    #region Add child elements

                    if (ChildElementsView)
                    {
                        try
                        {
                            var itemList = (from TreeNode node in tvEARelated.SelectedNode.Nodes
                                            orderby ((EaAccess.ConnectorList)node.Tag).ObjectName
                                            where node.Tag != null
                                                  && node.Tag is EaAccess.ConnectorList
                                            select new
                                                       {
                                                           ((EaAccess.ConnectorList)node.Tag).ObjectName,
                                                           ObjectGUID = ((EaAccess.ConnectorList)node.Tag).ObjectGuid,
                                                           node.Tag
                                                       }).Distinct();

                            UsingProgressBar(itemList.Count(),
                                             () =>
                                             {
                                                 foreach (var node in itemList)
                                                 {
                                                     var item = (EaAccess.ConnectorList)node.Tag;
                                                     imagenum =
                                                         EaCaliberGenEngine.GetImageIdUsingEaType(item.ObjectType);
                                                     lviElement = new ListViewItem(new[]
                                                                                           {
                                                                                               item.ObjectName
                                                                                           }, imagenum) { Tag = item };

                                                     lvRelated.Items.Add(lviElement);
                                                     toolStripProgressBar.PerformStep();
                                                 }
                                             });
                        }
                        catch (Exception)
                        {
                        }
                    }
                    #endregion

                    #region Add attributes

                    else if (ChildAttributesView)
                    {
                        var attribList = from Attribute attribute in connList.ObjectElement.Attributes
                                         orderby attribute.Pos
                                         select new { attribute.Pos, attribute };

                        UsingProgressBar(attribList.Count(),
                                         () =>
                                         {
                                             foreach (var item in attribList)
                                             {
                                                 imagenum = EaCaliberGenEngine.GetImageIdUsingEaType("Attribute");
                                                 lviElement = new ListViewItem(new[]
                                                                                       {
                                                                                           item.attribute.Name,
                                                                                           item.Pos.ToString()
                                                                                       }, imagenum) { Tag = (item.attribute) };

                                                 lvRelated.Items.Add(lviElement);
                                                 toolStripProgressBar.PerformStep();
                                             }
                                         });
                    }
                    #endregion

                    #region Add methods

                    else if (ChildMethodsView)
                    {
                        var methodList = from Method method in connList.ObjectElement.Methods
                                         orderby method.Pos
                                         select new { method.Pos, method };

                        UsingProgressBar(methodList.Count(),
                                         () =>
                                         {
                                             foreach (var item in methodList)
                                             {
                                                 imagenum =
                                                     EaCaliberGenEngine.GetImageIdUsingEaType(
                                                         "Operation");
                                                 lviElement = new ListViewItem(new[]
                                                                                       {
                                                                                           item.method.Name,
                                                                                           item.Pos.ToString()
                                                                                       }, imagenum) { Tag = (item.method) };

                                                 lvRelated.Items.Add(lviElement);
                                                 toolStripProgressBar.PerformStep();
                                             }
                                         });
                    }

                    #endregion
                }

                #endregion
            }
        }

        private void AddSelectedNodeChildren(bool expand)
        {
            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (!(tnSelected.Tag is EaAccess.ConnectorList)) return;

            var connList = (EaAccess.ConnectorList)tnSelected.Tag;

            if (connList.ObjectType == "Package" && tnSelected.Nodes.Count == 0)
            {
                AddPackageContents(connList.ObjectPackage, tnSelected, false);
            }
            else if (connList.ObjectType != "Diagram" && !NodeContainsLinksOrDiagrams(tnSelected)) // && connList.ConnectorType != null)
            {
                AddLinks(connList.ObjectElement, connList, tnSelected);
                //AddElement(connList.ObjectElement, tnSelected);
            }

            if (expand)
            {
                tnSelected.Expand();
            }

            UpdateScreenObjectInformation(connList);
        }

        private void UpdateScreenObjectInformation(EaAccess.ConnectorList connList)
        {
            txtObjectName.Text = connList.ObjectName;
            txtObjectStereotype.Text = connList.ObjectStereotype;
            txtObjectType.Text = connList.ObjectType;
            txtObjectModified.Text = connList.ObjectModified.ToShortDateString();
            txtObjectStatus.Text = connList.ObjectStatus;
            txtObjectNotes.Text = connList.ObjectNote;
            //txtEAGUID.Text = connList.ObjectGuid;

            txtFeatureType.Text = connList.To_ConnectorFeatureType;
            txtFeature.Text = connList.To_ConnectorFeature;
            txtConnectorType.Text = connList.ConnectorType;
            txtConnectorName.Text = connList.ConnectorName;

            //txtElementID.Text = connList.To_ElementID.ToString();
            txtElementType.Text = connList.To_ElementType;
            txtStereotype.Text = connList.To_ElementStereotype;
            txtToElementName.Text = connList.To_ElementName;

            //txtFromElementID.Text = connList.FromElementId.ToString();
            txtFromElementName.Text = connList.FromElementName;
            txtFromElementType.Text = connList.FromElementType;
            txtFromStereotype.Text = connList.FromElementStereotype;
        }


        //
        // Locate in Project browser
        // 

        private void LocateInBrowserTreeView(object sender, EventArgs e)
        {
            LocateInBrowser(tvEARelated.SelectedNode.Tag);
        }

        private void LocateInBrowserListView(object sender, EventArgs e)
        {
            LocateInBrowser(lvRelated.SelectedItems[0].Tag);
        }

        private static void LocateInBrowser(object tag)
        {
            var objElement = new object();

            EaAccess.ConnectorList connList;
            EaAccess.DiagramStruct diagStruct;

            if (tag is EaAccess.ConnectorList
                && !string.IsNullOrEmpty(((EaAccess.ConnectorList)tag).ObjectGuid)
                && ((EaAccess.ConnectorList)tag).ObjectType == "Package")
            {
                connList =
                    (EaAccess.ConnectorList)tag;

                objElement = AddInRepository.Instance.Repository.GetPackageByGuid(connList.ObjectGuid);
            }
            else if (tag is EaAccess.ConnectorList
                && !string.IsNullOrEmpty(((EaAccess.ConnectorList)tag).ObjectGuid))
            {
                connList =
                    (EaAccess.ConnectorList)tag;

                objElement = AddInRepository.Instance.Repository.GetElementByGuid(connList.ObjectGuid);
            }
            else
            {
                if (tag is EaAccess.DiagramStruct
                    && !string.IsNullOrEmpty(((EaAccess.DiagramStruct)tag).DiagramEA_GUID))
                {
                    diagStruct =
                        (EaAccess.DiagramStruct)tag;

                    objElement = AddInRepository.Instance.Repository.GetDiagramByGuid(diagStruct.DiagramEA_GUID);
                }
            }


            // Find element in project browser

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

        // -----------------------------------------------------------
        //  Place in Diagram
        // -----------------------------------------------------------
        private void placeInDiagramTreeView(object sender, EventArgs e)
        {
            placeInDiagram(new List<object> { tvEARelated.SelectedNode.Tag });
        }

        private void placeInDiagramListView(object sender, EventArgs e)
        {
            IEnumerable<object> tags = from ListViewItem s in lvRelated.SelectedItems
                                       select s.Tag;

            placeInDiagram(tags);
        }

        private void deleteListViewItemsFromEA(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Are you sure you want to delete these items?  This cannot be undone.", "Confirm Delete",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                IEnumerable<object> tags = from ListViewItem s in lvRelated.SelectedItems
                                           select s.Tag;

                if (deleteFromEA(tags))
                {
                    refreshHierarchy();
                }
            }
        }
        //private static void placeInDiagram(IEnumerable<object> tags)
        //{
        //    placeInDiagram(tags, true)
        //}

        private static void placeInDiagram(IEnumerable<object> tags, bool save = true)
        {
            Cursor.Current = Cursors.WaitCursor;
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            foreach (object tag in tags)
            {
                if (!(tag is EaAccess.ConnectorList)) continue;

                var connList = (EaAccess.ConnectorList)tag;

                // Get selected element from current diagram


                if (diag == null)
                    return;

                int elementID = Convert.ToInt32(connList.ObjectElementId);

                //
                // Check if the element selected is already placed
                //
                bool elementAlreadyPlaced = false;
                foreach (DiagramObject diagobj in diag.DiagramObjects)
                {
                    if (diagobj.ElementID == elementID)
                    {
                        elementAlreadyPlaced = true;
                    }
                }

                if (!elementAlreadyPlaced)
                {
                    object objElement = diag.DiagramObjects.AddNew("", "");
                    var diagObject = (DiagramObject)objElement;


                    if (elementID > 0)
                    {
                        diagObject.ElementID = elementID;
                        diagObject.Update();
                    }
                }
            }
            if (save)
            {
                diag.DiagramObjects.Refresh();
                diag.Update();
                AddInRepository.Instance.Repository.SaveDiagram(diag.DiagramID);
                AddInRepository.Instance.Repository.ReloadDiagram(diag.DiagramID);
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void StraightenDiagramLinks(object sender, EventArgs e)
        {
            Diagram diagram = AddInRepository.Instance.Repository.GetCurrentDiagram();

            foreach (DiagramLink link in diagram.DiagramLinks)
            {
                string[] tokens = link.Style.Split(new[] { '=', ';' });

                tokens[1] = "3";
                string style = string.Empty;

                for (int i = 0; i < tokens.Count(); i++)
                {
                    style += tokens[i];
                    i++;

                    if (i < tokens.Count())
                    {
                        style += "=" + tokens[i] + ";";
                    }
                }
                link.Style = style;
            }
        }

        private bool deleteFromEA(IEnumerable<object> tags)
        {
            bool itemsDeleted = false;

            Cursor.Current = Cursors.WaitCursor;

            UsingProgressBar(tags.Count(),
                             () =>
                             {
                                 foreach (object tag in tags)
                                 {
                                     if (!(tag is EaAccess.ConnectorList)) continue;

                                     var connList = (EaAccess.ConnectorList)tag;

                                     Element element = connList.ObjectElement;

                                     Package package =
                                         AddInRepository.Instance.Repository.GetPackageByID(element.PackageID);

                                     short i = 0;
                                     bool found = false;
                                     foreach (Element packageElement in package.Elements)
                                     {
                                         if (element.ElementGUID == packageElement.ElementGUID)
                                         {
                                             found = true;
                                             break;
                                         }
                                         i++;
                                     }
                                     if (found)
                                     {
                                         package.Elements.DeleteAt(i, false);
                                         itemsDeleted = true;
                                     }
                                     toolStripProgressBar.PerformStep();
                                 }
                             });

            Cursor.Current = Cursors.Arrow;
            return itemsDeleted;
        }

        // ----------------------------------------------------------------
        //         Unselect check boxes
        // ----------------------------------------------------------------

        private void openDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDiagram();
        }

        private void openDiagram()
        {
            // Get selected node
            TreeNode tnSelected = tvEARelated.SelectedNode;

            // Cast element
            EaAccess.DiagramStruct diag;

            if (tnSelected.Tag is EaAccess.DiagramStruct)
            {
                diag =
                    (EaAccess.DiagramStruct)tnSelected.Tag;
                AddInRepository.Instance.Repository.OpenDiagram(diag.Diagram_ID);
                
            }
        }

        private void expandOneLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSelectedNodeChildren(true);
        }

        private void ChangedAfterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerChangedAfter.Enabled = ChangedAfterCheckBox.Checked;
        }

        private void ChangedBeforeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerChangedBefore.Enabled = ChangedBeforeCheckBox.Checked;
        }

        private void ChildrenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ChangedAfterCheckBox.Enabled = LoadHierarchyCheckBox.Checked;
            ChangedBeforeCheckBox.Enabled = LoadHierarchyCheckBox.Checked;
            StatusCheckBox.Enabled = LoadHierarchyCheckBox.Checked;
            UnlinkedCheckBox.Enabled = LoadHierarchyCheckBox.Checked;
            NotUsedInDiagramsCheckBox.Enabled = LoadHierarchyCheckBox.Checked;

            if (!LoadHierarchyCheckBox.Checked)
            {
                dateTimePickerChangedAfter.Enabled = false;
                dateTimePickerChangedBefore.Enabled = false;
                StatusComboBox.Enabled = false;
            }
            else
            {
                dateTimePickerChangedAfter.Enabled = ChangedAfterCheckBox.Checked;
                dateTimePickerChangedBefore.Enabled = ChangedBeforeCheckBox.Checked;
                StatusComboBox.Enabled = StatusCheckBox.Checked;
            }
        }

        private void StatusCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            StatusComboBox.Enabled = StatusCheckBox.Checked;
        }

        private void CancelToolStripButton_Click(object sender, EventArgs e)
        {
            PerformOperation = false;
        }

        private void ShowLinksCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cbxShowDiagrams.Enabled = ShowLinksCheckBox.Checked;
            //cbence.Enabled = ShowLinksCheckBox.Checked;
            cbxShowDiagrams.Enabled = ShowLinksCheckBox.Checked;
            cbxCalls.Enabled = ShowLinksCheckBox.Checked;
            cbxCalledBy.Enabled = ShowLinksCheckBox.Checked;
        }

        private void LastExceptionLablel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lastException);
        }

        private void ViewComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChildItemsListView();
        }

        private void MoveSavedRelatedItems()
        {
            moveMode = false;

            if (lvRelated.SelectedIndices.Count > 0)
            {
                int start = lvRelated.SelectedIndices[0];
                ListViewItem startItem = lvRelated.Items[start];

                int first = lvRelated.SelectedIndices[0] < attributesToMove.Keys[0]
                                ? lvRelated.SelectedIndices[0]
                                : attributesToMove.Keys[0];

                //remove items from listview

                foreach (var pair in attributesToMove)
                {
                    lvRelated.Items.Remove(pair.Value);
                }

                //add them after the start item
                start = lvRelated.Items.IndexOf(startItem) + 1;

                foreach (var pair in attributesToMove)
                {
                    lvRelated.Items.Insert(start, pair.Value);
                    start++;
                }

                //update the indexes from the start to the finish

                int last = attributesToMove.Keys[attributesToMove.Count - 1] >
                           start
                               ? attributesToMove.Keys[attributesToMove.Count - 1]
                               : start;

                if (last > lvRelated.Items.Count - 1)
                {
                    last = lvRelated.Items.Count - 1;
                }

                UsingProgressBar(last - first, () =>
                                                   {
                                                       for (int i = first; i <= last; i++)
                                                       {
                                                           if (lvRelated.Items[i].Tag is Attribute)
                                                           {
                                                               var attribute = (Attribute)lvRelated.Items[i].Tag;

                                                               attribute.Pos = i;
                                                               attribute.Update();
                                                           }

                                                           toolStripProgressBar.PerformStep();
                                                       }

                                                       ((EaAccess.ConnectorList)tvEARelated.SelectedNode.Tag).
                                                           ObjectElement.Refresh();
                                                       ((EaAccess.ConnectorList)tvEARelated.SelectedNode.Tag).
                                                           ObjectElement.Update();
                                                   });
                //toolStripProgressBar.Visible = false;
            }
        }

        private void InitialiseProgressBarX(int maximum)
        {
            toolStripProgressBar.Value = 0;
            toolStripProgressBar.Maximum = maximum;
            toolStripProgressBar.Visible = true;
        }

        private void UsingProgressBar(int maximum, Action doMethod)
        {
            toolStripProgressBar.Value = 0;
            toolStripProgressBar.Maximum = maximum;
            toolStripProgressBar.Visible = true;

            //InitialiseProgressBar(maximum);

            doMethod();

            toolStripProgressBar.Visible = false;
        }

        private void SaveRelatedItemsSelection()
        {
            if (lvRelated.SelectedIndices.Count > 0)
            {
                moveMode = true;

                TreeNode node = tvEARelated.SelectedNode;

                if (node.Tag is EaAccess.ConnectorList)
                {
                    attributesToMove.Clear();


                    foreach (int index in lvRelated.SelectedIndices)
                    {
                        attributesToMove.Add(index, lvRelated.Items[index]);
                    }
                }

                string message = lvRelated.SelectedIndices.Count > 1
                                     ?
                                         "These items are about to be moved.  Please select the item to move these after and click 'Move After'."
                                     :
                                         "This item is about to be moved.  Please select the item to move these after and click 'Move After'.";

                MessageBox.Show(message);
            }
        }

        private void ConnectorTypesPickerButton_Click(object sender, EventArgs e)
        {
            var picker = new ConnectorTypesPicker();

            picker.ConnectorTypes = connectorTypes;

            picker.ShowDialog(this);

            if (picker.DialogResult == DialogResult.OK)
            {
                connectorTypes = picker.ConnectorTypes;

                ConnectorTypesTextBox.Text = BuildStringFromList(connectorTypes);
            }
        }

        private static string BuildStringFromList(List<string> items)
        {
            items.Sort();

            if (items.Count == 0) return string.Empty;

            string result = items[0];

            for (int i = 1; i < items.Count; i++)
            {
                result += ", " + items[i];
            }
            return result;
        }

        private void ApplyStatusChangeButton_Click(object sender, EventArgs e)
        {
            ApplyUpdateStatus();
        }

        private void ApplyStereotypeChangeButton_Click(object sender, EventArgs e)
        {
            ApplyUpdateStereotype();
        }

        private void DEEWREATreeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.HierarchyChildView = ViewComboBox.Text;
            Settings.Default.Save();
        }

        private void lvRelated_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12 && moveMode)
            {
                MoveSavedRelatedItems();
            }
            else if (e.KeyCode == Keys.F12 && !moveMode)
            {
                SaveRelatedItemsSelection();
            }
        }

        private void lvRelated_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            MoveReleatedItemsButton.Visible = lvRelated.SelectedIndices.Count > 0;

            lvRelated.ContextMenuStrip = BuildRelatedItemsContextMenu();
        }

        private void MoveReleatedItemsButton_Click(object sender, EventArgs e)
        {
            if (MoveReleatedItemsButton.Text == "Move")
            {
                SaveRelatedItemsSelection();
                MoveReleatedItemsButton.Text = "Move After";
            }
            else
            {
                MoveSavedRelatedItems();
                MoveReleatedItemsButton.Text = "Move";
                MoveReleatedItemsButton.Visible = false;
            }
        }

        private void comboBoxProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProfile.Text == "Structure Chart")
            {
                cbxCalls.Checked = true;
                cbxCalledBy.Checked = false;
                cbxShowDiagrams.Checked = false;
                connectorTypes = new List<string> { "Association" };
                ConnectorTypesTextBox.Text = BuildStringFromList(connectorTypes);
                cbxLoadRelatedOnSelect.Checked = false;
                ViewComboBox.Text = "Elements";
                cbxDisplayElementInfo.Checked = false;
                cbxDisplayElementInfo_CheckedChanged(null, null);
            }
            else if (comboBoxProfile.Text == "Elements not used in diagrams")
            {
                ShowLinksCheckBox.Checked = false;
                ShowLinksCheckBox_CheckedChanged(null, null);

                LoadHierarchyCheckBox.Checked = true;
                ChildrenCheckBox_CheckedChanged(null, null);

                NotUsedInDiagramsCheckBox.Checked = true;

                ViewComboBox.Text = "Elements";
                cbxDisplayElementInfo.Checked = false;
                cbxDisplayElementInfo_CheckedChanged(null, null);
            }
        }

        private void cbxDisplayElementInfo_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !cbxDisplayElementInfo.Checked;
        }

        #region Tree View Methods

        private void tvEARelated_DoubleClick(object sender, EventArgs e)
        {
            openDiagram();
        }

        private void tvEARelated_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BuildTreeViewContextMenu();

            if (tvEARelated.SelectedNode.Nodes.Count == 0 ||
                SelectedNodeConnectorNodes() == 0 && tvEARelated.SelectedNode.Text != "Diagrams" ||
                (LoadHierarchyCheckBox.Checked
                && !NodeContainsLinksOrDiagrams(tvEARelated.SelectedNode)
                && tvEARelated.SelectedNode.Text != "Diagrams"))
            {
                if (cbxLoadRelatedOnSelect.Checked)
                {
                    AddSelectedNodeChildren(cbxAutoExpand.Checked);
                    if (tvEARelated.SelectedNode != null)
                    {
                        tvEARelated.SelectedNode.Expand();
                    }
                }
                else
                {
                    tvEARelated.SelectedNode.Nodes.Add(new TreeNode("dummy"));
                    tvEARelated.SelectedNode.Collapse();
                }
            }
            if (tvEARelated.SelectedNode != null &&
                tvEARelated.SelectedNode.Tag != null &&
                tvEARelated.SelectedNode.Tag is EaAccess.ConnectorList)
            {
                if (cbxLoadRelatedOnSelect.Checked ||
                    (tvEARelated.SelectedNode.Nodes.Count > 0 && tvEARelated.SelectedNode.Nodes[0].Text != "dummy"))
                {
                    UpdateChildItemsListView();
                }
                UpdateScreenObjectInformation((EaAccess.ConnectorList)tvEARelated.SelectedNode.Tag);
            }
        }

        private int SelectedNodeConnectorNodes()
        {
            int result = 0;

            foreach (TreeNode node in tvEARelated.SelectedNode.Nodes)
            {
                if (node.Tag != null
                    && node.Tag is EaAccess.ConnectorList
                    && ((EaAccess.ConnectorList)node.Tag).ConnectorId != 0)
                {
                    result++;
                }
            }
            return result;
        }

        private bool NodeContainsLinksOrDiagrams(TreeNode treeNode)
        {
            var linkFound = false;
            var diagramsNodeFound = false;

            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)node.Tag;

                    if (node.Text == "Diagrams")
                    {
                        diagramsNodeFound = true;
                    }
                    if (!string.IsNullOrEmpty(connector.ConnectorType))
                    {
                        linkFound = true;
                    }
                }
            }
            return linkFound || diagramsNodeFound;
        }

        private void tvEARelated_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (!cbxLoadRelatedOnSelect.Checked &&
                e.Node.Nodes.Count == 1 &&
                e.Node.Nodes[0].Text == "dummy")
            {
                tvEARelated.SelectedNode = e.Node;
                tvEARelated.SelectedNode.Nodes.RemoveAt(0);
                AddSelectedNodeChildren(false);
                if (tvEARelated.SelectedNode.Nodes.Count > 0)
                {
                    tvEARelated_AfterSelect(sender, e);
                }
            }
        }

        #endregion

        private void StereoTypesTextBox_Validated(object sender, EventArgs e)
        {
            stereotypes = StereoTypesTextBox.Text.Split(',');

            UpdateIncludedAndExcludedStereotypes();
        }

        private void UpdateIncludedAndExcludedStereotypes()
        {
            if (IncludeStereotypesRadioButton.Checked)
            {
                includedStereotypes = stereotypes;
                excludedStereotypes = null;
            }
            else
            {
                includedStereotypes = null;
                excludedStereotypes = stereotypes;
            }
        }

        private void StereotypesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateIncludedAndExcludedStereotypes();
        }

        private void addAttributeStereotypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tnSelected = tvEARelated.SelectedNode;

            if (tnSelected != null)
            {
                if (tnSelected.Tag is EaAccess.ConnectorList)
                {
                    var connector = (EaAccess.ConnectorList)tnSelected.Tag;

                    if (connector.ObjectElement != null && connector.ObjectType == "Class")
                    {
                        var element = connector.ObjectElement;

                        var selectStereotype = new SelectStereotype();

                        var result = selectStereotype.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            foreach (Attribute attribute in element.Attributes)
                            {
                                if (selectStereotype.AppendExisitngStereotypes)
                                {
                                    attribute.StereotypeEx = attribute.StereotypeEx + "," +
                                                             selectStereotype.SelectedStereotype;
                                }
                                else
                                {
                                    attribute.StereotypeEx = selectStereotype.SelectedStereotype;
                                }
                                attribute.Stereotype = selectStereotype.SelectedStereotype;
                                attribute.Update();
                            }
                        }

                    }
                }
            }
        }


    }

    public class NumberComparer : IComparer<string>
    {
        #region IComparer<string> Members

        public int Compare(string x, string y)
        {
            if (Convert.ToInt32(x) <= Convert.ToInt32(y)) return -1;

            return 1;
        }

        #endregion
    }

    public class StringComparer : IComparer<string>
    {
        #region IComparer<string> Members

        public int Compare(string x, string y)
        {
            return string.Compare(x, y);
        }

        #endregion
    }
}