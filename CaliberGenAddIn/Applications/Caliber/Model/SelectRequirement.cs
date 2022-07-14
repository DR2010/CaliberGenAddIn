using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications;
using Starbase.CaliberRM.Interop;
using Collection=Starbase.CaliberRM.Interop.Collection;
using IProject=Starbase.CaliberRM.Interop.IProject;
using IRequirement=Starbase.CaliberRM.Interop.IRequirement;
using TreeNode=System.Windows.Forms.TreeNode;
using EAAddIn.Windows;

namespace EAAddIn
{
    public class SelectRequirements
    {
        private const string COL_CAIBERISMAPPED = "CaliberIsMapped";
        private const string COL_CALIBERCREATEDBY = "CreatedBy";
        private const string COL_CALIBERDERIVEDTYPE = "DerivedType";
        private const string COL_CALIBERFULLDESCRIPTION = "CaliberFullDescription";
        private const string COL_CALIBERHANDLE = "CaliberHandle";
        private const string COL_CALIBERHIERARCHY = "CaliberHierarchy";
        private const string COL_CALIBERID = "CaliberId";
        private const string COL_CALIBERPARENTID = "CaliberParentId";
        private const string COL_CALIBERPROJECT = "CaliberProject";
        private const string COL_CALIBERREQUIREMENT = "CaliberRequirement";
        private const string COL_CALIBERSOURCEID = "SourceCaliberID";
        private const string COL_CALIBERSOURCEPROJECTNAME = "CaliberSourceProjectName";
        private const string COL_CALIBERSOURCEREQUIREMENTVERSION = "CaliberSourceRequirementVersion";
        private const string COL_CALIBERSTATUS = "CaliberStatus";
        private const string COL_CALIBERTYPE = "CaliberType";
        private const string COL_EAELEMENT_NAME = "EAElementName";
        private const string COL_EAELEMENTIF = "EAElementID";
        private const string COL_EAELEMENTTYPE = "EAElementType";
        private const string COL_EAGUID = "EA_GUID";
        private const string COL_EASTATUS = "EaStatus";
        private const string COL_EASTEREOTYPE = "EAStereotype";
        private const string COL_LEVEL = "Level";
        private const string COL_LOADEDEA = "LoadedEA";
        private const string COL_MAPTABLEUNIQUEID = "MapTableUniqueID";
        private const string COL_PARENTID = "ParentID";
        private const string COL_REALCALIBERID = "RealCaliberID";
        private const string COL_SYNC_STATUS = "SyncStatus";
        private const string COL_UI_ELEM_PROP = "UIProperties";

        public static bool isBranched;
        public static bool isMapped;
        public static bool screenselected;
        private readonly IProject caliberproject;
        private readonly CaliberImportForm mainform;
        private readonly SelectorSerialTag selector = new SelectorSerialTag();
        public Repository m_Repository;

        // Dictionary of requirement attributes (keyed by name)
        //IDictionary<string, IAttributeValue> attributeDictionary;

        // Reference to the attached requirement
        private IRequirement requirement;
        public DataTable selectedItems = new DataTable("CaliberSelection");

        public SelectRequirements()
        {
            //InitializeComponent();
            selectedItems.Columns.Add(COL_LOADEDEA);
            selectedItems.Columns.Add(COL_CALIBERHIERARCHY);
            selectedItems.Columns.Add(COL_CALIBERSTATUS);
            selectedItems.Columns.Add(COL_CALIBERDERIVEDTYPE);
            selectedItems.Columns.Add(COL_CALIBERREQUIREMENT);
            selectedItems.Columns.Add(COL_CALIBERID);
            selectedItems.Columns.Add(COL_REALCALIBERID);
            selectedItems.Columns.Add(COL_CALIBERSOURCEID);
            selectedItems.Columns.Add(COL_CALIBERFULLDESCRIPTION);
            selectedItems.Columns.Add(COL_CALIBERPROJECT);
            selectedItems.Columns.Add(COL_CALIBERCREATEDBY);
            selectedItems.Columns.Add(COL_CALIBERTYPE);
            selectedItems.Columns.Add(COL_CAIBERISMAPPED);

            selectedItems.Columns.Add(COL_MAPTABLEUNIQUEID);
            selectedItems.Columns.Add(COL_EAGUID);
            selectedItems.Columns.Add(COL_EAELEMENTTYPE);
            selectedItems.Columns.Add(COL_EAELEMENTIF);
            selectedItems.Columns.Add(COL_EASTEREOTYPE);
            selectedItems.Columns.Add(COL_EASTATUS);
            selectedItems.Columns.Add(COL_EAELEMENT_NAME);
            selectedItems.Columns.Add(COL_LEVEL);
            selectedItems.Columns.Add(COL_SYNC_STATUS);
            selectedItems.Columns.Add(COL_UI_ELEM_PROP);

            selectedItems.Columns.Add(COL_CALIBERPARENTID);
            selectedItems.Columns.Add(COL_CALIBERHANDLE);
            selectedItems.Columns.Add(COL_CALIBERSOURCEPROJECTNAME);
            selectedItems.Columns.Add(COL_CALIBERSOURCEREQUIREMENTVERSION);
        }

