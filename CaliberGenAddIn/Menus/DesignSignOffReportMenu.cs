using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Windows;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class DesignSignOffReportMenu : IEAMenu
    {

        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAFinalDesignSignOffReport; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var dsr = new DesignSignOffReport();
            dsr.Show();
        }

        #endregion
    }
}
