using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EA;
using Microsoft.Office.Interop.Word;
using Application=Microsoft.Office.Interop.Word.Application;

namespace EAAddIn.Applications.Reports
{
    class DesignSignOff
    {
     private List<IDualElement> _projectsList = new List<IDualElement>();

        private readonly object _oTemplatePath =
            @"\\edmgt022\eakeystore$\EA Software\DEEWR AddIn\Resources\DesignSignOffTemplate.dot";

        private const string AllProjectText = "<All Projects>";

        //private object oTemplatePath = @"::ODMA\DME-MSE\dm1-728611";


        private void DesignSignOffReport_Load(object sender, EventArgs e)
        {
            PopulateReleaseList();
            //PopulateProjectList();

            //Element currentelement = EaAccess.GetSelectedElement();
            //if (currentelement != null && currentelement.Stereotype.ToUpper() == ReleaseComboBox.SelectedItem.ToString().ToUpper() &&
            //    currentelement.Type == "Class")
            //{
            //    ProjectComboBox.Text = currentelement.Name;
            //}
        }

        public List<string> PopulateReleaseList()
        {
            var releaseList = new EaAccess().ListReleaseStereotypes();
            releaseList.Insert(releaseList.Count, AllProjectText);
            return releaseList;
        }

        public List<IDualElement> PopulateProjectList(string selectedRelease, bool includeArchivedProjects)
        {
           
            if (string.IsNullOrEmpty(selectedRelease))
            {
                MessageBox.Show(@"Please ensure a value is entered into the Year field", @"Missing Project Year",
                                MessageBoxButtons.OK);
             
            }
            else
            {

                if (selectedRelease == AllProjectText)
                {
                    _projectsList = new EaAccess().ListProjects(includeArchivedProjects);
                }
                else
                {
                    _projectsList = new EaAccess().ListProjects(selectedRelease, includeArchivedProjects);
                }
            }
            return _projectsList;
        }



        public void CreateDesignSignOffReport(Element selectedProject)
        {
            Application oWord;
            Document oWordDoc;

            if (OpenWordDocument(out oWord, out oWordDoc, _oTemplatePath))
            {
                WordDocumentMailMerge(oWord, oWordDoc, selectedProject);
            }
            else
            {
                MessageBox.Show(@"Template not found: " + _oTemplatePath, @"Template Not Found", MessageBoxButtons.OK);
            }
        }

        private static Boolean OpenWordDocument(out Application oWord, out Document oWordDoc, object oTemplatePath)
        {
            Object oMissing = Missing.Value;

            oWord = new Application();
            oWordDoc = new Document();

            var file = new FileInfo(oTemplatePath.ToString());
            if (file.Exists)
            {
                oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                return true;
            }
            return false;
        }

