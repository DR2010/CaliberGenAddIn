using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Applications;

namespace EAAddIn.CustomSearches
{
    public static class XmlResultsBuilder
    {
        public static string BuildXmlResults(List<ObjectDefinition> elements)
        {
            string XMLResults;
            var searchResult = new SearchResults();

            searchResult.Fields.Add(new Field() { Name = "CLASSTYPE" });
            searchResult.Fields.Add(new Field() { Name = "CLASSGUID" });
            searchResult.Fields.Add(new Field() { Name = "Name" });
            searchResult.Fields.Add(new Field() { Name = "Type" });
            searchResult.Fields.Add(new Field() { Name = "Notes" });
            searchResult.Fields.Add(new Field() { Name = "Created" });
            searchResult.Fields.Add(new Field() { Name = "Modified" });

            foreach (ObjectDefinition definition in elements)
            {
                var row = new Row();

                row.Fields.Add(new Field() { Name = "CLASSTYPE", Value = definition.Type });
                row.Fields.Add(new Field() { Name = "CLASSGUID", Value = definition.Guid });
                row.Fields.Add(new Field() { Name = "Name", Value = definition.Name });
                row.Fields.Add(new Field() { Name = "Type", Value = definition.Type });
                row.Fields.Add(new Field() { Name = "Notes", Value = definition.Note });
                row.Fields.Add(new Field() { Name = "Created", Value = definition.Created.ToShortDateString() });
                row.Fields.Add(new Field() { Name = "Modified", Value = definition.Modified.ToShortDateString() });

                searchResult.Rows.Add(row);

            }

            var results = XmlHelpers.SerializeObject<SearchResults>(searchResult);

            // Remove the standard XML header
            XMLResults = results.Substring(39);
            return XMLResults;
        }

    }
}
