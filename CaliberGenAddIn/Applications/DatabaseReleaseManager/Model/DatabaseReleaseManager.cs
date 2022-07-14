///////////////////////////////////////////////////////////
//  DatabaseReleaseManager.cs
//  Implementation of the Class DatabaseReleaseManager
//  Generated by Enterprise Architect
//  Created on:      11-Nov-2009 15:52:48
//  Original author: Colin Richardson
///////////////////////////////////////////////////////////


using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EA;

namespace EAAddIn.Applications.DatabaseReleaseManager
{
    public class DatabaseReleaseManager
    {
        public Repository Repository;
        public Release Release;
        public List<Message> Messages = new List<Message>();
        public string ChangeType { get; set; }

        public Repository EARepository
        {
            get;
            set;
        }

        /// 
        /// <param name="release"></param>
        public void RemoveDatabaseStereotypes(string release)
        {

        }

        /// 
        /// <param name="release"></param>
        public void RemoveFutureReleaseDatabaseStereotypes(Release release)
        {

        }

        public string ValidateReleasePackage()
        {

            return "";
        }

        internal static List<Message> CleanupRelease(Release release)
        {
            var messages = ValidateOKtoCleanupRelease();

            if (messages.Count == 0)
            {
                messages = CleanupReleaseConfirmed(release);
            }

            return messages;

        }

        internal static List<Message> CleanupReleaseConfirmed(Release release)
        {
            var messages = new List<Message>();

            var stereotypeReferences = new Element().ListContainingStereotype(release.Name);

            foreach (var classReference in stereotypeReferences)
            {
                EA.Element element = AddInRepository.Instance.Repository.GetElementByGuid(classReference[0]);


                if (element.Stereotype.Contains("table"))
                {
                    if (element.StereotypeEx.Split(',').Count(item => item.Contains("table")) > 2)
                    {
                        messages.Add(new ErrorMessage("Multiple stereotypes found for table '" +
                            element.Name +
                            "'. Unable to clean table.") { Tag = element });


                    }
                    else if (element.StereotypeEx.Contains("deleted") && !element.StereotypeEx.Contains("to be deleted"))
                    {
                        messages.Add(new InformationMessage("table '" + element.Name + "' deleted."));
                        EaAccess.DeleteElement(element);
                    }
                    else
                    {
                        if (element.StereotypeEx.Contains("renamed"))
                        {
                            RemovePrevNameTagFromTable(element, messages);
                        }

                        messages.Add(new InformationMessage("Stereotype for table '" + element.Name + "' changed to 'table'.") { Tag = element });
                        var stereotype = element.Stereotype;
                        element.Stereotype = "table";
                        element.StereotypeEx = "table";
                        element.SetAppearance(1, 0, -1);
                        element.Update();

                        // clear the colour of all usages

                        List<EaAccess.DiagramStruct> objDiagList = new EaAccess().getDiagramList(element.ElementID);

                        foreach (EaAccess.DiagramStruct diagramStruct in objDiagList)
                        {
                            var diagram = AddInRepository.Instance.Repository.GetDiagramByID(diagramStruct.Diagram_ID);

                            foreach (DiagramObject diagramObject in diagram.DiagramObjects)
                            {
                                if (diagramObject.ElementID == element.ElementID)
                                {
                                    ApplyColourToDiagramElement(diagramObject,"none");
                                }
                            }
                        }
                        new EaAccess().RemoveElementStereotypes(element.ElementID, stereotype);
                    }
                }
            }

            stereotypeReferences = new Attribute().ListContainingStereotype(release.Name);
            foreach (var attributeReference in stereotypeReferences)
            {
                EA.Attribute attribute = AddInRepository.Instance.Repository.GetAttributeByGuid(attributeReference[0]);

                if (attribute.Stereotype.Contains("column"))
                {
                    if (attribute.StereotypeEx.Split(',').Count(item => item.Contains("column")) > 2)
                    {
                        messages.Add(new ErrorMessage("Multiple stereotypes found for column '" +
                            attribute.Name +
                            "' in table '" +
                            attributeReference[2] + "'. Unable to clean column.") { Tag = attribute });
                    }
                    else if (attribute.StereotypeEx.Contains("deleted") && !attribute.StereotypeEx.Contains("to be deleted"))
                    {
                        messages.Add(new InformationMessage("column '" + attribute.Name + "' in table '" + attributeReference[2] + "' deleted."));
                        new EaAccess().DeleteAttribute(attribute);
                    }
                    else
                    {
                        if (attribute.StereotypeEx.Contains("renamed"))
                        {
                            RemovePrevNameTagFromColumn(attribute, attributeReference[2], messages);
                        }


                        messages.Add(
                            new InformationMessage("Stereotype for column '" + attribute.Name + "' in table '" + attributeReference[2] + "' changed to 'column'.") { Tag = attribute });

                        attribute.Stereotype = "column";
                        attribute.StereotypeEx = "column";
                        attribute.Update();
                    }

                }
            }
            Diagram currentDiagram = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (currentDiagram != null)
            {
                currentDiagram.DiagramObjects.Refresh();
                currentDiagram.Update();
                AddInRepository.Instance.Repository.SaveDiagram(currentDiagram.DiagramID);
                AddInRepository.Instance.Repository.ReloadDiagram(currentDiagram.DiagramID);
            }

            return messages;
        }

