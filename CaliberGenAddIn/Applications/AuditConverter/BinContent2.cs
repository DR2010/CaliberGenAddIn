///////////////////////////////////////////////////////////
//  BinContent2.cs
//  Implementation of the Class BinContent2
//  Generated by Enterprise Architect
//  Created on:      15-Jul-2010 15:24:03
//  Original author: Wayne Lombard
///////////////////////////////////////////////////////////

using System.Xml.Linq;



namespace EAAddIn.Applications.AuditConverter
{
    public class BinContent2
    {

        public string rowNumber;
        public string levelName;
        public string levelGUID;
        public string levelLevelName;
        public string detailsUser;
        public string detailsDateTime;
        //public Audit audit;


        public void Decode(XElement bincontents2)
        {
            rowNumber = XMLHelper.GetAttribute(bincontents2, "Row", "Number");
            levelName = XMLHelper.GetAttribute(bincontents2, "Level", "Name");
            levelGUID = XMLHelper.GetAttribute(bincontents2, "Level", "GUID");
            levelLevelName = XMLHelper.GetAttribute(bincontents2, "Level", "LevelName");
            detailsUser = XMLHelper.GetAttribute(bincontents2, "Details", "User");
            detailsDateTime = XMLHelper.GetAttribute(bincontents2, "Details", "DateTime");
        }
    }//end BinContent2



}