        private static void WordDocumentMailMerge(_Application oWord, _Document oWordDoc, IDualElement selectedProject)
        {
            oWord.Visible = true;
            var myClassReportItem = new GetTaggedValuesForClass(selectedProject);

            foreach (Microsoft.Office.Interop.Word.Field myMergeField in oWordDoc.Fields)
            {
                Range rngFieldCode = myMergeField.Code;
                String fieldText = rngFieldCode.Text;

                if (fieldText.StartsWith(" MERGEFIELD"))
                {
                    Int32 endMerge = fieldText.IndexOf("\\");
                    //Int32 fieldNameLength = fieldText.Length - endMerge;
                    String fieldName = fieldText.Substring(11, endMerge - 11);

                    fieldName = fieldName.Trim();

                    switch (fieldName)
                    {
                        case "ProjectID":
                            myMergeField.Select();
                            oWord.Selection.TypeText(GetProgectId(selectedProject.Name));
                            break;
                        case "ProjectName":
                            myMergeField.Select();
                            oWord.Selection.TypeText(GetProgectName(selectedProject.Name));
                            break;
                        case "HIGHLEVELDM":
                            PopulateDm("High", oWord, myMergeField, rngFieldCode, myClassReportItem);
                            break;
                        case "HLStatus":
                            PopulateStatus("High", oWord, myMergeField, myClassReportItem);
                            break;
                        case "DBADM":
                            PopulateDm("Database", oWord, myMergeField, rngFieldCode, myClassReportItem);
                            break;
                        case "DBAStatus":
                            PopulateStatus("Database", oWord, myMergeField, myClassReportItem);
                            break;
                        case "LogicalDM":
                            PopulateDm("Logical", oWord, myMergeField, rngFieldCode, myClassReportItem);
                            break;
                        case "LOGStatus":
                            PopulateStatus("Logical", oWord, myMergeField, myClassReportItem);
                            break;
                        case "TECHNCIALDM":
                            PopulateDm("Technical", oWord, myMergeField, rngFieldCode, myClassReportItem);
                            break;
                        case "TECHStatus":
                            PopulateStatus("Technical", oWord, myMergeField, myClassReportItem);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static string GetProgectId(string projectName)
        {
            return projectName.Split(' ')[0];
        }

        private static string GetProgectName(string projectName)
        {
            int projectidlength = GetProgectId(projectName).Length+1;
            return projectName.Substring(projectidlength,
                                  projectName.Length - projectidlength);
        }

        private static void PopulateStatus(string stage, _Application oWord, Microsoft.Office.Interop.Word.Field myMergeField,
                                           GetTaggedValuesForClass myClassReportItem)
        {
            myMergeField.Select();
            myClassReportItem.GetTagValues(stage);
            oWord.Selection.TypeText(myClassReportItem.RepStatus);
        }

        private static void PopulateDm(string stage, _Application oWord, Microsoft.Office.Interop.Word.Field myMergeField, Range rngFieldCode,
                                       GetTaggedValuesForClass myClassReportItem)
        {
            object oMissing = Missing.Value;
            myMergeField.Select();
            myClassReportItem.GetTagValues(stage);
            if (myClassReportItem.RepDmIdFound)
            {
                oWord.Selection.TypeText(" ");
                oWord.Selection.Hyperlinks.Add(rngFieldCode, ref myClassReportItem.RepDmAdrress,
                                               ref oMissing, ref oMissing, ref myClassReportItem.RepDmLabel,
                                               ref oMissing);
            }
            else
            {
                oWord.Selection.TypeText(myClassReportItem.RepDmid);
            }
        }


    }

    internal class GetTaggedValuesForClass
    {
        private readonly List<IDualTaggedValue> _tagList = new List<IDualTaggedValue>();
        public object RepDmAdrress;
        public string RepDmid;
        public bool RepDmIdFound;
        public object RepDmLabel;
        public string RepStatus;

        public GetTaggedValuesForClass(IDualElement selectedProject)
        {
            for (short sh = 0; sh < selectedProject.TaggedValues.Count; sh++)
            {
                _tagList.Add((TaggedValue) (selectedProject.TaggedValues.GetAt(sh)));
            }
        }

        public void GetTagValues(string inTag)
        {
            RepStatus = "Incomplete";

            foreach (IDualTaggedValue t in _tagList)
            {
                if (t.Name.Contains(inTag))
                {
                    RepDmid = t.Notes;
                    ConvertToDmLink();
                    if (t.Value.Contains("Green"))
                    {
                        RepStatus = "Complete";
                    }
                    break;
                }
            }
        }

        private void ConvertToDmLink()
        {
            var dmPattern = new Regex(@"DM1.(\d{6})", RegexOptions.IgnoreCase);
            RepDmIdFound = false;
            RepDmAdrress = null;
            RepDmLabel = null;

            if (!String.IsNullOrEmpty(RepDmid))
            {
                //see if has a valid dm in it
                RepDmIdFound = dmPattern.IsMatch(RepDmid);

                if (RepDmIdFound)
                {
                    MatchCollection matchList = dmPattern.Matches(RepDmid);
                    RepDmid = matchList[0].Value.ToUpper();
                    RepDmAdrress = @"dme:\\" + RepDmid;
                    RepDmLabel = RepDmid;
                }
            }
            else
            {
                RepDmid = "Not Recorded";
            }
        }
    }
}
