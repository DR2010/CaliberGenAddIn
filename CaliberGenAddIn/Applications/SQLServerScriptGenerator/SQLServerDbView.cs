using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    class SQLServerDbView : IDbView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IDbColumn> Columns { get; set; }

        public string Code
        {
            get; set;
        }
    }
}
