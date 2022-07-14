using System;
using System.Collections.Generic;
using EA;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;



namespace EAAddIn.Windows
{
    public partial class DesignSignOffReport : Form
    {
        List<IDualElement> _projectsList = new List<IDualElement>();
        object oTemplatePath = @"\\edmgt022\eakeystore$\EA Software\CaliberGenEAAddIn\Resources\DesignSignOffTemplate.dot"; 
        //private object oTemplatePath = @"::ODMA\DME-MSE\dm1-728611";
        private const string CurrentReleaseStereotype = "projects 0910";

        public DesignSignOffReport()
        {
            InitializeComponent();
        }

        private void DesignSignOffReport_Load(object sender, EventArgs e)
        {
           
            PopulateProjectList();

            Element currentelement = EaAccess.getSelectedElement();
            if (currentelement != null && currentelement.Stereotype == CurrentReleaseStereotype && currentelement.Type == "Class")
            {
                ProjectComboBox.Text = currentelement.Name;
            }
        }

        private void PopulateProjectList()
        {
            _projectsList = new EaAccess().ListProjects(CurrentReleaseStereotype, "Class");
            ProjectComboBox.DataSource = _projectsList;
            ProjectComboBox.DisplayMember = "Name";
        }

        private void CreateReportButton_Click(object sender, EventArgs e)
        {
            if (ProjectComboBox != null)
                if (Equals(MessageBox.Show("Create a sign of report for " +
                                           ((Element)ProjectComboBox.SelectedItem).Name + "?",
                                           "Create Design Sign Off Report", MessageBoxButtons.YesNo),
                           DialogResult.Yes))
                {
                    CreateDesignSignOffReport();
                }
        }

        private void CreateDesignSignOffReport()
        {
            var selectedProject = (Element)ProjectComboBox.SelectedItem;

            Application oWord;
            Document oWordDoc;

            if (OpenWordDocument(out oWord, out oWordDoc, oTemplatePath))
            {
                WordDocumentMailMerge(oWord, oWordDoc, selectedProject);
            }
            else
            {
                MessageBox.Show("Template not found: " + oTemplatePath, "Template Not Found", MessageBoxButtons.OK);
            }
        }

        private static Boolean OpenWordDocument(out Application oWord, out Document oWordDoc, object oTemplatePath)
        {
            Object oMissing = System.Reflection.Missing.Value;

            oWord = new Application();
            oWordDoc = new Document();

            //  System.Diagnostics.Process.Start("dme://dm1-224360"); This works to open up the doc - but don't know how to conver this into a ref tempalte



            var file = new System.IO.FileInfo(oTemplatePath.ToString());
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
            foreach (Field myMergeField in oWordDoc.Fields)
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
                            oWord.Selection.TypeText(selectedProject.Name);
                            break;
                        case "ProjectName":
                            myMergeField.Select();
                            oWord.Selection.TypeText(selectedProject.Name);
                            break;
                        case "HIGHLEVELDM":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetDmid(
                                    (TaggedValue)
                                    selectedProject.TaggedValues.GetByName("1) High Level Design Review")));
                            break;
                        case "HLStatus":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetStatus(
                                    (TaggedValue)
                                    selectedProject.TaggedValues.GetByName("1) High Level Design Review")));
                            break;
                        case "DBADM":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetDmid(
                                    (TaggedValue)
                                    selectedProject.TaggedValues.GetByName("2) Database Initial Review")));
                            break;
                        case "DBAStatus":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetStatus(
                                    (TaggedValue)
                                    selectedProject.TaggedValues.GetByName("2) Database Initial Review")));
                            break;
                        case "LogicalDM":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetDmid(
                                    (TaggedValue) selectedProject.TaggedValues.GetByName("3) Logical Design Review")));
                            break;
                        case "LOGStatus":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetStatus(
                                    (TaggedValue) selectedProject.TaggedValues.GetByName("3) Logical Design Review")));
                            break;
                        case "TECHNCIALDM":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetDmid(
                                    (TaggedValue)
                                    selectedProject.TaggedValues.GetByName("4) Technical Design Review")));
                            break;
                        case "TECHStatus":
                            myMergeField.Select();
                            oWord.Selection.TypeText(
                                GetStatus(
                                    (TaggedValue)
                                    selectedProject.TaggedValues.GetByName("4) Technical Design Review")));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
   
        private static string GetDmid(IDualTaggedValue inTag)
            {
                if (inTag == null || inTag.Notes == "")
                {
                    return "DM Id Not Entered";
                }
                return inTag.Notes;
            }

        private static string GetStatus(IDualTaggedValue inTag)
        {
            if (inTag!= null && inTag.Value == "Green")
            {
                return "Complete";
            }
            return "Incomplete";
        }
    }
}
