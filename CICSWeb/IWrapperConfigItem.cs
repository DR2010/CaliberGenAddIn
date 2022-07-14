using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
namespace CICSWeb.Net
{
    public interface IWrapperConfigItem
    {
        [DataMember]
        string AName { get; }
        [DataMember]
        string ArrayKey { get; }
        [DataMember]
        long Attnum { get; }
        [DataMember]
        CICSParameterCollection Children { get; }
        IList<WrapperConfigItem> Templates { get; }
        [DataMember]
        char Domain { get; }
        [DataMember]
        string Key { get; }
        [DataMember]
        long Length { get; }
        [DataMember]
        long Level { get; }
        [DataMember]
        string Name { get; }
        [DataMember]
        string QName { get; }
        [DataMember]
        long Repeats { get; }
        [DataMember]
        char Type { get; }
        [DataMember]
        string Value { get; set; }
        void SetDefault(bool keep, Func<string, string> custom);
    }
}
