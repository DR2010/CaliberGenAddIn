using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EA;

namespace EAAddIn.Windows
{
    public partial class CreateDesignPackages : Form
    {
        List<string> _designComponents = new List<string>();
        private Package _packageTemplate;
        private Package _designViewPackage;


        public CreateDesignPackages()
        {
            InitializeComponent();
        }

        private void clonePackageButton_Click(object sender, EventArgs e)
        {
            var newComponent = ComponentsComboBox.Text;

            Package componentPackage;

            var componentPackageCreated = false;

            if (!_designComponents.Contains(newComponent))
            {
                componentPackage = _packageTemplate.Clone();

                componentPackage.ParentID = _designViewPackage.PackageID;
                componentPackage.Update();

                RenamePackageAndContents(_packageTemplate.Name, componentPackage, newComponent);
                LoadComponents();

                componentPackageCreated = true;
            }
            else
            {
                componentPackage = new EaAccess().GetPackageByName(newComponent, _designViewPackage.PackageID);
            }

            Package releasePackage;

            releasePackage = new EaAccess().GetPackageByName(newComponent + " Project Releases",
                                                                     componentPackage.PackageID);

            // If the release is not found by package name, use the current selected package.

            if (releasePackage == null)
            {
                releasePackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            }

            if (releasePackage == null)
            {
                MessageBox.Show("Please select destination package and try again.");
                return;
            }

            if (componentPackageCreated)
            {
                #region Component Package Created
                var defaultRelease = (Package)releasePackage.Packages.GetByName(newComponent + " Release 1");

                if (defaultRelease != null)
                {
                    if (string.IsNullOrEmpty(ProjectIDTextBox.Text))
                    {
                        // remove the default project entry
                        var enumerator = releasePackage.Packages.GetEnumerator();
                        short index = -1;

                        for (short i = 0; i < releasePackage.Packages.Count; i++)
                        {
                            enumerator.MoveNext();

                            if (((Package)enumerator.Current).Name == newComponent + " Release 1")
                            {
                                index = i;
                                break;
                            }
                        }
                        if (index != -1)
                        {
                            releasePackage.Packages.Delete(index);
                        }
                    }
                    else
                    {
                        RenamePackageAndContents(defaultRelease.Name, defaultRelease, ProjectIDTextBox.Text);
                    }
                }
                #endregion Component Package Created
            }
            else if (!string.IsNullOrEmpty(ProjectIDTextBox.Text))
            {
                // component exists, so just add the project release template
                // var release1Package = AddInRepository.Instance.Repository.GetPackageByGuid("{80E24A83-31FA-469b-AD23-172A6E683403}");
                var release1Package = AddInRepository.Instance.Repository.GetPackageByGuid("{469C1379-9ECD-4307-AC85-8657D4BAC3B6}");

                if (release1Package == null)
                {
                    MessageBox.Show("Template package not found. Contact the Solution Design Team \n GUID: {469C1379-9ECD-4307-AC85-8657D4BAC3B6}");
                    return;
                }

                Package projectReleasePackage = release1Package.Clone();

                int packid = releasePackage.PackageID;

                releasePackage = AddInRepository.Instance.Repository.GetPackageByID(packid);

                if (releasePackage == null)
                {
                    MessageBox.Show("Error retrieving owning package.");
                    return;
                }

                try
                {
                    projectReleasePackage.ParentID = releasePackage.PackageID;
                    projectReleasePackage.Update();

                    RenamePackageAndContents(release1Package.Name, projectReleasePackage, ProjectIDTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Process did not complete successfully. \n" +
                                    "Package parent could not be set.");

                    return;
                }
            }

            AddInRepository.Instance.Repository.RefreshModelView(componentPackage.PackageID);
            AddInRepository.Instance.Repository.RefreshModelView(_packageTemplate.PackageID);
        }

        public static void RenamePackageAndContents(string oldName, IDualPackage newPackage, string newName)
        {
            newPackage.Name = newPackage.Name.Replace(oldName, newName);
            newPackage.Update();

            foreach (Element element in newPackage.Elements)
            {
                RenameElementAndContents(oldName, element, newName);
            }

            foreach (Package package in newPackage.Packages)
            {
                RenamePackageAndContents(oldName, package, newName);
            }

            foreach (Diagram diagram in newPackage.Diagrams)
            {
                RenameDiagram(oldName, diagram, newName);
            }
        }
        private static void RenameDiagram(string oldName, IDualDiagram diagram, string newName)
        {
            diagram.Name = diagram.Name.Replace(oldName, newName);
            diagram.Update();
        }
        private static void RenameElementAndContents(string oldName, IDualElement element, string newName)
        {
            element.Name = element.Name.Replace(oldName, newName);
            element.Update();

            foreach (Element childElement in element.Elements)
            {
                RenameElementAndContents(oldName, childElement, newName);
            }
            foreach (Diagram diagram in element.Diagrams)
            {
                RenameDiagram(oldName, diagram, newName);
            }
        }

        private void CR0370AddIn_Load(object sender, EventArgs e)
        {
            _packageTemplate =
                AddInRepository.Instance.Repository.GetPackageByGuid("{19299BE3-8C0C-45bf-AC90-83A19D9B567C}");
            _designViewPackage =
                AddInRepository.Instance.Repository.GetPackageByGuid("{38FB7325-2C6B-40f9-945A-992DF32925BC}");

            LoadComponents();
        }

        private void LoadComponents()
        {
            _designComponents = new EaAccess().ListPackagesWithinPackage(_designViewPackage);

            ComponentsComboBox.DataSource = _designComponents;
        }

        private void applyDesignPlanButton_Click(object sender, EventArgs e)
        {
            Element element = EaAccess.GetSelectedElement();

            if (element != null)
            {
                Element project = AddInRepository.Instance.Repository.GetElementByGuid("{2EDDE6A8-091C-4ea6-A0DF-4C1345BEBF23}");
                foreach (Requirement  requirement in project.Requirements)
                {
                    Requirement designPlanRequirement = (Requirement) element.Requirements.AddNew(requirement.Name, requirement.Type);

                    designPlanRequirement.Update();
                }
            }
            AddInRepository.Instance.Repository.RefreshOpenDiagrams(true);
        }
    }
}
