using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    class SQLServerDbStoredProcedure : IDbStoredProcedure
    {
        public string Name
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }
    }
}
