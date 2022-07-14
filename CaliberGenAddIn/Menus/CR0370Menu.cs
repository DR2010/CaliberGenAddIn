using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Windows;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class CR0370Menu : IEAMenu
    {

        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.NATION_CR0370; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var dsr = new CR0370AddIn();
            dsr.Show();
        }

        #endregion
    }
}