        public SelectRequirements(IProject project, CaliberImportForm mainform, bool sortAlphabetically)
            : this()
        {
            this.mainform = mainform;
            //this.m_Repository = rep;

            var root = (IRequirementTreeNode) project.CurrentBaseline.RequirementTree.Root;

            caliberproject = project;

            var nodes = from IRequirementTreeNode c in root.Children
                        orderby c.Name
                        select c;

            foreach (var node in nodes)
            {
                AddNodeToTree(node, sortAlphabetically);
                
            }
        }

        /// <summary>
        /// Adds a IRequirementTreeNode and its children to the tree view
        /// </summary>
        /// <param name="node"></param>
        private void AddNodeToTree(IRequirementTreeNode node, bool sortAlphabetically)
        {
            TreeNode newNode = CreateTreeNode(node);
            AddChildNodeToTree(newNode, node, sortAlphabetically);
            mainform.treeViewCaliber.Nodes.Add(newNode);
        }

        private void AddChildNodeToTree(TreeNode parent, IRequirementTreeNode node, bool sortAlphabetically)
        {
            if (sortAlphabetically)
            {
                var nodes = from IRequirementTreeNode c in node.Children
                            orderby c.Name
                            select c;

                foreach (var child in nodes)
                {
                    TreeNode childTreeNode = CreateTreeNode(child);
                    childTreeNode.Tag = child.AssociatedObjectID;
                    parent.Nodes.Add(childTreeNode);
                    AddChildNodeToTree(childTreeNode, child, sortAlphabetically);
                }
            }
            else
            {
                foreach (IRequirementTreeNode child in node.Children)
                {
                    TreeNode childTreeNode = CreateTreeNode(child);
                    childTreeNode.Tag = child.AssociatedObjectID;
                    parent.Nodes.Add(childTreeNode);
                    AddChildNodeToTree(childTreeNode, child, sortAlphabetically);
                }

            }

        }


        private TreeNode CreateTreeNode(IRequirementTreeNode node)
        {
            TreeNode newNode;

            if (node.RequirementNode)
            {
                newNode = new TreeNode(node.Name + " " + node.SerialNumberTag, 0, 0);
                newNode.Tag = node.SerialNumberTag;
            }
            else
            {
                newNode = new TreeNode(node.Name, 1, 1);
            }

            newNode.Checked = false;
            return newNode;
        }


        public void AddTreeNodeToSelector(TreeNode node, SelectorSerialTag selector, DataTable dt, Action updateProgress)
        {
            updateProgress();

            if (node.Tag != null && node.Checked)
            {
                // selector.AddSerialTag((String)node.Tag);
                AddToDataTable(node, dt, m_Repository);
            }

            foreach (TreeNode child in node.Nodes)
            {
                AddTreeNodeToSelector(child, selector, dt, updateProgress);
            }
        }

        private void tvwRequirements_AfterCheck(object sender, TreeViewEventArgs e)
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


        /// <summary>
        /// Branches the requirement associated with the passed node if is it eligible then checks all
        /// children
        /// </summary>
        /// <param name="node"></param>
        /// <param name="target"></param>
        private void SelectEligibleRequirement(IRequirementTreeNode node)
        {
            if (isNodeRequired(node))
                //   target.BranchRequirement(node, true);

                foreach (IRequirementTreeNode childNode in node.Children)
                {
                    SelectEligibleRequirement(childNode);
                }
        }

        /// <summary>
        /// Examines a req tree node to determine if it will be branched or not
        /// </summary>
        /// <returns></returns>
        private bool isNodeRequired(IRequirementTreeNode node)
        {
            if (node.ProjectNode || node.RequirementTypeNode)
                return false;

            if (selector == null)
            {
                return true;
            }
            else
            {
                return selector.IsSelected(node);
            }
        }

        /// <summary>
        /// Given a req tree node returns the associated req object
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected IRequirement FetchRequirement(TreeNode node)
        {
            if (node.Tag != null)
            {
                if (caliberproject.Session.get((CaliberObjectID) node.Tag) is IRequirement)
                    return (IRequirement) caliberproject.Session.get((CaliberObjectID) node.Tag);
                else
                    return null;
            }
            return null;
        }

        //protected IRequirement FetchDeletedRequirement(System.Windows.Forms.TreeNode node)
        //{
        //    if (node.Tag != null)
        //    {
        //        if (caliberproject.Session.get((CaliberObjectID)node.Tag) is IRequirement)
        //            return (IRequirement)caliberproject.d.Session.get((CaliberObjectID)node.Tag);
        //        else
        //            return null;
        //    }
        //    return null;

        //}