        internal List<Message> CleanupDBRelease(Release release)
        {
            var messages = ValidateOKtoCleanupDbRelease();

            if (messages.Count == 0)
            {
                messages = CleanupDBReleaseConfirmed(release);
            }

            return messages;
        }

        private static List<Message> ValidateOKtoCleanupDbRelease()
        {
            var messages = new List<Message>();

            if (!(AddInRepository.Instance.Repository.GetTreeSelectedObject() is Package))
            {
                messages.Add(new ErrorMessage("You must select a data model package to cleanup."));
            }

            if (!AddInRepository.Instance.IsDbModel)
            {
                messages.Add(
                    new QuestionMessage(
                        "You are not connected to the Database repository. Do you want to continue cleaning future releases?"));
            }
            return messages;
        }
        private static List<Message> ValidateOKtoCleanupRelease()
        {
            var messages = new List<Message>();

            if (!AddInRepository.Instance.IsRelease)
            {
                messages.Add(
                    new QuestionMessage(
                        "You are not connected to the Release1 repository. Do you want to continue cleaning?"));
            }
            return messages;
        }

        public List<Message> CleanupDBReleaseConfirmed(Release release)
        {
            var messages = new List<Message>();

            if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Package)
            {
                messages = CleanupDBReleasePackage((Package)AddInRepository.Instance.Repository.GetTreeSelectedObject(),
                                                   release);
            }
            else
            {
                messages.Add(new ErrorMessage("You must select a data model package to cleanup."));

            }

            return messages;
        }

        private static List<Message> CleanupDBReleasePackage(IDualPackage releasePackage, Release release)
        {
            var messages = new List<Message>();

            foreach (EA.Element element in releasePackage.Elements)
            {
                messages.AddRange(CleanupDBReleaseElement(element, release));
            }
            foreach (Package package in releasePackage.Packages)
            {
                messages.AddRange(CleanupDBReleasePackage(package, release));
            }

            return messages;
        }

