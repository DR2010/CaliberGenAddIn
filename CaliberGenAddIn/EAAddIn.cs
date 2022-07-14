using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications;
using EAAddIn.Menus;
using EAAddIn.Properties;
using EAAddIn.Windows;
using EAStructures;
using File=System.IO.File;

// ---------------------------------------------------------------------
//
//  Add-in to import COOLGen wrapper structure and Caliber information
//
// ---------------------------------------------------------------------

namespace EAAddIn
{
    public class EAAddIn
    {
        public EAAddIn( Action<string> addinMessageHandler = null)
        {
            messageHandler = addinMessageHandler;
        }
        public EAAddIn()
        {
        }

        //public const string EAREP1ID =
        //    "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";

        public Collection eacollection;
        private bool m_ShowFullMenus;
        public SecurityInfo secInfo;
        public ESGAddinAboutBox theForm;
        public string user;
        private Action<string> messageHandler;

        public static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.Minor + "." + Assembly.GetExecutingAssembly().GetName().Version.Build; }
        }
            // This is the only repository to allow changes.
        //

        //Called Before EA starts to check Add-In Exists
        //
        public String EA_Connect(Repository Repository)
        {
            var file = @"\\edmgt022\eakeystore$\MDG\addin searches.xml";

            if (File.Exists(file))

            {
                var text = File.ReadAllText(file);

                if (!string.IsNullOrEmpty(text))
                {
                    Repository.AddDefinedSearches(text);
                }
            }

            CheckVersion();

            //No special processing required.

            return "a string";
        }

        //Called when user Click Add-Ins Menu item from within EA.
        //Populates the Menu with our desired selections.
        //
        public object EA_GetMenuItems(Repository Repository,
                                      string Location,
                                      string MenuName)
        {
            AddInRepository.Instance.Repository = Repository;

            return AddInMenu.List(MenuName);

        }

        //Sets the state of the menu depending if there is an active project or not
        //
        private bool IsProjectOpen()
        {
            try
            {
                eacollection = AddInRepository.Instance.Repository.Models;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Called once Menu has been opened to see what menu items are active.
        public void EA_GetMenuState(Repository Repository, string Location, string MenuName,
                                    string ItemName, ref bool IsEnabled,
                                    ref bool IsChecked)
        {
            if (IsProjectOpen())
            {
                if (ItemName == "Menu1")
                    IsChecked = m_ShowFullMenus;
                else if (ItemName == "Menu2")
                    IsEnabled = m_ShowFullMenus;
            }
            else
                // If no open project, disable all menu options
                IsEnabled = false;
        }

        //Called when user makes a selection in the menu.
        //This is your main exit point to the rest of your Add-in
        public void EA_MenuClick(Repository Repository, string Location, string MenuName, string ItemName, Action<string> messageHandler = null)
        {

            AddInRepository.Instance.Repository = Repository;

            if (String.IsNullOrEmpty(Repository.ConnectionString))
            {
                ShowMessage("This option is not available for a local EA file.  Exiting add-in...");
                return;
            }
            if (!Repository.IsSecurityEnabled)
            {
                ShowMessage("This option is not available when security is not enabled.  Exiting add-in...");
                return;
            }

            user = Repository.GetCurrentLoginUser();

            var menuItem = EAMenuFactory.GetEAMenu(ItemName);

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
                    AddToRecentlyUsedItems(menuItem.Name);
                }


                try
                {
                    menuItem.ActivateAddIn();
                }
                catch (Exception exception)
                {
                    var error = "Exception: " + exception.Message;

                    if (exception.InnerException != null)
                    {
                        error += Environment.NewLine + Environment.NewLine + "Inner exception: " +
                                 exception.InnerException.Message;
                    }
                    MessageBox.Show(error , "Add-in Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void AddToRecentlyUsedItems(string addIn)
        {
            if (addIn == Settings.Default.LastAddIn1) return;
            if (addIn == Settings.Default.LastAddIn2)
            {
                Settings.Default.LastAddIn2 = Settings.Default.LastAddIn1;
                Settings.Default.LastAddIn1 = addIn;
                return;
            }
            Settings.Default.LastAddIn3 = Settings.Default.LastAddIn2;
            Settings.Default.LastAddIn2 = Settings.Default.LastAddIn1;
            Settings.Default.LastAddIn1 = addIn;

        }

        
        public bool EA_OnPostNewElement(Repository Repository, EventProperties Info)
        {
            bool allow = true;
            if (IsRepositoryRelease1(Repository))
            {
                AddInRepository.Instance.Repository = Repository;

                var newElementEvent = new EaAccess();
                EventProperty epElement = Info.Get("ElementID");

                int elementInt = Convert.ToInt32(epElement.Value);

                Element element = Repository.GetElementByID(elementInt);
                if (element != null)
                {
                    allow = newElementEvent.postNewElement(element);
                }
            }

            if (AddInRepository.Instance.Repository != null)
            {
                AddInRepository.Instance.Repository.SuppressEADialogs = true;
            }
            return allow;
        }

        private bool IsRepositoryRelease1(Repository Repository)
        {
            return Repository.ConnectionString.ToUpper().Contains(SqlHelpers.EaRelease1DbConnectionString);
        }


        //public bool EA_FileOpen(Repository Repository, EventProperties Info)
        //{
        //    return true;
        //}


        public bool EA_OnPreDeleteConnector(Repository Repository, EventProperties Info)
        {
            // Identify read only actions
            // Only if the model is the Repository1
            bool allow = true;
            if (IsRepositoryRelease1(Repository))
            {
                var der = new DeleteElementRules();
                allow = der.allowConnectorRules(Repository, Info);
            }
            return allow;
        }

        public bool EA_OnPreNewConnector(Repository Repository, EventProperties Info)
        {
            bool allow = true;
            if (IsRepositoryRelease1(Repository))
            {
                var der = new DeleteElementRules();
                allow = der.allowConnectorRules(Repository, Info);
            }
            return allow;
        }
        public void EA_OnPostOpenDiagram(Repository Repository, int DiagramID)
        {
            AddInRepository.Instance.OpenDiagrams.Add(DiagramID);
        }
        public void EA_OnPostCloseDiagram(Repository Repository, int DiagramID)
        {
            AddInRepository.Instance.OpenDiagrams.Remove(DiagramID);
        }

        public bool CheckVersion()
        {
            return true;

            bool ret = false;

            // Get version from file
            var fileVersion =
                "\\\\edmgt022\\eakeystore$\\EA Software\\" +
                "DEEWR AddIn\\Latest Version\\readme.txt";

            if (File.Exists(fileVersion))
            {
                var barray = new byte[20];

                var releaseFile = File.Open(fileVersion, FileMode.Open, FileAccess.Read);
                releaseFile.Read(barray, 0, 20);

                var version = "";
                var cnt = 0;

                foreach (byte b in barray)
                {
                    if (cnt > 12)
                        break;

                    if (cnt >= 9)
                    {
                        version += (char) b;
                    }
                    cnt++;
                }
                releaseFile.Close();

                if (version == Version)
                {
                    ret = true;
                }
                else
                {
                    decimal runningVersion = 0;
                    decimal latestVersion = 0;

                    if (decimal.TryParse(Version, out runningVersion) &&
                        decimal.TryParse(version, out latestVersion) &&
                        latestVersion > runningVersion)
                    {
                        WindowsIdentity identity = WindowsIdentity.GetCurrent();
                        WindowsPrincipal principal = new WindowsPrincipal(identity);

                        var isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

                        if (isAdmin)
                        {
                            var result =
                                MessageBox.Show(
                                    "You have an old version of the add-in: " + Version + ".  The current version is " +
                                    version + "." +
                                    Environment.NewLine + Environment.NewLine +
                                    "Would you like the new version installed now?", "New Version Detected",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                InstallAddIn();
                            }
                        }
                        else if (Environment.OSVersion.Version.Major >= 6)
                        {
                            MessageBox.Show(
                                   "You have an old version of the add-in: " + Version + ".  The current version is " +
                                   version + "." +
                                   Environment.NewLine + Environment.NewLine +
                                   "You must run EA as administrator to update the add-in (right-click EA icon, Run As Administrator)", "New Version Detected",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            return ret;
        }

        public static void InstallAddIn()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            WindowsPrincipal principal = new WindowsPrincipal(identity);

            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show("You must be a member of the admininstor group on your PC to install the add-in",
                                "Install Add-In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var addInInstaller = @"\\edmgt022\eakeystore$\EA Software\DEEWR AddIn\MSI Deployment\DEEWR ESG AddIn Setup.msi";
                            
            try
            {
                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = addInInstaller;
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = Version;

                var running = Process.Start(startInfo);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public object Search(Repository Repository, string SearchTerm, out string XMLResults)
        {
            

            var searchResult = new SearchResults();

            searchResult.Fields.Add(new Field() { Name = "CLASSTYPE" });
            searchResult.Fields.Add(new Field() { Name = "CLASSGUID" });
            searchResult.Fields.Add(new Field() { Name = "1" });
            searchResult.Fields.Add(new Field() { Name = "2" });

            var row = new Row();

            row.Fields.Add(new Field() { Name = "CLASSTYPE", Value = "Class" });
            row.Fields.Add(new Field() { Name = "CLASSGUID", Value = "{04376129-3112-4d5f-96A1-2C8B2F0C329D}" });
            row.Fields.Add(new Field() { Name = "1", Value = "Val1" });
            row.Fields.Add(new Field() { Name = "2", Value = "Val2" });

            searchResult.Rows.Add(row);

            var results = XmlHelpers.SerializeObject<SearchResults>(searchResult);
            
            // Remove the standard XML header
            XMLResults = results.Substring(39);
            return results;
        }

        public object ChangedElementsSearch(Repository Repository, string SearchTerm, out string XMLResults)
        {
            return CustomSearches.ChangedItemsSearch.Search(Repository, SearchTerm, out XMLResults);
        }

        public object AuditSearch(Repository Repository, string SearchTerm, out string XMLResults, EaAccess access = null)
        {
            return CustomSearches.AuditSearch.Search(Repository, SearchTerm, out XMLResults, access);
        }

        public object UnlinkedElementsSearch(Repository Repository, string SearchTerm, out string XMLResults)
        {
            return CustomSearches.UnlinkedElementsSearch.Search(Repository, SearchTerm, out XMLResults);
        }
        public object ElementsNotUsedInDiagramsSearch(Repository Repository, string SearchTerm, out string XMLResults)
        {
            return CustomSearches.ElementsNotUsedInDiagramsSearch.Search(Repository, SearchTerm, out XMLResults);
        }
    }

}


