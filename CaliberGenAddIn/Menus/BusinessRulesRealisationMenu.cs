using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Windows.Controls;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class BusinessRulesRealisationMenu : IEAMenu 
    {
        public string Name
        {
            get { return AddInApplications.DBAReleaseManager; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> (); }
        }


        public void ActivateAddIn()
        {
            var drm = (BusinessRulesRealisationUserControl)AddInRepository.Instance.Repository.AddTab("Package Elements Relationships", "EAAddIn.Windows.Controls.BusinessRulesRealisationUserControl");
        }
    }
}




