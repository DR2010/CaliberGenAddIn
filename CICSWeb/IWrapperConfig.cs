using System;
namespace CICSWeb.Net
{
    public interface IWrapperConfig
    {
        string Bname { get; set; }
        CICSParameterCollection ChildrenExport { get; }
        CICSParameterCollection ChildrenImport { get; }
        long Exportstring { get; }
        byte[] Integrity { get; }
        System.Text.StringBuilder RawText { get; }
        ConfigSourceType Source { get; set; }
        DateTime Time { get; set; }
        string Wname { get; set; }
    }
}
