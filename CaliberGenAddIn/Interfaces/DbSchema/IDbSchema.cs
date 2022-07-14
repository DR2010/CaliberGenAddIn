using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EAAddIn.Interfaces.DbSchema
{
    [ComVisible(false)]
    public interface IDbSchema
    {
        List<IDbTable> Tables { get; set; }
        List<IDbStoredProcedure> StoredProcedures { get; set; }
        List<IDbFunction> Functions { get; set; }
        List<IDbView> Views { get; set; }
        string Database { get; set; }
        string Type { get; }
        void Load();
        IDbTable FindTable(string table);

        IDbFunction FindFunction(string function);

        IDbStoredProcedure FindStoredProcedure(string storedProcedure);

        IDbView FindView(string view);
    }
}
