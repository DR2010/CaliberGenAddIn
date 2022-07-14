using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications;

namespace EAAddIn.CustomSearches
{
    public static class ChangedItemsSearch
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
            var dates = SearchTerm.Split(',');

            DateTime from = new DateTime();
            DateTime to = new DateTime();

            if ((dates.Length == 1) 
                || (dates.Length == 2 && DateTime.TryParse(dates[0], out from) && DateTime.TryParse(dates[1], out to)) 
                || (dates.Length == 3 && DateTime.TryParse(dates[1], out from) && DateTime.TryParse(dates[2], out to)))
            {
                if (dates.Length == 1)
                {
                    from = DateTime.Now.AddDays(-14);
                    to = DateTime.Now;
                    var days = 0;

                    

                    if (!string.IsNullOrEmpty(dates[0]) )
                    {
                        if (int.TryParse(dates[0], out days))
                        {
                            from = DateTime.Now.AddDays(-days);
                        }
                        else
                        {
                            selectedPackage = AddInRepository.Instance.Repository.GetPackageByGuid(dates[0]);
                        }
                    }

                    from = new DateTime(from.Year, from.Month, from.Day);

                }
                var changedElements = ProcessPackage(selectedPackage, from, to);

                XMLResults = XmlResultsBuilder.BuildXmlResults(changedElements);
 
                return XMLResults;
            }

            MessageBox.Show("Two comma separated dates, or days to search, or a package GUID must be provided.");
            XMLResults = string.Empty;

            return null;
        }

        private static List<ObjectDefinition> ProcessPackage(Package selectedPackage, DateTime from, DateTime to)
        {
            string packageIds = new EaAccess().getPackageList(selectedPackage);
            return new EaAccess().GetChangedElementsFromPackages(packageIds, from, to);
        }
    }

}
