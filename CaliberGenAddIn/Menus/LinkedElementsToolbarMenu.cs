using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Windows.Controls;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class LinkedElementsToolbarMenu : IEAMenu
    {
        public string Name
        {
            get { return AddInApplications.EAPlaceLinkedElements; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.MainframeDeveloper,
                                            EASecurityGroups.BusinessAnalyst,
                                            EASecurityGroups.DBA    }; }
        }


        public void ActivateAddIn()
        {
            string tabName = AddInApplications.EAPlaceLinkedElements;

            var openTab = AddInRepository.Instance.Repository.IsTabOpen(tabName);
            
            switch (openTab)
            {
                case 2:
                    // 2 to indicate that a tab is open and active (top-most) 
                    break;
                case 1:
                    // 1 to indicate that it is open but not top-most
                    AddInRepository.Instance.Repository.ActivateTab(tabName);
                    break;
                case 0:
                    // 0 to indicate that it is not visible at all. 
                    var drm = (LinkedElementsToolbarControl)AddInRepository.Instance.Repository.AddWindow(
                                                                                    AddInApplications.EAPlaceLinkedElements, 
                                                                                    "EAAddIn.Windows.Controls.LinkedElementsToolbarControl");
                    //var resp = AddInRepository.Instance.Repository.ActivateToolbox(drm.Name, 0);
                    //AddInRepository.Instance.Repository.ActivateTab(tabName);

                    // AddInRepository.Instance.Repository.ShowWindow(10);

                    break;
                default:
                    break;
 
            }
        }
    }
}
