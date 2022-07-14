using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class EADbIndex : IDbIndex
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string InternalType { get; set; }

        public string Constraint { get; set; }
        public List<IDbColumn> Columns { get; set; }
        public List<IDbColumn> ForeignColumns { get; set; }
    }
}
