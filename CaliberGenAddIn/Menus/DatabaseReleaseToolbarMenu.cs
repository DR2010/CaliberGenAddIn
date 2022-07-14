using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Windows;

namespace EAAddIn.Menus {

    [ComVisible(false)]
    public class DatabaseReleaseToolbarMenu : IEAMenu
    {

        public string Name
        {
            get { return AddInApplications.DBAReleaseToolbar; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.DBA }; }
        }


		public void ActivateAddIn(){
            var drtb = new DatabaseReleaseToolbar();
            drtb.Show();
            //var drm = (DatabaseReleaseToolbarControl)AddInRepository.Instance.Repository.AddWindow("DB Release Toolbar", "EAAddIn.Windows.Controls.DatabaseReleaseToolbarControl");

            //var form = new RenamedItemForm();

            //form.ShowDialog();


            //AddInRepository.Instance.Repository.ActivateTab(form.NewName);
 
		}

	}

}
