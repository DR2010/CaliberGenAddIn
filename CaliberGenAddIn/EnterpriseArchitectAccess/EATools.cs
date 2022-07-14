using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EA;

namespace EAAddIn
{
    class EATools
    {

        private List<ProjectTemplate> mainList;

        // --------------------------------------------
        //      Replicate package structure
        // --------------------------------------------
        public void ApplyProjectTemplate2()
        {
            // Template source location
            //
            Package packageTemplate = 
                AddInRepository.Instance.Repository.GetPackageByGuid(
                "{19299BE3-8C0C-45bf-AC90-83A19D9B567C}");

            // Destination folder
            //
            Package destinationPackage = 
                 AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (destinationPackage == null)
                return;

            // Load Template
            AddPackageContents(packageTemplate, destinationPackage, destinationPackage.Name);

        }

        // --------------------------------------------
        // Replicate package structure
        // --------------------------------------------
        public void AddPackageContents(
                             Package sourceHolder, 
                             Package destinationHolder,
                             string projectName)
        {

            foreach (EA.Diagram diagram in sourceHolder.Diagrams)
            {
                ProjectTemplate proj = new ProjectTemplate
                {
                    DiagramType = diagram.Type,
                    ItemName = diagram.Name,
                    ItemType = "Diagram",
                    PackageOwner = destinationHolder
                };

                proj.ItemName = proj.ItemName.Replace("Project Template", projectName);

                Diagram diag = _createDiagram(proj, diagram);

            }

            foreach (EA.Element element in sourceHolder.Elements)
            {
                ProjectTemplate proj = new ProjectTemplate
                {
                    DiagramType = element.Type,
                    ItemName = element.Name,
                    ItemType = "Element",
                    PackageOwner = destinationHolder
                };

                proj.ItemName = proj.ItemName.Replace("Project Template", projectName);

                var linkedDocument = element.GetLinkedDocument();
                element.SaveLinkedDocument("C:\\tmp.rtf");

                var newElement = _createElement(proj);

                if (! string.IsNullOrEmpty(linkedDocument))
                {
                    newElement.LoadLinkedDocument("c:\\tmp.rtf");
                    newElement.SaveLinkedDocument("c:\\tmp.rtf");
                    newElement.Update();
                }
            }

            foreach (EA.Package packageSource in sourceHolder.Packages)
            {
                ProjectTemplate proj = new ProjectTemplate
                                           {
                                               DiagramType = "",
                                               ItemName = packageSource.Name,
                                               ItemType = "Package",
                                               PackageOwner = destinationHolder
                                           };

                proj.ItemName = proj.ItemName.Replace("Project Template", projectName);

                Package destinationNewFolder = _createPackage(proj);

                AddPackageContents(packageSource, destinationNewFolder, projectName);
                
            }
        }

        //
        // Project Template structure
        //
        public struct ProjectTemplate
        {
            public string ItemType;
            public EA.Package PackageOwner;
            public string ProjectName;
            public string ItemName;
            public string DiagramType;

        }

        private static Package _createPackage(Package packageOwner, string packageName)
        {
            Package packageNew =
                (Package)packageOwner.Packages.AddNew(packageName, "type");

            packageNew.Update();
            packageOwner.Packages.Refresh();

            return packageNew;
        }

        private static Package _createPackage(ProjectTemplate package)
        {
            Package packageNew =
                (Package)package.PackageOwner.Packages.AddNew(package.ItemName, "type");

            packageNew.Update();
            package.PackageOwner.Packages.Refresh();

            return packageNew;
        }

        private static Diagram _createDiagram(Package packageOwner, string diagramName, string diagramType)
        {
            Diagram diagramNew = (Diagram)packageOwner.Diagrams.AddNew(diagramName, diagramType);
            diagramNew.ShowDetails = 1;
            diagramNew.Update();
            packageOwner.Diagrams.Refresh();

            return diagramNew;

        }

        private static Diagram _createDiagram(ProjectTemplate package, EA.Diagram diagram)
        {
            Diagram diagramNew = (Diagram)package.PackageOwner.Diagrams.AddNew(
                package.ItemName, package.DiagramType);
            diagramNew.ShowDetails = diagram.ShowDetails;

            if (diagram.SwimlaneDef.Swimlanes.Count > 0)
            {
                diagramNew.SwimlaneDef.Orientation = diagram.SwimlaneDef.Orientation;

                foreach (EA.Swimlane swimlane in diagram.SwimlaneDef.Swimlanes)
                {
                    var newSwimLane = diagramNew.SwimlaneDef.Swimlanes.Add(swimlane.Title, swimlane.Width);

                    newSwimLane.BackColor = swimlane.BackColor;
                }
            }

            diagramNew.Update();
            package.PackageOwner.Diagrams.Refresh();

            return diagramNew;

        }

        private static Element _createElement(ProjectTemplate package)
        {
            Element elementNew = (Element)package.PackageOwner.Elements.AddNew(
                package.ItemName, package.DiagramType);

            elementNew.Update();
            package.PackageOwner.Diagrams.Refresh();

            return elementNew;
        }

    }
}
