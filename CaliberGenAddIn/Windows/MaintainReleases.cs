using System;

using System.Windows.Forms;

namespace EAAddIn.Windows
{
    public partial class MaintainReleases : Form, IMaintainReleases
    {
        private readonly BindingSource releasesBindingSource = new BindingSource();
        private readonly BindingSource streamsBindingSource = new BindingSource();
        private int previousReleaseIndex = -1;
        private int releaseIndex = -1;
        private bool suspendReleasesListBoxSelectedIndexChanged;

        public MaintainReleases()
        {
            InitializeComponent();
        }
        public BindingSource ReleasesBindingSource
        {
            get; set;
        }

        public string ReleasesDisplayMember
        {
            set { ReleasesListBox.DisplayMember = value; }
        }

        public string ReleasesValueMember
        {
            set { ReleasesListBox.ValueMember = value; }

        }

        public BindingSource StreamsBindingSource
        {
            get { return streamsBindingSource; }
        }

        public BindingSource MessagesBindingSource
        {
            get; set;
        }

        public void BindData()
        {
            ReleasesListBox.DataSource = ReleasesBindingSource;
            ReleaseStreamComboBox.DataSource = StreamsBindingSource;
            MessagesDataGridView.DataSource = MessagesBindingSource;
            MessagesDataGridView.Columns[1].Width = 700;
            MessagesDataGridView.Columns[2].Visible = false;
        }

        public bool ConfirmSave()
        {
            return
                (MessageBox.Show("Do you want to save changes?", "Unsaved Changes", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question) == DialogResult.Yes);
        }

        public void RestorePreviousReleaseSelection()
        {
            suspendReleasesListBoxSelectedIndexChanged = true;
            ReleasesListBox.SelectedIndex = previousReleaseIndex;
            suspendReleasesListBoxSelectedIndexChanged = false;
        }

        public void SaveEnabled(bool enabled)
        {
            SaveButton.Enabled = enabled;
        }

        public string ReleaseName
        {
            get { return NameTextBox.Text; }
            set { NameTextBox.Text = value; }
        }
        public DateTime ReleaseDate
        {
            get { return ReleaseDateDateTimePicker.Value;}
            set
            {
                if (value == new DateTime(1, 1, 1))
                {
                    ReleaseDateDateTimePicker.Value = DateTime.Now;
                }
                else
                {
                    ReleaseDateDateTimePicker.Value = value;
                    
                }
            }
        }
        public string Stream
        {
            get { return ReleaseStreamComboBox.SelectedItem.ToString(); }
            set { var index = ReleaseStreamComboBox.FindStringExact(value);
                ReleaseStreamComboBox.SelectedIndex = index == -1 ? 0 : index;
            }
        }

        public object SelectedRelease {
            get { return ReleasesListBox.SelectedValue;}
            set
            {
                var index = ReleasesListBox.Items.IndexOf(value);

                if (index > -1)
                {
                    ReleasesListBox.SelectedIndex = index;
                }
            }
        }

        public event EventHandler<EventArgs> ReleaseSelected;

        public event EventHandler<EventArgs> NewReleaseRequested;
        public event EventHandler<EventArgs> DeleteReleaseRequested;
        public event EventHandler<EventArgs> SaveReleaseRequested;
        public event EventHandler<EventArgs> ReleaseFormClosed;

        private void NewButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (NewReleaseRequested != null)
            {
                NewReleaseRequested(this, EventArgs.Empty);
            }
            Cursor.Current = Cursors.Default;
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (DeleteReleaseRequested != null)
            {
                DeleteReleaseRequested(this, EventArgs.Empty);
            }
            Cursor.Current = Cursors.Default;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (SaveReleaseRequested != null)
            {
                SaveReleaseRequested(this, EventArgs.Empty);
            }
            Cursor.Current = Cursors.Default;
        }

        private void ReleasesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suspendReleasesListBoxSelectedIndexChanged)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            previousReleaseIndex = releaseIndex;
            releaseIndex = ReleasesListBox.SelectedIndex;

            if (ReleaseSelected != null)
            {
                ReleaseSelected(this, EventArgs.Empty);
            }
            Cursor.Current = Cursors.Default;
        }

        private void MaintainReleases_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (ReleaseFormClosed != null)
            {
                ReleaseFormClosed(this, EventArgs.Empty);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
