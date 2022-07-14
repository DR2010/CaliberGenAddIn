using System.Collections.Generic;
using System.Runtime.InteropServices;
using EAAddIn.Windows;

namespace EAAddIn.Menus
{
    [ComVisible(false)]
    public class CSVClassImporterMenu : IEAMenu
    {

        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EACsvElementImporter; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var cci = new CsvClassImporter();
            cci.Show();
        }

        #endregion
    }
}
