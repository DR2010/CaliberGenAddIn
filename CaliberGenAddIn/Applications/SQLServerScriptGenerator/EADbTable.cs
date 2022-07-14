using System.Collections.Generic;
using System.Linq;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class EADbTable : IDbTable
    {
        #region IDbTable Members

        public string Name { get; set; }
        public List<IDbColumn> Columns { get; set; }
        public List<IDbIndex> Indices { get; set; }
        
        public IDbIndex FindIndex(string name)
        {
           return DbSchemaHelper.FindIndex(Indices, name);
        }

        public IDbColumn FindColumn(string name)
        {
            return DbSchemaHelper.FindColumn(Columns, name);
        }

        #endregion
    }
}