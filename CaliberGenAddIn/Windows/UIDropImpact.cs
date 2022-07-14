using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAStructures;


namespace EAAddIn.Windows
{
    public partial class UIDropImpact : Form
    {

        public DataTable elementSourceDataTable;
        public EaCaliberGenEngine EAEngine;
        public SecurityInfo secinfo;
        public bool response;

        public UIDropImpact()
        {
            InitializeComponent();

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

            dgvLinkedToElements.DataSource = elementSourceDataTable;

        }

        private void UIDropImpact_Load(object sender, EventArgs e)
        {
            RefreshContents();
        }

        private void RefreshContents()
        {
            // Retrieve Selected Element
            //
            Element element;

            try
            {
                element = (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch (Exception)
            {
                response = false;
                return;
            }

            txtElementID.Text = element.ElementID.ToString();
            txtElementName.Text = element.Name;
            txtElementStereotype.Text = element.Stereotype;
            txtElementType.Text = element.Type;

            // Get Caliber connection
            //
            var caliberTag = (TaggedValue) element.TaggedValues.GetByName("CaliberID");
            txtCaliberTagID.Text = "";
            if (caliberTag != null)
            {
                txtCaliberTagID.Text = caliberTag.Value;
            }

            if ( ! string.IsNullOrEmpty(txtCaliberTagID.Text))
            {
                int caliberID = 0;
                if (! string.IsNullOrEmpty(txtCaliberID.Text))
                {
                    caliberID = Convert.ToInt32(txtCaliberID.Text);
                }

                if (caliberID > 0)
                {
                    mtCaliberMapping caliberMapping = new mtCaliberMapping();
                    caliberMapping.getDetails(caliberID);

                    txtCaliberID.Text = caliberMapping.CaliberID.ToString();
                }
            }
            // Get GEN connection
            //
            var genTag = (TaggedValue) element.TaggedValues.GetByName("Load Module");
            if (genTag != null)
            {
                txtGENLoadModule.Text = genTag.Value;
            }

            // Get linked 
            //
            listElementsRelated(element.ElementID);
            
            // Get placed in diagrams
            //


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
                                            "",
                                            "",
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            response = false;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            response = true;
            this.Close();

        }

    }
}
