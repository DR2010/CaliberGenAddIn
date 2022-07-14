using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAAddIn.Interfaces.DbSchema
{
    public interface IDbFunction
    {
        string Name { get; set; }
        string Code { get; set; }
    }
}
