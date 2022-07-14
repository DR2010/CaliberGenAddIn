using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EAAddIn.Interfaces.DbSchema
{
    [ComVisible(false)]
    public interface IDbColumn
    {
        string Name { get; set; }
        string DataType { get; set; }
        int? Length { get; set; }
        int? Precision { get; set; }
        int? Scale { get; set; }
        bool PK { get; set; }
        bool FK { get; set; }
        bool Unique { get; set; }
        bool? NotNull { get; set; }
        string DefaultValue { get; set; }
        int Position { get; set; }
    }
}
