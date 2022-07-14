using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications.SpecificationGenerator;

namespace EAAddIn.Windows
{
    public partial class CR0370AddIn : Form
    {

        BindingList<ObjectDefinition> changedElementsBindingList;
        public CR0370AddIn()
        {
            InitializeComponent();
        }



        private void CR0370AddIn_Load(object sender, EventArgs e)
        {
        }

        private void GetChangedElementsButton_Click(object sender, EventArgs e)
        {
            Package selectedPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (selectedPackage == null) return;

            var changedElements = ProcessPackage(selectedPackage);

            changedElementsBindingList = new ObjectList(changedElements);

            changedElementsDataGridView.DataSource = changedElementsBindingList;

            changedElementsDataGridView.Columns[0].Visible = false;
            changedElementsDataGridView.Columns[1].Visible = false;
            changedElementsDataGridView.Columns[2].Visible = false;
            changedElementsDataGridView.Columns[7].Visible = false;
            changedElementsDataGridView.Columns[8].Visible = false;
            changedElementsDataGridView.Columns[9].Visible = false;
        }

        private List<ObjectDefinition> ProcessPackage(Package selectedPackage)
        {
            var packageElements = new List<ObjectDefinition>();
            string packageIds = new EaAccess().getPackageList(selectedPackage);
            return new EaAccess().GetChangedElementsFromPackages(packageIds, fromDateTimePicker.Value, ToDateTimePicker.Value);
        }

        private void changedElementsDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            object objectToLocate;

                objectToLocate =
                    AddInRepository.Instance.Repository.GetElementByID(
                        Convert.ToInt32(changedElementsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()));

            if (objectToLocate != null)
            {
                AddInRepository.Instance.Repository.ShowInProjectView(objectToLocate);
            }
        }

        private void grabImageForDiagramButton_Click(object sender, EventArgs e)
        {




            var generator = new ProcessSpecificationGenerator();

            generator.BuildSpecification();

        }
    }  
}


   

