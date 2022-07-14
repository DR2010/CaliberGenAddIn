using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAAddIn
{
    public class DBIndexDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> Columns { get; set; }
        public string Constraint { get; set; }
    }
}
