using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using Attribute = EA.Attribute;
using Diagram = EA.Diagram;

namespace EAAddIn.Applications.ProcessSpecificationGenerator
{
    public class ProcessSpecificationGenerator
    {
        object oEndOfDoc = "\\endofdoc";
        _Application Word;
        _Document Document;

        internal void BuildSpecification()
        {
            Cursor.Current = Cursors.WaitCursor;

            Element element = EaAccess.GetSelectedElement();

            if (element == null)
            {
                MessageBox.Show("Please select a batch element to generate a specification from.");
                return;
            }

            var openDiagrams = AddInRepository.Instance.OpenDiagrams.ToList();

            object oMissing = System.Reflection.Missing.Value;

            Word = new Application();

            // Word.Visible = true;
            Word.Visible = false;

            object template = @"\\edmgt022\eakeystore$\EA Software\DEEWR AddIn\Resources\documentgeneration.dot";

            Document = Word.Documents.Add(ref template, ref oMissing, 
                                       ref oMissing, ref oMissing);

            int level = 1;
            ProcessElement(element, level);

            FinaliseDocument(element.Name);

            Cursor.Current = Cursors.Default;

            var toClose = AddInRepository.Instance.OpenDiagrams.Except(openDiagrams).ToList();

            foreach (int openDiagram in toClose)
            {
                    AddInRepository.Instance.Repository.CloseDiagram(openDiagram);
                    AddInRepository.Instance.OpenDiagrams.Remove(openDiagram);
            }
            MessageBox.Show("Documentation generation complete.  Please review and save your newly created document.");
        }

        private List<Diagram> GetOpenDiagrams()
        {
            //todo use this work out which diagrams are open before the process - not sure how at the moment

            return null;
            
        }

        private void FinaliseDocument(string processName)
        {
            Document.Fields.Update();


            //This sets the selection to be the whole document

            Word.Selection.HomeKey(WdUnits.wdStory, WdMovementType.wdMove);

            //Select from the insertion point to the end of the document.                           

            Word.Selection.EndKey(WdUnits.wdStory, WdMovementType.wdExtend);

            object replaceAll = WdReplace.wdReplaceAll;
            object missing = System.Reflection.Missing.Value;
            Word.Selection.Find.ClearFormatting();
            Word.Selection.Find.Text = "<Specification Name>";

            Word.Selection.Find.Replacement.ClearFormatting();
            Word.Selection.Find.Replacement.Text = processName;

            Word.Selection.Find.Execute(
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing);
        }

        private void ProcessElement(Element element, int level)
        {
            foreach (Element childElement in element.Elements)
            {
                
                if (childElement.Type == "Requirement")
                {
                    CreateDocumentHeading(childElement.Name, level);

                    AddElementNotes(childElement, true);

                    var childConnectors = new List<Connector>();
                    foreach (Connector connector in childElement.Connectors)
                    {
                        childConnectors.Add(connector);
                    }
                    childConnectors.Sort(CompareConnectors);
                    ;
                    foreach (Connector connector in childConnectors)
                    {
                        
                    var linkedElementId = connector.ClientID;
                        
                        if (linkedElementId == childElement.ElementID)
                        {
                            linkedElementId = connector.SupplierID;
                        }

                        Element linkedElement = AddInRepository.Instance.Repository.GetElementByID(linkedElementId);

                        if (linkedElement != null)
                        {
                            if (linkedElement.Type == "UMLDiagram")
                            {
                                var diagram = (Diagram) AddInRepository.Instance.Repository.GetDiagramByID(Int32.Parse(linkedElement.MiscData[0]));

                                if (diagram != null)
                                {
                                    Project projectInterface = AddInRepository.Instance.Repository.GetProjectInterface();
                                    projectInterface.PutDiagramImageOnClipboard(diagram.DiagramGUID, 1);

                                    PasteIntoDocument();
                                }
                            }
                            else
                            {
                                AddElementNotes(linkedElement);
                            }
                        }
                    }
                    ProcessElement(childElement, level + 1);
                }
                //PasteIntoDocument();
            }
        }
        private static int CompareConnectors(Connector x, Connector y)
        {
            var intx = 0;
            var inty = 0;
            if (!int.TryParse(x.Name, out intx)) intx = int.MaxValue;
            if (!int.TryParse(y.Name, out inty)) inty = int.MaxValue;

            if (intx == int.MaxValue && inty == int.MaxValue)
            {
                return string.Compare(x.Name, y.Name);
            }

            if (intx < inty) return -1;
            if (intx == inty) return 0;
            return 1;
        }

