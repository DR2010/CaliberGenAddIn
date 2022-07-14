///////////////////////////////////////////////////////////
//  BinContent1.cs
//  Implementation of the Class BinContent1
//  Generated by Enterprise Architect
//  Created on:      15-Jul-2010 15:24:01
//  Original author: Wayne Lombard
///////////////////////////////////////////////////////////


using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EAAddIn.Applications.AuditConverter
{
    public class BinContent1
    {

        public string RowNumber;
        public string ColumnName;
        public string ColumnOldValue;
        public string ColumnNewValue;

        public static List<BinContent1> DecodeBin(XElement bincontents1)
        {
            List<BinContent1> binContents1List = new List<BinContent1>();

            var nonNullDetails = from e in bincontents1.Elements("Row").Elements("Column")
                                 where e.Attribute("Name").Value != null
                                 select e;

            foreach (var nonNullDetail in nonNullDetails)
            {
                var bc1 = new BinContent1();

                if (nonNullDetail != null)
                {
                    bc1.RowNumber = bincontents1.Element("Row").Attribute("Number").Value;
                    bc1.ColumnName = nonNullDetail.Attribute("Name").Value;
                    bc1.ColumnOldValue = nonNullDetail.Element("Old").Attribute("Value").Value;
                    bc1.ColumnNewValue = nonNullDetail.Element("New").Attribute("Value").Value;

                    binContents1List.Add(bc1);
                }
            }
            return binContents1List;
        }
    }//end BinContent1
}