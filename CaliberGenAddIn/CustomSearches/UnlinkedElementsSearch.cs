using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications;

namespace EAAddIn.CustomSearches
{
    public class UnlinkedElementsSearch
    {
        public static object Search(Repository Repository, string SearchTerm, out string XMLResults)
        {
            AddInRepository.Instance.Repository = Repository;

            Package selectedPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (selectedPackage == null)
            {
                MessageBox.Show("A package must be selected in the project browser");
                XMLResults = string.Empty;
                return null;
            }

            var unlinkedElements = ProcessPackage(selectedPackage, SearchTerm);

            XMLResults = XmlResultsBuilder.BuildXmlResults(unlinkedElements);
            return XMLResults;
        }


        private static List<ObjectDefinition> ProcessPackage(Package selectedPackage, string connectorTypes)
        {
            string packageIds = new EaAccess().getPackageList(selectedPackage);
            return new EaAccess().GetUnlinkedElementsFromPackages(packageIds, connectorTypes);
        }
    }
}