        private static List<Message> CleanupDBReleaseElement(EA.Element eaElement, Release release)
        {
            var messages = new List<Message>();
            var deleted = false;

            if (!string.IsNullOrEmpty(eaElement.StereotypeEx))
            {
                var releaseCompareResults = release.CompareReleaseStereotype(eaElement.StereotypeEx);

                if (releaseCompareResults == StereotypeStatus.MultipleStereotypesFound)
                {
                    messages.Add(new ErrorMessage("Multiple stereotypes found for table '" +
                                                  eaElement.Name +
                                                  "'. Unable to clean table.") { Tag = eaElement });

                }
                else
                {
                    var nonstandardStereotype = Release.GetNonstandardStereotype(eaElement.StereotypeEx);

                    if (releaseCompareResults == StereotypeStatus.FutureRelease)
                    {

                        if (nonstandardStereotype.StartsWith("new table"))
                        {
                            if (!EaAccess.DeleteElement(eaElement))
                            {
                                messages.Add(new ErrorMessage("Error deleting new table '" + eaElement.Name + "'") { Tag = eaElement });
                            }
                            else
                            {
                                messages.Add(new InformationMessage("new table '" + eaElement.Name + "' deleted"));
                                deleted = true;

                            }
                        }
                        else if (nonstandardStereotype.StartsWith("renamed table"))
                        {
                            var previousNameTaggedValue = (TaggedValue)eaElement.TaggedValues.GetByName("prev name");

                            if (previousNameTaggedValue == null ||
                                string.IsNullOrEmpty(previousNameTaggedValue.Value))
                            {
                                messages.Add(
                                    new ErrorMessage("'prev name' tag not found for renamed table '" + eaElement.Name +
                                                     "'") { Tag = eaElement });
                            }
                            else
                            {
                                var previousName = previousNameTaggedValue.Value;
                                messages.Add(
                                    new InformationMessage("Old name for renamed table '" + eaElement.Name +
                                                           "' restored to '" + previousName +
                                                           "' and 'prev name' tag removed"));

                                eaElement.Name = previousName;
                                eaElement.Stereotype = "table";
                                eaElement.StereotypeEx = "table";
                                eaElement.Update();

                                RemovePrevNameTagFromTable(eaElement, messages);
                            }
                        }
                        else if (nonstandardStereotype.StartsWith("deleted table"))
                        {
                            eaElement.Stereotype = "table";
                            eaElement.StereotypeEx = "table";
                            eaElement.Update();

                            messages.Add(
                                new InformationMessage("'deleted table' stereotype for table '" + eaElement.Name +
                                                       "' removed"));
                        }
                        else if (nonstandardStereotype.StartsWith("changed table"))
                        {
                            var message =
                                new WarningMessage(
                                    "Tables with a changed stereotype must be manually removed. Table '" +
                                    eaElement.Name +
                                    "'") { Tag = eaElement };
                            messages.Add(message);

                        }
                        else
                        {
                            messages.Add(
                                new WarningMessage("Unhandled stereotype '" + nonstandardStereotype + "' for table '" +
                                                   eaElement.Name +
                                                   "'") { Tag = eaElement });
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(nonstandardStereotype) &&
                            releaseCompareResults != StereotypeStatus.CurrentRelease)
                        {
                            messages.Add(
                                new WarningMessage("Unhandled stereotype '" + nonstandardStereotype + "' for table '" +
                                                   eaElement.Name +
                                                   "' (" + releaseCompareResults + ")") { Tag = eaElement });
                        }
                    }
                }
            }
            if (!deleted)
            {
                messages.AddRange(CleanupDBReleaseAttributes(eaElement, release));
            }

            return messages;
        }

        private static void RemovePrevNameTagFromTable(IDualElement eaElement, ICollection<Message> messages)
        {
            short index = -1;

            for (short i = 0; i < eaElement.TaggedValues.Count; i++)
            {
                var taggedValue = (TaggedValue)eaElement.TaggedValues.GetAt(i);

                if (taggedValue.Name == "prev name")
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                eaElement.TaggedValues.Delete(index);
            }
            else
            {
                messages.Add(
                    new ErrorMessage("Error deleting 'prev name' tag from renamed table '" +
                                     eaElement.Name +
                                     "'") { Tag = eaElement });
            }
        }

