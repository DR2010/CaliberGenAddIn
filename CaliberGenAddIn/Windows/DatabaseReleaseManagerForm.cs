using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using EAAddIn.Presenters;
// using EAAddIn.Reports;
using EAAddIn.Windows.Interfaces;
using System.Linq;


namespace EAAddIn.Windows
{
    public partial class DatabaseReleaseManagerForm : Form, IDatabaseReleaseManager
    {
        private readonly BindingSource releasesBindingSource = new BindingSource();

        public DatabaseReleaseManagerForm()
        {
            InitializeComponent();

            new DatabaseReleaseManagerPresenter(this);
        }

        public BindingSource ReleasesBindingSource
        {
            get { return releasesBindingSource; }
        }
        public string ReleasesDisplayMember
        {
            set { ReleasesComboBox.DisplayMember = value; }
        }
        public string ReleasesValueMember
        {
            set { ReleasesComboBox.ValueMember = value; }
        }
        public Object SelectedRelease
        {
            get { return ReleasesComboBox.SelectedItem; }
        }
        public BindingSource MessagesBindingSource
        {
            get;
            set;
        }
        public void BindReleases()
        {
            ReleasesComboBox.DataSource = releasesBindingSource;
        }
        public void BindMessages()
        {
            MessagesDataGridView.DataSource = MessagesBindingSource;
            MessagesDataGridView.Columns[1].Width = 700;
            MessagesDataGridView.Columns[2].Visible = false;
        }

        public event EventHandler<EventArgs> MaintainReleasesRequested;
        public event EventHandler<EventArgs> PreviewMessagesReportRequested;
        public event EventHandler<EventArgs> CreateStereotypesRequested;
        public event EventHandler<EventArgs> DeleteStereotypesRequested;
        public event EventHandler<EventArgs> CleanupEaRelease1Requested;
        public event EventHandler<EventArgs> CleanupEaDbRepositoryRequested;
        public event EventHandler<EventArgs> ReleaseSelected;
        public event Message.MessagesHandler LocateInBrowserRequested;
        public event Message.MessagesHandler ClearStereotypesRequested;
        public event Message.MessagesHandler FindStereotypeUsageRequested;


        private void MaintainReleasesButton_Click(object sender, EventArgs e)
        {
            if (MaintainReleasesRequested != null)
            {
                MaintainReleasesRequested(sender, e);
            }
        }
        private void CreateStereotypesButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (CreateStereotypesRequested != null)
            {
                CreateStereotypesRequested(sender, e);
            }

            Cursor.Current = Cursors.Default;
        }
        private void LocateInBrowserMenu_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (LocateInBrowserRequested != null)
            {
                var args = new MessagesEventArgs();
                if (MessagesDataGridView != null &&
                    MessagesDataGridView.SelectedRows.Count >= 1 &&
                    MessagesDataGridView.SelectedRows[0].Cells.Count >= 3)
                {
                    args.Tags = new List<object> {MessagesDataGridView.SelectedRows[0].Cells[2].Value};

                    LocateInBrowserRequested(sender, args);
                }
            }

            Cursor.Current = Cursors.Default;
        }
        private void ClearStereotypesMenu_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (ClearStereotypesRequested != null)
            {
                var args = new MessagesEventArgs();
                if (MessagesDataGridView != null && 
                    MessagesDataGridView.SelectedRows.Count >= 1 &&
                    MessagesDataGridView.SelectedRows[0].Cells.Count >= 3)
                {
                    var tagList = new List<object>();
                    foreach (DataGridViewRow selectedRow in MessagesDataGridView.SelectedRows)
                    {
                        if (selectedRow.Cells[2] != null)
                        {
                            tagList.Add(selectedRow.Cells[2].Value);
                        }
                    }
                    args.Tags = tagList;

                    ClearStereotypesRequested(sender, args);
                }
            }

            Cursor.Current = Cursors.Default;
        }
        private void CleanupEaRelease1Button_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (CleanupEaRelease1Requested != null)
            {
                CleanupEaRelease1Requested(sender, e);
            }
            Cursor.Current = Cursors.Default;
        }
        private void CleanupEaDbRepositoryButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (CleanupEaDbRepositoryRequested != null)
            {
                try
                {
                    CleanupEaDbRepositoryRequested(sender, e);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Unknown error: " + exception.Message, "Clean BR Repository", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            Cursor.Current = Cursors.Default;
        }
        private void ReleasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (ReleaseSelected != null)
            {
                ReleaseSelected(sender, e);
            }
            Cursor.Current = Cursors.Default;
        }
        private void DeleteStereotypesButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (DeleteStereotypesRequested != null)
            {
                DeleteStereotypesRequested(sender, e);
            }
            Cursor.Current = Cursors.Default;
        }
        private void previewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (PreviewMessagesReportRequested != null)
            {
                PreviewMessagesReportRequested(sender, e);
            }
            Cursor.Current = Cursors.Default;
        }
        private void MessagesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            BuildContextMenu();
        }
        private void BuildContextMenu()
        {
            MessagesContextMenuStrip = new ContextMenuStrip();

            MessagesContextMenuStrip.Items.Add("Preview Report...", null, previewReportToolStripMenuItem_Click);

            if (MessagesDataGridView.SelectedRows.Count == 1)
            {
                MessagesContextMenuStrip.Items.Add("Locate in project browser", null, LocateInBrowserMenu_Click);
            }
            if (MessagesDataGridView.SelectedRows.Count > 0)
            {
                var text = MessagesDataGridView.SelectedRows.Count == 1 ? "Clear stereotype" : "Clear stereotypes";

                MessagesContextMenuStrip.Items.Add(text, null, ClearStereotypesMenu_Click);
            }
            MessagesDataGridView.ContextMenuStrip = MessagesContextMenuStrip;
        }
        private void DatabaseReleaseManagerForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DeleteStereotypesButton.Visible = !DeleteStereotypesButton.Visible;
            CreateStereotypesButton.Visible = !CreateStereotypesButton.Visible;
        }
        private void FindUsagesButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (FindStereotypeUsageRequested != null)
            {
                var args = new MessagesEventArgs();

                FindStereotypeUsageRequested(sender, args);
            }

            Cursor.Current = Cursors.Default;
        }

    }
}
