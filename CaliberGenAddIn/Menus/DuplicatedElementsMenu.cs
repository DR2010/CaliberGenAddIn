using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Windows.Controls;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class DuplicatedElementsMenu : IEAMenu
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

            var drm = (UCDuplicatedElements)AddInRepository.Instance.Repository.AddWindow(
                                                                "Duplicated Elements",
                                                                "EAAddIn.Windows.Controls.UCDuplicatedElements");
        }
    }
}
