using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using EA;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using System.IO;
using File = EA.File;

namespace EAAddIn
{

    public class WordReport
    {
        object oEndOfDoc = "\\endofdoc";
        //
        // get list of project team phases from template
        //
        private static List<string> getTagsFromTemplate()
        {

            string tempGUID = "{A4A6D605-1208-4354-A668-890FD93EC952}";
            List<string> ret = new List<string>();

            // look for template in EA
            Package templatePackage =
                AddInRepository.Instance.Repository.GetPackageByGuid(tempGUID);
            Element templateElement = null;

            if (templatePackage == null)
            {
                return ret;
            }

            // Retrieve template
            foreach (Element currentElement in templatePackage.Elements)
            {
                if (currentElement.Stereotype == "project" &&
                    currentElement.Type == "Class")
                {
                    templateElement = currentElement;
                    break;
                }
            }

            //
            // Get Template Element
            //
            ret.Add("<< Project >>");

            foreach (TaggedValue fromTag in templateElement.TaggedValues)
            {
                ret.Add(fromTag.Name);
            }

            return ret;
        }


        public bool TrafficLightManagementReport(
                 EA.Package pkg,
                 DateTime startOfReport,
                 DateTime endOfReport,
                Action<string> updateStatus, bool isDetailed)
        {
            object oMissing = System.Reflection.Missing.Value;
            var pastPlannedActivities = string.Empty;

            const string projectStereotype = "project";

            List<string> phase = getTagsFromTemplate();
            var lights = new DesignTrafficLight();
            if (phase.Count - 1 != lights.TrafficLights.Count())
            {
                MessageBox.Show(
                    projectStereotype +
                    " template tagged value count does not match internal DesignTrafficLight class.  Exiting...",
                    "Traffic Light Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            updateStatus("Opening Word in background ...");

            //Start Word and create a new document.
            _Application oWord = new Application { Visible = false };
            _Document oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                                                 ref oMissing, ref oMissing);

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

            PrintToWord(oDoc, "Management Report - Weekly Design Status", 14, 0);
            PrintToWord(oDoc, " ", 8, 0);

            string filename = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetTempFileName(), "doc"));
            oDoc.SaveAs(filename);

            // Calculate the number of rows
            // 
            int numofcols = phase.Count;
            var sourceElements = new List<Element>();

            updateStatus("Getting project elements ...");

            ProcessPackage(projectStereotype, pkg, ref sourceElements);

            //Determine the number of tables
            var groups =
                from e in sourceElements
                group e by e.Stereotype
                    into g
                    orderby DateOrder(g.Key)
                    select new { Release = g.Key, ElementCount = g.Count() };