        private void AddToDataTable(TreeNode node, DataTable dt,
                                    Repository m_Repository)
        {
            DataRow dr = dt.NewRow();
            string description = String.Empty;
            IRequirement req = FetchRequirement(node);
            IRequirement parentreq = null;
            IRequirement parentparentreq = null;
            if (node.Parent != null)
            {
                parentreq = FetchRequirement(node.Parent);
            }

            if (node.Parent.Parent != null)
            {
                parentparentreq = FetchRequirement(node.Parent.Parent);
            }


            if (req != null)
            {
                dr[COL_CALIBERTYPE] = node.Parent;
                dr[COL_LEVEL] = node.Level;
                dr[COL_CALIBERPARENTID] = node.Parent.Handle;
                dr[COL_CALIBERHANDLE] = node.Handle;
                dr[COL_CALIBERPROJECT] = caliberproject.Name;

                var exReq = new ExtendedRequirement(req);
                var exParentReq = new ExtendedRequirement(parentreq);
                var exParentParentReq = new ExtendedRequirement(parentparentreq);
                IRequirementID mapID;

                dr[COL_CALIBERID] = exReq.Requirement.IDNumber;
                dr[COL_REALCALIBERID] = exReq.Requirement.IDNumber;

                dr[COL_CAIBERISMAPPED] = false;

                try
                {
                    //if the requirement has been mapped the mapid will return the original ID, otherwise 
                    //the typecast will fail
                    mapID = ((IRequirementDescriptionMapped) req.Description).MappedDescriptionRequirementID;
                    dr[COL_CALIBERID] = mapID.IDNumber;
                    dr[COL_CAIBERISMAPPED] = true;
                    isMapped = true;
                    MessageBox.Show("Map ID = " + mapID.IDNumber.ToString() + "\n" +
                                    "Req ID = " + req.IDNumber.ToString() + "\n" +
                                    "Req Name = " + req.Name, "Requirement is Mapped");
                    return;

                }
                catch
                {
                    //MessageBox.Show("not mapped");
                }


                // Retrieve UI element properties 
                if (node.FullPath.Substring(0, 3) == EaCaliberGenEngine.constCaliberUIObject)
                {
                    dr[COL_UI_ELEM_PROP] = GetElementProperties(exReq.Requirement.IDNumber.ToString());
                }

                if (exParentReq.Requirement != null)
                {
                    //if the parent requirement has been branched we need to use the source parent id
                    if (!exParentReq.IsNew)
                    {
                        dr[COL_PARENTID] = exParentReq.SourceRequirementID;
                        if (node.Parent.Text.Substring(0, 8) == EaCaliberGenEngine.TYPE_ELEMENTS)
                            dr[COL_PARENTID] = exParentParentReq.SourceRequirementID;
                    }

                    else
                    {
                        dr[COL_PARENTID] = exParentReq.Requirement.IDNumber;
                        //special rule for UI only
                        //If the parent is an element - take the parentid for the ParentID
                        if (node.Parent.Text.Substring(0, 8) == EaCaliberGenEngine.TYPE_ELEMENTS &&
                            exParentParentReq.Requirement != null)
                        {
                            dr[COL_PARENTID] = exParentParentReq.Requirement.IDNumber;
                        }
                    }
                }


                dr[COL_CALIBERREQUIREMENT] = FormatRequirement(req, exReq, node); //caliberRequirement;

                //branched requirements are not New
                if (!exReq.IsNew)
                {
                    dr[COL_CALIBERSOURCEID] = exReq.SourceRequirementID;
                    if (exParentReq.Requirement != null)
                        dr[COL_PARENTID] = exParentReq.SourceRequirementID;

                    //in some cases the parent id is 0, then we have to use the Caliberid for the parent
                    if (dr[COL_PARENTID].ToString() == "0")
                        dr[COL_PARENTID] = exParentReq.Requirement.IDNumber;

                    //special case for UI if the parent is an Element
                    //in this case the parent id needs to be taken from the parent.parent (the screen id)
                    if (node.Parent.Text.Substring(0, 8) == EaCaliberGenEngine.TYPE_ELEMENTS &&
                        exParentParentReq.Requirement != null)
                    {
                        dr[COL_PARENTID] = exParentParentReq.SourceRequirementID;

                        if (dr[COL_PARENTID].ToString() == "0")
                            dr[COL_PARENTID] = exParentParentReq.Requirement.IDNumber;
                    }

                    //overwrite CaliberId with Source ID, if the element has been branched
                    dr[COL_CALIBERID] = exReq.SourceRequirementID;

                    dr[COL_CALIBERSOURCEPROJECTNAME] = exReq.SourceProjectName;
                    dr[COL_CALIBERSOURCEREQUIREMENTVERSION] = exReq.SourceRequirementVersion;
                    isBranched = true;
                }

                //check needs to be done after the branched requirements check is done
                if (ItemExistsInEA(dr[COL_CALIBERID].ToString(), m_Repository))
                    dr[COL_LOADEDEA] = true;
                else
                    dr[COL_LOADEDEA] = false;

                dr[COL_CALIBERDERIVEDTYPE] = GetType(req, exReq, node, dr);
                dr[COL_CALIBERSTATUS] = exReq.Requirement.Status.SelectedValue.ToString();
                if (exReq.Requirement.Owner != null)
                    dr[COL_CALIBERCREATEDBY] = exReq.Requirement.Owner.UserIDString;

                dr[COL_CALIBERFULLDESCRIPTION] = exReq.Requirement.Description.Text;

                //only add rows if they are a requirement
                try
                {
                    dt.Rows.Add(dr);
                }
                catch (ConstraintException ex)
                {
                    MessageBox.Show(
                        dr[COL_CALIBERREQUIREMENT] + " has a duplicate Caliber ID (" + dr[COL_CALIBERID] +
                        ") and WILL NOT BE IMPORTED." + Environment.NewLine + Environment.NewLine +
                        "This occurs when a branched requirement has been copied and reused." + Environment.NewLine +
                        Environment.NewLine +
                        "Please reset the Branch Merge attributes of the copied requirement to 'Initial Value' and try again." +
                        Environment.NewLine + Environment.NewLine +
                        "If you are unable to correct this problem yourself, please contact the BARM Team for assistance. " +
                        Environment.NewLine + Environment.NewLine +
                        "Full error: " + ex.Message
                        , "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private string FormatRequirement(IRequirement req, ExtendedRequirement exReq, TreeNode node)
        {
            IRequirement reqType = req;
            ExtendedRequirement exReqType = exReq;
            TreeNode nodeType = node;

            string caliberId = exReqType.Requirement.IDNumber.ToString();
            string caliberRequirement = nodeType.Text;

            //don't format for DesginRules - return text with CaliberID
            if (nodeType.Text.Substring(0, 2) == EaCaliberGenEngine.TYPE_DR)
            {
                caliberRequirement = caliberRequirement.Remove(caliberId.Length + 2).Trim();
                if (exReq.Requirement.Status.SelectedValue.ToString() == "Deleted")
                    caliberRequirement = caliberRequirement + " (Deleted)";
                return caliberRequirement;
            }

                //add Caliberid for UI messages
            else if (String.Compare(nodeType.Text.Substring(0, 7),EaCaliberGenEngine.TYPE_UIMESSAGE, true) == 0)
        
            {
                return caliberRequirement;
            }

            else
            {
                caliberRequirement = Regex.Replace(caliberRequirement, caliberId, "");
                int lengthCaliberString = caliberRequirement.Length;

                //can be BDO, UC or AC at the end
                bool isBDO = caliberRequirement.EndsWith("BDO");
                if (isBDO)
                    caliberRequirement = caliberRequirement.Remove(caliberRequirement.Length - 3);
                else
                    caliberRequirement = caliberRequirement.Remove(caliberRequirement.Length - 2);

                //Remove any spaces at the end
                caliberRequirement = caliberRequirement.Trim();
            }
            return caliberRequirement;
        }

        private string GetType(IRequirement req, ExtendedRequirement exReq, TreeNode node, DataRow dr)
        {
            string caliberType = string.Empty;

            IRequirement reqType = req;
            ExtendedRequirement exReqType = exReq;

            // IRequirementID mapID;


            TreeNode nodeType = node;
            DataRow drType = dr;

            switch (node.FullPath.Substring(0, 3))
            {
                case "04.":
                    caliberType = EaCaliberGenEngine.Derivedtype.Usecase.ToString();
                    break;
                case "03.":
                    caliberType = EaCaliberGenEngine.Derivedtype.Actor.ToString();
                    break;
                case "06.":
                    caliberType = EaCaliberGenEngine.Derivedtype.Bdotype.ToString();
                    break;
                case "11.":
                    caliberType = EaCaliberGenEngine.Derivedtype.UI.ToString();
                    break;
                default:
                    break;
            }

            if (caliberType == EaCaliberGenEngine.Derivedtype.Usecase.ToString() ||
                caliberType == EaCaliberGenEngine.Derivedtype.Actor.ToString() ||
                caliberType == EaCaliberGenEngine.Derivedtype.Bdotype.ToString())
            {
                //set to true, to avoid displaying error message for UI
                screenselected = true;
            }

            #region Derive Types for Actor
            if (caliberType == EaCaliberGenEngine.Derivedtype.Actor.ToString())
            {
                caliberType = DeriveCaliberTypeForActor(exReq, caliberType);
            }

            #endregion

            #region Derive Types for Use Cases

            if (caliberType == EaCaliberGenEngine.Derivedtype.Usecase.ToString())
            {
                caliberType = DeriveCaliberTypeForUseCase(caliberType, nodeType);
            }

            #endregion

            #region Derive Types for BDOs

            if (caliberType == EaCaliberGenEngine.Derivedtype.Bdotype.ToString())
            {
                caliberType = DeriveCaliberTypeForBdo(exReq, node, dr, nodeType, reqType);
            }

            #endregion

            #region Derive Types for UI

            if (caliberType == EaCaliberGenEngine.Derivedtype.UI.ToString())
            {
                caliberType = DeriveCaliberTypeForUi(dr, nodeType);
            }

            //strip out characters before the ':' for elements
            if (caliberType == EaCaliberGenEngine.Derivedtype.ELEMENT.ToString())
            {
                var r = new Regex(":");
                // Find a single match in the string.
                Match m = r.Match(dr[COL_CALIBERREQUIREMENT].ToString());
                if (m.Success)
                {
                    if (dr[COL_CALIBERREQUIREMENT].ToString().Length < m.Index + 2)
                        MessageBox.Show(
                            "Error importing element '" + dr[COL_CALIBERREQUIREMENT] + "'. Name not found",
                            "Load Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    else
                        dr[COL_CALIBERREQUIREMENT] = dr[COL_CALIBERREQUIREMENT].ToString().Substring(m.Index + 2);
                }
            }

            #endregion

            return caliberType;
        }

        private string DeriveCaliberTypeForUi(DataRow dr, TreeNode nodeType)
        {
            string caliberType;
            if (nodeType.Text.Substring(0, 3) == EaCaliberGenEngine.TYPE_UIPACKAGE)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.SCREEN.ToString();
                screenselected = true;
            }
                //All DESIGN Rules start with DR
            else if (String.Compare(nodeType.Text.Substring(0, 2), EaCaliberGenEngine.TYPE_DR)==0)
                caliberType = EaCaliberGenEngine.Derivedtype.DESIGNRULE.ToString();
            else if (String.Compare(nodeType.Text.Substring(0, 7),EaCaliberGenEngine.TYPE_UIMESSAGE,true)==0)
                caliberType = EaCaliberGenEngine.Derivedtype.MESSAGE.ToString();
            else if (String.Compare(nodeType.Text.Substring(0, 10), EaCaliberGenEngine.TYPE_UIGROUPBOX,true)==0)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.ELEMENT.ToString();
                dr[COL_EASTEREOTYPE] = "Group Box";
            }
            else if (String.Compare(nodeType.Text.Substring(0, 10), EaCaliberGenEngine.TYPE_UIMENUITEM,true)==0)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.ELEMENT.ToString();
                dr[COL_EASTEREOTYPE] = "Menu Item";
            }
            else if (nodeType.Text.Length > 12 &&
                     String.Compare(nodeType.Text.Substring(0, 13), EaCaliberGenEngine.TYPE_UISUBMENUITEM,true)==0)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.ELEMENT.ToString();
                dr[COL_EASTEREOTYPE] = "Menu SubItem";
            }
            else if (String.Compare(nodeType.Text.Substring(0, 6), EaCaliberGenEngine.TYPE_UITEXTBOX,true)==0)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.ELEMENT.ToString();
                dr[COL_EASTEREOTYPE] = "Field";
            }
            else if (String.Compare(nodeType.Text.Substring(0, 7), EaCaliberGenEngine.TYPE_UIBUTTON,true)==0)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.ELEMENT.ToString();
                dr[COL_EASTEREOTYPE] = "Button";
            }
            else if (String.Compare(nodeType.Text.Substring(0, 9),EaCaliberGenEngine.TYPE_UICHECKBOX,true)==0)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.ELEMENT.ToString();
                dr[COL_EASTEREOTYPE] = "Checkbox";
            }
            else
            {
                // caliberType = EaCaliberGenEngine.Derivedtype.NONDEFINED.ToString();
                caliberType = EaCaliberGenEngine.Derivedtype.ELEMENT.ToString();
                dr[COL_EASTEREOTYPE] = "Field";
            }
            return caliberType;
        }

        private string DeriveCaliberTypeForBdo(ExtendedRequirement exReq, TreeNode node, DataRow dr, TreeNode nodeType, IRequirement reqType)
        {
            string caliberType;
            caliberType = EaCaliberGenEngine.Derivedtype.Ignore.ToString();

            if (nodeType.Text.Length > 14 &&
                nodeType.Text.Substring(0, EaCaliberGenEngine.BDO.Length) == EaCaliberGenEngine.BDO)
            {
                caliberType = EaCaliberGenEngine.Derivedtype.BDO.ToString();
            }

            //elements at the end of the tree have no children
            //the elements at the end of the tree are either Attribute,BusinessRule or Message
            if (reqType.ChildRequirements.Count == 0)
            {
                //R10, R11,R12 
                //node is an attribute, if no child nodes exist, and the parent node is 'attribute'
                if (nodeType.Parent.Text.Length > 9 && nodeType.Parent.Text.Substring(0, 10) ==
                                                       EaCaliberGenEngine.TYPE_ATTRIBUTE
                    || (nodeType.Parent.Parent != null && nodeType.Parent.Parent.Text.Length > 9 &&
                        nodeType.Parent.Parent.Text.Substring(0, 10) == EaCaliberGenEngine.TYPE_ATTRIBUTE)
                    || (nodeType.Parent.Parent != null &&
                        nodeType.Parent.Parent.Parent != null && nodeType.Parent.Parent.Parent.Text.Length > 9 &&
                        nodeType.Parent.Parent.Parent.Text.Substring(0, 10) == EaCaliberGenEngine.TYPE_ATTRIBUTE))
                {
                    caliberType = EaCaliberGenEngine.Derivedtype.Attribute.ToString();
                    //R13 if the parent is of type category, this attribute should get the stereotype of the parent
                    var parentexReq =
                        new ExtendedRequirement(
                            (IRequirement) caliberproject.Session.get((CaliberObjectID) nodeType.Parent.Tag));
                    string parentStatus = parentexReq.Requirement.Status.SelectedValue.ToString();
                    //parent is a category, but parent is not the main attribute category
                    if (parentStatus == EaCaliberGenEngine.TYPE_CATEGORY &&
                        nodeType.Parent.Text.Substring(0, 10) != EaCaliberGenEngine.TYPE_ATTRIBUTE)
                    {
                        string stereotype = string.Empty;

                        stereotype = node.Parent.Text;
                        //remove Caliber ID from stereotype
                        stereotype = Regex.Replace(stereotype,
                                                   ((IRequirement)
                                                    caliberproject.Session.get((CaliberObjectID) nodeType.Parent.Tag))
                                                       .IDNumber.ToString(), "");
                        //trim white spaces at the end
                        stereotype = stereotype.Trim();
                        //Remove word BDO from stereotype
                        stereotype = stereotype.Remove(stereotype.Length - 3);

                        dr[COL_EASTEREOTYPE] = stereotype;
                    }
                }
                    //R5 node is a business rule, if no child nodes exist, and the parent node is 'business rule'
                else if (nodeType.Parent.Text.Length > 13 &&
                         nodeType.Parent.Text.Substring(0, 14) == EaCaliberGenEngine.TYPE_BUSINESSRULES &&
                         nodeType.Text.Substring(0, 3) == EaCaliberGenEngine.TYPE_BR)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();

                    //R1 node is a message, if no child nodes exist, and the parent node is 'message'
                else if (nodeType.Parent.Text.Length > 7 &&
                         nodeType.Parent.Text.Substring(0, 8) == EaCaliberGenEngine.TYPE_MESSAGE)
                    caliberType = EaCaliberGenEngine.Derivedtype.Ignore.ToString();
                    //R2 messages don't always have a parent folder
                else if (nodeType.Text.Length > 7 &&
                         nodeType.Text.Substring(0, 8) == EaCaliberGenEngine.TYPE_MESSAGE)
                    caliberType = EaCaliberGenEngine.Derivedtype.Ignore.ToString();

                    //R6 check also parent level
                    //node is a business rule, if no child nodes exist, and the parent node is 'business rule'
                else if (nodeType.Parent.Parent.Text.Length > 13 &&
                         nodeType.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES &&
                         nodeType.Text.Substring(0, 3) == EaCaliberGenEngine.TYPE_BR)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();

                    //R9 if node.text starts with "Business Object set as "Package"
                else if (nodeType.Text.Length > 15 &&
                         nodeType.Text.Substring(0, 15) == EaCaliberGenEngine.TYPE_BDO)
                    caliberType = EaCaliberGenEngine.Derivedtype.Package.ToString();

                    //R7 node is a business rule, if no child nodes exist, and the parent node is 'business rule'
                else if (nodeType.Parent.Text.Length > 13 &&
                         nodeType.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES &&
                         nodeType.Text.Substring(0, 3) != EaCaliberGenEngine.TYPE_BR)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();

                    //R8 if parent.parent.text starts with "Business Rules" and 
                    // node.text does not start with "BR-" set as "Business Rule Parent"
                else if (nodeType.Parent.Parent.Text.Length > 13 &&
                         nodeType.Parent.Parent != null &&
                         nodeType.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)

                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();

                    //R8.1 if parent.parent.parent.text starts with "Business Rules" and node.text does not start with "BR-" set as "Business Rule Parent"
                else if (nodeType.Parent.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();

                    //R9 if node.text starts with "Attributes' set to Category, 
                    //folder for Attributes exist, but no Attributes have yet been added
                else if (nodeType.Text.Substring(0, 10) ==
                         EaCaliberGenEngine.TYPE_ATTRIBUTE)
                    caliberType = EaCaliberGenEngine.Derivedtype.Category.ToString();

                    // R9.1 If the node starts with BR- it is a business rule regardless where it is
                else if (nodeType.Text.Substring(0, 3) == EaCaliberGenEngine.TYPE_BR)
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.Businessrule.ToString();
            }

                //somewhere else in the tree
            else
            {
                //R14 if a child node exist, and the current node name is Attribute it must be a category
                if (nodeType.Text.Substring(0, 10) == EaCaliberGenEngine.TYPE_ATTRIBUTE)
                    caliberType = EaCaliberGenEngine.Derivedtype.Category.ToString();
                    
                    //????????
                    //node is a business rule, if no child nodes exist, and the parent node is 'business rule'
                else if (nodeType.Text.Length > 13 &&
                         nodeType.Text.Substring(0, 14) == EaCaliberGenEngine.TYPE_BUSINESSRULES &&
                         nodeType.Text.Substring(0, 3) == EaCaliberGenEngine.TYPE_BR)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();

                else if (nodeType != null && nodeType.Parent != null &&
                         nodeType.Parent.Text.Length > 13 && nodeType.Parent.Text.Substring(0, 14) == EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();


                else if (nodeType != null && nodeType.Parent != null && nodeType.Parent.Parent != null &&
                         nodeType.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();
                    

                    //R8.1 if parent.parent.parent.text starts with "Business Rules" and node.text does not start with "BR-" set as "Business Rule Parent"
                else if (nodeType != null && nodeType.Parent != null && nodeType.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();
                    

                    //R8.1 if parent.parent.parent.text starts with "Business Rules" and node.text does not start with "BR-" set as "Business Rule Parent"
                else if (nodeType != null && nodeType.Parent != null &&
                         nodeType.Parent.Parent != null && nodeType.Parent.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();
                    

                    //R8.1 if parent.parent.parent.text starts with "Business Rules" and node.text does not start with "BR-" set as "Business Rule Parent"
                else if (nodeType != null && nodeType.Parent != null &&
                         nodeType.Parent.Parent != null && nodeType.Parent.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent.Parent.Parent != null &&
                         nodeType.Parent.Parent.Parent.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();
                    
                    //set top node to Business Rule as well
                else if (nodeType.Text.Length > 13 &&
                         nodeType.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType = EaCaliberGenEngine.Derivedtype.Businessrule.ToString();

                    //R4 node is a message, if Text contains 'messages'
                else if (nodeType.Text.Substring(0, 8) == EaCaliberGenEngine.TYPE_MESSAGE)
                    caliberType = EaCaliberGenEngine.Derivedtype.Ignore.ToString();

                    //Assumption: Group of Attributes MUST be of type [Category]
                    //R15 if child node exists and parent node is Attribute and Status = [category] it must be a category 
                    //(grouping of attribute underneath attribute
                else if (nodeType.Parent.Text.Substring(0, 10) ==
                         EaCaliberGenEngine.TYPE_ATTRIBUTE
                         &&
                         exReq.Requirement.Status.SelectedValue.ToString() ==
                         EaCaliberGenEngine.TYPE_CATEGORY)
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.Category.ToString();

                    //Assumption: Group of Attributes MUST be of type [Category]
                    //if child node exists and parent parent node is Attribute and Status = [category] it must be a category 
                    //(grouping of attribute underneath attribute
                else if ((nodeType.Parent.Parent != null &&
                          nodeType.Parent.Parent.Text.Length > 10 &&
                          nodeType.Parent.Parent.Text.Substring(0, 10) ==
                          EaCaliberGenEngine.TYPE_ATTRIBUTE)
                         &&
                         exReq.Requirement.Status.SelectedValue.ToString() ==
                         EaCaliberGenEngine.TYPE_CATEGORY)
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.Category.ToString();

                    //R7 find any business rule categories in higher orders
                else if (nodeType.Parent.Text.Length > 13 &&
                         nodeType.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.Businessrule.
                            ToString();


                    //R7 find any business rule categories in higher orders
                else if (nodeType.Parent.Parent != null &&
                         nodeType.Parent.Parent.Text.Length > 13 &&
                         nodeType.Parent.Parent.Text.Substring(0, 14) ==
                         EaCaliberGenEngine.TYPE_BUSINESSRULES)
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.Businessrule.
                            ToString();
                        
                        
                  

                    //Assumption: Business Object MUST always have children
                    //R17 if node.text starts with "Business Object" set as BDO
                else if (nodeType.Text.Length > 15 &&
                         nodeType.Text.Substring(0, 15) ==
                         EaCaliberGenEngine.TYPE_BDO)
                {
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.BDO.
                            ToString();
                    //set stereotype
                    dr[COL_EASTEREOTYPE] = "Business Object";
                }

                    //not a BDO
                    //R19 if node.text does not start with "Business Object" and parent.text starts with 'Business Domain Model set as package
                else if (nodeType.Text.Length > 14 &&
                         nodeType.Text.Substring(0, 15) !=
                         EaCaliberGenEngine.TYPE_BDO &&
                         (nodeType.Parent.Text.Length > 21 &&
                          nodeType.Parent.Text.Substring(0, 21) ==
                          EaCaliberGenEngine.
                              TYPE_BUSINESSDOMAINMODEL))
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.
                            Package.ToString();

                    //R18 if node.text starts with "Business Domain Model" set as package
                else if (nodeType.Text.Length > 21 &&
                         nodeType.Text.Substring(0, 21) ==
                         EaCaliberGenEngine.
                             TYPE_BUSINESSDOMAINMODEL)
                    caliberType =
                        EaCaliberGenEngine.Derivedtype.
                            Package.ToString();

                else if (nodeType.Text.Length > 15 &&
                         nodeType.Text.Substring(0, 15) ==
                         EaCaliberGenEngine.TYPE_BDO)
                    caliberType =
                        EaCaliberGenEngine.
                            Derivedtype.Package.
                            ToString();

                    // R9.1 If the node starts with BR- it is a business rule regardless where it is
                else if (
                    nodeType.Text.Substring(0, 3) ==
                    EaCaliberGenEngine.TYPE_BR)
                    caliberType =
                        EaCaliberGenEngine.
                            Derivedtype.
                            Businessrule.
                            ToString();

                else
                    caliberType =
                        EaCaliberGenEngine.
                            Derivedtype.Category
                            .ToString();
            }
            return caliberType;
        }

        private string DeriveCaliberTypeForUseCase(string caliberType, TreeNode nodeType)
        {
            //if UC starts with characters UC- it is a USE Case, otherwise treat it as a package
            //note the BA's may not have finished all
            if (nodeType.Text.Substring(0, 3) == EaCaliberGenEngine.TYPE_UC)
                caliberType = EaCaliberGenEngine.Derivedtype.Usecase.ToString();
            else
                caliberType = EaCaliberGenEngine.Derivedtype.Package.ToString();
            return caliberType;
        }

        private string DeriveCaliberTypeForActor(ExtendedRequirement exReq, string caliberType)
        {

                if (exReq.Requirement.Status.SelectedValue.ToString() == EaCaliberGenEngine.TYPE_CATEGORY)
                    caliberType = EaCaliberGenEngine.Derivedtype.Package.ToString();
                else
                    caliberType = EaCaliberGenEngine.Derivedtype.Actor.ToString();
            
            return caliberType;
        }


        

       

        private bool ItemExistsInEA(string CaliberId, Repository m_Repository)
        {
            bool foundId = false;
            var mapping = new mtCaliberMapping();

            string caliberID = CaliberId;
            // Check if exists in mapping table
            //
            foundId = GetDetails(caliberID);

            return foundId;
        }

        // 
        // Get EA GUID for a given CaliberID from the Mapping table
        //
        public bool GetDetails(string caliberId)
        {
            bool found = false;

           

            var command = new SqlCommand(
                    string.Format("SELECT UniqueID, CaliberID, CaliberName , CaliberHierarchy, " +
                                  "EA_GUID, EAParentGUID, EAElementType, CaliberFullDescription, EAElementID " +
                                  "from CaliberMapping where CaliberID = {0}", caliberId),  SqlHelpers.MappingDbConnection);

                SqlDataReader reader = command.ExecuteReader();

                try
                {

                    if (reader.Read())
                    {
                        if (reader["EAElementID"] != null)
                        {
                            // Check in EA
                            var ea = new EaAccess();
                            EaAccess.sEAElement el =
                                ea.RetrieveEaElement(reader["EA_GUID"].ToString());

                            if (el.EA_GUID != null)
                            {
                                found = true;
                            }
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            

            return found;
        }


        public ElementProperties GetElementProperties(string CaliberID)
        {
            string caliberId = CaliberID;

            var elementProperties = new ElementProperties();
            IRequirement req = null;

            try
            {
                req = caliberproject.Session.getRequirement(System.Convert.ToInt32(caliberId));
                var exReq = new ExtendedRequirement(req);

                requirement = req;

                // Get UDAs from Caliber
                Collection sourceAttributes = exReq.Requirement.AttributeValues;

                //for (int i = 0; i < sourceAttributes.Count; i++)
                //{
                //    IAttributeValue sourceAttribute = (IAttributeValue)sourceAttributes[i];
                //    IAttributeValue targetAttribute = this.AttributeDictionary[sourceAttribute.Attribute.Name];
                //}


                // Dynamic variable?! - Later...
                string[] field = {
                                     "elementProperties.AccessibleName",
                                     "elementProperties.AccessibleDescription",
                                     "elementProperties.DisplayFormat"
                                 };


                for (int i = 0; i < sourceAttributes.Count; i++)
                {
                    var attribute = (IAttributeValue) sourceAttributes[i];

                    if (attribute.Attribute.Name == "Display Label")
                    {
                        try
                        {
                            elementProperties.DisplayLabel = ((IUDATextValue) sourceAttributes[i]).Value;
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Accessible Name")
                    {
                        try
                        {
                            elementProperties.AccessibleName = ((IUDATextValue) sourceAttributes[i]).Value;
                            // field[i] = ((IUDATextValue)sourceAttributes[i]).Value;
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Accessible Description")
                    {
                        try
                        {
                            elementProperties.AccessibleDescription =
                                ((IUDATextValue) sourceAttributes[i]).Value;
                        }
                        catch
                        {
                        }
                    }
                    if (attribute.Attribute.Name == "Access Key")
                    {
                        try
                        {
                            elementProperties.AccessKey =
                                ((IUDAListValue) sourceAttributes[i]).SelectedValue.ToString();
                        }
                        catch
                        {
                        }
                    }
                    if (attribute.Attribute.Name == "Display Length")
                    {
                        try
                        {
                            elementProperties.DisplayLength =
                                ((IUDAIntegerValue) sourceAttributes[i]).Value;
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Display Format")
                    {
                        try
                        {
                            elementProperties.DisplayFormat = ((IUDATextValue) sourceAttributes[i]).Value;
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Field Type")
                    {
                        try
                        {
                            elementProperties.FieldType =
                                ((IUDAListValue) sourceAttributes[i]).SelectedValue.ToString();
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Mandatory")
                    {
                        try
                        {
                            elementProperties.Mandatory =
                                ((IUDAListValue) sourceAttributes[i]).SelectedValue.ToString();
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "State")
                    {
                        try
                        {
                            elementProperties.State = ((IUDAListValue) sourceAttributes[i]).SelectedValue.ToString();
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Visible")
                    {
                        try
                        {
                            elementProperties.Visible = ((IUDAListValue) sourceAttributes[i]).SelectedValue.ToString();
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Field Source")
                    {
                        try
                        {
                            elementProperties.FieldSource = ((IUDATextValue) sourceAttributes[i]).Value;
                        }
                        catch
                        {
                        }
                    }

                    if (attribute.Attribute.Name == "Default Value")
                    {
                        try
                        {
                            elementProperties.DefaultValue = ((IUDATextValue) sourceAttributes[i]).Value;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return elementProperties;
        }

        #region Nested type: ElementProperties

        public class ElementProperties
        {
            public string AccessibleDescription;
            public string AccessibleName;
            public string AccessKey;
            public string DefaultValue;
            public string DisplayFormat;
            public string DisplayLabel;
            public int? DisplayLength;
            public string FieldSource;
            public string FieldType;
            public string Mandatory;
            public string State;
            public string Visible;
        }

        #endregion

        #region Nested type: symbol



        #endregion
    }
}
