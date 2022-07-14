using System.Collections.Generic;


namespace EAAddIn
{
    public class DBTableDefinition
    {
        public string Name
        {
            get; set;
        }

        public Dictionary<string, DBColumnDefinition> Columns
        {
            get;
            set;
        }
        public Dictionary<string, DBIndexDefinition> Indexes
        {
            get;
            set;
        }

        public DBTableDefinition()
        {
            Columns = new Dictionary<string, DBColumnDefinition>();
            Indexes = new Dictionary<string, DBIndexDefinition>();
        }
    }
}
