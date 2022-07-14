using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EA;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using Diagram = EA.Diagram;

namespace EAAddIn.Applications.SpecificationGenerator
{
    public class ProcessSpecificationGenerator : SpecificationEngine
    {
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

 
            object template = @"\\edmgt022\eakeystore$\EA Software\DEEWR AddIn\Resources\documentgeneration.dot";

            CreateDocumentFromTemplate(template);

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

        private void FinaliseDocument(string processName)
        {
            Document.Fields.Update();


            //This sets the selection to be the whole document

            Word.Selection.HomeKey(WdUnits.wdStory, WdMovementType.wdMove);

            //Select from the insertion point to the end of the document.                           

            Word.Selection.EndKey(WdUnits.wdStory, WdMovementType.wdExtend);

            object replaceAll = WdReplace.wdReplaceAll;
           
            Word.Selection.Find.ClearFormatting();
            Word.Selection.Find.Text = "<Specification Name>";

            Word.Selection.Find.Replacement.ClearFormatting();
            Word.Selection.Find.Replacement.Text = processName;

            Word.Selection.Find.Execute(
                ref Missing, ref Missing, ref Missing, ref Missing, ref Missing,
                ref Missing, ref Missing, ref Missing, ref Missing, ref Missing,
                ref replaceAll, ref Missing, ref Missing, ref Missing, ref Missing);
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





 
    }
}
