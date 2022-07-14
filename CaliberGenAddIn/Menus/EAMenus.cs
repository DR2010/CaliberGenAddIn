using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EA;
using EAAddIn.Menus;
using EAAddIn.Presenters;
using EAAddIn.Windows;
using StaticProfileBuilder=EAAddIn.Applications.StaticProfileBuilder.StaticProfileBuilder;

namespace EAAddIn
{
    public static class EAMenuFactory
    {
        public static IEAMenu GetEAMenu(string name)
        {
            switch (name)
            {
                case AddInApplications.About:
                    return new AboutMenu();
                case AddInApplications.BPMNReports:
                    return new BPMNReportsMenu();
                case AddInApplications.UpdateAddInVersion:
                    return new UpdateAddInVersionMenu();
                case AddInApplications.CalibreLoadObjects:
                    return new CalibreLoadObjectsMenu();
                case AddInApplications.COOLGENFixCABTags:
                    return new COOLGENFixCABTagsMenu();
                case AddInApplications.COOLGenFixTableTags:
                    return new COOLGenFixTableTagsMenu();
                case AddInApplications.COOLGenGetServiceNumber:
                    return new COOLGenGetServiceNumberMenu();
                case AddInApplications.COOLGenLoadStructureChart:
                    return new COOLGenLoadStructureChartMenu();
                case AddInApplications.COOLGenUpdateStereotype:
                    return new COOLGenUpdateStereotypeMenu();
                case AddInApplications.EAApplyTemplate:
                    return new EAApplyTemplateMenu();
                case AddInApplications.EACreateNewProject:
                    return new EACreateNewProjectMenu();
                case AddInApplications.EACreateProcessDocumentationTemplate:
                    return new EACreateProcessDocumentationTemplateMenu();
                //case AddInApplications.EABusinessRuleRealisation:
                //    return new EABusinessRuleRealisationMenu();

                case AddInApplications.EABusinessRuleRealisation:
                    return new BusinessRulesRealisationMenu();

                case AddInApplications.EACleanClass:
                    return new EACleanClassMenu();
                case AddInApplications.EAElementLinker:
                    return new ElementLinkerMenu();
                case AddInApplications.EAExplorer:
                    return new EAExplorerMenu();
                //case AddInApplications.EAPlaceLinkedElements:
                //    return new EAPlaceLinkedElementsMenu();

                case AddInApplications.EAPlaceLinkedElements:
                    return new LinkedElementsToolbarMenu();
                case AddInApplications.EADuplicatedElements:
                    return new DuplicatedElementsMenu();

                case AddInApplications.EALocateComposite:
                    return new EALocateCompositeMenu();
                case AddInApplications.EAProcessSpecificationGenerator:
                    return new ProcessSpecificationGeneratorMenu();
                case AddInApplications.EAPromoteElement:
                    return new EAPromoteElementMenu();
                case AddInApplications.EAReports:
                    return new EAReportsMenu();
                case AddInApplications.EAUpdateStereotype:
                    return new EAUpdateStereotypeMenu();
                case AddInApplications.StaticProfileBuilder:
                    return new StaticProfileBuilderMenu();
                case AddInApplications.SQLCreateChangeScripts:
                    return new SQLCreateChangeScriptsMenu();
                case AddInApplications.DBAReleaseManager:
                    return new DatabaseReleaseManagerMenu();
                case AddInApplications.DBAReleaseToolbar:
                    return new DatabaseReleaseToolbarMenu();
                case AddInApplications.EACsvElementImporter:
                    return new CSVClassImporterMenu();
                case AddInApplications.AuditLogMaintenance:
                    return new AuditLogMaintenance();
                case AddInApplications.EAFindByPath:
                    return new GetCurrentPath();
                case AddInApplications.LocateClassifier:
                    return new LocateClassifier();
              case AddInApplications.EAExtractAndClearAuditLogs:
                    return new ExtractAndClearAuditLogsMenu();
                case AddInApplications.NATION_CR0370:
                    return new CR0370Menu();
                case AddInApplications.EAInstallAddIn:
                    return new AddInInstallerMenu();

                case AddInApplications.GENWrapperTest:
                    return new GENTestMenu();

                default:
                    MessageBox.Show("Add-In application not defined!");
                    return null;
            }
        }
    }
    [ComVisible(false)]
    public interface IEAMenu
    {
        string Name { get; }
        List<string> SecurityRoles { get; }
        void ActivateAddIn();
    }
    [ComVisible(false)]
    public class AboutMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return "About"; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            new EAAddIn().CheckVersion();