            foreach (var @group in groups)
            {
                if (@group.Release == "project team" || @group.Release == "no project") continue;

                PrintToWord(oDoc, " ", 8, 1);

                Range wrdRng;
                Table oTable;

                if (isDetailed)
                {
                    PrintToWord(oDoc, @group.Release + "s", 12, 1);

                    //Insert a table, fill it with data, and make the first row
                    //bold and italic.
                    wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    
                    oTable = oDoc.Tables.Add(wrdRng, @group.ElementCount + 1, numofcols, ref oMissing, ref oMissing);
                    oTable.Range.ParagraphFormat.SpaceAfter = 6;
                    // 
                    // Header
                    //
                    for (int i = 0; i < phase.Count; i++)
                    {
                        oTable.Cell(1, i + 1).Range.Text = phase[i];
                    }

                }
                else
                {

                    wrdRng = oDoc.Bookmarks.get_Item(oEndOfDoc).Range;
                    oTable = oDoc.Tables.Add(wrdRng, 1, 3, ref oMissing, ref oMissing);

                    oTable.Rows[1].HeadingFormat = -1;

                    Row headingRow = oTable.Rows[1];

                    ApplyHeadingStyle(headingRow.Cells[1]);
                    headingRow.Cells[1].Range.Text = @group.Release + "s";

                    ApplyHeadingStyle(headingRow.Cells[2], 50);
                    headingRow.Cells[2].Range.Text = "Status";

                    ApplyHeadingStyle(headingRow.Cells[3]);
                    headingRow.Cells[3].Range.Text = "Notes";

                }
                //
                // Produce trafic lights
                //
                int row = 2;

                //Sort the list
                var sortedReportElements =
                    from e in sourceElements
                    where e.Stereotype == @group.Release
                    orderby e.Stereotype, e.Name ascending
                    select e;

                var reportElements = sortedReportElements.ToList();

                foreach (Element reportElement in reportElements)
                {
                    if (reportElement.Name == "Solution Architects" || reportElement.Name == "Database Team") continue;

                    updateStatus("Processing " + reportElement.Name + " ...");

                    if (isDetailed)
                    {
                        oTable.Cell(row, 1).Range.Text = reportElement.Name;

                        for (int i = 0; i < reportElement.TaggedValues.Count; i++)
                        {

                            // If extra tags are added, they will be ignored
                            if (i >= phase.Count)
                                break;


                            short s = (short)i;

                            TaggedValue tv = (TaggedValue)reportElement.TaggedValues.GetAt(s);

                            if (tv == null)
                                continue;

                            var lightIndex = GetDesignTrafficLightIndex(tv.Name);

                            if (lightIndex == -1) continue;


                            if (tv.Value == "Green")
                            {
                                Clipboard.SetImage(Properties.Resources.ImageGreen);
                                oTable.Cell(row, lightIndex + 2).Range.Paste();
                            }
                            if (tv.Value == "Orange")
                            {
                                Clipboard.SetImage(Properties.Resources.ImageOrange);
                                oTable.Cell(row, lightIndex + 2).Range.Paste();
                            }
                            if (tv.Value == "Red")
                            {
                                Clipboard.SetImage(Properties.Resources.ImageRed);
                                oTable.Cell(row, lightIndex + 2).Range.Paste();
                            }

                        }

                        // Set font
                        oTable.Rows[row].Range.Font.Name = "Arial";
                        oTable.Rows[row].Range.Font.Size = 8;

                        row++;

                    }
                    else
                    {
                        Row projectRow = oTable.Rows.Add(ref oMissing);
                        projectRow.HeadingFormat = 0;


                        ApplyContentsStyle(projectRow.Cells[1]);
                        projectRow.Cells[1].Range.Text = reportElement.Name;

                        string designStatus = "Green";
                        string nonGreenLights = string.Empty;

                        for (int i = 0; i < reportElement.TaggedValues.Count; i++)
                        {

                            // If extra tags are added, they will be ignored
                            if (i >= phase.Count)
                                break;

                            var tv = (TaggedValue) reportElement.TaggedValues.GetAt((short) i);

                            if (tv == null || GetDesignTrafficLightIndex(tv.Name) == -1) continue;

                            if (tv.Value == "Orange" || tv.Value == "Red")
                            {
                                if (!string.IsNullOrEmpty(nonGreenLights)) nonGreenLights += ", ";

                                if (string.IsNullOrEmpty(tv.Notes))
                                {
                                    nonGreenLights += tv.Name.Substring(3) + " " + tv.Value;
                                }
                                else
                                {
                                    nonGreenLights += tv.Notes;
                                }
                            }
                            if (tv.Value == "Orange" && (designStatus == "Green"))
                            {
                                designStatus = "Orange";
                            }
                            else if (tv.Value == "Red")
                            {
                                designStatus = "Red";

                            }
                        }

                        if (designStatus == "Orange")
                        {
                            Clipboard.SetImage(Properties.Resources.ImageOrange);
                        }
                        else if (designStatus == "Red")
                        {
                            Clipboard.SetImage(Properties.Resources.ImageRed);
                        }
                        else
                        {
                            Clipboard.SetImage(Properties.Resources.ImageGreen);
                        }

                        ApplyContentsStyle(projectRow.Cells[2]);
                        projectRow.Cells[2].Range.Paste();


                        if (!string.IsNullOrEmpty(reportElement.Notes))
                        {
                            if (!string.IsNullOrEmpty(nonGreenLights)) nonGreenLights += ", ";
                            nonGreenLights += reportElement.Notes;
                        }

                        if (!string.IsNullOrEmpty(nonGreenLights))
                        {
                            ApplyContentsStyle(projectRow.Cells[3]);
                            projectRow.Cells[3].Range.Text = nonGreenLights;
                        }

                        row++;
                    }
                }
            }
            //
            // Print activity for this week
            // for each project
            //
            string repTitle = "Weekly Project Update (" +
                              startOfReport.ToString().Substring(0, 10) +
                              " - " + endOfReport.ToString().Substring(0, 10) + ")";

            PrintToWord(oDoc, " ", 12, 1);
            PrintToWord(oDoc, repTitle, 12, 1);

            var updatesReportElements =
                from e in sourceElements
                orderby e.Name ascending
                select e;


            foreach (Element reportElement in updatesReportElements)
            {
                updateStatus("Adding activity for " + reportElement.Name + " ...");

                var elementAttList = GetAttributesInDateOrder(startOfReport, endOfReport, reportElement);

                if (elementAttList.Count > 0)
                {
                    PrintToWord(oDoc, " ", 10, 1);
                    PrintToWord(oDoc, reportElement.Name, 10, 1);

                    foreach (var attribute in elementAttList)
                    {
                        PrintToWord(oDoc, attribute.Value.EventDescription, 8, 0);

                        if (attribute.Value.EventDescription.ToUpper().Contains("PLANNED") ||
                            attribute.Value.EventDescription.ToUpper().Contains("REQUIRED"))
                        {
                            pastPlannedActivities += Environment.NewLine + reportElement.Name + ": " +
                                                     attribute.Value.EventDescription;
                        }
                    }
                }
            }

            //
            // Print next week information
            // for each project
            //
            startOfReport = endOfReport.NextWeekDay();
            endOfReport = startOfReport.AddDays(+7).PreviousWeekDay();

            //Insert another paragraph.
            repTitle = "Planned for Next Week (" +
                       startOfReport.ToString().Substring(0, 10) +
                       " - " + endOfReport.ToString().Substring(0, 10) + ")";