        private void AddElementNotes(Element element, bool suppressName = false)
        {
            if (!suppressName)
            {
                AddItalisisedText(element.Name + ": ");
            }

            var rtfLinkedDocument = element.GetLinkedDocument();

            if (!string.IsNullOrEmpty(rtfLinkedDocument))
            {
                Clipboard.SetData(DataFormats.Rtf, rtfLinkedDocument);

                AddRtfTextFromClipboard();
            }
            else if (!string.IsNullOrEmpty(element.Notes))
            {
                var notes = element.Notes;

                notes = CleanNotes(notes);
               var formattedText = AddInRepository.Instance.Repository.GetFormatFromField("RTF",
                                                                                           notes);
                Clipboard.SetData(DataFormats.Rtf, formattedText);

                AddRtfTextFromClipboard();
            }
        }

        private string CleanNotes(string notes)
        {
            var converted = notes;

            while (notes.Contains(Environment.NewLine + " " + Environment.NewLine))
            {
                notes = notes.Replace(Environment.NewLine + " " + Environment.NewLine, Environment.NewLine);
            }
            while (notes.Contains(Environment.NewLine + Environment.NewLine))
            {
                notes = notes.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
            }

            return notes;
        }

        private void AddText(string text)
        {
            Paragraph oPara;
            object oRng =
                   Document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara = Document.Content.Paragraphs.Add(ref oRng);
            oPara.Range.Text = HtmlHelper.StripHtmlAdvanced(text);
            object oStyleName = WdBuiltinStyle.wdStyleNormal;
            oPara.Range.set_Style(ref oStyleName);

            oPara.Range.InsertParagraphAfter();
        }
        private void AddRtfTextFromClipboard()
        {
            Paragraph oPara;
            object oRng =
                   Document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara = Document.Content.Paragraphs.Add(ref oRng);
            oPara.Range.Paste();

            oPara.Range.InsertParagraphAfter();
        }

        private void AddItalisisedText(string text)
        {
            Paragraph oPara;
            object oRng =
                   Document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara = Document.Content.Paragraphs.Add(ref oRng);
            oPara.Range.Italic = 1; 
            oPara.Range.Text = HtmlHelper.StripHtmlAdvanced(text);
            object oStyleName = WdBuiltinStyle.wdStyleNormal;
            oPara.Range.set_Style(ref oStyleName);
            oPara.Range.Select();
            oPara.Range.Italic = 1;
            oPara.Range.InsertParagraphAfter();
        }
        private void PasteIntoDocument()
        {
            Paragraph oPara;
            object oRng =
                   Document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara = Document.Content.Paragraphs.Add(ref oRng);
            oPara.Range.Paste();
            oPara.Range.InsertParagraphAfter();
        }
        private void CreateDocumentHeading(string Name, int level = 1)
        {
            Paragraph oPara;
            object oRng = Document.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara = Document.Content.Paragraphs.Add(ref oRng);
           

            object oStyleName;

            switch (level)
            {
                case 1:
                    oStyleName = WdBuiltinStyle.wdStyleHeading1;
                    break;
                case 2:
                    oStyleName = WdBuiltinStyle.wdStyleHeading2;
                    break;
                case 3:
                    oStyleName = WdBuiltinStyle.wdStyleHeading3;
                    break;
                case 4:
                    oStyleName = WdBuiltinStyle.wdStyleHeading4;
                    break;
                case 5:
                    oStyleName = WdBuiltinStyle.wdStyleHeading5;
                    break;
                default:
                    oStyleName = WdBuiltinStyle.wdStyleHeading5;
                    break;
            }

            
            oPara.Range.Text = Name;
            oPara.Range.set_Style(oStyleName);
            oPara.Range.InsertParagraphAfter();
        }
    }
}