            var anAbout = new ESGAddinAboutBox();
            anAbout.ShowDialog();
        }
    #endregion
    }
    [ComVisible(false)]
    public class BPMNReportsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.BPMNReports; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>{EASecurityGroups.BusinessProcessModeller}; }
        }

        public void ActivateAddIn()
        {
            var bpmnrep = new BPMNReport();
            bpmnrep.Show();
        }


        #endregion
    }
    [ComVisible(false)]
    public class CalibreLoadObjectsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.CalibreLoadObjects; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.BusinessAnalyst }; }
        }

        public void ActivateAddIn()
        {
            if (!CaliberImportForm.IsShown)
            {
                //if (!new EAAddIn().CheckVersion())
                //    return;

                var cif = new CaliberImportForm();
                cif.Show();
                Application.DoEvents();
                cif.LoginToCaliber();
            }
        }

        #endregion
    }
    
    [ComVisible(false)]
    public class COOLGENFixCABTagsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.COOLGENFixCABTags; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.Administrators }; }
        }

        public void ActivateAddIn()
        {
            if (MessageBox.Show("Are you sure?",
                                "GEN - Fix CAB tags",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var eaac = new EaAccess();
                eaac.fixLoadModuleTaggedValue();
            }
        }


        #endregion
    }
    
    [ComVisible(false)]
    public class UpdateAddInVersionMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.UpdateAddInVersion; }
        }
        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.Administrators }; }
        }

        public void ActivateAddIn()
        {
            var eaac = new EaAccess();
            eaac.UpdateAddInVersion();
        }


        #endregion
    }
    
    [ComVisible(false)]
    public class ExtractAndClearAuditLogsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAExtractAndClearAuditLogs; }
        }
        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.Administrators }; }
        }

        public void ActivateAddIn()
        {
            var ecal = new ExtractAndClearAuditLogs();
            ecal.Show();
        }


        #endregion
    }
    
    [ComVisible(false)]
    public class COOLGenFixTableTagsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.COOLGenFixTableTags; }
        }
        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.Administrators }; }
        }

        public void ActivateAddIn()
        {
            if (MessageBox.Show("Are you sure?",
                                "GEN - Fix CAB tags",
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var eaac = new EaAccess();
                eaac.fixTableTaggedValue();
            }
        }


        #endregion
    }
    [ComVisible(false)]
    public class COOLGenGetServiceNumberMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.COOLGenGetServiceNumber; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>{EASecurityGroups.MainframeDeveloper}; }
        }

        public void ActivateAddIn()
        {
            var gns = new GetNewGenServiceNumber();
            gns.Show();
        }

        #endregion
    }
    [ComVisible(false)]
    public class COOLGenLoadStructureChartMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.COOLGenLoadStructureChart; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.MainframeDeveloper }; }
        }

        public void ActivateAddIn()
        {
            var wfs = new WrapperFileSelection();
            wfs.Show();
        }

        #endregion
    }
    [ComVisible(false)]
    public class COOLGenUpdateStereotypeMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.COOLGenUpdateStereotype; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.MainframeDeveloper }; }
        }

        public void ActivateAddIn()
        {
            if (MessageBox.Show("Are you sure?",
                    "GEN Stereotype", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EaCaliberGenEngine.UpdateGenStereotype(
                    AddInRepository.Instance.Repository.GetTreeSelectedPackage());
            }
        }


        #endregion
    }
    [ComVisible(false)]
    public class EAApplyTemplateMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAApplyTemplate; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>();}
        }

        public void ActivateAddIn()
        {
            var tempApply = new EaAccess();
            Element element;
            try
            {
                element = (Element)AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch
            {
                return;
            }

            if (element != null)
            {
                tempApply.applyTemplate(element);
            }
        }
        #endregion
    }

    [ComVisible(false)]
    public class EACreateNewProjectMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EACreateNewProject; }
        }

        public List<string> SecurityRoles
        {
            //get { return new List<string>(); }
            get { return new List<string> { EASecurityGroups.Administrators }; }
        }

        public void ActivateAddIn()
        {
            var createDesignPackages = new CreateDesignPackages();

            createDesignPackages.Show();

            //// Destination folder
            ////
            //Package destinationPackage =
            //     AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            //if (destinationPackage != null)
            //{

            //    var eat = new EATools();

            //    var response = MessageBox.Show("Are you sure?", "Create new project", MessageBoxButtons.YesNo);

            //    if (response == DialogResult.Yes)
            //    {
            //        Cursor.Current = Cursors.WaitCursor;
            //        eat.ApplyProjectTemplate2();
            //        Cursor.Current = Cursors.Arrow;

            //        MessageBox.Show("Project Created Successfully.");
            //    }
            //}
        }
        #endregion
    }

    [ComVisible(false)]
    public class EACreateProcessDocumentationTemplateMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EACreateProcessDocumentationTemplate; }
        }

        public List<string> SecurityRoles
        {
            //get { return new List<string>(); }
            get { return new List<string> { EASecurityGroups.Administrators }; }
        }

        public void ActivateAddIn()
        {
            var processDocumenation = new ProcessDocumentation();

            processDocumenation.Show();
        }
        #endregion
    }

    [ComVisible(false)]
    public class EALocateCompositeMenu : IEAMenu
    {
        //
        // Locate composite diagram or element
        //

        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EALocateComposite; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {

            //
            // Check if a diagram or element is selected
            //

            // Destination folder
            //
            object selectedObject = 
                AddInRepository.Instance.Repository.GetTreeSelectedObject();

            Diagram diagramSelected = null;
            Element elementSelected = null;
            string returnElementType = "";

            EaAccess eac = new EaAccess();
            string eaguid = "";

            try
            {
                elementSelected = (Element)selectedObject;
                returnElementType = "Diagram";
                eaguid = elementSelected.ElementGUID;
            }
            catch( Exception ex )
            {
                try
                {
                    diagramSelected = (Diagram)selectedObject;
                    returnElementType = "Element";
                    eaguid = diagramSelected.DiagramGUID;
                }
                catch (Exception ex2)
                {
                    return;
                }
            }

            var selED = eac.GetCompositeDiagram(eaguid, returnElementType);

            try
            {
                // Find element in project browser
                var objectToLocate = new object();
                objectToLocate = null;

                if (!string.IsNullOrEmpty(selED.EA_GUID))
                {
                    if (returnElementType == "Element")
                        objectToLocate = AddInRepository.Instance.Repository.GetElementByGuid(selED.EA_GUID);

                    if (returnElementType == "Diagram")
                        objectToLocate = AddInRepository.Instance.Repository.GetDiagramByGuid(selED.EA_GUID);
                }

                if (objectToLocate != null)
                {
                    AddInRepository.Instance.Repository.ShowInProjectView(objectToLocate);
                }
                else
                {
                    MessageBox.Show("Element not found in EA.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }
        #endregion
    }

    [ComVisible(false)]
    public class EABusinessRuleRealisationMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EABusinessRuleRealisation; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>();}
        }

        public void ActivateAddIn()
        {
            var brr = new BusinessRulesRealisation();
            brr.Show();
        }
        #endregion
    }
    [ComVisible(false)]
    public class EACleanClassMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EACleanClass; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            Element elementToClean;
            try
            {
                elementToClean = (Element)AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch
            {
                return;
            }

            if (elementToClean != null)
            {
                EaCaliberGenEngine.CleanClass(elementToClean);
            }
        }

        #endregion
    }
    [ComVisible(false)]
    public class EAExplorerMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAExplorer; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var detv = new EAExplorer();
            detv.Show();
        }

        #endregion
    }
    [ComVisible(false)]
    public class EAPlaceLinkedElementsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAPlaceLinkedElements; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var le = new LinkedElements();
            le.Show();
        }

        #endregion
    }
    [ComVisible(false)]
    public class EAPromoteElementMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAPromoteElement; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { }; }
        }

        public void ActivateAddIn()
        {
            var pe = new PromoteElement();
            pe.Show();
        }
        #endregion
    }
    [ComVisible(false)]
    public class EAReportsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAReports; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var ear = new EaReports();
            ear.Show();
        }
        #endregion
    }
    [ComVisible(false)]
    public class EAUpdateStereotypeMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAUpdateStereotype; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var sr = new StereotypeReplace();
            sr.Show();
        }

        #endregion
    }
    [ComVisible(false)]
    public class SQLCreateChangeScriptsMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.SQLCreateChangeScripts; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { "DBA" }; }
        }

        public void ActivateAddIn()
        {
            var sqlScripts = new SQLScriptCreator();
            sqlScripts.Show();
        }

        #endregion
    }
    [ComVisible(false)]
    public class StaticProfileBuilderMenu : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.StaticProfileBuilder; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> (); }
        }

        public void ActivateAddIn()
        {
            var staticProfileBuilderPresenter
                = new StaticProfileBuilderPresenter(
                    new StaticProfileBuilder(AddInRepository.Instance.Repository),
                    new StaticProfileBuilderForm());
        }

        #endregion
    }

    public class AuditLogMaintenance : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.AuditLogMaintenance; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string> { EASecurityGroups.Administrators }; }
        }

        public void ActivateAddIn()
        {
            var sqlScripts = new AuditHistory( );
            sqlScripts.Show();
        }

        #endregion
    }

    public class GetCurrentPath: IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.EAFindByPath; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {
            var uigetCurrentPath = new UIGetCurrentPath();
            uigetCurrentPath.Show();
        }

        #endregion
    }


    public class LocateClassifier : IEAMenu
    {
        #region IEAMenu Members

        public string Name
        {
            get { return AddInApplications.LocateClassifier; }
        }

        public List<string> SecurityRoles
        {
            get { return new List<string>(); }
        }

        public void ActivateAddIn()
        {

            var locateClassifier = new EaAccess();
            Element element;
            try
            {
                element = (Element)AddInRepository.Instance.Repository.GetTreeSelectedObject();
            }
            catch
            {
                return;
            }

            if (element != null)
            {
                int classifierID = locateClassifier.GetClassifier(element);

                try
                {
                    // Find element in project browser
                    var o = new object();

                    if (o != null)
                    {
                        o = AddInRepository.Instance.Repository.GetElementByID(classifierID);
                    }

                    if (o != null)
                    {
                        AddInRepository.Instance.Repository.ShowInProjectView(o);
                    }
                    else
                    {
                        MessageBox.Show("Element not found in EA.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion
    }

}
