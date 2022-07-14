using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using CICSWeb.Net;
using WrapperTool.MVP;

namespace WrapperTool.WindowsForm
{
    internal partial class GroupDialog : Form, IGroupView
    {
        public DialogResult ShowDialog(IWin32Window parent, bool readOnly)
        {
            ReadOnly = readOnly;
            LoadGroup(this, new EventArgs());
            return base.ShowDialog(parent);
        }
        public GroupDialog()
        {
            InitializeComponent();
        }
        public GroupDialog(IWrapperConfigItem model) : this()
        {
            this.Presenter = new GroupPresenter(this, model);
        }
        public int Count
        {
            get { var dt = gridControl1.DataSource as DataTable;
            return dt.Rows.Count;
            }
        }
        public IEnumerable<IWrapperConfigItem> BuildListItems()
        {
            return Presenter.BuildListItems(gridControl1.DataSource as DataTable);
       }



        #region IView<IWrapperConfigItem> Members

        public IGroupPresenter<IWrapperConfigItem> Presenter
        {
            get;
            set;
        }

        IPresenter<IWrapperConfigItem> IView<IWrapperConfigItem>.Presenter
        {
            get { return Presenter; }
        }


        #endregion

        #region IGroupView Members

        public event EventHandler LoadGroup = delegate { };

        #endregion
        private bool ReadOnly
        {
            get;
            set;
        }
        #region IGroupView Members

        public void Loaded(DataTable data)
        {
            Presenter.Model.Templates.ToList().ForEach(x => gridControl1.Columns.Add(
            new DataGridViewTextBoxColumn { Name = x.Key, HeaderText = x.AName, DataPropertyName = x.Key }));

            gridControl1.AutoGenerateColumns = false;
            gridControl1.DataSource = data;
            gridControl1.ReadOnly = ReadOnly;
            btnSave.Enabled = !ReadOnly;
            this.Text = Presenter.Model.AName;

        }

        #endregion
    }
}
