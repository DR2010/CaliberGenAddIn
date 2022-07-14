using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EAAddIn.Interfaces.DbSchema
{
    [ComVisible(false)]
    public interface IDbTable
    {
        string Name { get; set; }

        List<IDbColumn> Columns { get; set; }
        List<IDbIndex> Indices { get; set; }

        IDbIndex FindIndex(string name);
        IDbColumn FindColumn(string name);
    }
}
