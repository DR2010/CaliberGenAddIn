using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications;
using System.Data.SqlClient;

namespace EAAddIn.CustomSearches
{
    public class AuditSearch
    {
        public static object Search(Repository repository, string term, out string XMLResults, EaAccess access = null)
        {
            AddInRepository.Instance.Repository = repository;

            Package package = null;

            Element element = EaAccess.GetSelectedElement();

            if (element == null)
            {
                package = repository.GetTreeSelectedPackage();

                if (package == null)
                {
                    XMLResults = string.Empty;
                    MessageBox.Show("Please select an element or a package to search for audit entries");
                    return new SearchResults();
                }
            }

            SearchResults searchResults;

            if (package == null)
            {
                searchResults = (access ?? new EaAccess()).GetAuditEntriesForElement(element.ElementGUID);
            }
            else
            {
                searchResults = (access ?? new EaAccess()).GetAuditEntriesForPackageId(package.PackageID);
            }

            var xmlResults = XmlHelpers.SerializeObject<SearchResults>(searchResults);

            // Remove the standard XML header
            XMLResults = xmlResults.Substring(39);

            return xmlResults;
        }


    }
}