        private static List<Message> CleanupDBReleaseAttributes(IDualElement eaElement, Release release)
        {
            var toDelete = new List<EA.Attribute>();
            var messages = new List<Message>();

            foreach (EA.Attribute attribute in eaElement.Attributes)
            {

                var nonstandardStereotype = Release.GetNonstandardStereotype(attribute.StereotypeEx);

                if (nonstandardStereotype == null)
                {
                    messages.Add(new ErrorMessage("Multiple stereotypes found for column '" +
                              attribute.Name
                              + "' in table '" + eaElement.Name + "'.  Unable to clean column"));

                }

                if (!string.IsNullOrEmpty(nonstandardStereotype))
                {
                    var compareResult = release.CompareReleaseStereotype(attribute.StereotypeEx);

                    if (compareResult == StereotypeStatus.NotReleaseStereotype)
                    {
                        messages.Add(
                            new WarningMessage("Unhandled stereotype '" + nonstandardStereotype + "' for column '" +
                                               attribute.Name
                                               + "' in table '" +
                                               eaElement.Name +
                                               "' (Not a release stereotype)."));
                    }
                    else if (compareResult == StereotypeStatus.FutureRelease)
                    {
                        if (nonstandardStereotype.StartsWith("new column"))
                        {
                            toDelete.Add(attribute);
                        }
                        else if (nonstandardStereotype.StartsWith("renamed column"))
                        {
                            //AttributeTag previousNameTaggedValue = (AttributeTag)attribute.TaggedValues.GetByName("prev name");

                            short tagIndex = -1;
                            short loopIndex = 0;

                            foreach (AttributeTag tag in attribute.TaggedValues)
                            {
                                //AttributeTag tag = (AttributeTag)tagObject;
                                if (tag.Name == "prev name")
                                {
                                    tagIndex = loopIndex;
                                    break;
                                }
                                loopIndex++;
                            }

                            if (tagIndex == -1 ||
                                attribute.TaggedValues.GetAt(tagIndex) == null ||
                                string.IsNullOrEmpty(((AttributeTag)attribute.TaggedValues.GetAt(tagIndex)).Value))
                            {
                                messages.Add(new ErrorMessage("'prev name' tag not found for renamed column '" +
                                                              attribute.Name
                                                              + "' in table '" + eaElement.Name + "'") { Tag = attribute });

                            }
                            else
                            {
                                var previousNameTaggedValue =
                                    (AttributeTag)attribute.TaggedValues.GetAt(tagIndex);

                                var previousName = previousNameTaggedValue.Value;

                                messages.Add(new InformationMessage("Old name for renamed column '" + attribute.Name
                                                                    + "' in table '" + eaElement.Name +
                                                                    "' restored to '" +
                                                                    previousName + "' and 'prev name' tag removed"));

                                attribute.Name = previousName;
                                attribute.Stereotype = "column";
                                attribute.StereotypeEx = "column";
                                attribute.Update();

                                RemovePrevNameTagFromColumn(attribute, eaElement.Name, messages);
                            }
                        }
                        else if (nonstandardStereotype.StartsWith("deleted column"))
                        {
                            attribute.Stereotype = "column";
                            attribute.StereotypeEx = "column";
                            attribute.Update();

                            messages.Add(new InformationMessage("'deleted column' stereotype for column '" +
                                                                attribute.Name
                                                                + "' in table '" + eaElement.Name + "' removed"));
                        }
                        else if (nonstandardStereotype.StartsWith("changed column"))
                        {
                            messages.Add(
                                new WarningMessage("Columns with a changed stereotype must be manually removed. Column '" +
                                                   attribute.Name
                                                   + "' in table '" +
                                                   eaElement.Name +
                                                   "'") { Tag = attribute });
                        }
                        else
                        {
                            messages.Add(
                                new WarningMessage("Unhandled stereotype '" + nonstandardStereotype + "' for column '" +
                                                   attribute.Name
                                                   + "' in table '" +
                                                   eaElement.Name +
                                                   "'") { Tag = attribute });
                        }
                    }
                    else
                    {
                        if (compareResult != StereotypeStatus.CurrentRelease)
                        {
                            messages.Add(new WarningMessage("Unhandled stereotype '" + nonstandardStereotype + "' for column '" +
                                       attribute.Name
                                       + "' in table '" +
                                       eaElement.Name +
                                       "' (" + compareResult + ").") { Tag = attribute });
                        }
                    }
                }
            }
            if (toDelete.Count > 0)
            {
                foreach (EA.Attribute attributeToDelete in toDelete)
                {

                    if (!new EaAccess().DeleteAttribute(attributeToDelete))
                    {
                        messages.Add(new ErrorMessage("Error deleting new column '" +
                                                      attributeToDelete.Name
                                                      + "' in table '" + eaElement.Name + "'") { Tag = attributeToDelete });

                    }
                    else
                    {
                        messages.Add(new InformationMessage("new column '" +
                                                            attributeToDelete.Name
                                                            + "' in table '" + eaElement.Name + "' deleted"));
                    }
                }

            }

            return messages;
        }

