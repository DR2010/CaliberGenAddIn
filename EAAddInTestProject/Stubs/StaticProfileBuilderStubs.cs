using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Windows.Interfaces;

namespace EAAddInTestProject.Stubs
{
    class StaticProfileBuilderStubs
    {
    }

    public class StaticProfileBuilderViewStub : IStaticProfileBuilder
    {

        #region IStaticProfileBuilder Members

        public void Show()
        {
            return;
        }

        public string Diagram
        {
            get; set;
        }

        #endregion
    }
}
