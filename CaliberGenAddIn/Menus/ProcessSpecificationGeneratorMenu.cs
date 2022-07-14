using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Applications.SpecificationGenerator;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class ProcessSpecificationGeneratorMenu : IEAMenu
    {

        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAProcessSpecificationGenerator; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var dsr = new ProcessSpecificationGenerator();
            dsr.BuildSpecification();
        }

        #endregion
    }
}
