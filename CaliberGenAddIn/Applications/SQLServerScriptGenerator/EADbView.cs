using System.Collections.Generic;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class EADbView : IDbView
    {
        #region IDbView Members

        public int Id { get; set; }
        public string Name { get; set; }
        public List<IDbColumn> Columns { get; set; }
        public string Code
        {
            get;
            set;
        }
        #endregion
    }
}