using System;

namespace CICSWeb.Net
{
    public static class WrapperConfigExtention
    {

        public static IWrapperConfigItem GetValueByKey(this WrapperConfig data, string key, IWrapperConfigItem parent)
        {

            CICSParameterCollection list = null;
            if (parent == null)
            {
                if (key.StartsWith("IN"))
                    list = data.ChildrenImport;
                else if (key.StartsWith("OUT"))
                    list = data.ChildrenExport;
                else
                    throw new ArgumentException();
            }
            else
                list = parent.Children;

            if (list.Dictionary.ContainsKey(key))
                return list.Dictionary[key];
            else
            {
                foreach (var item in list.Dictionary)
                {
                    var item1 = GetValueByKey(data, key, item.Value);
                    if (item1 != null) return item1;
                }
            }
            return null;
        }

        public static WrapperConfigItem GetValueByArrayKey(this WrapperConfig data, int flag, string key, IWrapperConfigItem parent)
        {
            CICSParameterCollection list = null;
            if (parent == null)
            {
                if (flag == 0)
                    list = data.ChildrenImport;
                else
                    list = data.ChildrenExport;
            }
            else
                list = parent.Children;
            if (list.Dictionary.ContainsKey(key))
                return (WrapperConfigItem)list.Dictionary[key];
            foreach (var item in list.Dictionary)
            {
                WrapperConfigItem item1 = (WrapperConfigItem)GetValueByArrayKey(data, -1, key, item.Value);
                if (item1 != null) return item1;
            }
            return null;
        }
    }
}
