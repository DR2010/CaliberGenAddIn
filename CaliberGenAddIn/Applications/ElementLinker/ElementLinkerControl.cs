using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using Attribute = EA.Attribute;

namespace EAAddIn.Applications.ElementLinker
{
    public partial class ElementLinkerControl : UserControl
    {
        public ElementLinkerControl()
        {
            InitializeComponent();

            SelectElement(ref textBoxFromElementName, ref fromElement);
        }

        EaAccess access = new EaAccess();
        private Element fromElement;
        private Element toElement;

        DataTable features = new DataTable("features");

       
      
        private void buttonSelectCurrentAsFromElement_Click(object sender, EventArgs e)
        {
            SelectElement(ref textBoxFromElementName, ref fromElement);
        }

        private void SelectElement(ref TextBox textBox, ref Element element)
        {
            Element selectedElement = EaAccess.GetSelectedElement();

            if (selectedElement == null)
            {
                MessageBox.Show("You can only link elements.");
                return;
            }

            element = selectedElement;
            textBox.Text = selectedElement.Name;

            if (toElement != null && fromElement != null)
            {
                PopulateFeatures();
            }
        }

        private void PopulateFeatures()
        {
            features.Clear();

            features.Columns.Clear();

            features.Columns.Add("Name");

            if (radioButtonMethods.Checked)
            {
                features.Columns.Add("Feature", Type.GetType("EA.Method,Interop.EA"));
            }
            else
            {
                features.Columns.Add("Feature", Type.GetType("EA.Attribute,Interop.EA"));

            }

            if (radioButtonMethods.Checked)
            {
                foreach (Method elementMethod in toElement.Methods)
                {
                    features.Rows.Add(elementMethod.Name, elementMethod);
                }
            }
            else
            {
                {
                    foreach (Attribute attribute in toElement.Attributes)
                    {
                        features.Rows.Add(attribute.Name, attribute);
                    }
                }
            }

            dataGridViewFeatures.DataSource = features;
            dataGridViewFeatures.Columns[0].Width = 250;
            dataGridViewFeatures.Columns[1].Visible = false;
        }

        private void buttonSelectCurrentAsToElement_Click(object sender, EventArgs e)
        {
            SelectElement(ref textBoxToElementName, ref toElement);

        }

        private void dataGridViewFeatures_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CreateConnector();
        }

        private void CreateConnector()
        {
            var connector = (Connector)fromElement.Connectors.AddNew("Implementation", "Realization");

            connector.ClientID = toElement.ElementID;

            if (radioButtonMethods.Checked)
            {
                connector.StyleEx = "LFSP=" + ((Method)dataGridViewFeatures.SelectedRows[0].Cells[1].Value).MethodGUID +
                                    "L;";
            }
            else
            {
                connector.StyleEx = "LFSP=" + ((Attribute)dataGridViewFeatures.SelectedRows[0].Cells[1].Value).AttributeGUID +
                                    "L;";
            }
            connector.Update();

            //fromElement.Connectors.Refresh();
            //toElement.Connectors.Refresh();

            var diagram = AddInRepository.Instance.Repository.GetCurrentDiagram();

            AddInRepository.Instance.Repository.SaveDiagram(diagram.DiagramID);
            AddInRepository.Instance.Repository.ReloadDiagram(diagram.DiagramID);
        }

        private void buttonCreateLink_Click(object sender, EventArgs e)
        {
            CreateConnector();
        }

        private void radioButtonMethods_CheckedChanged(object sender, EventArgs e)
        {
            PopulateFeatures();
        }

        private void radioButtonAttributes_CheckedChanged(object sender, EventArgs e)
        {
            PopulateFeatures();
        }

        private void buttonSwapFromAndTo_Click(object sender, EventArgs e)
        {
            var temp = fromElement;

            fromElement = toElement;
            toElement = temp;

            if (fromElement != null)
                textBoxFromElementName.Text = fromElement.Name;

            if (toElement != null)
                textBoxToElementName.Text = toElement.Name;
        }
    }
}
