using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class AddInInstallerMenu : IEAMenu
    {

        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAInstallAddIn; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            EAAddIn.InstallAddIn();
        }

        #endregion
    }
}
