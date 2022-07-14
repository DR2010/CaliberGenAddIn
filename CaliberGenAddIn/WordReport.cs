using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
// using iTextSharp.text.pdf;
using System.Text;
using System.Windows.Forms;
using EA;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using Application=Microsoft.Office.Interop.Word.Application;

namespace EAAddIn
{
    
    public class WordReport
    {

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
                if (currentElement.Stereotype == "projects 0910" &&
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

        //
        // Insert picture - just a test
        //
        public static void insertPicture(_Application oWord,
                                         _Document oDoc,
                                         string pictureFile)
        {
            object oMissing = System.Reflection.Missing.Value;
            //oWord.Visible = true;

            // oWord.ActiveWindow.Selection.Range.InlineShapes.AddPicture(pictureFile, ref oMissing, ref oMissing, ref oMissing);
            oDoc.ActiveWindow.Selection.Range.InlineShapes.AddPicture(pictureFile, ref oMissing, ref oMissing, ref oMissing);

            oDoc.Content.InlineShapes.AddPicture(
                          "C:\\img\\Activity.jpg", ref oMissing, ref oMissing, ref oMissing);
        }


        //
        // New Management Report from 14/08/2009
        //
        public static void managementReport2(
                         EA.Package pkg,
                         DateTime startOfReport,
                         DateTime endOfReport)
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            string projectStereotype = "projects 0910";

            List<string> phase = getTagsFromTemplate();
            var lights = new DesignTrafficLight();
            if (phase.Count - 1 != lights.TrafficLights.Count())
            {
                MessageBox.Show(
                    projectStereotype +
                    " template tagged value count does not match internal DesignTrafficLight class.  Exiting...",
                    "Traffic Light Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Start Word and create a new document.
            _Application oWord;
            _Document oDoc;
            oWord = new Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                                       ref oMissing, ref oMissing);

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

            printToWord(oDoc, "Management Report - Weekly Design Status", 14, 0);
            printToWord(oDoc, " ", 14, 0);

            // Calculate the number of rows
            // 
            int numofcols = phase.Count;
            int numofrows = 1;
            List<Element> reportElements = new List<Element>();

            foreach (EA.Package package in pkg.Packages)
            {
                foreach (EA.Element element in package.Elements)
                {
                    numofrows += GetProjectElements(element, projectStereotype, ref reportElements);
                }
            }


            //Insert a table, fill it with data, and make the first row
            //bold and italic.
            Table oTable;
            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, numofrows, numofcols, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            // int r, c;

            // 
            // Header
            //

            for (int i = 0; i < phase.Count; i++)
            {

                oTable.Cell(1, i + 1).Range.Text = phase[i];
            }


            //
            // Produce trafic light report
            //
            int row = 2;

            foreach (Element reportElement in reportElements)
            {
                if (reportElement.Name == "Solution Architects")
                    continue;

                oTable.Cell(row, 1).Range.Text = reportElement.Name;

                for (int i = 0; i < reportElement.TaggedValues.Count; i++)
                {

                    // If extra tags are added, they will be ignored
                    if (i >= phase.Count)
                        break;


                    short s = (short) i;

                    string tag = phase[i];
                    // EA.TaggedValue tv = (EA.TaggedValue)element.TaggedValues.GetByName(tag);
                    EA.TaggedValue tv = (EA.TaggedValue) reportElement.TaggedValues.GetAt(s);

                    if (tv == null)
                        continue;

                    var lightIndex = GetDesignTrafficLightIndex(tv.Name);

                    if (lightIndex == -1) continue;


                    string notes = tv.Notes;

                    // oTable.Cell(row, i + 2).Range.Text = "\n" + notes;

                    if (tv.Value == "Green")
                    {
                        oTable.Cell(row, lightIndex + 2).Range.InlineShapes.AddPicture(
                            @"C:\Program Files\Sparx Systems\EA\Images\Green.jpg", ref oMissing, ref oMissing,
                            ref oMissing);
                    }
                    if (tv.Value == "Orange")
                    {
                        oTable.Cell(row, lightIndex + 2).Range.InlineShapes.AddPicture(
                            @"C:\Program Files\Sparx Systems\EA\Images\Orange.jpg", ref oMissing, ref oMissing,
                            ref oMissing);
                    }
                    if (tv.Value == "Red")
                    {
                        oTable.Cell(row, lightIndex + 2).Range.InlineShapes.AddPicture(
                            @"C:\Program Files\Sparx Systems\EA\Images\Red.jpg", ref oMissing, ref oMissing,
                            ref oMissing);
                    }

                }

                // Set font
                oTable.Rows[row].Range.Font.Name = "Arial";
                oTable.Rows[row].Range.Font.Size = 8;

                row++;


            }

            string repTitle = "Weekly Project Update (" +
                              startOfReport.ToString().Substring(0, 10) +
                              " - " + endOfReport.ToString().Substring(0, 10) + ")";

            printToWord(oDoc, " ", 18, 1);
            printToWord(oDoc, repTitle, 18, 1);

            foreach (Element reportElement in reportElements)
            {
                //var elementAttList = new SortedList<string, sAttributeDetails>();

                var elementAttList = GetAttributesInDateOrder(startOfReport, endOfReport, reportElement);

                if (elementAttList.Count > 0 || !string.IsNullOrEmpty(reportElement.Notes))
                {
                    printToWord(oDoc, " ", 14, 0);
                    printToWord(oDoc, reportElement.Name, 14, 0);

                    foreach (var attribute in elementAttList)
                    {
                        printToWord(oDoc, attribute.Value.EventDescription, 12, 0);
                    }

                    if (!string.IsNullOrEmpty(reportElement.Notes))
                    {
                        printToWord(oDoc, " ", 12, 0);
                        printToWord(oDoc, reportElement.Notes, 12, 0);
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
            repTitle = "Next Week Planning (" +
                       startOfReport.ToString().Substring(0, 10) +
                       " - " + endOfReport.ToString().Substring(0, 10) + ")";

            printToWord(oDoc, " ", 18, 1);
            printToWord(oDoc, repTitle, 18, 1);

            foreach (Element reportElement in reportElements)
            {
                var elementAttList = GetAttributesInDateOrder(startOfReport, endOfReport, reportElement);

                if (elementAttList.Count > 0)
                {
                    printToWord(oDoc, " ", 14, 0);
                    printToWord(oDoc, reportElement.Name, 14, 0);

                    foreach (var element in elementAttList)
                    {
                        printToWord(oDoc, element.Value.EventDescription, 12, 0);

                    }
                }
            }
        }

        private static SortedList<string, sAttributeDetails> GetAttributesInDateOrder(DateTime startOfReport, DateTime endOfReport, Element reportElement)
        {
            var elementAttList = new SortedList<string, sAttributeDetails>();

            foreach (EA.Attribute _attribute in reportElement.Attributes)
            {
                // Move attributes to list with a real date
                //
                var _attributeDetails = new sAttributeDetails();

                string atDate = _attribute.Name.Substring(0, 8);
                string reversedAtDate = atDate.Reverse().ToString();
                try
                {
                    _attributeDetails.EventDate = Convert.ToDateTime(atDate);
                }
                catch
                {
                    // Ignore attribute
                    continue;
                }
                if (_attributeDetails.EventDate >= startOfReport &&
                    _attributeDetails.EventDate <= endOfReport)
                {
                    _attributeDetails.EventDescription = _attribute.Name;
                    elementAttList.Add(reversedAtDate + _attribute.Name, _attributeDetails);
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
            else if (element.Type == "Class" && element.Stereotype == projectStereotype)
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


        private struct sAttributeDetails
        {
            public DateTime EventDate;
            public string EventDescription;
        }

        private static void printToWord(_Document oDoc, string toPrint, int fontSize, int bold )
        {
                object oEndOfDoc = "\\endofdoc"; 
                Paragraph oPara;
                object oRng =
                       oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara = oDoc.Content.Paragraphs.Add(ref oRng);
                oPara.Range.Font.Bold = bold;
                oPara.Range.Font.Size = fontSize;
                oPara.Range.Text = toPrint;
                // oPara.Format.SpaceAfter = 1;
                oPara.Range.InsertParagraphAfter();

        }


        //// -----------------------------------------------
        ////               Demo
        //// -----------------------------------------------
        //public static void PdfCreate()
        //{
        //    string iCreatePDFdir = "C:\\iCreatePDF\\PDFTest";
        //    string iCreatePDFTest = iCreatePDFdir + "\\PDFTest.pdf";

        //    Directory.CreateDirectory(iCreatePDFdir);

        //    var pdfDocCreatePDF = new iTextSharp.text.Document();

        //    PdfWriter.GetInstance(pdfDocCreatePDF, new FileStream(iCreatePDFTest, FileMode.Create));

        //    pdfDocCreatePDF.Open();
        //    pdfDocCreatePDF.AddTitle("PDF Title");

        //    // pdfDocCreatePDF.Add( r );
        //    pdfDocCreatePDF.Add(new iTextSharp.text.Paragraph("Hey PDF"));
        //    pdfDocCreatePDF.Close();

        //    return;
        //}



        // ----------------------------------------------------------

        //
        // Obsolete from 14/08/2009
        //
        public static void managementReport(EA.Package pkg)
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord;
            _Document oDoc;
            oWord = new Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

            oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

            //Insert a paragraph at the beginning of the document.
            Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Management Report - Weekly Design Status";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            //Insert another paragraph.
            Paragraph oPara3;
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara3.Range.Text = "";
            oPara3.Range.Font.Bold = 0;
            oPara3.Format.SpaceAfter = 24;
            oPara3.Range.InsertParagraphAfter();


            // Calculate the number of rows
            // 
            int numofcols = 8 + 1;
            int numofrows = 1;

            foreach (EA.Package package in pkg.Packages)
            {
                foreach (EA.Element element in package.Elements)
                {
                    if (element.Stereotype != "project team")
                    {
                        // Ignore
                    }
                    else
                    {
                        numofrows++;
                    }

                }
            }


            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            Table oTable;
            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, numofrows, numofcols, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            // int r, c;

            // 
            // Header
            //
            string[] phase = {"<< Project >>",
                              "1) Design Phase Initiated",
                              "2) Lead Designer Nominated",
                              "3) Solutions Overview",
                              "4) Agreement Form Signoff",
                              "5) Conceptual Design Review",
                              "6) Technical Design Review",
                              "7) Solution Design Signoff",
                              "8) Design Update"
                             };

            for (int i = 0; i < phase.Length; i++)
            {
                oTable.Cell(1, i + 1).Range.Text = phase[i];

            }


            int row = 2;
            foreach (EA.Package package in pkg.Packages)
            {

                foreach (EA.Element element in package.Elements)
                {

                    if (element.Stereotype != "project team")
                        continue;

                    oTable.Cell(row, 1).Range.Text = element.Name;

                    for (int i = 0; i < element.TaggedValues.Count; i++)
                    {

                        // If extra tags are added, they will be ignored
                        if (i > 7)
                            break;

                        short s = (short)i;

                        string tag = phase[i];
                        // EA.TaggedValue tv = (EA.TaggedValue)element.TaggedValues.GetByName(tag);
                        EA.TaggedValue tv = (EA.TaggedValue)element.TaggedValues.GetAt(s);

                        if (tv == null)
                            continue;

                        string notes = tv.Notes;

                        oTable.Cell(row, i + 2).Range.Text = "\n" + notes;

                        if (tv.Value == "Green")
                        {
                            oTable.Cell(row, i + 2).Range.InlineShapes.AddPicture(
                                @"C:\img\Green.jpg", ref oMissing, ref oMissing, ref oMissing);
                        }
                        if (tv.Value == "Orange")
                        {
                            oTable.Cell(row, i + 2).Range.InlineShapes.AddPicture(
                                @"C:\img\Orange.jpg", ref oMissing, ref oMissing, ref oMissing);
                        }
                        if (tv.Value == "Red")
                        {
                            oTable.Cell(row, i + 2).Range.InlineShapes.AddPicture(
                                @"C:\img\Red.jpg", ref oMissing, ref oMissing, ref oMissing);
                        }

                    }

                    // Set font
                    oTable.Rows[row].Range.Font.Name = "Arial";
                    oTable.Rows[row].Range.Font.Size = 8;

                    row++;
                }

                // Set font
                oTable.Rows[1].Range.Font.Bold = 1;
                oTable.Rows[1].Range.Font.Size = 9;

            }


            //Insert another paragraph.
            Paragraph oPara6;
            object oRng6 =
                   oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara6 = oDoc.Content.Paragraphs.Add(ref oRng6);
            oPara6.Range.Underline = WdUnderline.wdUnderlineWords;
            oPara6.Format.SpaceAfter = 1;
            oPara6.Range.Text = "\n\n  General Comments \n\n";
            oPara6.Format.SpaceAfter = 1;
            oPara6.Range.Underline = WdUnderline.wdUnderlineNone;
            oPara6.Range.InsertParagraphAfter();

            // Print extra information
            // for each project
            //
            foreach (EA.Package package in pkg.Packages)
            {
                foreach (EA.Element element in package.Elements)
                {
                    if (element.Notes != "")
                    {
                        //Insert another paragraph.
                        Paragraph oPara4;
                        object oRng1 =
                               oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                        oPara4 = oDoc.Content.Paragraphs.Add(ref oRng1);
                        oPara4.Range.Font.Bold = 1;
                        oPara4.Range.Text = element.Name + "\n";
                        oPara4.Range.Font.Bold = 0;
                        oPara4.Range.Text = element.Notes.ToString() + "\n";
                        oPara4.Format.SpaceAfter = 1;
                        oPara4.Range.InsertParagraphAfter();

                    }
                }
            }
        }


        // -----------------------------------------------
        //               Demo
        // -----------------------------------------------
        public static void printReport()
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            _Application oWord;
            _Document oDoc;
            oWord = new Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

            //Insert a paragraph at the beginning of the document.
            Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Heading 1";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            //Insert a paragraph at the end of the document.
            Paragraph oPara2;
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara2.Range.Text = "Heading 2";
            oPara2.Format.SpaceAfter = 6;
            oPara2.Range.InsertParagraphAfter();

            //Insert another paragraph.
            Paragraph oPara3;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara3.Range.Text = "This is a sentence of normal text. Now here is a table:";
            oPara3.Range.Font.Bold = 0;
            oPara3.Format.SpaceAfter = 24;
            oPara3.Range.InsertParagraphAfter();

            


            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            Table oTable;
            Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, 3, 5, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            int r, c;
            string strText;
            for (r = 1; r <= 3; r++)
                for (c = 1; c <= 5; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Italic = 1;

            //Add some text after the table.
            Paragraph oPara4;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara4.Range.InsertParagraphBefore();
            oPara4.Range.Text = "And here's another table:";
            oPara4.Format.SpaceAfter = 24;
            oPara4.Range.InsertParagraphAfter();

            //Insert a 5 x 2 table, fill it with data, and change the column widths.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, 5, 2, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            for (r = 1; r <= 5; r++)
                for (c = 1; c <= 2; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Columns[1].Width = oWord.InchesToPoints(2); //Change width of columns 1 & 2
            oTable.Columns[2].Width = oWord.InchesToPoints(3);

            
            
            
            
            //Keep inserting text. When you get to 7 inches from top of the
            //document, insert a hard page break.
            object oPos;
            double dPos = oWord.InchesToPoints(7);
            oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range.InsertParagraphAfter();
            do
            {
                wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                wrdRng.ParagraphFormat.SpaceAfter = 6;
                wrdRng.InsertAfter("A line of text");
                wrdRng.InsertParagraphAfter();
                oPos = wrdRng.get_Information
                               (WdInformation.wdVerticalPositionRelativeToPage);
            }
            while (dPos >= Convert.ToDouble(oPos));
            object oCollapseEnd = WdCollapseDirection.wdCollapseEnd;
            object oPageBreak = WdBreakType.wdPageBreak;
            wrdRng.Collapse(ref oCollapseEnd);
            wrdRng.InsertBreak(ref oPageBreak);
            wrdRng.Collapse(ref oCollapseEnd);
            wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
            wrdRng.InsertParagraphAfter();

            
            
            //Insert a chart.
            InlineShape oShape;
            object oClassType = "MSGraph.Chart.8";
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing);

            //Demonstrate use of late bound oChart and oChartApp objects to
            //manipulate the chart object with MSGraph.
            object oChart;
            object oChartApp;
            oChart = oShape.OLEFormat.Object;
            oChartApp = oChart.GetType().InvokeMember("Application",
                BindingFlags.GetProperty, null, oChart, null);

            //Change the chart type to Line.
            object[] Parameters = new Object[1];
            Parameters[0] = 4; //xlLine = 4
            oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty,
                null, oChart, Parameters);

            //Update the chart image and quit MSGraph.
            oChartApp.GetType().InvokeMember("Update",
                BindingFlags.InvokeMethod, null, oChartApp, null);
            oChartApp.GetType().InvokeMember("Quit",
                BindingFlags.InvokeMethod, null, oChartApp, null);

            //... If desired, you can proceed from here using the Microsoft Graph 
            //Object model on the oChart and oChartApp objects to make additional
            //changes to the chart.

            //Set the width of the chart.
            oShape.Width = oWord.InchesToPoints(6.25f);
            oShape.Height = oWord.InchesToPoints(3.57f);

            //Add text after the chart.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();
            wrdRng.InsertAfter("THE END.");

        }



    }

}
