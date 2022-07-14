using System;
using System.Windows.Forms;
using EA;
using EAAddIn.Windows;

namespace EAAddIn.Applications.ProcessDocumentationTemplate
{
    internal static class ProcessTemplate
    {
        public static bool TransferPackage(string sourceGuid, Element destinationElement, string replaceStringWhat, string replaceStringWith)
        {
            Element toplevelbr = null;

            if (sourceGuid == null)
            {
                MessageBox.Show(@"Template Package GUID Not passed in", @"Error", MessageBoxButtons.OK);
                return false;
            }

            //select template package
            var sourcePackage =
                AddInRepository.Instance.Repository.GetPackageByGuid(sourceGuid.ToString());
            if (sourcePackage == null)
            {
                MessageBox.Show(@"Template Package GUID Not found", @"Error", MessageBoxButtons.OK);
                return false;
            }

            //select destination 
            if (destinationElement == null)
            {
                MessageBox.Show(@"Destination Element Not passed in", @"Error", MessageBoxButtons.OK);
                return false;
            }

            //Ensure Document Name is entered
            if (replaceStringWith == null)
            {
                MessageBox.Show(@"Document Name must be entered", @"Error", MessageBoxButtons.OK);
                return false;
            }

            //Ensure Document Name is entered
            if (replaceStringWhat == null)
            {
                MessageBox.Show(@"Search String must be entered", @"Error", MessageBoxButtons.OK);
                return false;
            }


            //Clone template
            Package clonedPackage = sourcePackage.Clone();
            int packid = clonedPackage.PackageID;

            //Goto Clone location
            clonedPackage = AddInRepository.Instance.Repository.GetPackageByID(packid);
            if (clonedPackage == null)
            {
                MessageBox.Show(@"Error Retrieving Clone", @"Error", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                CreateDesignPackages.RenamePackageAndContents(replaceStringWhat, clonedPackage, replaceStringWith);
            }

            //Grab the top level BR to move it
            foreach (Element e in clonedPackage.Elements)
            {
                if (
                    e.Stereotype == "Generator")
                {
                    toplevelbr = e;
                    break;
                }
            }


            //Move Clone BR to Destination
            if (toplevelbr != null)
            {
                toplevelbr.ParentID = destinationElement.ElementID;
                toplevelbr.Update();
                destinationElement.Update();

                var eaacess = new EaAccess();
                bool packageDeletedOk = eaacess.DeletePackage(clonedPackage);
            }
            else
            {
                MessageBox.Show(@"Top level BR Not found", @"Error", MessageBoxButtons.OK);
                return false;
            }
            AddInRepository.Instance.Repository.RefreshModelView(destinationElement.ParentID);
            AddInRepository.Instance.Repository.RefreshModelView(sourcePackage.ParentID);
            return true;
        }
    }
}