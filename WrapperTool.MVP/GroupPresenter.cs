using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CICSWeb.Net;
using WrapperTool.Config;

namespace WrapperTool.MVP
{
    public class GroupPresenter : IGroupPresenter<IWrapperConfigItem>
    {
        private readonly IGroupView view;
        private readonly IWrapperConfigItem model;
        private GroupPresenter() { }
        public GroupPresenter(IGroupView view, IWrapperConfigItem model)
            : this() 
        {
            this.view = view;
            this.model = model;
            this.view.LoadGroup += view_LoadGroup;
        }

        void view_LoadGroup(object sender, EventArgs e)
        {
            BuildList();
        }
        private DataTable BuildXml()
        {
            DataTable dt = new DataTable();
            Model.Templates.ToList().ForEach(
                x => dt.Columns.Add(x.Key, typeof(string))
                );
            if (Model.Children != null)
            {
                long count;
                long.TryParse(Model.Value, out count);
                for (int i = 1; i <= count; i++)
                {
                    var dr = dt.NewRow();
                    foreach (var template in Model.Templates)
                    {
                        WrapperConfigItem template1 = template;
                        int i1 = i;
                        dr[template.Key] = Model.Children.SingleOrDefault
                            (x => x.ArrayKey == string.Format("{0},{1}"
                                , template1.Attnum, i1)).With(x => x.Value);
                    }
                    dt.Rows.Add(dr);
                }
            }
            dt.AcceptChanges();
            return dt;
        }
        private void BuildList()
        {
            if (Model.Templates == null || Model.Templates.Count == 0)
                throw new InvalidOperationException("Templates cannot be empty");

            var dataSet = new DataSet();
            dataSet.Tables.Add(BuildXml());
            view.Loaded(dataSet.Tables[0]);

        }
        public IEnumerable<IWrapperConfigItem> BuildListItems(DataTable dt)
        {
            dt.AcceptChanges();
            var list = new List<IWrapperConfigItem>();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                foreach (var template in Model.Templates)
                {
                    var key = string.Format("{0},{1}", template.Attnum, i);
                    list.Add(
                        new WrapperConfigItem(template, i, null, key) { Value = dr[template.Key].ReturnDb(x=>(string)x,string.Empty) });
                }
                i++;
            }
            return list;
        }

        #region IPresenter Members

        public IWrapperConfigItem Model
        {
            get { return model; }
        }

        public IView<IWrapperConfigItem> View
        {
            get { return view; }
        }

        public IEnumerable<WrapperToolSetting> Settings
        {
            get { return WrapperToolSetting.GetConfig(); }
        }

        #endregion
    }
}
