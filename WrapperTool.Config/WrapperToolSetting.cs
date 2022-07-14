using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace WrapperTool.Config
{
    public class WrapperToolSetting
    {

        private static List<WrapperToolSetting> settings;
        protected string name;
        protected Dictionary<string, object> values = new Dictionary<string, object>();

        public WrapperToolSetting(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return name; }
        }
        public void Add<T>(string name,T value)
        {
            values.Add(name, value);
        }
        public T GetValue<T>(string name)
        {
            return (T)values[name]; 
        }
        public override string ToString()
        {
            return name;
        }
        public static IEnumerable<WrapperToolSetting> GetConfig()
        {
            if (settings == null)
            {
                settings = new List<WrapperToolSetting>();
                LoadConfig();
            }
            return settings;
        }
        private static void LoadConfig()
        {
            var fi = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var doc = XDocument.Load(string.Format("{0}\\.\\{1}", fi.Directory, "Settings.xml"));
            doc.Element("WrapperToolSettings").Elements().Where(x => x.Name.LocalName == "Environment").ToList().ForEach(
                delegate(XElement el)
                {
                    var a = new WrapperToolSetting(el.Attribute("Value").Value);
                    a.Add<string>(
                        "TargetHostUrl", el.Element("TargetHostUrl").Attribute("Value").Value
                        );
                    a.Add<string>("SourceCVSTemplatePath",
                        el.Element("SourceCVSTemplatePath").Attribute("Value").Value
                        );
                    a.Add<string>("SourceXMLTemplatePath",
                        el.Element("SourceXMLTemplatePath").Attribute("Value").Value
                        );

                    settings.Add(a);
                }
                );
        }
    }
}
