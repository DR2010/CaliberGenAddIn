using System;
using System.Windows.Forms;
using EA;

using EAAddIn.Applications.ProcessDocumentationTemplate;

namespace EAAddIn.Windows
{
    public partial class ProcessDocumentation : Form
    {
        private Element _selectedElement;
 
        public ProcessDocumentation()
        {
            InitializeComponent();
            
            cbTemplateList.DataSource = Template.setTemplateValues();
            cbTemplateList.DisplayMember = "templateMenuName";

            _selectedElement = GetSelectedElement();
            if (_selectedElement!=null)
            {
                tbProcessDocName.Text = _selectedElement.Name;
            }
        }


        private static Element GetSelectedElement()
        {
            Element selectedelement = null;

            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Element)
            {
                selectedelement = (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }

            if (selectedelement == null)
            {
                MessageBox.Show(@"Please select a element from the project browser where you wish to place your Documentation",
                                @"Error", MessageBoxButtons.OK);
                return null;
            }

            if (!selectedelement.GetType().IsClass)
            {
                MessageBox.Show(@"Please select a element that is a class",
                                @"Error", MessageBoxButtons.OK);
                return null;
            }

            return selectedelement;

        }

        private void btnCopyTemplate_Click(object sender, EventArgs e)
        {
            var selectedTemplate = (Template) cbTemplateList.SelectedItem;

            if (_selectedElement==null)
            {
                _selectedElement = GetSelectedElement();
            }
            if (_selectedElement != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                bool tempCopiedOk = ProcessTemplate.TransferPackage(selectedTemplate.templateGuid, _selectedElement, selectedTemplate.replaceStringWhat, tbProcessDocName.Text);
                Cursor.Current = Cursors.Default;
            }
            
        }
    }
}
