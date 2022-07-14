using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Applications.ElementLinker;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class ElementLinkerMenu : IEAMenu
    {
        public string Name
        {
            get { return AddInApplications.EAElementLinker; }
        }

        public List<string> SecurityRoles
        {
            get
            {
                return new List<string> ();
            }
        }

        public void ActivateAddIn()
        {
            if (AddInRepository.Instance.Repository.IsTabOpen(AddInApplications.EAElementLinker) == 0)
            {
                AddInRepository.Instance.Repository.ActivateTab(AddInApplications.EAElementLinker);
                return;
            }

            AddInRepository.Instance.Repository.AddWindow(AddInApplications.EAElementLinker,
                                                                 "EAAddIn.Applications.ElementLinker.ElementLinkerControl");

        }
    }
}
