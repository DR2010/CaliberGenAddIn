using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    class SQLServerDbTable : IDbTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IDbColumn> Columns { get; set; }
        public List<IDbIndex> Indices { get; set; }

        public IDbIndex FindIndex(string name)
        {
            return DbSchemaHelper.FindIndex(Indices, name);

        }
        /// <summary>
        /// Find a column based on it's name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>IDbColumn</returns>
        public IDbColumn FindColumn(string name)
        {
            return DbSchemaHelper.FindColumn(Columns, name);
        }
    }
}
