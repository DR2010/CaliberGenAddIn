using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EAAddIn.Interfaces.DbSchema
{
    [ComVisible(false)]
    public interface IDbView
    {
        int Id { get; set; }
        string Name { get; set; }
        List<IDbColumn> Columns { get; set; }
        string Code { get; set; }
    }
}
