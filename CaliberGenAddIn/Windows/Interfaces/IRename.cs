using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAAddIn.Windows.Interfaces
{
    interface IRename
    {
        string NewName { get; }
        string OldName { set; }
    }
}
