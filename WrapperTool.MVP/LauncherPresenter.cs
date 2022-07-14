using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using CICSWeb.Net;
using WrapperTool.Config;

namespace WrapperTool.MVP
{
    public class LauncherPresenter : IPresenter<WrapperConfig>
    {
        private static void Restore(WrapperConfig target, WrapperConfig source)
        {
            foreach (var item in target.ChildrenImport.Dictionary)
            {
                if (source.ChildrenImport.Dictionary.ContainsKey(item.Value.ArrayKey))
                {
                    item.Value.Value = source.ChildrenImport.Dictionary[item.Key].Value;
                    Restore(item.Value, source.ChildrenImport.Dictionary[item.Key]);
                }
            }
        }
        private static void Restore(IWrapperConfigItem target, IWrapperConfigItem source)
        {
            if (source.Children == null)
            {
                target.Children.Dictionary.Clear();
                return;
            }
            if (source.Children != null)
            {
                target.Children.Dictionary.Clear();
                source.Children.ToList().ForEach(x => target.Children.Add(x));
            }
            foreach (var item in target.Children.Dictionary)
            {
                if (source.Children.Dictionary.ContainsKey(item.Value.ArrayKey))
                    Restore(item.Value, source.Children.Dictionary[item.Key]);
            }
        }
        private static WrapperConfig LoadFile(string filename)
        {
            //// TODO: Add code here to save the current contents of the form to a file.
            var xmlSerializer = new DataContractSerializer(typeof(WrapperConfig), new Type[] { typeof(IWrapperConfigItem) });
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                WrapperConfig wrapperData = null;
                file.Seek(0, SeekOrigin.Begin);
                wrapperData = (WrapperConfig)xmlSerializer.ReadObject(file);
                wrapperData.Source = ConfigSourceType.Deserialized;
                return wrapperData;
            }


        }
        private static void Save(WrapperConfig wrapperData, string filename)
        {
            if (wrapperData == null) new ArgumentNullException("wrapperData");
            //// TODO: Add code here to save the current contents of the form to a file.
            var xmlSerializer = new DataContractSerializer(typeof(WrapperConfig), new Type[] { typeof(IWrapperConfigItem) });
            using (var file = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                file.Seek(0, SeekOrigin.Begin);
                xmlSerializer.WriteObject(file, wrapperData);
            }
        }

        private static string DefaultValues(string key)
        {
            if (string.Compare(key, "INWSYSTEMDATAWSYSSYSTEMDATE", true) == 0)
            {
                return CICSWeb.Net.Helper.CicsDateTimeToString(DateTime.Now);
            }
            if (string.Compare(key, "INWSYSTEMDATAWSYSUSERTYPE", true) == 0)
            {
                return "DYA";
            }
            if (string.Compare(key, "INWSYSTEMDATAWSYSOFFICECODE", true) == 0)
            {
                return "USRS";
            }

            return null;
        }

        private readonly ILauncherView view;
        private WrapperConfig model;
        public IEnumerable<WrapperToolSetting> Settings { get { return WrapperToolSetting.GetConfig(); } }
        protected internal LauncherPresenter(){
        }
        public LauncherPresenter(ILauncherView view)
            : this()
        {
            if (view == null) throw new ArgumentNullException("view");
            this.view = view;
            this.view.Clear += view_Clear;
            this.view.ClearEmpty += view_ClearEmpty;
            this.view.KillGroupsOnly += view_KillGroupsOnly;
            this.view.Execute += view_Execute;
            this.view.RefreshData += view_Refresh;
            this.view.SaveAs += view_Save;
            this.view.LoadData += view_LoadData;
            this.view.LoadTemplate += view_LoadTemplate;
        }

        void view_LoadTemplate(object sender, EventArgs e)
        {
            var reader = new ConfigFileReader(view.FileName);
            if (model != null)
            {
                var data = model;
                model = reader.LoadConfig();
                if (string.Compare(data.Wname, model.Wname) == 0
                    && view.NeedsToRestoreInput("New wrapper config seems to be the same as previous. Do you want to restore input?"))
                    Restore(model, data);
            }
            else
                model = reader.LoadConfig();
            view.UpdateAfterLoadTemplate();
        }

        void view_LoadData(object sender, EventArgs e)
        {
            model = LoadFile(view.FileName);
            view.UpdateAfterLoadData();
        }

        void view_Execute(object sender, EventArgs e)
        {
            if (model != null)
            {
                model.ChildrenImport.SetDefault(true, null);
                
                view.UpdateBeforeExecute();
                model.ChildrenExport.SetDefault(false, null);
                var setting = view.CurrentSettings;
                Uri url = new Uri(setting.GetValue<string>("TargetHostUrl"));
                var httpRequestString = Request.Build(model);
                view.ShowRequest(httpRequestString);
                string responseString = Http.GetCicsWebResponse(httpRequestString, url, model.Wname);

                var response = new Response(responseString);
                model = response.ReadResponseToData(model);
                view.ShowResponse(responseString);
                view.UpdateAfterExecute();
            }
        }

        void view_Refresh(object sender, EventArgs e)
        {
            string setting = view.CurrentSettings.GetValue<string>("SourceCVSTemplatePath");
            var reader = new ConfigFileReader(Path.Combine(setting, model.Wname + ".cvs"));
            var data = reader.LoadConfig();
            Restore(data, model);
            data.Source = ConfigSourceType.Deserialized;

            //TODO
            model = data;
            view.UpdateAfterRefresh();

        }

        void view_Save(object sender, SaveEventArgs e)
        {
            Save(model,e.FileName);
        }

        void view_Clear(object sender, EventArgs e)
        {
            model.ChildrenImport.SetDefault(false, DefaultValues);
            view.UpdateAfterClear();
        }
        void view_ClearEmpty(object sender, EventArgs e)
        {
            model.ChildrenImport.SetDefault(true, DefaultValues);
            view.UpdateAfterClear();
        }

        void view_KillGroupsOnly(object sender, EventArgs e)
        {
            view.KillGroups();
            view.UpdateAfterClear();
        }
        
        #region IPresenter Members

        public WrapperConfig Model
        {
            get { return model; }
        }

        public IView<WrapperConfig> View
        {
            get { return view; }
        }

        #endregion
    }
}