        private static void RemovePrevNameTagFromColumn(IDualAttribute attribute, string elementName, ICollection<Message> messages)
        {
            short index = -1;

            for (short i = 0; i < attribute.TaggedValues.Count; i++)
            {
                var taggedValue = (AttributeTag)attribute.TaggedValues.GetAt(i);

                if (taggedValue.Name == "prev name")
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                attribute.TaggedValues.Delete(index);
            }
            else
            {
                messages.Add(
                    new ErrorMessage("Error deleting 'prev name' tag from renamed column '" +
                                     attribute.Name
                                     + "' in table '" + elementName +
                                     "'"));
            }
        }

        internal static void LocateInBrowser(object p)
        {
            EA.Element objElement;


            if (p is EA.Element)
            {
                objElement = (EA.Element)p;
            }

            else if (p is EA.Attribute)
            {
                objElement = AddInRepository.Instance.Repository.GetElementByID(((EA.Attribute)p).ParentID);
            }
            else
            {
                return;
            }

            if (objElement != null)
            {
                AddInRepository.Instance.Repository.ShowInProjectView(objElement);
            }
            else
            {
                MessageBox.Show("Element not found in EA.");
            }
        }

        public bool ClearStereotype(object tag)
        {
            bool ok = false;

            if (tag is EA.Element)
            {
                var element = (EA.Element)tag;

                element.Stereotype = "table";
                element.StereotypeEx = "table";
                ok = element.Update();
            }

            else if (tag is EA.Attribute)
            {
                var attribute = (EA.Attribute)tag;

                attribute.Stereotype = "column";
                attribute.StereotypeEx = "column";
                ok = attribute.Update();
            }
            return ok;
        }

        internal void ApplyStereotypeToSelectedTables(List<EA.Element> elements, List<DiagramObject> diagramObjects, List<string> newNames)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                var diagramObject = diagramObjects[i];

