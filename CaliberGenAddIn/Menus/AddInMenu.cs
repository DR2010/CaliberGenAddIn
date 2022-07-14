using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;
using EAAddIn.Properties;

namespace EAAddIn.Menus
{
    public class AddInMenu
    {
        private Action<string> messageHandler;

        public static string[] List(string MenuName)
        {
            var addin = "-&DEEWR AddIn v" + EAAddIn.Version;

            if (string.IsNullOrEmpty(MenuName)) return new[]{addin};

            if (MenuName == addin)
            {
                var addIns = new List<string>();

                if (!string.IsNullOrEmpty(Settings.Default.LastAddIn1))
                {
                    addIns.Add(Settings.Default.LastAddIn1);
                    if (!string.IsNullOrEmpty(Settings.Default.LastAddIn2))
                    {
                        addIns.Add(Settings.Default.LastAddIn2);
                    }
                    if (!string.IsNullOrEmpty(Settings.Default.LastAddIn3))
                    {
                        addIns.Add(Settings.Default.LastAddIn3);
                    }
                    addIns.Add("-");

                }
                if (WindowsIdentity.GetCurrent().Name.Split('\\')[1] == "CR0370" || WindowsIdentity.GetCurrent().Name.Split('\\')[1] == "DM0874")
                {
                    addIns.Add(AddInApplications.NATION_CR0370);
                    addIns.Add(AddInApplications.GENWrapperTest);
                }
                addIns.AddRange(new[]
                           {
                               AddInApplications.CalibreLoadObjects,
                               AddInApplications.COOLGenGetServiceNumber,
                               AddInApplications.COOLGenLoadStructureChart,
                               AddInApplications.DBAReleaseManager,
                               AddInApplications.DBAReleaseToolbar,
                               AddInApplications.EAApplyTemplate,
                               AddInApplications.EABusinessRuleRealisation,
                               AddInApplications.EACreateNewProject,
                               AddInApplications.EACreateProcessDocumentationTemplate,
                               AddInApplications.EACsvElementImporter,
                               AddInApplications.EAElementLinker,
                               AddInApplications.EAExplorer,
                               AddInApplications.EADuplicatedElements,
                               AddInApplications.EAFindByPath,
                               AddInApplications.EALocateComposite,
                               AddInApplications.EAPlaceLinkedElements,
                               AddInApplications.EAPromoteElement,
                               AddInApplications.EAProcessSpecificationGenerator,
                               AddInApplications.EAReports,
                               "-Admin",
                               "-",
                               //AddInApplications.EAInstallAddIn,
                               "About"
                               
                           });
                return addIns.ToArray();


            }

            if (MenuName == "-Admin")
            {
                return new[]
                           {
                               AddInApplications.BPMNReports,
                               AddInApplications.COOLGENFixCABTags,
                               AddInApplications.COOLGenFixTableTags,
                               AddInApplications.COOLGenUpdateStereotype,
                               AddInApplications.EACleanClass,
                               AddInApplications.EAUpdateStereotype,
                               AddInApplications.UpdateAddInVersion,
                               AddInApplications.EAExtractAndClearAuditLogs
                           };
            }

            return new[] {string.Empty};
        }

        public static void Execute(string item)
        {
            if (String.IsNullOrEmpty(AddInRepository.Instance.Repository.ConnectionString))
            {
                MessageBox.Show("This option is not available for a local EA file.  Exiting add-in...");
                return;
            }
            if (!AddInRepository.Instance.Repository.IsSecurityEnabled)
            {
                MessageBox.Show("This option is not available when security is not enabled.  Exiting add-in...");
                return;
            }

            var user = AddInRepository.Instance.Repository.GetCurrentLoginUser();

            var menuItem = EAMenuFactory.GetEAMenu(item);

            // check security

            var hasAccess = menuItem.SecurityRoles.Count == 0
                                 ? true
                                 : false;

            var EA = new EaAccess();

            if (!EA.IsSqlRepository)
            {
                MessageBox.Show("Repository does not allow updates.");
                return;
            }


            foreach (var role in menuItem.SecurityRoles)
            {
                if (EA.UserAccess(user, role))
                {
                    hasAccess = true;
                    continue;
                }
            }

            if (hasAccess)
            {
                if (menuItem.Name != AddInApplications.About)
                {
                    //AddToRecentlyUsedItems(menuItem.Name);
                }


                menuItem.ActivateAddIn();
            }
            else
            {
                MessageBox.Show("You must have one of the following roles to access this add-in: " +
                                menuItem.SecurityRoles.ToFormattedString());
            }
        }
        private void ShowMessage(string text)
        {
            if (messageHandler != null)
            {
                messageHandler(text);
            }
            else
            {
                MessageBox.Show(text);

            }
        }
    }
}
