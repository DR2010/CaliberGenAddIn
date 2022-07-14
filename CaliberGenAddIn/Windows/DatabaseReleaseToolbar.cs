using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications.DatabaseReleaseManager;
using EAAddIn.Presenters;
// using EAAddIn.Reports;
using EAAddIn.Windows.Interfaces;
using System.Linq;
using Element=EA.Element;


namespace EAAddIn.Windows
{
    public partial class DatabaseReleaseToolbar : Form, IDatabaseReleaseToolbar
    {
        

        public DatabaseReleaseToolbar()
        {
            InitializeComponent();

            new DatabaseReleaseToolbarPresenter(this);
        }

        private readonly BindingSource releasesBindingSource = new BindingSource();
        public event EventHandler<EventArgs> ApplyStereotypeToTableRequested;
        public event EventHandler<EventArgs> HideUnchangedColumnsRequested;
        public event EventHandler<EventArgs> ApplyStereotypeToColumnsRequested;
        public event EventHandler<EventArgs> GetColumnsRequested;
        public event EventHandler<EventArgs> ReleaseSelected;
        private Element element;
        private void ApplyStereotypeToTableButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (ApplyStereotypeToTableRequested != null)
            {
                ApplyStereotypeToTableRequested(sender, e);
            }
            Cursor.Current = Cursors.Default;
        }
        public BindingSource ReleasesBindingSource
        {
            get { return releasesBindingSource; }
        }
        public BindingSource ColumnsBindingSource
        {
            get; set;
        }
        public List<string> SelectedColumns
        {
            get
            {
                var columns = new List<string>();
                foreach (DataGridViewRow row in ColumnsDataGridView.SelectedRows)
                {
                    columns.Add(row.Cells[0].Value.ToString());
    
                }
                return columns;
            }
        }
        public void BindColumns()
        {
            ColumnsDataGridView.DataSource = ColumnsBindingSource;
            ColumnsDataGridView.Columns[0].Width = 200;
            ColumnsDataGridView.Columns[1].Visible = false;
        }
        public Element Element
        {
            get { return element; }
            set
            {
                if (value == null) return;
                element = value;
                ElementNameTextBox.Text = value.Name;
            }
        }
        public DiagramObject DiagramObject
        {
            get; set;
        }
        public string ReleasesDisplayMember
        {
            set { ReleasesComboBox.DisplayMember = value; }
        }
        public string ReleasesValueMember
        {
            set { ReleasesComboBox.ValueMember = value; }
        }
        public string ChangeType
        {
            get
            {
                if (NewTableRadioButton.Checked) return "new";
                if (DeletedTableRadioButton.Checked) return "deleted";
                if (RenamedTableRadioButton.Checked) return "renamed";
                if (ChangedTableRadioButton.Checked) return "changed";
                if (ToBeDeletedRadioButton.Checked) return "to be deleted";
                if (NoChangesRadioButton.Checked) return "none";
                return string.Empty;
            }
        }
        public Object SelectedRelease
        {
            get { return ReleasesComboBox.SelectedItem; }
            set { ReleasesComboBox.SelectedItem = value;}
        }
        public void BindReleases()
        {
            ReleasesComboBox.DataSource = releasesBindingSource;
        }
        private void ReleasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReleaseSelected != null)
            {
                ReleaseSelected(sender, e);
            }
        }
        private void GetColumnsButton_Click(object sender, EventArgs e)
        {
            if (GetColumnsRequested != null)
            {
                GetColumnsRequested(sender, e);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ApplyStereotypeToColumnsRequested != null)
            {
                ApplyStereotypeToColumnsRequested(sender, e);
            }

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void HideUnchangedColumnsButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (HideUnchangedColumnsRequested != null)
            {
                HideUnchangedColumnsRequested(sender, e);
            }
            Cursor.Current = Cursors.Default;
        }
      }
}
