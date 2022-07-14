using System;
using System.Data;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications.CSVClassImporter;
using EAAddIn.Windows.Interfaces;


namespace EAAddIn.Presenters {
	public class CsvClassImporterPresenter {

       
        public CSVImportEngine Model{
	        get;
	        set;
	    }
	    public ICsvClassImporter View{
	        get;
	        set;
	    }

	    private Package package;

	    private readonly BindingSource classes = new BindingSource();
	    /// Implements BR-1234
		/// <param name="view"></param>
        public CsvClassImporterPresenter(ICsvClassImporter view)
		{
		    View = view;
            Model = new CSVImportEngine();

		    view.ImportCsvRequested += view_ImportCsvRequested;
            view.SetPackageToCurrentRequested += view_SetPackageToCurrentRequested;
            view.CreateClassesRequested += view_CreateClassesRequested;

            SetPackage();

		}

        void view_CreateClassesRequested(object sender, EventArgs e)
        {
            Model.CreateElementsFromDataTable(package, (DataTable) View.ClassesBindingSource.DataSource);
        }

        void view_SetPackageToCurrentRequested(object sender, EventArgs e)
        {
            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Package)
            {
                SetPackage();
            }
            else
            {
                MessageBox.Show("Please select a package.",
                                "Select Package", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void SetPackage()
        {
            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Package)
            {
                package = (Package) AddInRepository.Instance.Repository.GetTreeSelectedObject();
                View.PackageName = package.Name;
            }
        }

	    private void view_ImportCsvRequested(object sender, EventArgs e)
	    {
            if (string.IsNullOrEmpty(View.CsvFileName))
            {
                MessageBox.Show("Please enter a CSV file to import.", "CSV Import", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

	        DataTable dataTable = Model.CreateDataTableFromCSV(View.CsvFileName);

            if (dataTable.Rows.Count > 1)
            {
                classes.DataSource = dataTable;

                View.ClassesBindingSource = classes;
            }
            else
            {
                MessageBox.Show("No data found in CSV file.", "CSV Import", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            }
            View.BindClasses();
	    }



	            
	  
        

        

	   

       
	}

}