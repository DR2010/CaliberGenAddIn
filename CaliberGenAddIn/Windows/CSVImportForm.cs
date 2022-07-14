using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EAAddIn.Applications.CSVClassImporter;
using EAAddIn.Presenters;
using EAAddIn.Windows.Interfaces;

namespace EAAddIn.Windows
{
    public partial class CsvClassImporter : Form, ICsvClassImporter
    {
        public CsvClassImporter()
        {
            InitializeComponent();

            new CsvClassImporterPresenter(this);

        }

        DataTable CsvDataTable { get; set;}

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (ImportCsvRequested != null)
            {
                ImportCsvRequested(this, EventArgs.Empty);
            }
        }

        private void CSVFilePickerButton_Click(object sender, EventArgs e)
        {
            if (openCSVFileDialog.ShowDialog() == DialogResult.OK)
            {
                CSVFileTextBox.Text = openCSVFileDialog.FileName;
            }
        }



        #region ICsvClassImporter Members


        public event EventHandler<EventArgs> SetPackageToCurrentRequested;
        public event EventHandler<EventArgs> ImportCsvRequested;
        public event EventHandler<EventArgs> CreateClassesRequested;

        public BindingSource ClassesBindingSource
        {
            get; set;
        }

        public void BindClasses()
        {
            CSVImportDataGridView.DataSource = ClassesBindingSource;
        }
        public string CsvFileName
        {
            get
            {
                return CSVFileTextBox.Text;
            }
            set
            {
                CSVFileTextBox.Text = value;
            }
        }

        public string PackageName
        {
            set { PackageTextBox.Text = value; }
        }

        #endregion

        private void SetPackageButton_Click(object sender, EventArgs e)
        {
            if (SetPackageToCurrentRequested != null)
            {
                SetPackageToCurrentRequested(this, EventArgs.Empty);
            }
        }

        private void CreateClassesButton_Click(object sender, EventArgs e)
        {
            if (CreateClassesRequested != null)
            {
                CreateClassesRequested(this, EventArgs.Empty);
            }

        }
    }
}
