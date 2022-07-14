using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CICSWeb.Net
{
    [DataContract]
    [KnownType("GetKnownTypes")]
    public class CICSParameterCollection : IEnumerable<IWrapperConfigItem>
    {
        private IDictionary<string, IWrapperConfigItem> dictionary = new Dictionary<string, IWrapperConfigItem>();
        public void SetDefault(bool keep, Func<string,string> custom)
        {
            if (dictionary != null)
            {
                dictionary.ToList().ForEach(item => item.Value.SetDefault(keep, custom));                   
            }
        }
        public void Clear()
        {
            if (dictionary != null)
            {
                dictionary.Clear();
            }
        }
        [DataMember]
        public IDictionary<string, IWrapperConfigItem> Dictionary
        {
            get { return dictionary; }
            private set { dictionary = value; }
        }

        public void Add(IWrapperConfigItem item)
        {
            dictionary.Add(item.ArrayKey, item);
        }
        public static IEnumerable<Type> GetKnownTypes()
        {
            System.Collections.Generic.List<System.Type> knownTypes =
                new System.Collections.Generic.List<System.Type>();
            // Add any types to include here.
            knownTypes.Add(typeof(WrapperConfig));
            knownTypes.Add(typeof(WrapperConfigItem));
            return knownTypes;
        }

        #region IEnumerable<IWrapperConfigItem> Members

        IEnumerator<IWrapperConfigItem> IEnumerable<IWrapperConfigItem>.GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        #endregion

    }
    public enum ConfigSourceType
    {
        Template,
        Deserialized
    }
    [DataContract]
    public class WrapperConfig : CICSWeb.Net.IWrapperConfig
    {
        ConfigSourceType source = ConfigSourceType.Template;
        private CICSParameterCollection childrenImport = new CICSParameterCollection();
        private CICSParameterCollection childrenExport = new CICSParameterCollection();
        private byte[] integrity = new byte[34];

        internal StringBuilder rawText = new StringBuilder();
        public ConfigSourceType Source
        {
            get { return source; }
            set { source = value; }
        }
        [DataMember]
        public string Wname
        {
            get;
            set;
        }
        [DataMember]
        public string Bname
        {
            get;
            set;
        }
        [DataMember]
        public DateTime Time
        {
            get;
            set;
        }
        [DataMember]
        public byte[] Integrity
        {
            get { return integrity; }
            private set { integrity = value; }
        }
        [DataMember]
        public CICSParameterCollection ChildrenImport
        {
            get { return childrenImport; }
            private set { childrenImport = value; }
        }
        [DataMember]
        public CICSParameterCollection ChildrenExport
        {
            get { return childrenExport; }
            internal set { childrenExport = value; }
        }
        [DataMember]
        public long Exportstring
        {
            get;
            internal set;
        }
        [DataMember]
        public StringBuilder RawText
        {
            get { return rawText; }
            private set { rawText = value; }
        }
        internal void AddExport(WrapperConfigItem item)
        {
            childrenExport.Add(item);
        }
        internal void AddImport(WrapperConfigItem item)
        {
            childrenImport.Add(item);
        }
        internal void ExtendGroup(WrapperConfigItem parent)
        {
            if (parent==null || string.IsNullOrEmpty(parent.Value)) return;
            int count = -1;
            if (!int.TryParse(parent.Value, out count) || count < 1) return;
            if (parent.Children == null) return;
            int j = 0;
            foreach (var template in parent.Templates)
            {
                for (int i = 0; i < count; i++)
                {
                    var item = new WrapperConfigItem(template);
                    item.ArrayKey = string.Format("{0},{1}", parent.Attnum + 1 + j, i + 1);
                    parent.Children.Add(item);
                }
                j++;

            }
        }
    }
    [DataContract]
    public class WrapperConfigItem : IWrapperConfigItem
    {
        private CICSParameterCollection children = new CICSParameterCollection();
        private IList<WrapperConfigItem> templates = new List<WrapperConfigItem>();
        //internal int index;
        internal string name;
        private string value;
        [DataMember]
        public IList<WrapperConfigItem> Templates
        {
            get { return templates; }
            internal set { templates = value; }
        }

        [DataMember]
        public long Level
        {
            get;
            internal set;
        }
        [DataMember]
        public long Repeats
        {
            get;
            internal set;
        }

        [DataMember]
        public char Domain
        {
            get;
            internal set;
        }


        [DataMember]
        public string Name
        {
            get
            {
                string name = string.Empty;
                if ('G'.Equals(this.Type))
                    name = this.name;
                else
                    name = string.Format("{0} {1}", this.QName, this.AName);
                if (name.StartsWith("IN ")
                    ||
                    name.StartsWith("IN_"))
                {
                    return name.Substring(3);
                }
                else if (name.StartsWith("OUT ")
                    ||
                    name.StartsWith("OUT_"))
                {
                    return name.Substring(4);
                }
                return name;
            }
            private set
            {
                this.name = value;
            }
        }
        [DataMember]
        public string QName
        {
            get;
            internal set;
        }
        [DataMember]
        public string AName
        {
            get;
            internal set;
        }
        [DataMember]
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
        [DataMember]
        public string ArrayKey
        {
            get;
            internal set;
        }
        [DataMember]
        public string Key
        {
            get;
            internal set;
        }
        [DataMember]
        public char Type
        {
            get;
            internal set;
        }
        [DataMember]
        public long Length
        {
            get;
            internal set;
        }

        [DataMember]
        public CICSParameterCollection Children
        {
            get { return children; }
            internal set { children = value; }
        }
        [DataMember]
        public long Attnum
        {
            get;
            internal set;
        }
        public WrapperConfigItem() { }
        public WrapperConfigItem(WrapperConfigItem template,long attNum,string key, string arrayKey)
        {
            this.name = template.name;
            this.Key = key;
            this.AName = template.AName;
            this.QName = template.QName;
            this.Length = template.Length;
            this.Repeats = template.Repeats;
            this.Level = template.Level;
            this.Type = template.Type;
            this.Domain = template.Domain;
            this.Attnum = attNum;
            this.ArrayKey = arrayKey;
        }
        internal WrapperConfigItem(WrapperConfigItem template)
        {
            this.name = template.name;
            this.Key = template.Key;
            this.AName = template.AName;
            this.QName = template.QName;
            this.Length = template.Length;
            this.Repeats = template.Repeats;
            this.Level = template.Level;
            this.Type = template.Type;
            this.Domain = template.Domain;
            this.Attnum = template.Attnum;
            this.ArrayKey = template.ArrayKey;
        }

        #region ICICSParameter Members

        public void SetDefault(bool keep, Func<string, string> custom)
        {
            if ((keep && string.IsNullOrEmpty(this.value))
                || (!keep))
            {
                if (!Type.Equals('G'))
                {
                    string value = null;
                    if (custom != null)
                        value = custom(this.Key);

                    if (value == null)
                    {
                        if (Domain.Equals('D'))
                            this.Value = "00000000";
                        else if (Domain.Equals('N'))
                            this.Value = "0";
                        else
                            this.Value = " ";
                    }
                    else
                    {
                        this.Value = value;
                    }
                }
            }
            if (children != null)
                children.Dictionary.Values.ToList().ForEach(item => item.SetDefault(keep,custom));

        }

        #endregion
    }
}

