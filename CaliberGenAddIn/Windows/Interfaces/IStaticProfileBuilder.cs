using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAAddIn.Windows.Interfaces
{
    public interface IStaticProfileBuilder
    {
        void Show();
        string Diagram { get; set; }
    }
}
