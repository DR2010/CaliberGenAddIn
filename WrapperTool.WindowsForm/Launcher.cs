using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CICSWeb.Net;
using WeifenLuo.WinFormsUI.Docking;
using WrapperTool.Config;
using WrapperTool.MVP;

namespace WrapperTool.WindowsForm
{
    public partial class Launcher : DockContent, ILauncherView
    {
        CICSParameterCollectionView improtList;
        CICSParameterCollectionView exportList;
        void LoadList(
            DataGridView grid, IList<ICICSParameterView<IWrapperConfigItem>> list)
        {
            grid.DataSource = list;
            Helper.ResizeGrid(grid);
        }
        void EditGroup(ICICSParameterView<IWrapperConfigItem> item)
        {
            using (var dialog = new GroupDialog(item.Source))
            {
                if (dialog.ShowDialog(this, false) == DialogResult.OK)
                {
                    var list = dialog.BuildListItems();
                    item.Source.Children.Dictionary.Clear();
                    list.ToList().ForEach(x=> item.Source.Children.Add(x));
                    item.Value = dialog.Count.ToString();
                }
            }

        }
        void ShowGroup(ICICSParameterView<IWrapperConfigItem> item)
        {
            using (var dialog = new GroupDialog(item.Source))
            {
                dialog.ShowDialog(this,true);
            }

        }

        public Launcher()
        {
            InitializeComponent();
            gridControl1.AutoGenerateColumns = true;
            this.Presenter = new LauncherPresenter(this);
            toolStripComboBox1.Items.AddRange(this.Presenter.Settings.ToArray());
            toolStripComboBox1.SelectedIndex = 0;

        }

        public string FileName
        {
            get;
            private set;
        }

        private void RefreshLists()
        {
            if (Presenter.Model == null) return;
            gridControl1.ReadOnly = false;
            LoadList(gridControl1, improtList.List());
            gridControl2.ReadOnly = true;
            LoadList(gridControl2, exportList.List());
        }
        private void RefreshLists(bool afterExecuted)
        {
            if (afterExecuted)
            {
                if (Presenter.Model == null) return;
                gridControl2.ReadOnly = true;
                LoadList(gridControl2, exportList.List());
            }
            else
            {
                RefreshLists();
            }
        }

        private void toolStrip1_Click(object sender, EventArgs e)
        {
            try
            {
                execute(this, new EventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        public void UpdateBeforeExecute()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

        }
        public void ShowResponse(string response)
        {
            textBox2.Text = response;
        }
        public void UpdateAfterExecute()
        {
            exportList = new CICSParameterCollectionView(Presenter.Model.ChildrenExport);
            RefreshLists(true);
        }
        public void ShowRequest(string request)
        {
            textBox1.Text = request;
        }
        private void Launcher_Resize(object sender, EventArgs e)
        {
            Helper.ResizeGrid(gridControl1);
            Helper.ResizeGrid(gridControl2);

        }

        private void splitContainer1_Panel1_ClientSizeChanged(object sender, EventArgs e)
        {
            Helper.ResizeGrid(gridControl1);
            Helper.ResizeGrid(gridControl2);
        }

        private void splitContainer1_Panel2_ClientSizeChanged(object sender, EventArgs e)
        {
            Helper.ResizeGrid(gridControl1);
            Helper.ResizeGrid(gridControl2);
        }
        public void UpdateAfterRefresh()
        {
            ShowFile();

        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            refresh(this,new EventArgs());
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            clear(this, new EventArgs());
        }
        public void KillGroups()
        {
            foreach (var item in improtList.List())
            {
                if (item.Source.Type.Equals('G'))
                {
                    item.Source.Children.Clear();
                    item.Value = "0";
                }
            }
        }

        public void UpdateAfterClear()
        {
            RefreshLists();
        }
        public void OpenFromTemplate(string fileName)
        {
            FileName = fileName;
            loadTemplate(this, new EventArgs());
            Text = fileName;
            TabText = fileName;
        }
        public void OpenFromData(string fileName)
        {
            FileName = fileName;
            loadData(this, new EventArgs());
            Text = fileName;
            TabText = fileName;
        }
        public void Save()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Application.ExecutablePath; //Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                saveAs(this,new SaveEventArgs(saveFileDialog.FileName));
            }

        }
        private void ShowFile()
        {
            improtList = new CICSParameterCollectionView(Presenter.Model.ChildrenImport);
            exportList = new CICSParameterCollectionView(Presenter.Model.ChildrenExport);
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = Presenter.Model.RawText.ToString();
            RefreshLists();
            this.ToolTipText = FileName;

        }

        private void gridControl1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dataGrid = sender as DataGridView;
            var item = dataGrid.Rows[e.RowIndex].DataBoundItem as ICICSParameterView<IWrapperConfigItem>;
            if (item.Source.Type.Equals('G'))
            {
                e.Cancel = true;
                EditGroup(item);
            }
        }

        private void gridControl2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGrid = sender as DataGridView;
            var item = dataGrid.Rows[e.RowIndex].DataBoundItem as ICICSParameterView<IWrapperConfigItem>;
            if (item.Source.Type.Equals('G'))
            {
                ShowGroup(item);
            }
        }


        #region IView Members
        public WrapperToolSetting CurrentSettings
        {
            get { return (WrapperToolSetting)toolStripComboBox1.SelectedItem; }
        }


        public IPresenter<WrapperConfig> Presenter
        {
            get;
            set;
        }

        event EventHandler clear = delegate { };
        event EventHandler clearEmpty = delegate { };
        event EventHandler refresh = delegate { };
        event EventHandler execute = delegate { };
        event EventHandler<SaveEventArgs> saveAs = delegate { };
        event EventHandler loadTemplate = delegate { };
        event EventHandler loadData = delegate { };
        event EventHandler killGroupsOnly = delegate { };

        public event EventHandler ClearEmpty
        {
            add { clearEmpty += value; }
            remove { clearEmpty -= value; ; }
        }

        public event EventHandler LoadTemplate
        {
            add { loadTemplate += value; }
            remove { loadTemplate -= value; ; }
        }
        public event EventHandler LoadData
        {
            add { loadData += value; }
            remove { loadData -= value; ; }
        }



        public event EventHandler Execute
        {
            add { execute += value; }
            remove { execute -= value; ; }
        }

        public event EventHandler<SaveEventArgs> SaveAs
        {
            add { saveAs += value; }
            remove { saveAs -= value; ; }
        }
        public event EventHandler RefreshData
        {
            add { refresh += value; }
            remove { refresh -= value; ; }
        }
        public event EventHandler Clear
        {
            add { clear += value; }
            remove { clear -= value; ; }
        }

        public event EventHandler KillGroupsOnly
        {
            add { killGroupsOnly += value; }
            remove { killGroupsOnly -= value; ; }
        }


        #endregion

        #region IView Members

        public bool NeedsToRestoreInput(string text)
        {
            return MessageBox.Show(text, "Load", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        #endregion

        #region IView Members


        public void UpdateAfterLoadTemplate()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = Presenter.Model.RawText.ToString();
            improtList = new CICSParameterCollectionView(Presenter.Model.ChildrenImport);
            exportList = new CICSParameterCollectionView(Presenter.Model.ChildrenExport);

            RefreshLists();
            this.ToolTipText = FileName;
        }

        public void UpdateAfterLoadData()
        {
            ShowFile();
        }

        #endregion

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            killGroupsOnly(this, new EventArgs());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            clearEmpty(this, new EventArgs());
        }
    }
}
