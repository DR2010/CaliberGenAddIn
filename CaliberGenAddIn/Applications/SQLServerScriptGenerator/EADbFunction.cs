using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class EADbFunction : IDbFunction
    {
        #region IDbFunction Members

        public string Name { get; set; }

        public string Code { get; set; }

        #endregion
    }
}