using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class GENTestMenu : IEAMenu
    {
        public string Name
        {
            get { return AddInApplications.EADuplicatedElements; }
        }

        public List<string> SecurityRoles
        {
            get
            {
                return new List<string> { EASecurityGroups.MainframeDeveloper,
                                            EASecurityGroups.BusinessAnalyst,
                                            EASecurityGroups.DBA    };
            }
        }


        public void ActivateAddIn()
        {


        }
    }
}
