using System;   
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAStructures;
using System.Collections;
using System.Collections.Generic;


namespace EAAddIn.Windows.Controls
{
    public partial class LinkedElementsToolbarControl : UserControl
    {

        public EaCaliberGenEngine EAEngine;
        public DataTable elementSourceDataTable;
        public SecurityInfo secinfo;
        public ArrayList visitedArray;
        public LinkedElementsToolbarControl()
        {
            InitializeComponent();
            visitedArray = new ArrayList();
            btnBack.Enabled = false;

            //var dbcon = new dbConnections();
            //secinfo.EARepository = dbcon.CSEARepository;
            //secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            //secinfo.MigrateToolDB = dbcon.csMigrateTool;

            EAEngine = new EaCaliberGenEngine(secinfo, "caliber");

            var RelatedElement = new DataColumn("RelatedElement", typeof(String));
            var ElementType = new DataColumn("ElementType", typeof(String));
            var Stereotype = new DataColumn("Stereotype", typeof(String));
            var ElementID = new DataColumn("ElementID", typeof(String));
            var Direction = new DataColumn("Direction", typeof(String));
            var Name = new DataColumn("Name", typeof(String));
            var Alias = new DataColumn("Alias", typeof(String));
            var Note = new DataColumn("Note", typeof(String));

            elementSourceDataTable = new DataTable("ElementSourceDataTable");

            elementSourceDataTable.Columns.Add(RelatedElement);
            elementSourceDataTable.Columns.Add(ElementType);
            elementSourceDataTable.Columns.Add(Stereotype);
            elementSourceDataTable.Columns.Add(ElementID);
            elementSourceDataTable.Columns.Add(Name);
            elementSourceDataTable.Columns.Add(Direction);
            elementSourceDataTable.Columns.Add(Note);
            elementSourceDataTable.Columns.Add(Alias);

            dgvLinkedElements.DataSource = elementSourceDataTable;
        }

        // ----------------------------------------------
        //    List linked elements
        // ----------------------------------------------
        private void btnListLinked_Click(object sender, EventArgs e)
        {
            visitedArray.Clear();

            Cursor.Current = Cursors.WaitCursor;

            // Get selected element from current diagram
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            DiagramObject objectSelected = null;
            foreach (DiagramObject diagobj in diag.SelectedObjects)
            {
                objectSelected = diagobj;
                break;
            }

            // dgvLinkedElements.DataSource = ElementSourceDataTable;

            if (objectSelected != null)
            {
                listElementsRelated(objectSelected.ElementID);
                visitedArray.Add(objectSelected.ElementID);
            }

            Cursor.Current = Cursors.Arrow;
        }

        // ----------------------------------------------
        //    List Related Elements
        // ----------------------------------------------
        private void listElementsRelated(int elementID)
        {
            Element elementSelect =
                AddInRepository.Instance.Repository.GetElementByID(elementID);

            txtElementType.Text = elementSelect.Type;
            txtElementName.Text = elementSelect.Name;

            var eainst = new EaAccess();
            var listOfRelatedItems =
                eainst.getLinkedElementsSQL(elementSelect,
                                            txtStereotype.Text,
                                            txtObjectType.Text,
                                            true,
                                            true, new List<string>(),
                                            null, null, 0);

            elementSourceDataTable.Clear();

            foreach (object item in listOfRelatedItems)
            {
                var connList = new EaAccess.ConnectorList();
                connList = (EaAccess.ConnectorList)item;

                DataRow elementRow = elementSourceDataTable.NewRow();

                elementRow["RelatedElement"] = connList.ObjectName;
                elementRow["ElementType"] = connList.ObjectType;
                elementRow["Stereotype"] = connList.ObjectStereotype;
                elementRow["ElementID"] = connList.ConnectorStartId;

                // Select the other end of the relationship
                if (connList.ConnectorStartId == elementID)
                {
                    elementRow["ElementID"] = connList.ConnectorEndId;
                }

                elementRow["Direction"] = connList.ConnectorDirection;
                elementRow["Note"] = connList.ObjectNote;
                elementRow["Name"] = connList.ConnectorName;
                elementRow["Alias"] = connList.ObjectAlias;

                elementSourceDataTable.Rows.Add(elementRow);
            }
        }

        // ----------------------------------------------
        // Place selected elements
        // ----------------------------------------------
        private void btnPlace_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            // Get selected element from current diagram
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag == null)
                return;

            foreach (DataGridViewRow dr in dgvLinkedElements.SelectedRows)
            {
                int elementID = Convert.ToInt32(dr.Cells["ElementID"].Value);

                EaAccess.placeElementInDiagram(elementID);
            }

            diag.DiagramObjects.Refresh();
            diag.Update();
            AddInRepository.Instance.Repository.SaveDiagram(diag.DiagramID);
            AddInRepository.Instance.Repository.ReloadDiagram(diag.DiagramID);

            Cursor.Current = Cursors.Arrow;
        }

        // ----------------------------------------------
        // Drill down
        // ----------------------------------------------
        private void dgvLinkedElements_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get selected row
            var elementID = "";
            var ielementID = 0;

            foreach (DataGridViewRow dgvr in dgvLinkedElements.SelectedRows)
            {
                elementID = dgvr.Cells["ElementID"].Value.ToString();
                ielementID = Convert.ToInt32(elementID);
                break;
            }

            try
            {
                // Just to check if it is an Element
                Element elementSelect =
                    AddInRepository.Instance.Repository.GetElementByID(ielementID);
            }
            catch (Exception)
            {
                // item selected is not an Element (it may be package etc, we dont expand)
                return;
            }

            visitedArray.Add(ielementID);

            if (visitedArray.Count > 1)
                btnBack.Enabled = true;

            listElementsRelated(ielementID);
        }

        // ----------------------------------------------
        // Back click
        // ----------------------------------------------
        private void btnBack_Click(object sender, EventArgs e)
        {
            int last = visitedArray.Count - 1;
            visitedArray.RemoveAt(last);

            var elementID = (int)visitedArray[last - 1];

            if (visitedArray.Count <= 1)
                btnBack.Enabled = false;

            listElementsRelated(elementID);
        }
    }
}
