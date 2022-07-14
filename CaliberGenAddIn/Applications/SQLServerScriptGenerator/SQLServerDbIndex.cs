using System.Collections.Generic;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    internal class SQLServerDbIndex : IDbIndex
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public int DefaultColumnId { get; set; }

        #region IDbIndex Members

        public string Name { get; set; }
        public string Type { get; set; }
        public string InternalType { get; set; }
        public string Constraint { get; set; }
        public List<IDbColumn> Columns { get; set; }
        public List<IDbColumn> ForeignColumns { get; set; }

        #endregion
    }
}