using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;

namespace EAAddIn.CustomSearches
{
    public class ElementsNotUsedInDiagramsSearch
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

            var unlinkedElements = ProcessPackage(selectedPackage);

            XMLResults = XmlResultsBuilder.BuildXmlResults(unlinkedElements);
            return XMLResults;
        }


        private static List<ObjectDefinition> ProcessPackage(Package selectedPackage)
        {
            string packageIds = new EaAccess().getPackageList(selectedPackage);
            return new EaAccess().GetElementsNotUsedInDiagramsFromPackages(packageIds);
        }
    }
}
