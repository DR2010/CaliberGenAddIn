using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class EADbColumn : IDbColumn
    {
        #region IDbColumn Members

        public string Name { get; set; }
        public string DataType { get; set; }
        public int? Length { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public bool PK { get; set; }
        public bool FK { get; set; }
        public bool Unique { get; set; }
        public bool? NotNull { get; set; }
        public string DefaultValue { get; set; }
        public int Position { get; set; }

        #endregion
    }
}