            PrintToWord(oDoc, " ", 12, 1);
            PrintToWord(oDoc, repTitle, 12, 1);

            if (updatesReportElements.Count() == 0)
            {
                PrintToWord(oDoc, "No planned activities.", 8, 0);
            }

            foreach (Element reportElement in updatesReportElements)
            {
                updateStatus("Adding new activity for " + reportElement.Name + " ...");

                var elementAttList = GetAttributesInDateOrder(startOfReport, endOfReport, reportElement);

                if (elementAttList.Count > 0)
                {
                    PrintToWord(oDoc, " ", 10, 1);
                    PrintToWord(oDoc, reportElement.Name, 10, 1);

                    foreach (var element in elementAttList)
                    {
                        PrintToWord(oDoc, element.Value.EventDescription, 8, 0);
                    }
                }
            }

            if (!string.IsNullOrEmpty(pastPlannedActivities))
            {
                MessageBox.Show(
                    "Report contains planned items for the last week! " + Environment.NewLine + pastPlannedActivities,
                    "Generated Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            oDoc.Save();
            oDoc.Close();

            updateStatus(string.Empty);

            oWord.Visible = true;
            oWord.Documents.Open(FileName: filename);
            oWord.Activate();

            return true;
        }

        private static void ApplyHeadingStyle(Cell cell, int width = 300  )
        {
            cell.Width = width;
            
            cell.Range.Font.Name = "Arial";
            cell.Range.Font.Size = 10;
            cell.Range.Font.Bold = 1;

        }
        private static void ApplyContentsStyle(Cell cell, WdCellVerticalAlignment verticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter)
        {
            cell.VerticalAlignment = verticalAlignment;
            cell.Range.Font.Name = "Arial";
            cell.Range.Font.Size = 8;
            cell.Range.Font.Bold = 0;
        }

        private static List<Element> ProcessPackage(string projectStereotype, Package pkg, ref List<Element> sourceElements)
        {
            foreach (EA.Package package in pkg.Packages)
            {
                ProcessPackage(projectStereotype, package, ref sourceElements);
            }

            foreach (EA.Element element in pkg.Elements)
            {
                GetProjectElements(element, projectStereotype, ref sourceElements);
            }
            return sourceElements;
        }

        private static int DateOrder(string date)
        {
            date = date.Replace("project", string.Empty);

            var converted = new DateTime();
            
            if (DateTime.TryParse(date, out converted))
            {
                return converted.Year*100 + converted.Month;
            }
            return 999999; 

        }

        private static SortedList<string, AttributeDetails> GetAttributesInDateOrder(DateTime startOfReport, DateTime endOfReport, Element reportElement)
        {
            var elementAttList = new SortedList<string, AttributeDetails>();

            foreach (EA.Attribute _attribute in reportElement.Attributes)
            {
                // Move attributes to list with a real date
                //
                var attributeDetails = new AttributeDetails();

                string atDate = _attribute.Name.Substring(0, 8);
                string reversedAtDate = atDate.Reverse().ToString();
                try
                {
                    attributeDetails.EventDate = Convert.ToDateTime(atDate);
                }
                catch
                {
                    // Ignore attribute
                    continue;
                }
                if (attributeDetails.EventDate >= startOfReport &&
                    attributeDetails.EventDate <= endOfReport)
                {
                    attributeDetails.EventDescription = _attribute.Name;
                    if (elementAttList.ContainsKey(reversedAtDate + _attribute.Name) == false)
                    {
                        elementAttList.Add(reversedAtDate + _attribute.Name, attributeDetails);
                    }
                }
            }
            return elementAttList;
        }

        private static int GetProjectElements(Element element, string projectStereotype, ref List<Element> reportElements)
        {
            var projectElementCount = 0;

            if (element.Elements.Count > 0)
            {
                foreach (Element childElement in element.Elements)
                {
                    projectElementCount += GetProjectElements(childElement, projectStereotype, ref reportElements);
                }
            }
            else if (element.Type == "Class" 
                && element.Stereotype.Contains(projectStereotype)
                && element.Status != "Archived")
            {
                projectElementCount++;
                reportElements.Add(element);
            }
            return projectElementCount;
        }

        private static int GetDesignTrafficLightIndex(string item)
        {
            var lights = new DesignTrafficLight();

            var index = -1;

            for (int j = 0; j < lights.TrafficLights.Count(); j++)
            {
                if (item.Contains(lights.TrafficLights[j]))
                {
                    index = j;
                    break;
                }
            }
            return index;
        }


        private struct AttributeDetails
        {
            public DateTime EventDate;
            public string EventDescription;
        }

        private void PrintToWord(_Document oDoc, string toPrint, int fontSize, int bold)
        {
            object oRng =
                   oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            Paragraph oPara = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara.Range.Font.Name = "Arial";
            oPara.Range.Font.Bold = bold;
            oPara.Range.Font.Size = fontSize;
            oPara.Range.Text = toPrint;
            oPara.Range.InsertParagraphAfter();

        }
    }
}