                if (elements[i].Type == "Class"
                    && element.Stereotype.Contains("table")
                    && !string.IsNullOrEmpty(ChangeType)
                    && ( new DefaultStereotypes().Defaults.Contains(ChangeType + " table") || ChangeType == "none" )
                    && Release != null)
                {
                    var newName = string.Empty;

                    if (ChangeType == "renamed" || ChangeType == "to be deleted")
                    {
                        newName = newNames[i];
                    }
                    ApplyStereotypeToTable(element, diagramObject, newName);
                }
            }
        }

        internal void ApplyStereotypeToSelectedColumns(IDualElement element, DiagramObject diagramObject, List<string> attributeNames, List<string> newAttributeNames)
        {
            for (int i = 0; i < attributeNames.Count(); i++)
            {
                if (ChangeType == "renamed")
                {
                    ApplyStereotypeToColumn(element, diagramObject, attributeNames[i], newAttributeNames[i]);
                }
                else
                {
                    ApplyStereotypeToColumn(element, diagramObject, attributeNames[i], string.Empty);
                }
            }


        }
        void ApplyStereotypeToTable(IDualElement element, DiagramObject diagramObject, string newName)
        {
            ApplyStereotypeToTable(element, diagramObject, ChangeType, newName);
        }
        void ApplyChangedStereotypeToTable(IDualElement element, DiagramObject diagramObject)
        {
            ApplyStereotypeToTable(element, diagramObject, "changed", string.Empty);
        }
        void ApplyStereotypeToTable(IDualElement element, DiagramObject diagramObject, string changeType, string newName)
        {
            var applyColourChange = (element.StereotypeEx == "table");

            if (changeType == "none")
            {
                element.Stereotype = "table";
                element.StereotypeEx = "table";
            }
            else if (!element.Stereotype.Contains(changeType + " table " + Release.Name))
            {
                element.StereotypeEx += "," + changeType + " table " + Release.Name;
            }

            if ((ChangeType == "renamed" || ChangeType == "to be deleted")
                && !string.IsNullOrEmpty(newName))
            {
                var tv = (TaggedValue) element.TaggedValues.GetByName("prev name") ??
                         (TaggedValue) element.TaggedValues.AddNew("prev name", element.Name);
                element.Name = newName;
                tv.Update();
            }
            //if (changeType == "none")
            //{
            //    element.SetAppearance(1,0,0);
            //}
            element.Update();

            if (applyColourChange)
            {
                ApplyColourToElement(element, changeType);
                ApplyColourToDiagramElement(diagramObject, changeType);
            }
        }

        void ApplyStereotypeToColumn(IDualElement element, DiagramObject diagramObject, string attributeName, string newAttributeName)
        {
            var attribList = from EA.Attribute attrib in element.Attributes
                             where attrib.Name == attributeName
                             select attrib;

            if (attribList.Count() != 1) return;

            var attribute = attribList.First();

            if (ChangeType == "none")
            {
                attribute.Stereotype = "column ";
                attribute.StereotypeEx = "column ";
            }
            else
            {
                attribute.Stereotype = ChangeType + " column " + Release.Name;
                attribute.StereotypeEx = ChangeType + " column " + Release.Name;
            }


            if (ChangeType == "renamed" && !string.IsNullOrEmpty(newAttributeName))
            {
                attribute.Name = newAttributeName;

                var newTag = attribute.TaggedValues.AddNew("prev name", attributeName);
                var tv = (AttributeTag)newTag;
                tv.Update();
            }
            attribute.Update();

            if (element.StereotypeEx.Contains("table"))
            {
                ApplyChangedStereotypeToTable(element, diagramObject);
            }
        }
        static void ApplyColourToDiagramElement(DiagramObject element, string changeType)
        {
            var backgroundColour = GetChangeColour(changeType);

            var style = element.Style.ToString();

            if (style.Contains("BCol="))
            {
                var bgPos = style.IndexOf("BCol=");
                var semiColonPos = style.Substring(bgPos + 5).IndexOf(";");

                //if (changeType == "none")
                //{
                //    style = style.Substring(0, bgPos)
                //            + style.Substring(bgPos + semiColonPos + 5);
                //}
                //else
                //{
                    style = style.Substring(0, bgPos + 5)
                            + backgroundColour
                            + style.Substring(bgPos + semiColonPos + 5);
                //}

            }
            else
            {
                //if (changeType != "none")
                //{
                    style = style + "BCol=" + backgroundColour + ";";
                //}
            }
            element.Style = style;
            element.Update();
        }
        public void ShowPKsAndChangedColumnsOnlyOnDiagramElement(Diagram diagram, DiagramObject diagramElement)
        {
            AddInRepository.Instance.Repository.SaveDiagram(diagram.DiagramID);

            var element = AddInRepository.Instance.Repository.GetElementByID(diagramElement.ElementID);
            string hiddenAttributesList = string.Empty;

            foreach (EA.Attribute attribute in element.Attributes)
            {
                bool hide;

                if (element.Stereotype.Contains("new") || element.StereotypeEx.Contains("new"))
                {
                    hide = (attribute.Name.ToUpper().StartsWith("CREATION_") ||
                            attribute.Name.ToUpper().StartsWith("UPDATE_") ||
                            attribute.Name.ToUpper().StartsWith("INTEGRITY_"));
                }
                else
                {
                    hide = (!attribute.IsOrdered &&
                            (attribute.Stereotype == "column" || attribute.StereotypeEx == "column"));
                }
                if (hide)
                {
                    if (hiddenAttributesList == string.Empty)
                    {
                        hiddenAttributesList +=  attribute.AttributeGUID.Substring(1, 6);
                    }
                    else
                    {
                        hiddenAttributesList += "," + attribute.AttributeGUID.Substring(1, 6);
                    }
                }


            }
            if (string.IsNullOrEmpty(hiddenAttributesList)) return;

            hiddenAttributesList = "S_" + element.ElementGUID.Substring(1, 6) + "=" + hiddenAttributesList + ":";

            var style = diagram.StyleEx;

            if (style.Contains("SPL="))
            {
                var hiddenAttributesTokenPos = style.IndexOf("SPL=");
                var startOfStyle = style.Substring(0, hiddenAttributesTokenPos);
                var semiColonPos = style.Substring(hiddenAttributesTokenPos + 4).IndexOf(";");
                if (semiColonPos == -1) semiColonPos = style.Substring(hiddenAttributesTokenPos + 4).Length;

                var endOfStyle = style.Substring(semiColonPos + hiddenAttributesTokenPos + 4);

                var splToken = style.Substring(hiddenAttributesTokenPos + 4, semiColonPos);

                if (splToken.IndexOf(element.ElementGUID.Substring(1, 6)) > -1)
                {
                    var diagramObjectPosition = splToken.IndexOf(element.ElementGUID.Substring(1, 6));

                    var startOfDiagramObject = splToken.Substring(0, diagramObjectPosition - 2);
                    var endOfDiagramObject = splToken.Substring(splToken.IndexOf(":")+1);

                    splToken = startOfDiagramObject + hiddenAttributesList + endOfDiagramObject;
                    
                }
                else
                {
                    splToken = splToken + hiddenAttributesList;
                }
                style = startOfStyle + "SPL=" + splToken + endOfStyle;

                
             
            }
            else
            {
                style += "SPL=" + hiddenAttributesList + ":";
            }


            diagram.StyleEx = style;
            diagram.Update();

            AddInRepository.Instance.Repository.ReloadDiagram(diagram.DiagramID);

        }
        
        void ApplyColourToElement(IDualElement element, string changeType)
        {
            var backgroundColour = GetChangeColour(changeType);

            element.SetAppearance(1, 0, backgroundColour);
            element.Update();


        }

        internal EA.Element GetSelectedTableElement()
        {
            Diagram diagram = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diagram != null && diagram.SelectedObjects.Count == 1)
            {
                var diagobj = (DiagramObject)diagram.SelectedObjects.GetAt(0);

                if (diagobj.ObjectType.ToString() != "Package")
                {
                    return AddInRepository.Instance.Repository.GetElementByID(
                        diagobj.ElementID);
                }
            }
            else
            {
                if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is EA.Element)
                {
                    return (EA.Element)AddInRepository.Instance.Repository.GetTreeSelectedObject();
                }

            }
            return null;
        }
        internal DiagramObject GetSelectedTableDiagramObject()
        {
            Diagram diagram = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diagram != null && diagram.SelectedObjects.Count == 1)
            {
                var diagobj = (DiagramObject)diagram.SelectedObjects.GetAt(0);

                if (diagobj.ObjectType.ToString() != "Package")
                {
                    return diagobj;
                }
            }
            return null;
        }
        
        internal IList<Column> GetColumnsForSelectedTable()
        {
            var columns = new List<Column>();


            EA.Element element = GetSelectedTableElement();

            if (element != null
                && element.ElementID > 0
                && element.Type == "Class"
                && element.Stereotype.Contains("table"))
            {
                foreach (EA.Attribute attribute in element.Attributes)
                {
                    columns.Add(new Column { Name = attribute.Name, AttributeID = attribute.AttributeID });
                }
            }
            return columns;
        }

        public void ApplyStereotypeToSelectedConnector(EA.Connector connector)
        {
            var backgroundColour = (ChangeType == "new" ? 65280 : GetChangeColour(ChangeType));

            connector.Color = backgroundColour;
            connector.Width = 1;
            connector.Update();

            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag != null) AddInRepository.Instance.Repository.ReloadDiagram(diag.DiagramID);
        }

        private static int GetChangeColour(string changeType)
        {
            var backgroundColour = -1;

            if (changeType.StartsWith("new"))
            {
                backgroundColour = 13565902;
            }
            else if (changeType.StartsWith("changed"))
            {
                backgroundColour = 13353215;
            }
            else if (changeType.StartsWith("deleted"))
            {
                backgroundColour = 255;
            }
            else if (changeType.StartsWith("to be deleted"))
            {
                backgroundColour = 4418795;
            }
            else if (changeType.StartsWith("none"))
            {
                backgroundColour = 16777215;
            }
            return backgroundColour;
        }
    }
    //end DatabaseReleaseManager

}//end namespace Applications