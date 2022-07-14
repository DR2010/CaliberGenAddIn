using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAAddIn
{
    public class DBColumnDefinition
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Length { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool PK { get; set; }
        public bool FK { get; set; }
        public bool NotNull { get; set; }
        public bool Unique { get; set; }

    }
}
