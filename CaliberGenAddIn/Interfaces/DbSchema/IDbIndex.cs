using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EAAddIn.Interfaces.DbSchema
{
    [ComVisible(false)]
    public interface IDbIndex
    {
        string Name { get; set; }
        string Type { get; set; }
        string InternalType { get; set; }
        string Constraint { get; set; }
        List<IDbColumn> Columns { get; set; }
        List<IDbColumn> ForeignColumns { get; set; }

    }
}
