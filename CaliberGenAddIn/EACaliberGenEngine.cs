using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using EA;
using EAStructures;
using Attribute=EA.Attribute;
using File=System.IO.File;

namespace EAAddIn
{
    public class EaCaliberGenEngine
    {
        #region classVariables

        public enum Derivedtype
        {
            BDO,
            Bdotype,
            Usecase,
            Actor,
            Businessrule,
            Package,
            Category,
            Ignore,
            Businessruleparent,
            Attribute,
            MESSAGE,
            UI,
            DESIGNRULE,
            ELEMENT,
            SCREEN,
            NONDEFINED
        } ;

        public const string Actor = "Actor";
        public const string BDO = "Business Object"; //15
        public const string Businessrule = "Business Rule";

        private const string COL_CALIBERHANDLE = "CaliberHandle";
        private const string COL_CALIBERPARENTID = "CaliberParentId";
        private const string COL_CALIBERREQUIREMENT = "CaliberRequirement";
        private const string COL_CALIBERSOURCEID = "SourceCaliberID";
        private const string COL_CALIBERSOURCEPROJECTNAME = "CaliberSourceProjectName";

        private const string COL_CALIBERSOURCEREQUIREMENTVERSION =
            "CaliberSourceRequirementVersion";

        private const string COL_CALIBERSTATUS = "CaliberStatus";

        private const string COL_CALIBERTYPE = "CaliberType";
        private const string COL_EAELEMENT_NAME = "EAElementName";
        private const string COL_EAELEMENTIF = "EAElementID";
        private const string COL_EAELEMENTTYPE = "EAElementType";
        private const string COL_EAGUID = "EA_GUID";
        private const string COL_EAGUPARENTID = "EAGUIDParentID";
        private const string COL_EASTATUS = "EaStatus";
        private const string COL_EASTEREOTYPE = "EAStereotype";
        private const string COL_LEVEL = "Level";
        private const string COL_LOADEDEA = "LoadedEA";
        private const string COL_MAPTABLEUNIQUEID = "MapTableUniqueID";
        private const string COL_PARENTID = "ParentID";
        private const string COL_REALCALIBERID = "RealCaliberID";
        private const string COL_SYNC_STATUS = "SyncStatus";
        private const string COL_UI_ELEM_PROP = "UIProperties";

        public const string constCaliberActor = "03. Actors (AC)";
        public const string constCaliberBDO = "06. Business Objects (BDO)";
        public const string constCaliberUIObject = "11.";
        public const string constCaliberUseCase = "04. Use Cases (UC)";

        public const string TYPE_ATTRIBUTE = "Attributes";

        public const string TYPE_BDO = "Business Object";
        public const string TYPE_BR = "BR-";
        public const string TYPE_BUSINESSDOMAINMODEL = "Business Domain Model";
        public const string TYPE_BUSINESSRULES = "Business Rules";
        public const string TYPE_CATEGORY = "[Category]";

        public const string TYPE_DR = "DR";
        public const string TYPE_ELEMENTS = "Elements"; //8
        public const string TYPE_MESSAGE = "Messages";
        public const string TYPE_UC = "UC-";

        public const string TYPE_UIBUTTON = "Button:"; //7
        public const string TYPE_UICHECKBOX = "CheckBox:"; //9
        public const string TYPE_UIGROUPBOX = "Group Box:"; //10
        public const string TYPE_UIMENUITEM = "Menu Item:"; //10
        public const string TYPE_UIMESSAGE = "Message";
        public const string TYPE_UIPACKAGE = "UI_";
        public const string TYPE_UISUBMENUITEM = "Menu SubItem:"; //13
        public const string TYPE_UITEXTBOX = "Field:"; //6
        public const string USECASE = "Use Case";
        public static SqlConnection EADBConnection;
        public static SqlConnection MappingTableConnection;
        private readonly SecurityInfo secInfo;
        private int CabsProcessedCount;
        public int countAlreadyLoadedCAB;
        public int countNewLoadedCAB;
        public MessagesDataSet dtMessageLog;
        public string EADestinationPackage;
        public DataTable elementSourceDataTable;

        public GenEAList newItem;

        #endregion classVariables

        //
        // EaCaliberGenEngine Constructor
        //
        public EaCaliberGenEngine(SecurityInfo sec,
                                  string loadType
            )
        {
            secInfo.EAGenCaliberSQL2005Repository = sec.EAGenCaliberSQL2005Repository;
            secInfo.EARepository = sec.EARepository;
            secInfo.packageDestination = sec.packageDestination;

            // Set the main repository
            //if (rc != null)
            //{
            //    // Using the current EA instance.
            //    m_Repository = rc;
            //}
            //else
            //{
            //    // Using the connection string
            //    if (m_Repository == null)
            //    {
            //        m_Repository = new EA.RepositoryClass();
            //        m_Repository.OpenFile(sec.EaRepository);
            //    }
            //}

            // If the repository is not opened, it should abend.
            if (AddInRepository.Instance.Repository == null)
            {
                // Error
                return;
            }

            //
            // Open Mapping table connection
            //
            if (sec.EAGenCaliberSQL2005Repository.Substring(0, 8) == "Provider")
            {
                //var MyConnection = new OleDbConnection(
                //    sec.EAGenCaliberSQL2005Repository);
            }
            else
            {
                MappingTableConnection = new SqlConnection(
                    secInfo.EAGenCaliberSQL2005Repository);
                MappingTableConnection.Open();
            }


            EADBConnection = new SqlConnection(secInfo.EARepository);
            EADBConnection.Open();

            // Common columns
            elementSourceDataTable = new DataTable("ElementSourceDataTable");

            var Level = new DataColumn("Level", typeof (String));
            var EA_GUID = new DataColumn("EA_GUID", typeof (String));
            var EAGUIDPARENT = new DataColumn("EAGUIDPARENT", typeof (String));
            var EAElementType = new DataColumn("EAElementType", typeof (String));
            var EAStereotype = new DataColumn("EAStereotype", typeof (String));
            var EAStatus = new DataColumn("EaStatus", typeof (String));
            var SyncStatus = new DataColumn("SyncStatus", typeof (String));
            var EAElementName = new DataColumn("EAElementName", typeof (String));
            var EAElementID = new DataColumn("EAElementID", typeof (String));

            if (loadType == "coolgen")
            {
                //
                // Columns for ElementSourceDataTable
                //

                var GENModule = new DataColumn("GENModule", typeof (String));
                var GENElementName =
                    new DataColumn("GENElementName", typeof (String));
                var GENElementType =
                    new DataColumn("GENElementType", typeof (String));

                elementSourceDataTable.Columns.Add(GENModule);
                elementSourceDataTable.Columns.Add(GENElementName);
                elementSourceDataTable.Columns.Add(GENElementType);
            }

            if (loadType == "caliber")
            {
                //
                // Columns for ElementSourceDataTable
                //
                var LoadedEA = new DataColumn("LoadedEA", typeof (Boolean));
                var CaliberProject = new DataColumn("CaliberProject", typeof (String));
                var CaliberType = new DataColumn("CaliberType", typeof (String));
                var DerivedType = new DataColumn("DerivedType", typeof (String));
                var CaliberRequirement = new DataColumn("CaliberRequirement", typeof (String));
                var CaliberID = new DataColumn("CaliberID", typeof (String));
                var RealCaliberID = new DataColumn("RealCaliberID", typeof (String));
                var ParentID = new DataColumn("ParentId", typeof (String));
                var CaliberIsMapped = new DataColumn("CaliberIsMapped", typeof (Boolean));
                var UIProperties = new DataColumn("UIProperties", typeof (SelectRequirements.ElementProperties));
                var MasterID = new DataColumn("MasterID", typeof (String));

                var CaliberHierarchy = new DataColumn("CaliberHierarchy", typeof (String));
                var CaliberStatus = new DataColumn("CaliberStatus", typeof (String));
                var CaliberFullDescription = new DataColumn("CaliberFullDescription", typeof (String));
                var CreatedBy = new DataColumn("CreatedBy", typeof (String));
                var SourceCaliberID = new DataColumn("SourceCaliberID", typeof (String));
                var MapTableUniqueID = new DataColumn("MapTableUniqueID", typeof (String));


                elementSourceDataTable.Columns.Add(COL_CALIBERPARENTID);
                elementSourceDataTable.Columns.Add(COL_CALIBERHANDLE);
                elementSourceDataTable.Columns.Add(COL_CALIBERSOURCEPROJECTNAME);
                elementSourceDataTable.Columns.Add(COL_CALIBERSOURCEREQUIREMENTVERSION);

                elementSourceDataTable.Columns.Add(LoadedEA);
                elementSourceDataTable.Columns.Add(CaliberHierarchy);
                elementSourceDataTable.Columns.Add(CaliberStatus);
                elementSourceDataTable.Columns.Add(UIProperties);
                elementSourceDataTable.Columns.Add(DerivedType);
                elementSourceDataTable.Columns.Add(CaliberIsMapped);
                elementSourceDataTable.Columns.Add(CaliberRequirement);
                elementSourceDataTable.Columns.Add(CaliberID);
                elementSourceDataTable.Columns.Add(RealCaliberID);
                elementSourceDataTable.Columns.Add(COL_PARENTID);
                elementSourceDataTable.Columns.Add(SourceCaliberID);
                elementSourceDataTable.Columns.Add(CaliberFullDescription);
                elementSourceDataTable.Columns.Add(CaliberProject);
                elementSourceDataTable.Columns.Add(CreatedBy);
                elementSourceDataTable.Columns.Add(CaliberType);
                elementSourceDataTable.Columns.Add(MapTableUniqueID);

                // Set the DeptNumber as the primary key
                var myPrimaryKeyColumns = new DataColumn[1];
                myPrimaryKeyColumns[0] = elementSourceDataTable.Columns["CaliberID"];
                elementSourceDataTable.PrimaryKey = myPrimaryKeyColumns;

                //// Define CaliberID as primary key
                //ElementSourceDataTable.PrimaryKey = 
                //    new DataColumn[] { ElementSourceDataTable.Columns["CaliberID"] };
            }

            elementSourceDataTable.Columns.Add(EA_GUID);
            elementSourceDataTable.Columns.Add(COL_EAGUPARENTID);
            elementSourceDataTable.Columns.Add(EAElementType);
            elementSourceDataTable.Columns.Add(EAElementID);
            elementSourceDataTable.Columns.Add(EAStereotype);
            elementSourceDataTable.Columns.Add(EAStatus);
            elementSourceDataTable.Columns.Add(EAElementName);
            elementSourceDataTable.Columns.Add(Level);
            elementSourceDataTable.Columns.Add(SyncStatus);


            // Columns for dtErrorLog
            //
            dtMessageLog = new MessagesDataSet();

            //var logDescription = new DataColumn("logDescription", typeof (String));
            //dtMessageLog.Columns.Add(logDescription);
        }

        public void CloseConnection()
        {
            EADBConnection.Close();
            MappingTableConnection.Close();
            AddInRepository.Instance.Repository.Exit();
        }

        public void ClearElementSourceDataTable()
        {
            elementSourceDataTable.Clear();
        }

        #region COOLGEN

        // ------------------------------------------------------------------
        //  Update CAB connections
        //  SYNC  
        // ------------------------------------------------------------------
        public static void updateCABConnections(List<callCompare> cabList)
        {
            AddInRepository.Instance.InitialiseGenResults();

            callCompare firstLine = cabList[0];
            Element EAfrom = null;
            Element EADestination= null;

            AddInRepository.Instance.WriteGenResults(
                    "Sync Started" + firstLine.GENcall.loadModuleName, 1000);

            Package ownerPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (ownerPackage == null)
            {
                // Error
                //
                AddInRepository.Instance.WriteGenResults(
                            "No package selected." , 1000);
                AddInRepository.Instance.WriteGenResults(
                            "Sync Completed." , 1000);

                return;
            }


            // -------------------------------------
            //  Create source element if necessary
            // -------------------------------------
            if (firstLine.decision == "CREATE MAIN ELEMENT")
            {
                //
                // Add new element
                //

                EAfrom =
                     (Element)ownerPackage.Elements.AddNew(
                              firstLine.GENcall.CabName, "Class");
                EAfrom.Update();

                EaAccess.addTaggedValue(EAfrom,
                             "Load Module", firstLine.GENcall.loadModuleName);

                AddInRepository.Instance.WriteGenResults(
                        "Main Element Added" + firstLine.GENcall.loadModuleName, 1000);

            }
            else
            {

                EAfrom =
                      AddInRepository.Instance.Repository.GetElementByID(firstLine.GENcall.elementID);
            }

            if (EAfrom == null)
            {
                return;
            }


            // -------------------------------------------------------
            // For each record, check if the connector has to be
            // added, delete or if the element has to be created
            // before the connector
            // -------------------------------------------------------
            foreach (callCompare cab in cabList)
            {
                if (cab.decision == "NEW ELEMENT CALLS")
                {
                    //
                    // Add new element
                    //

                    // Get Module
                    var gm = new mtCABType.genModule();
                    gm = mtCABType.getStereotypeForGen(MappingTableConnection,
                                   cab.GENcall.loadModuleName,
                                   cab.GENcall.loadModuleName);

                    EA.Element newEAElement =
                         (Element)ownerPackage.Elements.AddNew(
                         cab.GENcall.CabName, "Class");

                    newEAElement.Stereotype = gm.stereotype;
                    newEAElement.Update();
                    ownerPackage.Elements.Refresh();

                    EaAccess.addTaggedValue(newEAElement, 
                                 "Load Module", cab.GENcall.loadModuleName);

                    AddInRepository.Instance.WriteGenResults(
                            "New Element Added" + cab.GENcall.CabName, 1000);

                    //
                    // Add new connector
                    //
                    EA.Connector
                            con = (Connector)EAfrom.Connectors.AddNew("Uses", "Association");
                    con = (Connector)EAfrom.Connectors.AddNew("Uses", "Association");
                    con.SupplierID = newEAElement.ElementID;
                    con.Stereotype = "GEN Use";
                    con.Update();
                    EAfrom.Connectors.Refresh();

                    AddInRepository.Instance.WriteGenResults(
                         "New Connector Added Calls" + cab.GENcall.loadModuleName, 1000);

                }

                if (cab.decision == "NEW ELEMENT CALLEDBY")
                {
                    //
                    // Add new element
                    //

                    // Get Module
                    var gm = new mtCABType.genModule();
                    gm = mtCABType.getStereotypeForGen(MappingTableConnection,
                                   cab.GENcall.loadModuleName,
                                   cab.GENcall.loadModuleName);

                    EA.Element newEAElement =
                         (Element)ownerPackage.Elements.AddNew(
                         cab.GENcall.CabName, "Class");

                    newEAElement.Stereotype = gm.stereotype;
                    newEAElement.Update();
                    ownerPackage.Elements.Refresh();

                    EaAccess.addTaggedValue(newEAElement,
                                 "Load Module", cab.GENcall.loadModuleName);

                    AddInRepository.Instance.WriteGenResults(
                            "New Element Added" + cab.GENcall.loadModuleName, 1000);

                    //
                    // Add new connector
                    //
                    EA.Connector
                            con = (Connector)newEAElement.Connectors.AddNew("Uses", "Association");
                    
                    con.SupplierID =  EAfrom.ElementID;
                    con.Stereotype = "GEN Use";
                    con.Update();
                    EAfrom.Connectors.Refresh();

                    AddInRepository.Instance.WriteGenResults(
                         "New Connector Added Called By" + cab.GENcall.loadModuleName, 1000);

                }

                if (cab.decision == "NEW CONNECTOR CALLS")
                {
                    
                    EA.Connector
                            con = (Connector)EAfrom.Connectors.AddNew("Uses", "Association");
                    con.SupplierID = cab.GENcall.elementID;
                    con.Stereotype = "GEN Use";
                    con.Update();
                    EAfrom.Connectors.Refresh();

                    AddInRepository.Instance.WriteGenResults(
                         "New Connector Added" + cab.GENcall.loadModuleName, 1000);

                }

                if (cab.decision == "NEW CONNECTOR CALLEDBY")
                {

                    if (cab.GENcall.elementID > 0)
                    {
                        EADestination =
                           AddInRepository.Instance.Repository.GetElementByID(cab.GENcall.elementID);


                        EA.Connector
                                con = (Connector)EADestination.Connectors.AddNew("Uses", "Association");
                        con.SupplierID = EAfrom.ElementID;
                        con.Stereotype = "GEN Use";
                        con.Update();
                        EAfrom.Connectors.Refresh();

                        AddInRepository.Instance.WriteGenResults(
                             "New Connector Added" + cab.GENcall.loadModuleName, 1000);
                    }

                }

                if (cab.decision == "DELETE LINK")
                {
                    for (short s = 0; s < EAfrom.Connectors.Count; s++)
                    {
                        EA.Connector connector = (Connector)EAfrom.Connectors.GetAt(s);
                        if (connector.SupplierID == cab.EAcall.elementID)
                        {
                            EAfrom.Connectors.DeleteAt(s, false);
                            EAfrom.Connectors.Refresh();
                        }
                    }
                }
            }

            AddInRepository.Instance.WriteGenResults(
                "Sync Completed" + firstLine.GENcall.loadModuleName, 1000);

            AddInRepository.Instance.Repository.RefreshOpenDiagrams(true);

            return;

        }

        // ------------------------------------------------------------------
        //
        // Load XML with CAB connections
        //   
        // ------------------------------------------------------------------
        public static List<callCompare> LoadCABConnections(string fileName)
        {
            List<callCompare> cabList = new List<callCompare>();
            List<callCompare> cabFinalList = new List<callCompare>();
            List<callCompare> cabGENList = new List<callCompare>();
            List<callCompare> cabEAList = new List<callCompare>();

            var eac = new EaAccess();
            var MailnElemInEA = new EaAccess.sElementTag();

            var eaListCalls = new List<EaAccess.ConnectorList>();
            var eaListCalledBy = new List<EaAccess.ConnectorList>();
            var eaListTables = new List<EaAccess.ConnectorList>();

            // C:\GENEAFinalSync\test.xml

            // 1) Load GEN cab connections into:
            //    - Calls
            //    - CalledBy
            // 2) Load current EA cab connections
            //    - Calls
            //    - CalledBy
            // 3) Check if connection already exists
            //

            if (!File.Exists(fileName))
            {
                return null;
            }


            //
            // Get gen call members from xml
            //
            XmlTextReader reader = new XmlTextReader(fileName);

            string callType = "";
            while (reader.Read())
            {
                string readerName = reader.Name;
                string cabName = "";
                string loadModule = "";
                var elemInEA = new EaAccess.sElementTag();
                int elemInEAINT = 0;

                callProgram genCall = new callProgram();
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.

                        genCall.callType = "Element: " + reader.Name;
                        switch (readerName)
                        {
                            case "CrossReference":
                                callType = "MAINELEMENT";
                                break;
                            case "Calls":
                                callType = "Calls";
                                break;
                            case "ActionBlock":
                                break;
                            case "CalledBy":
                                callType = "CalledBy";
                                break;
                            case "Tables":
                                callType = "Tables";
                                break;
                            case "table":
                                break;
                        }


                        while (reader.MoveToNextAttribute()) // Read the attributes.
                        {
                            if (reader.Name == "name")
                            {
                                cabName = reader.Value;
                            }
                            if (reader.Name == "member")
                            {
                                loadModule = reader.Value;

                                elemInEA = eac.getElementByTaggedValue("Load Module", loadModule, null);
                                elemInEAINT = Convert.ToInt32(elemInEA.Object_ID);

                                if (callType == "MAINELEMENT")
                                    MailnElemInEA = eac.getElementByTaggedValue("Load Module", loadModule, null);
                            }

                            if (reader.Name == "logical")
                            {
                                loadModule = reader.Value;

                                elemInEA = eac.getElementByTaggedValue("GenLogicalName", loadModule, null);
                                elemInEAINT = Convert.ToInt32(elemInEA.Object_ID);
                            }

                            genCall.callType += ";" + reader.Name + ": " + reader.Value;
                        }

                        break;
                    case XmlNodeType.Text: //Display the text in each element.

                        genCall.callType = "Text: ";
                        genCall.callType = reader.Name + " " + reader.Value;

                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.

                        genCall.callType = "End: ";
                        genCall.callType = reader.Name + " " + reader.Value;
                        break;
                }

                //
                // Add element to list
                //
                if (!string.IsNullOrEmpty(loadModule))
                {

                    var mainElement = new callCompare();

                    mainElement.GENcall.callType = callType;
                    mainElement.GENcall.loadModuleName = loadModule;
                    mainElement.GENcall.CabName = cabName;
                    mainElement.GENcall.elementID = elemInEAINT;

                    if (elemInEAINT > 0)
                    {
                        if (callType == "MAINELEMENT")
                            mainElement.decision = "MAIN ELEMENT EXISTS";
                    }
                    else
                    {
                        if (callType == "MAINELEMENT")
                            mainElement.decision = "CREATE MAIN ELEMENT";

                    }
                    cabGENList.Add(mainElement);
                }
            }

            // -----------------------------------------------------
            // Get EA Related Elements
            // -----------------------------------------------------

            // If element exists in EA
            //
            int ElemID = Convert.ToInt32(MailnElemInEA.Object_ID);

            if (ElemID > 0)
            {
                // Calls
                eaListCalls =
                    eac.getLinkedElementsSQL(null,
                                             "GEN", null, true, false, null, null, null, ElemID);

                eaListCalledBy =
                    eac.getLinkedElementsSQL(null,
                                             "GEN", null, false, true, null, null, null, ElemID);

                eaListTables =
                    eac.getLinkedElementsSQL(null,
                                             "table", null, true, true, null, null, null, ElemID);
            }

            foreach (callCompare eaItem in cabGENList)
            {

                callCompare cc = new callCompare();
                cc.GENcall.callType = eaItem.GENcall.callType;
                cc.GENcall.elementID = eaItem.GENcall.elementID;
                cc.GENcall.loadModuleName = eaItem.GENcall.loadModuleName;
                cc.decision = eaItem.decision;

                //
                // Check against calls
                //
                foreach (EaAccess.ConnectorList genItem in eaListCalls)
                {
                    if (eaItem.GENcall.elementID == genItem.ObjectElementId &&
                        eaItem.GENcall.callType == "Calls")
                    {
                        cc.EAcall.elementID = genItem.ObjectElementId;
                        cc.EAcall.callType = "Calls";
                        cc.EAcall.loadModuleName = eac.getTaggedValue(
                                                       genItem.ObjectElementId,
                                                       "Load Module");
                    }
                }

                //
                // Check against called by
                //
                foreach (EaAccess.ConnectorList genItem in eaListCalledBy)
                {
                    if (eaItem.GENcall.elementID == genItem.ObjectElementId &&
                        eaItem.GENcall.callType == "CalledBy")
                    {
                        cc.EAcall.elementID = genItem.ObjectElementId;
                        cc.EAcall.callType = "CalledBy";
                        cc.EAcall.loadModuleName = eac.getTaggedValue(
                                                       genItem.ObjectElementId,
                                                       "Load Module");
                    }
                }

                //
                // Check against tables
                //
                foreach (EaAccess.ConnectorList genItem in eaListTables)
                {
                    if (eaItem.GENcall.elementID == genItem.ObjectElementId &&
                        eaItem.GENcall.callType == "Tables")
                    {
                        cc.EAcall.elementID = genItem.ObjectElementId;
                        cc.EAcall.callType = "Tables";
                        cc.EAcall.loadModuleName = eac.getTaggedValue(
                                                       genItem.ObjectElementId,
                                                       "GENLOGICALNAME");
                    }
                }

                cabList.Add(cc);

            }

            //
            // Check EA links to remove no longer needed ones
            //

            //
            // Check against calls
            //
            foreach (EaAccess.ConnectorList genItem in eaListCalls)
            {
                bool found = false;
                foreach (callCompare eaItem in cabGENList)
                {
                    if (eaItem.GENcall.callType == "Calls" &&
                        eaItem.GENcall.elementID == genItem.ObjectElementId)
                    {
                        found = true;
                    }
                }

                if (! found)
                {
                    // delete

                    callCompare cc = new callCompare();
                    cc.EAcall.callType = "Calls";
                    cc.EAcall.elementID = genItem.ObjectElementId;
                    cc.EAcall.loadModuleName = eac.getTaggedValue(
                                                   genItem.ObjectElementId,
                                                   "Load Module");
                    cc.EAcall.eaConnectorID = genItem.ConnectorId;
                    cc.decision = "DELETE LINK";
                    cabList.Add(cc);
                }
            }


            //
            // Check against CALLED BY
            //
            foreach (EaAccess.ConnectorList genItem in eaListCalledBy)
            {
                bool found = false;
                foreach (callCompare eaItem in cabGENList)
                {
                    if (eaItem.GENcall.callType == "CalledBy" &&
                        eaItem.GENcall.elementID == genItem.ObjectElementId)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    // delete

                    callCompare cc = new callCompare();
                    cc.EAcall.callType = "CalledBy";
                    cc.EAcall.elementID = genItem.ObjectElementId;
                    cc.EAcall.loadModuleName = eac.getTaggedValue(
                                                   genItem.ObjectElementId,
                                                   "Load Module");
                    cc.decision = "DELETE LINK";
                    cabList.Add(cc);
                }
            }

            //
            // Check against TABLES
            //
            foreach (EaAccess.ConnectorList genItem in eaListTables)
            {
                bool found = false;
                foreach (callCompare eaItem in cabGENList)
                {
                    if (eaItem.GENcall.callType == "Tables" &&
                        eaItem.GENcall.elementID == genItem.ObjectElementId)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    // delete

                    callCompare cc = new callCompare();
                    cc.EAcall.callType = "Tables";
                    cc.EAcall.elementID = genItem.ObjectElementId;
                    cc.EAcall.loadModuleName = eac.getTaggedValue(
                                                   genItem.ObjectElementId,
                                                   "GENLOGICALNAME");
                    cc.decision = "DELETE LINK";
                    cabList.Add(cc);
                }
            }



            foreach (callCompare genItem in cabList)
            {
                callCompare finalItem = new callCompare();
                finalItem.EAcall.callType = genItem.EAcall.callType;
                finalItem.EAcall.elementID = genItem.EAcall.elementID;
                finalItem.EAcall.loadModuleName = genItem.EAcall.loadModuleName;

                finalItem.GENcall.callType = genItem.GENcall.callType;
                finalItem.GENcall.elementID = genItem.GENcall.elementID;
                finalItem.GENcall.loadModuleName = genItem.GENcall.loadModuleName;

                finalItem.decision = genItem.decision;

                if (finalItem.decision == "" || finalItem.decision == null)
                {
                    if ((finalItem.GENcall.elementID == 0)
                      && finalItem.GENcall.loadModuleName != "")
                    {
                     if (finalItem.GENcall.callType == "Calls")
                        finalItem.decision = "NEW ELEMENT CALLS";
                     if (finalItem.GENcall.callType == "CalledBy")
                         finalItem.decision = "NEW ELEMENT CALLEDBY";
                    }

                    if (finalItem.GENcall.elementID > 0 &&
                        finalItem.EAcall.elementID == 0)
                    {
                        if (finalItem.GENcall.callType == "Calls")
                            finalItem.decision = "NEW CONNECTOR CALLS";
                        if (finalItem.GENcall.callType == "CalledBy")
                            finalItem.decision = "NEW CONNECTOR CALLEDBY";
                    }
                }

                cabFinalList.Add(finalItem);

            }


            return cabFinalList;
        }

        public struct callProgram
        {
            public string callType;
            public string loadModuleName;
            public string CabName;
            public int elementID;
            public int eaConnectorID;
        }

        public struct callCompare
        {
            public callProgram EAcall;
            public callProgram GENcall;
            public string decision;
        }


        // ------------------------------------------------------------------
        // (MEMORY) Load Wrapper 
        //   It opens the XML for the wrapper and loads into a data table
        // ------------------------------------------------------------------
        public DataTable LoadWrapper(string wrapperName, TreeView tv)
        {
            if (! File.Exists(wrapperName))
            {
                return null;
            }

            Package myPackage;

            var doc = new XmlDocument();
            doc.Load(wrapperName);

            myPackage = AddInRepository.Instance.Repository.GetPackageByGuid("{56C67C96-ED26-4eb0-A252-56D8FA8B5B45}");

            XmlNode chart = doc.SelectSingleNode("chart");

            if (AddCabToList(myPackage, chart, tv.TopNode))
            {
                DrillDownWrapper(myPackage, chart, tv.TopNode);
            }

            return elementSourceDataTable;
        }

        // ------------------------------------------------------------------
        // (MEMORY) addCABtoList 
        //    Add program to the data table
        // ------------------------------------------------------------------
        public bool AddCabToList(Package parentPackage, XmlNode parent, TreeNode node)
        {
            var cabInfo = new mtCABMapping();
            string module;
            string name;
            int img;
            DataRow cabRow = elementSourceDataTable.NewRow();

            var element = parent as XmlElement;

            name = element.GetAttribute("name");
            module = element.GetAttribute("module");

            if (module == "" || module == null)
            {
                MessageBox.Show("Please generate the XML again.");
                return false;
            }
            // Get Module name 
            //
            var gm = new mtCABType.genModule();
            gm = mtCABType.getStereotypeForGen(MappingTableConnection, module, name);

            module = gm.module;
            name = gm.name;

            cabRow["EA_GUID"] = "0";
            cabRow["GENModule"] = module;
            cabRow["GENElementName"] = name;

            // Add info to datatable
            elementSourceDataTable.Rows.Add(cabRow);

            // Get info from mapping table 
            cabInfo.CAB = module;
            // 29/01/2009 - Stop using mapping table
            // cabInfo.getGuidByCABName(MappingTableConnection);
            cabInfo.getGuidByCABName_EA();
            cabInfo.CABName = name;

            // Retrieve EA Element by SQL and not API.
            //
            EaAccess.sEAElement eaCABx;
            var eac = new EaAccess();
            eaCABx = eac.RetrieveEaElement(EADBConnection, cabInfo.EA_GUID);

            img = 4; // Question mark
            if (eaCABx.EA_GUID == null)
            {
                cabInfo.EA_GUID = "EA guid Not found.";
                cabRow["EA_GUID"] = "EA guid Not found.";
                img = 2;
            }
            else
            {
                cabInfo.EA_GUID = eaCABx.EA_GUID;
                cabInfo.EAElementType = eaCABx.Type;
                cabInfo.EAStereotype = eaCABx.Stereotype;
                cabInfo.EAStatus = eaCABx.Status;
                cabInfo.EAElementName = eaCABx.Name;
                cabInfo.EAElementID = eaCABx.ElementID;

                cabRow["EA_GUID"] = cabInfo.EA_GUID;
                cabRow["EAElementType"] = cabInfo.EAElementType;
                cabRow["EAStereotype"] = cabInfo.EAStereotype;
                cabRow["EaStatus"] = cabInfo.EAStatus;
                cabRow["EAElementName"] = cabInfo.EAElementName;
                cabRow["EAElementID"] = cabInfo.EAElementID;


                switch (cabInfo.EAStatus)
                {
                    case "Proposed":
                        img = 3;
                        break;
                    case "Implemented":
                        img = 1;
                        break;
                    case "SDApproved":
                        img = 7;
                        break;
                    case "UnderDevelopment":
                        img = 5;
                        break;
                    case "To Be Reviewed":
                        img = 8;
                        break;
                    case "Changes Required":
                        img = 6;
                        break;
                }
            }

            // add to tree view
            var tn = new TreeNode(module, img, 0);
            tn.Tag = cabInfo;

            node.Nodes.Add(tn);

            return true;
        }

        // ------------------------------------------------------------------
        // (MEMORY) addTABLEtoList 
        //    Add program to the data table
        // ------------------------------------------------------------------
        public void AddTableToList(Package parentPackage, XmlNode parent, TreeNode node)
        {
            string actions;
            string name;
            int img;

            var element = parent as XmlElement;

            // name = element.GetAttribute("name");
            // element.ChildNodes[0].Attributes[0].Value
            // name = element.ChildNodes[0].Attributes[0].Value;

            //  for each ... element.ChildNodes[2].Attributes[0].Value
            //

            foreach (XmlNode tbl in element.ChildNodes)
            {
                var tableInfo = new mtCABMapping();

                // actions = element.GetAttribute("actions");
                name = tbl.Attributes["name"].Value;
                actions = tbl.Attributes["actions"].Value;

                string linkName = GetNameOfTableConnection(actions);

                tableInfo.CABName = name;
                tableInfo.EAElementType = "Table";
                tableInfo.EAElementName = name;
                tableInfo.EAStatus = linkName;

                img = 9; // table

                // add to tree view
                //

                var tn = new TreeNode(name, img, 0);
                tn.Tag = tableInfo;

                node.Nodes.Add(tn);
            }
        }

        //
        // Determine the name of the CAB connection with a table
        //
        private string GetNameOfTableConnection(string actions)
        {
            string linkName = "";

            // Determine name of the link
            // 
            if (actions.Contains("R"))
            {
                linkName = "-Read";
            }
            if (actions.Contains("D"))
            {
                linkName = linkName.Trim() + "-Delete";
            }
            if (actions.Contains("U"))
            {
                linkName = linkName.Trim() + "-Update";
            }
            if (actions.Contains("C"))
            {
                linkName = linkName.Trim() + "-Create";
            }
            if (linkName == "")
            {
                linkName = "unespecified";
            }

            return linkName;
        }


        // ------------------------------------------------------------------
        // (MEMORY) drillDrownWrapper 
        //    It does drill down the wrapper, adding cabs to dataTable
        //    The cab is not added to EA with this program
        // ------------------------------------------------------------------
        private void DrillDownWrapper(Package package, XmlNode parent, TreeNode node)
        {
            XmlNodeList programs = parent.SelectNodes("program");
            XmlNodeList tables = parent.SelectNodes("tables");

            node = node.LastNode;

            // add tables
            foreach (XmlNode table in tables)
            {
                AddTableToList(package, table, node);
            }

            foreach (XmlNode program in programs)
            {
                AddCabToList(package, program, node);
                DrillDownWrapper(package, program, node);
            }
        }

        // ------------------------------------------------------------------
        // (EA READ) Load CAB EA guids from Mapping
        // ------------------------------------------------------------------
        public void MappingGenloadEaGuid()
        {
            foreach (DataRow dataRow in elementSourceDataTable.Rows)
            {
                // Retrieve EA GUID from mapping table
                var cabMapping = new mtCABMapping();
                cabMapping.CAB = dataRow["GENModule"].ToString();

                // cabMapping.getGuidByCABName(MappingTableConnection);
                cabMapping.getGuidByCABName_EA();

                // Retrieve EA info by guid
                Element eaCAB;
                eaCAB = RetrieveEaElementByGuid(cabMapping.EA_GUID);

                if (eaCAB == null)
                {
                    dataRow["SyncStatus"] = "EA guid Not found.";
                }
                else
                {
                    dataRow["EA_GUID"] = cabMapping.EA_GUID;
                    // dataRow["MapTableUniqueID"] = cabMapping.UniqueID;

                    dataRow["EAElementType"] = eaCAB.Type;
                    dataRow["EAStereotype"] = eaCAB.Stereotype;
                    dataRow["EaStatus"] = eaCAB.Status;
                    dataRow["EAElementName"] = eaCAB.Name;
                    dataRow["EAElementID"] = eaCAB.ElementID;
                }

                // check if element exists in EA 
                // if it is missing, we have to delete the element
            }
        }

        // ----------------------------------------------
        // (EA UPDATE) - Sync COOLGEN with EA
        // ----------------------------------------------
        public void EaCoolGenSync(string file, ProgressBar progbar)
        {
            // Clear Error log
            dtMessageLog.Messages.Clear();

            Package myPackage;
            countNewLoadedCAB = 0;
            countAlreadyLoadedCAB = 0;


            var doc = new XmlDocument();
            doc.Load(file);

            myPackage = AddInRepository.Instance.Repository.GetPackageByGuid(EADestinationPackage);
            if (myPackage != null)
            {
                CabsProcessedCount = 0;

                XmlNode chart = doc.SelectSingleNode("chart");

                EaAddProgram(myPackage, chart, progbar);
                EaTraverseProgram(myPackage, chart, progbar);
            }
            return;
        }

        // ----------------------------------------------
        // (EA UPDATE) - Add element to EA
        // ----------------------------------------------
        private void EaAddProgram(Package parentPackage, XmlNode parent,
                                  ProgressBar progbar)
        {
            var module = string.Empty;
            var name = string.Empty;
            //string packageName;
            string stereotype;

            string cabRootPackageGUID = null;

            if (AddInRepository.Instance.ReadOnly)
            {
                cabRootPackageGUID = parentPackage.PackageGUID;
            }
            else
            {
                cabRootPackageGUID = "{5473478E-10DA-4ab8-AB5D-95CADA211037}";
            }

            try
            {
                Package package;

                var element = parent as XmlElement;

                name = element.GetAttribute("name");
                module = element.GetAttribute("module");

                // The programs will be loaded under one package
                package = AddInRepository.Instance.Repository.GetPackageByGuid(cabRootPackageGUID);

                if (package == null)
                {
                    AddInRepository.Instance.WriteGenResults(
                        "Package GUID not found " + cabRootPackageGUID, 1000);

                    return;
                }

                // Instantiate the cab mapping class 
                var cabInfo = new mtCABMapping();

                // Get Module
                //
                var gm = new mtCABType.genModule();
                gm = mtCABType.getStereotypeForGen(MappingTableConnection, module, name);

                module = gm.module;
                stereotype = gm.stereotype;
                name = gm.name;

                if (module == "")
                {
                    AddInRepository.Instance.WriteGenResults("Module name invalid: " + module, 1000);
                    return;
                }

                // CAB name
                Element EAElement = null;
                cabInfo.CAB = module;
                cabInfo.EA_GUID = "";
                cabInfo.CABName = name;

                // bool MTfound = cabInfo.getGuidByCABName(MappingTableConnection);
                cabInfo.getGuidByCABName_EA();
                EAElement = AddInRepository.Instance.Repository.GetElementByGuid(cabInfo.EA_GUID);

                // If not found.
                if (cabInfo.EA_GUID == null)
                {
                    var newClass =
                        (Element) package.Elements.AddNew(
                                      element.GetAttribute("name"), "Class");

                    newClass.Stereotype = stereotype.Trim();
                    newClass.Status = "Implemented";
                    newClass.Update();
                    package.Update();
                    package.Elements.Refresh();

                    // Add tagged value - which is the link to GEN from 29/01/2009
                    //
                    EaAccess.addTaggedValue(newClass, "Load Module", cabInfo.CAB);
                    EaAccess.addTaggedValue(newClass, "LoadedOn", DateTime.Today.ToString());

                    cabInfo.EA_GUID = newClass.ElementGUID;

                    AddInRepository.Instance.WriteGenResults(cabInfo.CAB + " = New cab created", 1000);

                    countNewLoadedCAB++;
                }
                else
                {
                    dtMessageLog.AddMessage(cabInfo.CAB + " = already loaded");

                    countAlreadyLoadedCAB++;

                    // Update status to IMPLEMENTED

                    EAElement.Name = name;
                    EAElement.Status = "Implemented";
                    //EAElement.StereotypeEx = "";
                    //EAElement.Stereotype = stereotype;
                    EAElement.Update();
                    EAElement.Refresh();

                    // Add tagged value if necessary
                    //
                    EaAccess.addTaggedValue(EAElement, "Load Module", cabInfo.CAB);
                    EaAccess.addTaggedValue(EAElement, "LastUpdatedOn", DateTime.Today.ToString());
                }


                // Update progress bar...
                CabsProcessedCount++;
                progbar.Value = CabsProcessedCount;
            }
            catch(System.Runtime.InteropServices.COMException ex)
            {
                var message = "Error adding or updating object '" + name + "' (" + ex.Message + ")";

                dtMessageLog.AddMessage("Error", message);

                if (ex.Message == "Element locked")
                {
                    message += Environment.NewLine +
                               Environment.NewLine +
                               "Please check you have security to modify Gen artefacts and try again.";
                }
                MessageBox.Show(message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        // ----------------------------------------------
        // (EA UPDATE) Walk through wrapper tree and add to EA
        // ----------------------------------------------
        private void EaTraverseProgram(Package package, XmlNode parent,
                                       ProgressBar progbar)
        {
            XmlNodeList programs = parent.SelectNodes("program");
            XmlNodeList tables = parent.SelectNodes("tables/table");
            foreach (XmlNode table in tables)
            {
                EaAddTableConnector(parent, table);
            }

            foreach (XmlNode program in programs)
            {
                EaAddProgram(package, program, progbar);
                EaAddProgramConnector(parent, program);
                EaTraverseProgram(package, program, progbar);
            }
        }

        // ----------------------------------------------
        // Add CAB connection with table (EA)
        // ----------------------------------------------
        private void EaAddTableConnector(XmlNode parent, XmlNode table)
        {
            string fromGuid = null, toGuid = null;
            bool found = false;
            //GenEAList newItem = new GenEAList();

            var sqlCommand1 = new SqlCommand();
            var sqlCommand2 = new SqlCommand();

            sqlCommand1 = MappingTableConnection.CreateCommand();
            sqlCommand2 = MappingTableConnection.CreateCommand();

            var fromElement = parent as XmlElement;
            var toElement = table as XmlElement;

            string module = fromElement.GetAttribute("module");
            string name = fromElement.GetAttribute("name");

            string actions = toElement.GetAttribute("actions");
            string linkName = GetNameOfTableConnection(actions);

            // Get Module name - the top level doesn't have a module (Batch, Wrapper, Service)
            //
            var gm = new mtCABType.genModule();
            gm = mtCABType.getStereotypeForGen(MappingTableConnection, module, name);
            module = gm.module;

            // Retrieve calling CAB information
            //
            var cabFROM = new mtCABMapping();
            cabFROM.CAB = module;
            cabFROM.getGuidByCABName_EA();
            fromGuid = cabFROM.EA_GUID;

            Element EAfrom = AddInRepository.Instance.Repository.GetElementByGuid(fromGuid);
            if (EAfrom == null)
            {
                dtMessageLog.AddErrorMessage(module +
                            " Cab accessing Table not found " + fromGuid);
                return;
            }

            //
            // Find Table
            //
            string tableName = toElement.GetAttribute("name");

            var tableMap = new mtTableMapping {alternateName = tableName};
            
            Element EAtable = tableMap.getTableByTaggedValue();
            if (EAtable == null)
            {
                EAtable = tableMap.getTableByName();
            }
            if (EAtable == null)
            {
                dtMessageLog.AddErrorMessage("Table " + tableName + " not found <<<<<<< Please report to EA Administrators");
                return;
            }

            toGuid = EAtable.ElementGUID;

            Element EAto = AddInRepository.Instance.Repository.GetElementByGuid(toGuid);
            if (EAto == null)
            {
                dtMessageLog.AddErrorMessage(toElement.GetAttribute("name") +
                            " To Element not found " + toGuid);
                return;
            }

            // Add a connector and set values
            Connector con;
            for (short i = 0; i < EAfrom.Connectors.Count; i++)
            {
                con = (Connector) EAfrom.Connectors.GetAt(i);
                if (con.SupplierID == EAto.ElementID)
                {
                    found = true;
                    con.Name = linkName;
                    con.Update();
                    break;
                }
            }
            if (!found)
            {
                try
                {
                    con = (Connector) EAfrom.Connectors.AddNew(linkName, "Association");
                    con.SupplierID = EAto.ElementID;
                    con.Update();
                    EAfrom.Connectors.Refresh();
                }
                catch (Exception e)
                {
                    dtMessageLog.AddErrorMessage(e.ToString());
                }
            }
        }

        // ----------------------------------------------
        // Add CAB program connector 
        // ----------------------------------------------
        private void EaAddProgramConnector(XmlNode from, XmlNode to)
        {
            var fromElement = from as XmlElement;
            var toElement = to as XmlElement;
            string fromGuid = null, toGuid = null;
            bool found = false;
            string cabName = fromElement.GetAttribute("name");

            //
            //  FROM element
            // 
            string fromCab = fromElement.GetAttribute("module");
            string fromCabName = fromElement.GetAttribute("name");

            // Get Module name - the top level doesn't have a 
            // module YET (Batch, Wrapper, Service)
            //
            var gm = new mtCABType.genModule();
            gm = mtCABType.getStereotypeForGen(MappingTableConnection, fromCab, fromCabName);

            fromCab = gm.module;

            if (fromCab == "")
            {
                fromCab = "ERROR";
            }

            var cabFROM = new mtCABMapping();
            cabFROM.CAB = fromCab;
            cabFROM.getGuidByCABName_EA();
            fromGuid = cabFROM.EA_GUID;

            //
            //  TO element
            // 
            string toCab = toElement.GetAttribute("module");
            string toCabName = fromElement.GetAttribute("name");

            // Get Module name - the top level doesn't have a 
            // module YET (Batch, Wrapper, Service)
            //
            var gmTO = new mtCABType.genModule();
            gmTO = mtCABType.getStereotypeForGen(MappingTableConnection,
                                                 toCab, toCabName);

            toCab = gmTO.module;

            if (toCab == "")
            {
                toCab = "ERROR";
            }

            // 29/01/2009 - Stop using mapping table
            var cabTO = new mtCABMapping();
            cabTO.CAB = toCab;
            cabTO.getGuidByCABName_EA();
            toGuid = cabTO.EA_GUID;

            Element EAfrom = AddInRepository.Instance.Repository.GetElementByGuid(fromGuid);

            if (EAfrom == null)
            {
                dtMessageLog.AddErrorMessage(fromCab + " From Element not found " + fromGuid);
                return;
            }

            Element EAto = AddInRepository.Instance.Repository.GetElementByGuid(toGuid);

            if (EAto == null)
            {
                dtMessageLog.AddErrorMessage(toCab + " To Element not found " + toGuid);
                return;
            }

            // Add/ Update a connector and set values
            // To be done: Compare with current calls, delete or create additional

            Connector con;
            for (short i = 0; i < EAfrom.Connectors.Count; i++)
            {
                con = (Connector) EAfrom.Connectors.GetAt(i);
                if (con.SupplierID == EAto.ElementID)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                con = (Connector) EAfrom.Connectors.AddNew("", "Association");
                con.SupplierID = EAto.ElementID;
                con.Stereotype = "GEN Use";
                con.Update();
                EAfrom.Connectors.Refresh();
            }
        }

        #endregion COOLGEN

        #region CALIBER

        // ----------------------------------------------
        // Load Caliber elements guids from EA.
        // ----------------------------------------------
        public void MappingCaliberloadEaGuid()
        {
            foreach (DataRow dataRow in elementSourceDataTable.Rows)
            {
                EaAccess.sEAElement eaElementViaSQL;

                var eaInfo = new mtCaliberMapping();

                // Retrieve EA info from mapping table
                //eaInfo.getDetails(Convert.ToInt32(dataRow["CaliberID"]), MappingTableConnection);
                eaInfo.getDetails(Convert.ToInt32(dataRow["CaliberID"]));

                dataRow["SyncStatus"] = "";

                if (eaInfo == null)
                {
                    dataRow["SyncStatus"] = "Caliber Mapping Record not found.";
                }
                else
                {
                    // Alternative way of getting EA Element.
                    // 
                    var eac = new EaAccess();
                    eaElementViaSQL = eac.GetEaElement(EADBConnection, eaInfo.EA_GUID);

                    if (eaElementViaSQL.EA_GUID == null)
                    {
                        dataRow["SyncStatus"] = "EA guid Not found.";
                    }
                    else
                    {
                        dataRow["EA_GUID"] = eaInfo.EA_GUID;
                        dataRow["MapTableUniqueID"] = eaInfo.UniqueID;

                        dataRow["EAElementType"] = eaElementViaSQL.Type;
                        dataRow["EaStatus"] = eaElementViaSQL.Status;
                        dataRow["EAElementName"] = eaElementViaSQL.Name;
                        dataRow["EAElementID"] = eaElementViaSQL.ElementID;
                    }
                }
            }
        }


        // ----------------------------------------------
        // Check config file
        // ----------------------------------------------
        public static bool CheckConfigInfo()
        {
            string envVar = "";
            bool ret = false;

            foreach (DictionaryEntry var in Environment.GetEnvironmentVariables())
            {
                if (var.Key.ToString() == "EAGENTOOL")
                {
                    envVar = var.Value.ToString();
                    break;
                }
            }

            if (envVar == "Y")
            {
                ret = true;
            }
            return ret;
        }


        // --------------------------------------------------------------------
        // CALIBER = Walk through the data table and determine the element type
        // --------------------------------------------------------------------
        public string DetermineElementTypeForList()
        {
            /* Determination of the CALIBER element type in EA.
             *(1) 1           Business Domain Model   [Category] - Not created in EA
             *(2) 1.1         Business Object		   [????????] - ( type=Class, stereotype="Business Object, status="")
             *(3) 1.1.1       Attributes			   [Category] - it means the attributes will start coming soon
             *(4) 1.1.1.1     Name  (attribute)       [????????] - if it doesn't say "Category" it is an attribute for BDO 1.1
             *(4) 1.1.1.1     Name  (stereotype)      [Category] - if it says category, uses as stereotype for next attributes of BDO 1.1
             *(5) 1.1.1.1.1   recursively checks for categories/ attributes
             * 
             * 
             * After an Attribute Category
             *     - it will be an attribute or
             *     - it will be a stereotype of the next attributes
             *     - the owner is the previous BDO.
             * 
             * 
             * (3)1.1.1       Business Rules		   [Category] - it means the BRs will follow
             * (4)1.1.1.1     Name  (BR-%)            [????????] - if it is not Category, just a BR.
             * (4)1.1.1.1     Name  (requirement)     [Category] - if it says category, create as BR and uses it for next BRs 
             *                                                  under the next level.
             * (5) 1.1.1.1.1   recursively checks for br's and br's under br's
            */

            // Determine load type by first element
            // 
            string loadType = "";
            int level = 0;
            int previousLevel = 0;
            string levelstr = "";

            string currentBDO = "";
            string currentStereotype = "";
            string sector = "";
            string previousCaliberType = "";

            foreach (DataRow dataRow in elementSourceDataTable.Rows)
            {
                //if (dataRow["CaliberRequirement"].ToString() == "Attributes" ||
                //    dataRow["CaliberRequirement"].ToString() == "Business Rules" ||
                //    dataRow["CaliberRequirement"].ToString() == "Messages")
                //{
                //    dataRow["CaliberStatus"] = "[Category]";
                //}

                // Determine Level
                levelstr = dataRow["Level"].ToString();
                level = Convert.ToInt32(levelstr);

                // Determine load type
                if (loadType == "")
                {
                    if (dataRow["CaliberType"].ToString() == constCaliberUseCase)
                    {
                        loadType = constCaliberUseCase;
                    }
                    if (dataRow["CaliberType"].ToString() == constCaliberBDO)
                    {
                        loadType = constCaliberBDO;
                    }
                    if (dataRow["CaliberType"].ToString() == constCaliberActor)
                    {
                        loadType = constCaliberActor;
                    }
                }

                // Actor
                //

                #region Actor

                if (loadType == constCaliberActor)
                {
                    dataRow["DerivedType"] = Derivedtype.Actor;
                }

                #endregion Actor

                // Use Case derivation
                //

                #region UseCase

                if (loadType == constCaliberUseCase)
                {
                    if (dataRow["CaliberRequirement"].ToString().Substring(0, 3) == "UC-")
                    {
                        dataRow["DerivedType"] = Derivedtype.Usecase;
                    }
                    else
                    {
                        dataRow["DerivedType"] = Derivedtype.Package; //"Package";
                    }
                }

                #endregion Use Case

                // BDO & it's elements derivation
                //

                #region BDO

                if (loadType == constCaliberBDO)
                {
                    if (dataRow["CaliberRequirement"].ToString().StartsWith("Business Object "))
                    {
                        // BDO
                        sector = "BDO";
                        dataRow["DerivedType"] = Derivedtype.BDO;
                        dataRow["EAStereotype"] = "Business Object";
                        currentBDO = dataRow["CaliberRequirement"].ToString();
                    }
                    else
                    {
                        if (dataRow["CaliberRequirement"].ToString().StartsWith("BR-"))
                        {
                            // Business Rule
                            dataRow["DerivedType"] = Derivedtype.Businessrule;
                            dataRow["EAStereotype"] = currentStereotype;
                        }
                        else
                        {
                            if (level == 1)
                            {
                                dataRow["DerivedType"] = Derivedtype.Package;
                            }
                            else
                            {
                                // Every category is here
                                // Status is Category for BDO
                                if (dataRow["CaliberStatus"].ToString() == "[Category]")
                                {
                                    string dataIdentified = "N";

                                    if (dataRow["CaliberRequirement"].ToString() == "Attributes")
                                    {
                                        dataIdentified = "Y";
                                        // The attributes section is starting now.
                                        // It means the previous one is a BDO.
                                        dataRow["DerivedType"] = Derivedtype.Category; //"category";
                                        sector = "Attributes";
                                        currentStereotype = "";
                                    }
                                    if (dataRow["CaliberRequirement"].ToString() == "Business Rules")
                                    {
                                        dataIdentified = "Y";
                                        // The Business Rules section is starting now.
                                        currentStereotype = "";
                                        dataRow["DerivedType"] = Derivedtype.Category; // "category";
                                        sector = "Business Rules";
                                    }
                                    if (dataRow["CaliberRequirement"].ToString() == "Messages")
                                    {
                                        dataIdentified = "Y";
                                        // The Messages section is starting now.
                                        currentStereotype = "";
                                        dataRow["DerivedType"] = Derivedtype.Ignore;
                                        sector = "Messages";
                                    }
                                    if (sector == "Attributes" &&
                                        dataRow["CaliberRequirement"].ToString() != "Attributes")
                                    {
                                        dataIdentified = "Y";
                                        // Group of attributes, tag as stereotype
                                        dataRow["DerivedType"] = Derivedtype.Category;
                                        currentStereotype = dataRow["CaliberRequirement"].ToString();
                                    }
                                    if (sector == "Business Rules" &&
                                        dataRow["CaliberRequirement"].ToString() != "Business Rules")
                                    {
                                        dataIdentified = "Y";
                                        // Group of business rule, it will be business rule with rules under it
                                        dataRow["DerivedType"] = Derivedtype.Businessruleparent;
                                        dataRow["EAStereotype"] = currentStereotype;
                                    }

                                    if (sector == "Messages" &&
                                        dataRow["CaliberRequirement"].ToString() != "Messages")
                                    {
                                        dataIdentified = "Y";
                                        if (level < previousLevel)
                                        {
                                            // it may be a BDO package
                                            sector = "BDO";
                                            dataRow["DerivedType"] = Derivedtype.Package;
                                            dataRow["EAStereotype"] = "";
                                            // currentBDO = dataRow["CaliberRequirement"].ToString();
                                            currentBDO = "";
                                        }
                                        else
                                        {
                                            dataRow["DerivedType"] = Derivedtype.Ignore;
                                        }
                                    }
                                    if (dataIdentified == "N")
                                    {
                                        dataRow["DerivedType"] = Derivedtype.Package;
                                        currentBDO = "";
                                    }
                                }
                                else
                                    // IF it is not a category...
                                {
                                    if (currentBDO == "")
                                    {
                                        dataRow["DerivedType"] = Derivedtype.BDO;
                                        dataRow["EAStereotype"] = "Business Object";
                                        currentBDO = dataRow["CaliberRequirement"].ToString();
                                    }
                                    else
                                    {
                                        // If the level falls twice... the BDO is over.
                                        if (level < previousLevel - 1)
                                        {
                                            // Back to BDO
                                            sector = "BDO";
                                            dataRow["DerivedType"] = Derivedtype.BDO;
                                            dataRow["EAStereotype"] = "Business Object";
                                            currentBDO = dataRow["CaliberRequirement"].ToString();
                                        }

                                        if (sector == "Attributes")
                                        {
                                            dataRow["DerivedType"] = Derivedtype.Attribute;
                                            dataRow["EAStereotype"] = currentStereotype;
                                        }
                                        if (sector == "Business Rules")
                                        {
                                            if (dataRow["CaliberRequirement"].ToString().Substring(0, 3) == "BR-")
                                            {
                                                dataRow["DerivedType"] = Derivedtype.Businessrule;
                                                dataRow["EAStereotype"] = currentStereotype;
                                            }
                                            else
                                            {
                                                // Group of business rule with incorrect tag
                                                dataRow["DerivedType"] = Derivedtype.Businessruleparent;
                                                dataRow["EAStereotype"] = currentStereotype;
                                            }
                                        }
                                        if (sector == "Messages")
                                        {
                                            dataRow["DerivedType"] = Derivedtype.Ignore;

                                            if (level < previousLevel)
                                            {
                                                // Back to BDO
                                                sector = "BDO";
                                                dataRow["DerivedType"] = Derivedtype.BDO;
                                                dataRow["EAStereotype"] = "Business Object";
                                                currentBDO = dataRow["CaliberRequirement"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion BDO

                previousLevel = level;
                previousCaliberType = dataRow["CaliberType"].ToString();
            }
            return " ";
        }

        // ----------------------------------------------
        // Verify integrity of file loaded
        // ----------------------------------------------
        public bool VerifyElementIntegrity()
        {
            // 1) CaliberRequirement in ("Attributes", "Business Rules", "Messages")
            //        should have status of "[Category]"

            // 2) Attributes must come after BDO
            //

            // Determine load type by first element
            // 

            string previousElementName = "";
            string previousDerivedType = "";
            int previousCaliberID = 0;
            bool successful = true;
            dtMessageLog.Messages.Clear();

            foreach (DataRow dataRow in elementSourceDataTable.Rows)
            {
                dataRow["SyncStatus"] = "";

                if (dataRow["CaliberRequirement"].ToString() == "Attributes" ||
                    dataRow["CaliberRequirement"].ToString() == "Business Rules" ||
                    dataRow["CaliberRequirement"].ToString() == "Messages")
                {
                    if (dataRow["CaliberStatus"].ToString() != "[Category]")
                    {
                        dataRow["SyncStatus"] = "Tag should be [Category]";
                        dtMessageLog.AddMessage(dataRow["CaliberID"] + " " +
                                            dataRow["SyncStatus"]);
                        successful = false;
                    }
                }

                // if BDO is found, attribute is expected.
                if (dataRow["CaliberRequirement"].ToString() == "Attributes")
                {
                    if (previousDerivedType != Derivedtype.BDO.ToString())
                    {
                        UpdateSyncStatusByCaliberId(previousCaliberID, "BDO not tagged properly");
                        dtMessageLog.AddErrorMessage(previousCaliberID + " BDO not tagged properly "
                                            + dataRow["SyncStatus"]);
                        successful = false;
                    }
                }
                previousDerivedType = dataRow["DerivedType"].ToString();
                previousElementName = dataRow["CaliberRequirement"].ToString();
                try
                {
                    previousCaliberID = Convert.ToInt32(dataRow["CaliberID"]);
                }
                catch
                {
                    dtMessageLog.AddErrorMessage("Invalid Caliber ID: " + dataRow["CaliberID"]);
                }


                if (dataRow["DerivedType"].ToString() == "")
                {
                    dtMessageLog.AddErrorMessage(previousCaliberID + " Not tagged.");
                    successful = false;
                }

                // Check if id has string.
                int caliberID = 0;
                try
                {
                    caliberID = Convert.ToInt32(dataRow["CaliberID"]);
                }
                catch (Exception e)
                {
                    dtMessageLog.AddErrorMessage("Caliber ID is not numeric: " +
                                        dataRow["CaliberID"] + e);
                    successful = false;
                }

                if (caliberID <= 0)
                {
                    dtMessageLog.AddErrorMessage("Invalid Caliber ID" + dataRow["CaliberID"]);
                    successful = false;
                }
            }
            return successful;
        }


        // ------------------------------------------------------
        // Fix CSV loaded file
        // ------------------------------------------------------
        public bool FixCaliberInput()
        {
            // 1) CaliberRequirement in ("Attributes", "Business Rules", "Messages")
            //        should have status of "[Category]"

            // 2) Attributes must come after BDO
            //

            // Determine load type by first element
            // 

            bool successful = true;

            foreach (DataRow dataRow in elementSourceDataTable.Rows)
            {
                if (dataRow["SyncStatus"].ToString() == "Tag should be [Category]")
                {
                    dataRow["CaliberStatus"] = "[Category]";
                }
                if (dataRow["SyncStatus"].ToString() == "BDO not tagged properly")
                {
                    dataRow["CaliberStatus"] = "Draft";
                }
                dataRow["SyncStatus"] = "";
            }
            return successful;
        }

        // ------------------------------------------------
        // Update item using Caliber ID
        // ------------------------------------------------
        private void UpdateSyncStatusByCaliberId(int CaliberID, string SyncStatus)
        {
            // Find Caliber ID, Update Sync Status

            foreach (DataRow dataRow in elementSourceDataTable.Rows)
            {
                if (Convert.ToInt32(dataRow["CaliberID"]) == CaliberID)
                {
                    dataRow["SyncStatus"] = SyncStatus;
                    break;
                }
            }
            return;
        }

        // ------------------------------------------------
        // ------------------------------------------------
        // ------------------------------------------------
        // ------------------------------------------------
        // >>>>>        Sync EA and Caliber        <<<<<<<<
        // ------------------------------------------------
        // ------------------------------------------------
        // ------------------------------------------------
        // ------------------------------------------------
        // ------------------------------------------------
        // ------------------------------------------------

        public string CaliberEaSync()
        {
            string ret = "Successful";
            string ucRootPackageGuid = "";
            Package ownerPackage = null;
            Package DesignPackage = null;
            Package ScreenMasterDesignPackage = null;
            Package eAuseCaseRootPackage = null;

            Element UIMessageOwner = null;

            Element BDO;
            Element screen = null;
            string BDO_EAGUID = "";
            int previousLevel = 0;
            int level = 0;
            int BusinessRuleParentID = 0;
            int caliberID = 0;
            int BusinessRuleParentBDOID = 0;
            int UIParentID = 0;

            // Find Root Package
            ucRootPackageGuid = EADestinationPackage;
            if (ucRootPackageGuid == "")
            {
                return "Error: Package not found.";
            }

            eAuseCaseRootPackage =
                AddInRepository.Instance.Repository.GetPackageByGuid(ucRootPackageGuid);

            ownerPackage =
                eAuseCaseRootPackage; // just in case no package is created.


            if (eAuseCaseRootPackage == null)
            {
                // dr["SyncStatus"] = "Root Package not found!!!";
                return "Root Package not found!!!";
            }

            // Use Case load package GUID = "{956B96BC-56C3-4044-AAE9-6C455136014B}"

            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //
            // For each element, create package or use case/ BDO.
            //
            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


            AddInRepository.Instance.Repository.BatchAppend = true;
            AddInRepository.Instance.Repository.EnableUIUpdates = false;

            foreach (DataRow dr in elementSourceDataTable.Rows)
            #region CaliberEASync Main Loop
            {
                Element EAElement;
                Element EAElementParent = null;
                var eaInfo = new mtCaliberMapping();
                var eaParentInfo = new mtCaliberMapping();
                string msg = "";
                level = Convert.ToInt32(dr["Level"]);
                caliberID = Convert.ToInt32(dr["CaliberID"]);
                int realCaliberID = Convert.ToInt32(dr[COL_REALCALIBERID]);
                bool requirementIsMapped = Convert.ToBoolean(dr["CaliberIsMapped"]);

                // Get CaliberSourceID
                int caliberSourceID = 0;
                try
                {
                    caliberSourceID = Convert.ToInt32(dr[COL_CALIBERSOURCEID]);
                }
                catch 
                {
                    caliberSourceID = 0;
                }


                // 
                // Delete mapping from mapping table if requirement
                // has been pushed to EA from the Project Release.
                // It can't be only deleted, but remapped
                //

                #region remap partially branched requirement

                if (caliberSourceID > 0)
                {
                    // The real caliber ID should not be mapped
                    // where the source ID is present
                    //
                    var realCaliberIDMap = new mtCaliberMapping();
                    realCaliberIDMap.getDetails(realCaliberID);
                    //realCaliberIDMap.getDetails(realCaliberID, MappingTableConnection);

                    // If the real caliber ID is in the mapping table...
                    //
                    if (realCaliberIDMap.CaliberID > 0)
                    {
                        // Delete mapping for the real caliber ID
                        //
                        realCaliberIDMap.delete(MappingTableConnection);

                        //
                        // 11/09 - Check if the new element is already linked
                        // and remove it if necessary
                        //
                        var eaPreviousMap = new mtCaliberMapping();
                        eaPreviousMap.CaliberID = caliberID;
                        msg = eaPreviousMap.delete(MappingTableConnection);

                        // Delete element from EA
                        //
                        // Still to be developed.... dm0874 todo

                        // Re-map the EA_GUID to new caliber ID
                        //
                        realCaliberIDMap.CaliberID = caliberID;
                        realCaliberIDMap.add();

                        Element eaEl = AddInRepository.Instance.Repository.GetElementByGuid(realCaliberIDMap.EA_GUID);

                        if (eaEl != null)
                        {
                            EaAccess.addTaggedValue(eaEl, "CaliberID", caliberID.ToString());

                            AddInRepository.Instance.WriteCaliberResults("Requirement re-mapped after partial merge. " +
                                                                            " From: " + realCaliberID +
                                                                            " To: " + caliberID,
                                                                            realCaliberID);
                        }
                        else
                        {
                            AddInRepository.Instance.WriteCaliberResults(
                                                                            "<< Tag Not Added >> Requirement re-mapped after partial merge. " +
                                                                            " From: " + realCaliberID +
                                                                            " To: " + caliberID,
                                                                            realCaliberID);
                        }
                    }
                }

                #endregion remap partially branched requirement

                //
                // Retrieve EA info from mapping table
                //
                eaInfo.getDetails(caliberID);

                //
                // Retrieve Parent info from mapping table
                //
                try
                {
                    int calParId = Convert.ToInt32(dr["ParentId"]);

                    // Retrieve EA information for Caliber Parent
                    eaParentInfo.getDetails(calParId);
                }
                catch 
                {
                    // Parent may not exist
                }

                // Process Use Cases, BDOs, Attributes & Packages ONLY
                string derivedType = dr["DerivedType"].ToString();
                string stereotype = dr[COL_EASTEREOTYPE].ToString();


                //
                // If requirement is mapped do nothing.
                //
                if (requirementIsMapped)
                {
                    AddInRepository.Instance.WriteCaliberResults(
                                                                    "Requirement # " + caliberID +
                                                                    " is mapped - it won't be imported. ",
                                                                    caliberID);

                    // Ignore requirement
                    //
                    derivedType = Derivedtype.Ignore.ToString();
                }

                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                //                      Element Type 
                // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //
                // Find/ Create Package
                // 

                #region Package

                if (derivedType == Derivedtype.Package.ToString())
                {
                    msg = "Package guid created/retrieved.";

                    //
                    // if package guid exists in table, check if it exists in EA.
                    //
                    if (eaInfo.EA_GUID != null)
                    {
                        ownerPackage = AddInRepository.Instance.Repository.GetPackageByGuid(eaInfo.EA_GUID);

                        // if package guid doesn't exist in EA, delete from mapping and create it again
                        if (ownerPackage == null)
                        {
                            eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);

                            // Delete connection
                            //
                            msg = eaInfo.delete(MappingTableConnection);

                            // create package again
                            eaInfo.EA_GUID = null;
                        }
                    }

                    //
                    // if package doesn't exist, create a package.
                    //
                    if (eaInfo.EA_GUID == null)
                    {
                        ownerPackage =
                            CreateEaPackage(eAuseCaseRootPackage, dr["CaliberRequirement"].ToString());

                        if (ownerPackage == null)
                        {
                            // Abend
                            // 
                            msg = "Error creating package. Contact the add-in developer";
                            return msg;
                        }
                        //
                        // Add mapping record (Package)
                        //

                        eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                        eaInfo.CaliberName = dr["CaliberRequirement"].ToString();
                        eaInfo.CaliberHierarchy = dr["CaliberHierarchy"].ToString();
                        eaInfo.EA_GUID = ownerPackage.PackageGUID; // pkg specific
                        eaInfo.EAElementID = ownerPackage.PackageID;
                        eaInfo.EAParentGUID = eAuseCaseRootPackage.PackageGUID; // pkg specific
                        eaInfo.EAElementType = derivedType;
                        eaInfo.CaliberFullDescription = dr["CaliberFullDescription"].ToString();

                        // add element to Mapping table
                        msg = eaInfo.add();

                        if (msg != "Item created successfully")
                        {
                            // Abend
                            // 
                            return msg;
                        }
                    }
                }

                #endregion Package

                // ---------------------------------------------------
                // Use Cases and BDO's
                // ---------------------------------------------------
                #region UseCaseBDO

                if (derivedType == Derivedtype.Usecase.ToString()
                    || derivedType == Derivedtype.BDO.ToString()
                    || derivedType == Derivedtype.Businessruleparent.ToString()
                    || derivedType == Derivedtype.Businessrule.ToString()
                    || derivedType == Derivedtype.Actor.ToString()
                    )
                {
                    string elementType = "";
                    #region determine elementType
                    if (derivedType == Derivedtype.Actor.ToString())
                    {
                        elementType = "Actor";
                        stereotype = "";
                    }

                    if (derivedType == Derivedtype.Usecase.ToString())
                    {
                        elementType = "UseCase";
                        stereotype = "";
                    }
                    if (derivedType == Derivedtype.BDO.ToString())
                    {
                        elementType = "Class";
                        stereotype = "Business Object";
                    }
                    if (derivedType == Derivedtype.Businessruleparent.ToString())
                    {
                        elementType = "Requirement";
                        stereotype = "";
                    }
                    if (derivedType == Derivedtype.Businessrule.ToString())
                    {
                        elementType = "Requirement";
                        stereotype = "";
                    }
                    #endregion determine elementType
                    
                    //
                    // Retrieve parent element
                    //
                    if (eaParentInfo.EA_GUID != null)
                    {
                        EAElementParent =
                            AddInRepository.Instance.Repository.GetElementByGuid(eaParentInfo.EA_GUID);
                    }
                    if (EAElementParent == null)
                    {
                        EAElementParent =
                            AddInRepository.Instance.Repository.GetElementByGuid(ownerPackage.PackageGUID);
                    }

                    // ---------------------------------------------------
                    // Use Cases and BDO's region
                    // if guid exists in table, check if it exists in EA.
                    // ---------------------------------------------------
                    if (eaInfo.EA_GUID != null)
                    #region ExistingEAElement
                    {
                        // Retrieve EA info by guid
                        EAElement = AddInRepository.Instance.Repository.GetElementByGuid(eaInfo.EA_GUID);

                        if (EAElement == null)
                        {
                            //
                            // Delete mapping record
                            //
                            eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                            msg = eaInfo.delete(MappingTableConnection);

                            // set guid to null to enable creation
                            eaInfo.EA_GUID = null;
                        }
                        else
                        {
                            // Element exists in EA



                            //
                            // Update mapping table
                            // 
                            eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                            eaInfo.CaliberFullDescription =
                                dr["CaliberFullDescription"].ToString();

                            // Update
                            msg = eaInfo.update(MappingTableConnection);

                            // Update EA
                            //
                            if (EAElement.Notes == dr["CaliberFullDescription"].ToString() &&
                                EAElement.Status == dr["CaliberStatus"].ToString() &&
                                EAElement.Name == dr["CaliberRequirement"].ToString())
                            {
                                // No updates
                                // But still need to check parent
                            }
                            else
                            {
                                AddInRepository.Instance.WriteCaliberResults(
                                                                                derivedType +
                                                                                " element has been UPDATED: " +
                                                                                dr["CaliberRequirement"],
                                                                                EAElement.ElementID);

                                EAElement.Tag = "Changed";

                                EAElement.Notes = dr["CaliberFullDescription"].ToString();
                                EAElement.Status = dr["CaliberStatus"].ToString();
                                EAElement.Name = dr["CaliberRequirement"].ToString();
                            }

                            // -------------------------------------
                            // In case of Update, move to new parent
                            // -------------------------------------

                            //EAElement.ParentID = ownerPackage.PackageID;

                            if (derivedType == Derivedtype.Businessrule.ToString()
                                || derivedType == Derivedtype.Businessruleparent.ToString())
                            {
                                if (EAElementParent != null)
                                {
                                    if (EAElementParent.Type == "Package")

                                        if (EAElementParent.PackageID > 0)
                                        {
                                            EAElement.PackageID = EAElementParent.PackageID;
                                        }
                                        else
                                        {
                                            //
                                            // <><><> ABORT 001 <><><>
                                            //
                                            ret =
                                                "<>ABR001<> Error setting parent info Element Name: " +
                                                EAElement.Name +
                                                "Parent Package ID: " +
                                                EAElementParent.PackageID;
                                            MessageBox.Show(ret);

                                            AddInRepository.Instance.WriteCaliberResults( ret, caliberID);

                                            return ret;
                                        }
                                    else
                                    {
                                        if (EAElementParent.ElementID > 0)
                                        {
                                            EAElement.ParentID = EAElementParent.ElementID;
                                        }
                                    }
                                }
                                else
                                {
                                    EAElement.PackageID = EAElementParent.PackageID;
                                }
                            }
                            if (! EAElement.Update())
                            {
                                MessageBox.Show(EAElement.GetLastError());
                            }
                            EAElement.Refresh();
                            ownerPackage.Elements.Refresh();
                            EAElement.ApplyGroupLock(EASecurityGroups.BusinessAnalyst);

                            ret = "Existing Use Cases or BDOs have been updated. " + Environment.NewLine + Environment.NewLine +
                                  "This means that you may not find the elements in the folder " +
                                  "you have selected. "  + Environment.NewLine + Environment.NewLine + 
                                  "Please use CTRL+F in EA to search for it.";

                            #region StoreCaliberIDasTag

                            // Store the Caliber ID as a tagged value
                            //

                            // Retrieve CaliberID tag value
                            object tagVal = EAElement.TaggedValues.GetByName("CaliberID");

                            if (tagVal != null)
                            {
                                // Tag was found, ignore.
                            }
                            else
                            {
                                object newTag =
                                    EAElement.TaggedValues.AddNew("CaliberID", dr["CaliberID"].ToString());

                                var tv = (TaggedValue) newTag;

                                if (! tv.Update())
                                {
                                    MessageBox.Show(tv.GetLastError());
                                }

                                EAElement.TaggedValues.Refresh();
                                EAElement.Refresh();
                                ownerPackage.Elements.Refresh();
                            }

                            #endregion StoreCaliberIDasTag
                        }
                    }

                    #endregion ExistingEAElement

                    // ---------------------------------------------------
                    // Use Cases and BDO's region
                    // if guid doesn't exist in mapping table, create it.
                    // ---------------------------------------------------
                    if (eaInfo.EA_GUID == null)
                    #region Non-Existing EA Element
                    {

                        //
                        // UC Mapping Record not found >> Add EA Use Case
                        //

                        // Find parent
                        if (eaParentInfo.EA_GUID != null)
                        {
                            EAElementParent =
                                AddInRepository.Instance.Repository.GetElementByGuid(eaParentInfo.EA_GUID);
                        }
                        if (EAElementParent == null)
                        {
                            EAElementParent =
                                AddInRepository.Instance.Repository.GetElementByGuid(ownerPackage.PackageGUID);
                        }


                        //
                        // Add EA element
                        //
                        Element newEAElement = null;
                        try
                        {
                            newEAElement =
                                (Element) ownerPackage.Elements.AddNew(dr["CaliberRequirement"].ToString(),
                                                                       elementType);

                            AddInRepository.Instance.WriteCaliberResults(
                                                                            derivedType + " element added: " +
                                                                            dr["CaliberRequirement"],
                                                                            caliberID);
                        }
                        catch (Exception ex)
                        {
                            // <!><!><!><!><!><!><!><!><!><!><!><!>
                            //           ABEND 
                            // <!><!><!><!><!><!><!><!><!><!><!><!>
                            ret = "Error creating EA element. " +
                                  dr["CaliberRequirement"] +
                                  " " + ex;
                            MessageBox.Show(ret);
                            return ret;
                        }

                        // --------------------------------
                        // Identify parent.  
                        // --------------------------------

                        if (derivedType == Derivedtype.Businessrule.ToString()
                            || derivedType == Derivedtype.Businessruleparent.ToString())
                        {
                            if (EAElementParent != null)
                            {
                                if (EAElementParent.Type == "Package")

                                    if (EAElementParent.PackageID > 0)
                                    {
                                        newEAElement.PackageID = EAElementParent.PackageID;
                                    }
                                    else
                                    {
                                        //
                                        // <><><> ABORT 001 <><><>
                                        //
                                        ret = "<>ABR001<> Error setting parent info Element Name: " +
                                              newEAElement.Name +
                                              "Parent Package ID: " +
                                              EAElementParent.PackageID;
                                        MessageBox.Show(ret);
                                        return ret;
                                    }
                                else
                                {
                                    if (EAElementParent.ElementID > 0)
                                    {
                                        newEAElement.ParentID = EAElementParent.ElementID;
                                    }
                                }
                            }
                            else
                            {
                                newEAElement.PackageID = EAElementParent.PackageID;
                            }
                        }


                        // --------------------------------
                        // Derive status
                        // --------------------------------
                        if (derivedType == Derivedtype.BDO.ToString())
                        {
                            newEAElement.Status = dr["CaliberStatus"].ToString();
                        }
                        else
                        {
                            newEAElement.Status = "Implemented";
                        }

                        newEAElement.StereotypeEx = "";
                        newEAElement.Stereotype = dr[COL_EASTEREOTYPE].ToString();
                        newEAElement.Notes = dr["CaliberFullDescription"].ToString();
                        newEAElement.Status = dr["CaliberStatus"].ToString();
                        newEAElement.Update();
                        newEAElement.Refresh();
                        ownerPackage.Elements.Refresh();

                        //
                        // Store the Caliber ID as a tagged value
                        //
                        if (newEAElement != null)
                        {
                            EaAccess.addTaggedValue(newEAElement, "CaliberID", dr["CaliberID"].ToString());
                        }

                        newEAElement.Refresh();
                        ownerPackage.Elements.Refresh();

                        //
                        // Add mapping record
                        //
                        eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                        eaInfo.CaliberName = dr["CaliberRequirement"].ToString();
                        eaInfo.CaliberHierarchy = dr["CaliberHierarchy"].ToString();
                        eaInfo.EA_GUID = newEAElement.ElementGUID;
                        eaInfo.EAElementID = newEAElement.ElementID;
                        eaInfo.EAParentGUID = EAElementParent.ElementGUID;
                        eaInfo.EAElementType = derivedType;
                        eaInfo.CaliberFullDescription = dr["CaliberFullDescription"].ToString();

                        msg = eaInfo.add();
                        if (msg != "Item created successfully")
                        {
                            // Abend
                            // 
                            return msg;
                        }
                    }

                    #endregion Non-Existing EA Element

                    if (derivedType == Derivedtype.BDO.ToString())
                    {
                        BDO_EAGUID = eaInfo.EA_GUID;
                        BusinessRuleParentBDOID = eaInfo.EAElementID;
                        BusinessRuleParentID = 0; // reset Business Rule Parent ID.
                    }
                    if (derivedType == Derivedtype.Businessruleparent.ToString())
                    {
                        BusinessRuleParentID = eaInfo.EAElementID;
                    }
                }

                #endregion UseCaseBDO

                //
                // Attributes for BDO
                //
                #region BDOAttribute

                if (derivedType == Derivedtype.Attribute.ToString())
                {
                    Attribute eaAttribute = null;

                    // Retrieve BDO from EA
                    //

                    BDO = AddInRepository.Instance.Repository.GetElementByGuid(BDO_EAGUID);
                    if (BDO == null)
                    {
                        msg = "BDO not found " + BDO_EAGUID;

                        AddInRepository.Instance.WriteCaliberResults(
                                                                        msg, 1000);
                    }
                    else
                    {
                        if (eaInfo.EA_GUID == null)
                        {
                            // Attribute information not found in mapping. Proceed to Create
                        }
                        else
                        {
                            // BDO found in EA
                            // Attribute found in mapping table

                            // Look for EA attribute ID 
                            foreach (Attribute eaAttri in BDO.Attributes)
                            {
                                if (eaAttri.AttributeGUID == eaInfo.EA_GUID)
                                {
                                    eaAttribute = eaAttri;
                                    break;
                                }
                            }

                            // If attribute is found, update the name if necessary
                            //
                            if (eaAttribute != null)
                            {
                                //always update
                                string attStereotype = dr["EAStereotype"].ToString();
                                string attName = dr["CaliberRequirement"].ToString();
                                string atNameTrim = attName.Trim();

                                string attNotes = dr["CaliberFullDescription"].ToString();

                                if (attStereotype.Length > 49)
                                {
                                    attStereotype = attStereotype.Substring(0, 49);
                                }
                                eaAttribute.Stereotype = attStereotype;

                                // Check if the element notes has changed
                                //
                                if (eaAttribute.Name != atNameTrim)
                                {
                                    AddInRepository.Instance.WriteCaliberResults(
                                                                                    derivedType +
                                                                                    " attribute has been UPDATED: " +
                                                                                    eaAttribute.Name,
                                                                                    eaAttribute.AttributeID);
                                }

                                eaAttribute.Name = atNameTrim;
                                eaAttribute.Notes = attNotes.Trim();

                                try
                                {
                                    eaAttribute.Update();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex +
                                                    "\n Att Error: " + eaAttribute.GetLastError());

                                    try
                                    {
                                        eaAttribute.Update();
                                    }
                                    catch (Exception ex2)
                                    {
                                        MessageBox.Show(ex2 +
                                                        "\n (2) Att Error: " + eaAttribute.GetLastError());
                                    }
                                }

                                #region StoreCaliberIDasTag

                                // Store the Caliber ID as a tagged value
                                //  << Attribute >>

                                // Retrieve CaliberID tag value
                                object tagVal = eaAttribute.TaggedValues.GetByName("CaliberID");

                                if (tagVal != null)
                                {
                                    // Tag was found, ignore.
                                }
                                else
                                {
                                    object newTag =
                                        eaAttribute.TaggedValues.AddNew(
                                            "CaliberID", dr["CaliberID"].ToString());

                                    var tv = (AttributeTag) newTag;

                                    if (!tv.Update())
                                    {
                                        MessageBox.Show(tv.GetLastError());
                                    }

                                    eaAttribute.TaggedValues.Refresh();
                                }

                                #endregion StoreCaliberIDasTag

                                BDO.Refresh();
                            }
                            else
                            {
                                // Delete from mapping and add again
                                eaInfo.delete(MappingTableConnection);
                                eaInfo.EA_GUID = null;
                            }
                        }

                        if (eaInfo.EA_GUID == null)
                        {
                            // Attribute not found or deleted, create one
                            //

                            eaAttribute =
                                (Attribute) BDO.Attributes.AddNew(
                                                dr["CaliberRequirement"].ToString(), "string");
                            eaAttribute.Stereotype = dr["EAStereotype"].ToString();
                            eaAttribute.Update();
                            BDO.Refresh();

                            AddInRepository.Instance.WriteCaliberResults(
                                                                            eaAttribute.Name +
                                                                            "<<<<<< !!! NEW attribute " +
                                                                            caliberID + " " + derivedType,
                                                                            eaAttribute.AttributeID);

                            // Add tag to attribute
                            //

                            #region StoreCaliberIDasTag

                            //
                            // Store the Caliber ID as a tagged value
                            //

                            object tagAttribute =
                                eaAttribute.TaggedValues.AddNew(
                                    "CaliberID", dr["CaliberID"].ToString());

                            var attrTagValue = (AttributeTag) tagAttribute;

                            if (!attrTagValue.Update())
                            {
                                MessageBox.Show(attrTagValue.GetLastError());
                            }

                            eaAttribute.TaggedValues.Refresh();

                            #endregion StoreCaliberIDasTag

                            //
                            // Add mapping record
                            //
                            eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                            eaInfo.CaliberName = dr["CaliberRequirement"].ToString();
                            eaInfo.CaliberHierarchy = dr["CaliberHierarchy"].ToString();
                            eaInfo.EA_GUID = eaAttribute.AttributeGUID;
                            eaInfo.EAParentGUID = BDO_EAGUID;
                            eaInfo.EAElementType = derivedType;
                            eaInfo.CaliberFullDescription = dr["CaliberFullDescription"].ToString();
                            msg = eaInfo.add();

                            if (msg != "Item created successfully")
                            {
                                // Abend
                                // 
                                return msg;
                            }
                        }
                    }
                }

                #endregion BDOAttribute

                //
                // User Interface
                //
                #region UserInterface

                if (derivedType == Derivedtype.SCREEN.ToString()
                    || derivedType == Derivedtype.DESIGNRULE.ToString()
                    || derivedType == Derivedtype.ELEMENT.ToString()
                    || derivedType == Derivedtype.MESSAGE.ToString()
                    )
                {
                    string elementType = "";

                    if (derivedType == Derivedtype.SCREEN.ToString())
                        elementType = "Screen";
                    if (derivedType == Derivedtype.DESIGNRULE.ToString())
                        elementType = "Requirement";
                    if (derivedType == Derivedtype.MESSAGE.ToString())
                        elementType = "Requirement";
                    if (derivedType == Derivedtype.ELEMENT.ToString())
                        elementType = "GUIElement";

                    // ----------------------------------------------------
                    // Retrieve parent element
                    // ----------------------------------------------------
                    if (eaParentInfo.EA_GUID != null)
                    {
                        EAElementParent =
                            AddInRepository.Instance.Repository.GetElementByGuid(eaParentInfo.EA_GUID);
                    }
                    if (EAElementParent == null)
                    {
                        EAElementParent =
                            AddInRepository.Instance.Repository.GetElementByGuid(ownerPackage.PackageGUID);
                    }


                    // ----------------------------------------------------
                    // User Interface UI region
                    // if guid exists in table, check if it exists in EA.
                    // ----------------------------------------------------
                    if (eaInfo.EA_GUID != null)
                    {
                        #region ElementFoundInEA

                        // Retrieve EA info by guid
                        EAElement = AddInRepository.Instance.Repository.GetElementByGuid(eaInfo.EA_GUID);
                        //
                        // If element does not exist in EA... delete mapping
                        //
                        if (EAElement == null)
                        {
                            #region DeleteEAElement

                            //
                            // Delete mapping record
                            //
                            eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                            msg = eaInfo.delete(MappingTableConnection);

                            // set guid to null to enable creation
                            eaInfo.EA_GUID = null;

                            #endregion DeleteEAElement
                        }
                        else
                        {
                            // Check if types are different.
                            //
                            if (elementType.ToUpper() != EAElement.Type.ToUpper())
                            {
                                ret = "Element " + EAElement.Name + "'s EA type (" + 
                                    EAElement.Type + ") is different from Caliber (" + 
                                    elementType + ")" + Environment.NewLine + Environment.NewLine +
                                    "Please contact the BARM team. \n"
                                    + "Caliber ID: "+ eaInfo.CaliberID;
                                MessageBox.Show(ret);

                                // <<<<<<<<<<<<<<<<<<<<<<
                                //         EXIT POINT
                                // <<<<<<<<<<<<<<<<<<<<<<
                                return ret;
                            }

                            // Retrieve screen Information
                            if (derivedType == Derivedtype.SCREEN.ToString())
                            {
                                screen = 
                                    AddInRepository.Instance.Repository.GetElementByGuid(eaInfo.EA_GUID);

                                // Find the owner package
                                ownerPackage = 
                                    AddInRepository.Instance.Repository.GetPackageByID(screen.PackageID);
                            }

                            // -----------------------------------------
                            // Element Exists in Mapping table and in EA
                            // -----------------------------------------

                            // If element exists in EA, 
                            // Update mapping table
                            // 
                            eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);

                            // -----------------------------------------
                            //               Update SCREEN 
                            // -----------------------------------------
                            if (derivedType == Derivedtype.SCREEN.ToString())
                            {
                                #region ScreenUpdate

                                // Retrieve Package that contains the design rules
                                // for elements on a screen - default
                                //
                                ScreenMasterDesignPackage =
                                    AddInRepository.Instance.Repository.GetPackageByID(ownerPackage.PackageID);

                                // If the package GUID is stored, do some checks
                                // Finding the screen master design package
                                //
                                if (eaInfo.UIDesignRulePackage != "")
                                {
                                    ScreenMasterDesignPackage =
                                        AddInRepository.Instance.Repository.GetPackageByGuid(eaInfo.UIDesignRulePackage);

                                    // If the package has been deleted, reset it
                                    //
                                    if (ScreenMasterDesignPackage == null)
                                    {
                                        eaInfo.UIDesignRulePackage = "";
                                    }

                                    // If the package is the same as the screen, reset it
                                    if (screen.PackageID > 0)
                                    {
                                        Package sp =
                                            AddInRepository.Instance.Repository.GetPackageByID(screen.PackageID);
                                        if (sp.PackageGUID == eaInfo.UIDesignRulePackage)
                                        {
                                            eaInfo.UIDesignRulePackage = "";
                                        }

                                        // Well, if it exists we can still reuse it
                                        foreach (Package orp in ownerPackage.Packages)
                                        {
                                            if (orp.Name == "UI Design Rules")
                                            {
                                                ScreenMasterDesignPackage =
                                                    AddInRepository.Instance.Repository.GetPackageByID(orp.PackageID);

                                                eaInfo.UIDesignRulePackage =
                                                    ScreenMasterDesignPackage.PackageGUID;
                                            }
                                        }
                                    }
                                }

                                //
                                // If the design rule package is not set,
                                // create one
                                //
                                if (eaInfo.UIDesignRulePackage == "")
                                {
                                    // Create Screen Master Design Package
                                    //
                                    ScreenMasterDesignPackage =
                                        CreateEaPackage(ownerPackage, "UI Design Rules");
                                }
                                else
                                {
                                    // Update the package name
                                    ScreenMasterDesignPackage.Name = "UI Design Rules";
                                    ScreenMasterDesignPackage.Update();
                                    ScreenMasterDesignPackage.Elements.Refresh();
                                }

                                // This is for the update
                                eaInfo.UIDesignRulePackage = ScreenMasterDesignPackage.PackageGUID;

                                // Set message owner
                                UIMessageOwner = null;


                                ScreenMasterDesignPackage.Notes = "Design Package for Caliber ID: " +
                                   caliberID + "  UI Element: " +
                                   dr["CaliberRequirement"];
                                ScreenMasterDesignPackage.Update();

                                // Update screen tagged values and last update date
                                //
                                UpdateUiElementTag(EAElement, "LastUpdatedOn", 
                                    DateTime.Today.ToString());
                                UpdateUiElementTag(EAElement, "CaliberID", caliberID);

                                #endregion ScreenUpdate
                            }
                            else
                            {
                                if (derivedType == Derivedtype.ELEMENT.ToString())
                                #region Update UI ELEMENT
                                {
                                    // 
                                    // This part if for the element Location ONLY 
                                    // and NOT for package-element mirror location
                                    //
                                    if (screen.ElementID > 0)
                                    {
                                        // DM0874 - 10jul2009 - Do not change element parent id
                                        // EAElement.ParentID = screen.ElementID;
                                    }

                                    // Find the parent
                                    if (EAElementParent != null)
                                    {
                                        if (EAElementParent.Type == "Package")
                                        {
                                            if (EAElementParent.PackageID > 0)
                                            {
                                                EAElement.PackageID = EAElementParent.PackageID;
                                            }
                                            else
                                            {
                                                //
                                                // <><><> ABORT 001 <><><>
                                                //
                                                ret = "<>ABR001<> Error setting parent info Element Name: " +
                                                      EAElement.Name +
                                                      "Parent Package ID: " +
                                                      EAElementParent.PackageID;
                                                MessageBox.Show(ret);
                                                return ret;
                                            }
                                        }
                                         else
                                        {
                                            // DM0874 08JUL2009
                                            // This is the place where the element may change 
                                            // the parent. We don't want to do it no longer
                                            // because it can damage the existing UI
                                            // diagrams
                                            //
                                            if (EAElementParent.ElementID > 0)
                                            {
                                                // DM0874 - 10jul2009 - Do not change element parent id
                                                // EAElement.ParentID = EAElementParent.ElementID;

                                            }
                                        }
                                    }


                                    //
                                    // This part is to find a package-mirror of the Element
                                    // to store the design rules
                                    //
                                    #region package mirror
                                    // Find package for the screen
                                    Package eaScreenPackage =
                                        AddInRepository.Instance.Repository.GetPackageByID(screen.PackageID);

                                    // If the UIDesignRule package is not defined,
                                    // Create one now.
                                    if (eaInfo.UIDesignRulePackage == "")
                                    {
                                        // Create Design Package
                                        //
                                        if (dr["CaliberRequirement"].ToString() == "Object Design Rules")
                                        {
                                            DesignPackage =
                                                CreateEaPackage(ownerPackage,
                                                                dr["CaliberRequirement"].ToString());

                                        }
                                        else
                                        {
                                            DesignPackage =
                                                CreateEaPackage(ScreenMasterDesignPackage,
                                                                dr["CaliberRequirement"].ToString());
                                        }

                                        // Save the design package to the mapping table
                                        eaInfo.UIDesignRulePackage = DesignPackage.PackageGUID;
                                    }
                                    else
                                    {
                                        // Retrieve package of the UI Element
                                        DesignPackage =
                                            AddInRepository.Instance.Repository.GetPackageByGuid(
                                                eaInfo.UIDesignRulePackage);

                                        if (DesignPackage == null)
                                        {
                                            //
                                            // Create a design package
                                            //
                                            DesignPackage =
                                                CreateEaPackage(ownerPackage,
                                                                dr["CaliberRequirement"].ToString());

                                            // Store it in the database
                                            //
                                            eaInfo.UIDesignRulePackage = DesignPackage.PackageGUID;
                                        }
                                        else

                                        // Move Design Package under screen master design package
                                        //DesignPackage.ParentID = screen.PackageID;
                                        if (dr["CaliberRequirement"].ToString() == "Object Design Rules")
                                        {
                                            DesignPackage.ParentID = ownerPackage.PackageID;
                                        }
                                        else
                                        {
                                            DesignPackage.ParentID = ScreenMasterDesignPackage.PackageID;
                                        }
                                        DesignPackage.Name = dr["CaliberRequirement"].ToString();
                                        DesignPackage.Update();
                                        DesignPackage.Elements.Refresh();
                                    }
                                    #endregion package mirror

                                    // Set message owner
                                    UIMessageOwner = null;

                                    DesignPackage.Notes = "Design Package for Caliber ID: " +
                                       caliberID.ToString() + "  UI Element: " +
                                       dr["CaliberRequirement"].ToString();
                                    DesignPackage.Update();

                                    UpdateUiElementTags(EAElement,(SelectRequirements.ElementProperties)dr[COL_UI_ELEM_PROP], caliberID);
                                }
                                #endregion Update UI ELEMENT
                                else
                                #region Update Design Rule + Message
                                {
                                    // -----------------------------------------
                                    // Update DESIGN RULE (stored under packages)
                                    // -----------------------------------------
                                    if (derivedType == Derivedtype.DESIGNRULE.ToString())
                                    {
                                        if (DesignPackage != null && DesignPackage.PackageGUID != "")
                                        {
                                            // Retrieve DesignPackage
                                            DesignPackage = AddInRepository.Instance.Repository.GetPackageByGuid(
                                                DesignPackage.PackageGUID);
                                            if (DesignPackage.PackageID > 0)
                                            {
                                                EAElement.PackageID = DesignPackage.PackageID;
                                            }
                                        }

                                        // Set message owner
                                        UIMessageOwner = EAElement;
                                    }

                                    // -----------------------------------------
                                    // Update MESSAGE
                                    // -----------------------------------------
                                    if (derivedType == Derivedtype.MESSAGE.ToString())
                                    {
                                        if (DesignPackage != null && DesignPackage.PackageGUID != "")
                                        {
                                            // Retrieve DesignPackage
                                            DesignPackage =
                                                AddInRepository.Instance.Repository.GetPackageByGuid(
                                                    DesignPackage.PackageGUID);
                                            if (DesignPackage.PackageID > 0)
                                            {
                                                EAElement.ParentID = UIMessageOwner.ElementID;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }

                            // Update the mapping table
                            //
                            msg = eaInfo.update(MappingTableConnection);

                            // <!><!><!><!><!><!><!><!><!><!><!><!>
                            //           ABEND 
                            // <!><!><!><!><!><!><!><!><!><!><!><!>
                            if (msg != "Item updated successfully")
                            {
                                ret = "Error updating EA element. " +
                                      dr["CaliberRequirement"] +
                                      " " + msg;
                                MessageBox.Show(ret);
                                return ret;
                            }

                            // Update EA
                            //        Move packages if necessary.

                            if (EAElement.Notes == dr["CaliberFullDescription"].ToString() &&
                                EAElement.Status == dr["CaliberStatus"].ToString() &&
                                EAElement.Name == dr["CaliberRequirement"].ToString())
                            {
                                // nothing to update
                            }
                            else
                            {
                                // Details updated
                                AddInRepository.Instance.WriteCaliberResults(
                                                                                derivedType +
                                                                                " Element has been UPDATED: " +
                                                                                EAElement.Name + " CaliberID: " +
                                                                                caliberID,
                                                                                EAElement.ElementID);

                                EAElement.Tag = "Changed";

                                // Update details
                                //
                                EAElement.Notes = dr["CaliberFullDescription"].ToString();
                                EAElement.Status = dr["CaliberStatus"].ToString();
                                EAElement.Name = dr["CaliberRequirement"].ToString();
                            }

                            // The update has to be called...
                            // The parent may have changed, that's the main reason.
                            //
                            EAElement.Update();
                            EAElement.Refresh();
                            ownerPackage.Elements.Refresh();

                            //// Add the tags if necessary - this part happens when the UI Element
                            //// is being UPDATED

                            //if (EAElement.MetaType != "Requirement")
                            //    updateUItags(EAElement,
                            //                 (SelectRequirements.ElementProperties) dr[COL_UI_ELEM_PROP],
                            //                 caliberID);
                        }

                        #endregion ElementFoundInEA
                    }


                    // -----------------------------------------------------
                    // -----------------------------------------------------
                    // -----------------------------------------------------
                    // -----------------------------------------------------
                    //           NEW SCREENS, ELEMENTS, RULES ETC
                    // -----------------------------------------------------
                    // -----------------------------------------------------
                    // -----------------------------------------------------
                    // -----------------------------------------------------
                    if (eaInfo.EA_GUID == null)

                        #region ElementNotFoundInEA

                    {
                        // Instantiate the new element
                        //
                        Element newEAElement = null;

                        // If it is a screen, create the message and the package infrastructure
                        //
                        if (derivedType == Derivedtype.SCREEN.ToString())

                            #region screenCreate

                        {
                            // Create package to keep UI Screen
                            ownerPackage =
                                CreateEaPackage(eAuseCaseRootPackage,
                                                dr["CaliberRequirement"].ToString());

                            // Create Screen Master Design Package
                            //
                            ScreenMasterDesignPackage =
                                CreateEaPackage(ownerPackage, "UI Design Rules");

                            ScreenMasterDesignPackage.Notes = "Design Package for Caliber ID: " +
                                   caliberID.ToString() + "  UI Element: " +
                                   dr["CaliberRequirement"].ToString();
                            ScreenMasterDesignPackage.Update();

                            
                            // Create the Screen
                            //
                            screen =
                                (Element) ownerPackage.Elements.AddNew(
                                              dr["CaliberRequirement"].ToString(), elementType);

                            UIParentID = screen.ElementID;
                            screen.StereotypeEx = "";
                            screen.Stereotype = dr[COL_EASTEREOTYPE].ToString();
                            screen.Notes = dr["CaliberFullDescription"].ToString();
                            screen.Status = dr["CaliberStatus"].ToString();
                            screen.Update();
                            screen.Refresh();
                            ownerPackage.Elements.Refresh();

                            UpdateUiElementTag(screen, 
                                "LastUpdatedOn", System.DateTime.Today.ToString());


                            //
                            // Add mapping record for screen
                            //
                            eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                            eaInfo.CaliberName = dr["CaliberRequirement"].ToString();
                            eaInfo.CaliberHierarchy = dr["CaliberHierarchy"].ToString();
                            eaInfo.EA_GUID = screen.ElementGUID;
                            eaInfo.EAParentGUID = ownerPackage.PackageGUID;
                            eaInfo.EAElementID = screen.ElementID;
                            eaInfo.EAElementType = derivedType;
                            eaInfo.CaliberFullDescription = dr["CaliberFullDescription"].ToString();
                            eaInfo.UIDesignRulePackage = ScreenMasterDesignPackage.PackageGUID;

                            msg = eaInfo.add();

                            if (msg != "Item created successfully")
                            {
                                // Abend
                                // 
                                return msg;
                            }

                            AddInRepository.Instance.WriteCaliberResults(
                                                                            derivedType + " element added: " +
                                                                            dr["CaliberRequirement"],
                                                                            caliberID);
                        }
                            #endregion screenCreate

                        else
                            #region ElementDesignRuleCreate

                        {
                            // If it is NOT a screen, process from here

                            // ----------------------------------
                            // Create a new design element in EA
                            // ---------------------------------- 
                            if (derivedType == Derivedtype.ELEMENT.ToString())

                                #region NewElementEA

                            {
                                if (screen == null)
                                {
                                    AddInRepository.Instance.WriteCaliberResults(
                                                dr["CaliberRequirement"] +
                                                "<<<<<< !!! Element ignored ",0);

                                }
                                else
                                {
                                    // Create new Element under the screen
                                    newEAElement =
                                        (Element) screen.Elements.AddNew(dr["CaliberRequirement"].ToString(),
                                                                         elementType);

                                    AddInRepository.Instance.WriteCaliberResults(
                                        newEAElement.Name +
                                        "<<<<<< !!! NEW attribute " +
                                        caliberID + " " + derivedType,
                                        newEAElement.ElementID);

                                    // For the screen rules, the package will be under the main 
                                    // screen package
                                    if (dr["CaliberRequirement"].ToString() == "Object Design Rules")
                                    {
                                        DesignPackage =
                                            CreateEaPackage(ownerPackage,
                                                            dr["CaliberRequirement"].ToString());

                                        DesignPackage.Notes = "Design Package for Caliber ID: " +
                                                              caliberID.ToString() + "  Screen: " +
                                                              dr["CaliberRequirement"].ToString();
                                        DesignPackage.Update();

                                    }
                                    else
                                    {
                                        // Create a package to store the design rules
                                        // under the master screen design rules package
                                        //
                                        DesignPackage =
                                            CreateEaPackage(ScreenMasterDesignPackage,
                                                            dr["CaliberRequirement"].ToString());

                                        DesignPackage.Notes = "Design Package for Caliber ID: " +
                                                              caliberID.ToString() + "  UI Element: " +
                                                              dr["CaliberRequirement"].ToString();

                                        DesignPackage.Update();


                                    }
                                    // Set the dr package element stereotype
                                    //
                                    newEAElement.StereotypeEx = "";
                                    if (dr["CaliberRequirement"].ToString() == "Elements" ||
                                        dr["CaliberRequirement"].ToString() == "Object Design Rules")
                                    {
                                        DesignPackage.StereotypeEx = "";
                                    }
                                    else
                                    {
                                        DesignPackage.StereotypeEx = dr[COL_EASTEREOTYPE].ToString();
                                    }
                                    // Update the package stereotype
                                    DesignPackage.Update();
                                    DesignPackage.Elements.Refresh();

                                    // The element can have a parent of an element
                                    //
                                    if (EAElementParent.Type == "Package")
                                    {
                                        newEAElement.PackageID = EAElementParent.PackageID;
                                    }
                                    else
                                    {
                                        newEAElement.ParentID = EAElementParent.ElementID;
                                    }

                                    UpdateUiElementTags(newEAElement,
                                                        (SelectRequirements.ElementProperties) dr[COL_UI_ELEM_PROP],
                                                        caliberID);

                                    // Reset the message owner because elements can't contain
                                    // requirements
                                    UIMessageOwner = null;
                                }
                            }

                            #endregion NewElementEA

                            // -------------------------------------------------
                            // Create a new requirement/ UI Design rule in EA
                            // -------------------------------------------------
                            if (derivedType == Derivedtype.DESIGNRULE.ToString())

                                #region NewDesignRuleEA

                            {
                                // Create the element and change the owner
                                //
                                newEAElement =
                                    (Element) DesignPackage.Elements.AddNew(
                                                  dr["CaliberRequirement"].ToString(), elementType);

                                UpdateUiElementTag(newEAElement, 
                                     "LastUpdatedOn", System.DateTime.Today.ToString());
                                UpdateUiElementTag(newEAElement, "CaliberID", caliberID);

                                // Message Onwer 
                                UIMessageOwner = newEAElement;
                            }

                            #endregion NewDesignRuleEA

                            // -------------------------------------------------
                            // Create a new message in EA, under a Design Rule
                            // -------------------------------------------------
                            if (derivedType == Derivedtype.MESSAGE.ToString())

                                #region NewMessageEA

                            {
                                if (UIMessageOwner != null)
                                {
                                    // The owner is a design rules
                                    newEAElement =
                                        (Element) UIMessageOwner.Elements.AddNew(
                                                      dr["CaliberRequirement"].ToString(), elementType);
                                }

                                // Reset the message owner because elements can't contain
                                // requirements
                                UIMessageOwner = null;
                            }

                            #endregion NewMessageEA

                            // ----------------------------------
                            // Update Element just created in EA
                            // ----------------------------------



                            if (newEAElement != null)
                            {
                                #region UpdateElementEA
                                newEAElement.StereotypeEx = "";
                                if (dr["CaliberRequirement"].ToString() == "Elements" ||
                                    dr["CaliberRequirement"].ToString() == "Object Design Rules")
                                {
                                    newEAElement.Stereotype = "";
                                }
                                else
                                {
                                    newEAElement.Stereotype = dr[COL_EASTEREOTYPE].ToString();
                                }
                                newEAElement.Notes = dr["CaliberFullDescription"].ToString();
                                newEAElement.Status = dr["CaliberStatus"].ToString();
                                newEAElement.Update();
                                newEAElement.Refresh();
                                ownerPackage.Elements.Refresh();

                                #endregion UpdateElementEA

                                //
                                // Add mapping record
                                //

                                #region AddMappingRecord

                                eaInfo.CaliberID = Convert.ToInt32(dr["CaliberID"]);
                                eaInfo.CaliberName = dr["CaliberRequirement"].ToString();
                                eaInfo.CaliberHierarchy = dr["CaliberHierarchy"].ToString();
                                eaInfo.EA_GUID = newEAElement.ElementGUID;
                                eaInfo.EAParentGUID = ownerPackage.PackageGUID;
                                eaInfo.EAElementID = newEAElement.ElementID;
                                eaInfo.EAElementType = derivedType;
                                eaInfo.CaliberFullDescription = dr["CaliberFullDescription"].ToString();
                                eaInfo.UIDesignRulePackage = DesignPackage.PackageGUID;

                                msg = eaInfo.add();

                                if (msg != "Item created successfully")
                                {
                                    // Abend
                                    // 
                                    return msg;
                                }
                                #endregion AddMappingRecord
                            }
                            else
                                MessageBox.Show("The following Element was not loaded. Please contact the BARM team to check the structure in Caliber for this element." + dr["CaliberRequirement"].ToString());

                            
                        }

                        #endregion ElementDesignRuleCreate
                    }

                    #endregion ElementNotFoundInEA
                }

                #endregion UserInterface

                previousLevel = level;
                dr["SyncStatus"] = msg;
            }
            #endregion CaliberEASync Main Loop


            AddInRepository.Instance.Repository.BatchAppend = false;
            AddInRepository.Instance.Repository.EnableUIUpdates = true;
            AddInRepository.Instance.Repository.RefreshModelView(eAuseCaseRootPackage.PackageID);

            Package currentPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            //if (currentPackage != null)
            //{
            //    currentPackage.Elements.Refresh();
            //    currentPackage.Diagrams.Refresh();
            //    currentPackage.Packages.Refresh();
            //    currentPackage.Connectors.Refresh();
            //}

            MessageBox.Show("Reload current package contents.");
            return ret;
        }

        // ---------------------------------------------
        // Update EA UI Element tags
        // ---------------------------------------------
        public void UpdateUiElementTags(Element eaElement,
                                 SelectRequirements.ElementProperties uiProperties, int caliberID)
        {
            if (eaElement.Name.ToUpper() == "OBJECT DESIGN RULES"
                || eaElement.Name.ToUpper() == "ELEMENTS"
                || eaElement.Name.ToUpper() == "MESSAGE"
                || eaElement.Name.ToUpper() == "MESSAGES"
                )
                return;

            AddInRepository.Instance.WriteCaliberResults(eaElement.Name + ": Updating element properties", caliberID);


            UpdateUiElementTag(eaElement, "LastUpdatedOn", System.DateTime.Today.ToString());
            UpdateUiElementTag(eaElement, "CaliberID", caliberID);

            if (eaElement.Type == "Requirement")
            {
                // Design rule, just need the Caliber ID
            }
            else
            {
                UpdateUiElementTag(eaElement, "DisplayLabel", uiProperties.DisplayLabel);
                UpdateUiElementTag(eaElement, "AccessibleName", uiProperties.AccessibleName);
                UpdateUiElementTag(eaElement, "AccessibleDescription", uiProperties.AccessibleDescription);
                UpdateUiElementTag(eaElement, "AccessKey", uiProperties.AccessKey);
                UpdateUiElementTag(eaElement, "DisplayLength", uiProperties.DisplayLength);
                UpdateUiElementTag(eaElement, "DisplayFormat", uiProperties.DisplayFormat);
                UpdateUiElementTag(eaElement, "State", uiProperties.State);
                UpdateUiElementTag(eaElement, "FieldSource", uiProperties.FieldSource);
                UpdateUiElementTag(eaElement, "DefaultValue", uiProperties.DefaultValue);
                UpdateUiElementTag(eaElement, "FieldType", uiProperties.FieldType);
                UpdateUiElementTag(eaElement, "Visible", uiProperties.Visible);
                UpdateUiElementTag(eaElement, "Mandatory", uiProperties.Mandatory);
            }
        }

        private static void UpdateUiElementTag<P>(Element eaElement, string propertyName, P uiProperty)
        {
            if (uiProperty != null) 
                EaAccess.addTaggedValue(eaElement, propertyName, uiProperty.ToString());
            
        }

        // ------------------------------------------------------
        //        Return list of images
        // ------------------------------------------------------
        public ImageList GetImageList()
        {
            // Image list
            //
            var imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.ImageWhite); // image 0 
            imageList.Images.Add(Properties.Resources.ImageRootModel); // image 1
            imageList.Images.Add(Properties.Resources.ImagePackage); // image 2
            imageList.Images.Add(Properties.Resources.ImageView); // image 3
            imageList.Images.Add(Properties.Resources.ImageUseCase); // image 4
            imageList.Images.Add(Properties.Resources.ImageClass); // image 5
            imageList.Images.Add(Properties.Resources.ImageAttribute); // image 6
            imageList.Images.Add(Properties.Resources.ImageQuestionMark); // image 7
            imageList.Images.Add(Properties.Resources.ImageRequirement); // image 8
            imageList.Images.Add(Properties.Resources.ImageCategory); // image 9
            imageList.Images.Add(Properties.Resources.ImageActor); // image 10
            imageList.Images.Add(Properties.Resources.ImageScreen); // image 11
            imageList.Images.Add(Properties.Resources.ImageElement); // image 12

            imageList.Images.Add(Properties.Resources.ImageClassNotFound); // image 13
            imageList.Images.Add(Properties.Resources.ImageRequirementNotFound); // image 14
            imageList.Images.Add(Properties.Resources.ImageUseCaseNotFound); // image 15
            imageList.Images.Add(Properties.Resources.ImageActorNotFound); // image 16
            imageList.Images.Add(Properties.Resources.ImageAttributeNotFound); // image 17
            imageList.Images.Add(Properties.Resources.ImageScreenNotFound); // image 18
            imageList.Images.Add(Properties.Resources.ImageElementNotFound); // image 19
            imageList.Images.Add(Properties.Resources.ImageTableNotFound); // image 20
            imageList.Images.Add(Properties.Resources.ImagePackageNotFound); // image 21

            imageList.Images.Add(Properties.Resources.ImageComponent); // image 22
            imageList.Images.Add(Properties.Resources.ImageActivity); // image 23
            imageList.Images.Add(Properties.Resources.ImageDecision); // image 24
            imageList.Images.Add(Properties.Resources.ImageNote); // image 25

            imageList.Images.Add(Properties.Resources.ImageActivityDiagram); // image 26
            imageList.Images.Add(Properties.Resources.ImageSequenceDiagram); // image 27
            imageList.Images.Add(Properties.Resources.ImageClassDiagram); // image 28
            imageList.Images.Add(Properties.Resources.ImageUIDiagram); // image 29
            imageList.Images.Add(Properties.Resources.ImageUseCaseDiagram); // image 30
            imageList.Images.Add(Properties.Resources.ImageCollaboration); // image 31
            imageList.Images.Add(Properties.Resources.ImageOperation); // image 32
            imageList.Images.Add(Properties.Resources.ImageInteractionOccurrence); // image 33
            imageList.Images.Add(Properties.Resources.ImageInterface); // image 34
            imageList.Images.Add(Properties.Resources.ImageASPpage); // image 35
            imageList.Images.Add(Properties.Resources.ImageText); // image 36
            imageList.Images.Add(Properties.Resources.ImageBoundary); // image 37

            imageList.Images.Add(Properties.Resources.ImageActivityPartition); // image 38
            imageList.Images.Add(Properties.Resources.ImageEvent); // image 39
            imageList.Images.Add(Properties.Resources.ImageNotFound); // image 40
            imageList.Images.Add(Properties.Resources.ImageObject); // image 41
            imageList.Images.Add(Properties.Resources.ImageFragment); // image 42
            
            return imageList;
        }


        // ------------------------------------------------------
        //           Display tree from Data Table
        // ------------------------------------------------------
        public void DisplayEaTreePreview(TreeView treeViewEA)
        {
            // Image list
            //
            ImageList imageList = GetImageList();

            // Binding
            treeViewEA.ImageList = imageList;

            // Clear nodes
            treeViewEA.Nodes.Clear();

            int image = 7; // Question Mark

            // Root
            var rootNode = new TreeNode("Business View", 3, 0);
            treeViewEA.Nodes.Add(rootNode);

            TreeNode currentNode = rootNode;
            TreeNode packageNode = rootNode;
            TreeNode root = rootNode;

            foreach (DataRow dr in elementSourceDataTable.Rows)
            {
                image = 7;
                image = GetImageId(dr["DerivedType"].ToString(), (bool) dr[COL_LOADEDEA]);

                if (dr["DerivedType"].ToString() ==
                    Derivedtype.Category.ToString())
                {
                    continue;
                }

                //
                //
                var treeNode = new TreeNode(dr["CaliberRequirement"].ToString(), image, 0);
                treeNode.Tag = dr["CaliberID"].ToString();

                //
                //
                if (dr["DerivedType"].ToString() == Derivedtype.Package.ToString())
                {
                    // Add package to root
                    // treeViewEA.Nodes.Add(treeNode);
                    root.Nodes.Add(treeNode);

                    // Set current node to Package node
                    currentNode = treeNode;
                    packageNode = treeNode;
                }
                else
                {
                    if (dr["DerivedType"].ToString() == Derivedtype.BDO.ToString())
                    {
                        // Add BDO to package
                        packageNode.Nodes.Add(treeNode);
                        currentNode = treeNode;
                    }
                    else
                    {
                        currentNode.Nodes.Add(treeNode);
                    }
                }
            }
        }

        #endregion CALIBER


        #region EA

        // -------------------------------------------------------------------
        //                            Display EA tree
        // -------------------------------------------------------------------
        public void DisplayEaTree(TreeView tv, string startFrom)
        {
            // Image list
            //
            ImageList imageList = GetImageList();

            // Binding
            tv.ImageList = imageList;

            // Clear nodes
            tv.Nodes.Clear();

            string RootPackageGUID = "{719B20CF-B40B-4f67-A4B2-701E0E4C9A9B}";

            Package RootPackage;

            // Find Root Package

            if (startFrom == "Business View")
            {
                RootPackageGUID = "{719B20CF-B40B-4f67-A4B2-701E0E4C9A9B}";
            }

            RootPackage = AddInRepository.Instance.Repository.GetPackageByGuid(RootPackageGUID);

            if (RootPackage == null)
            {
                return;
            }

            var tn = new TreeNode(RootPackage.Name, 2, 0);
            tn.Tag = RootPackage;
            tv.Nodes.Add(tn);

            AddNodeFromEa(RootPackage, tv.TopNode);
        }

        // -------------------------------------------------------------------
        //                            Add node from EA
        // -------------------------------------------------------------------
        private void AddNodeFromEa(Package pk, TreeNode node)
        {
            foreach (Package package in pk.Packages)
            {
                var previousNode = new TreeNode();
                var newNode = new TreeNode(package.Name, 2, 0);
                newNode.Tag = package;

                node.Nodes.Add(newNode);

                if (package.Packages.Count > 0)
                {
                    previousNode = node;
                    node = node.LastNode;

                    AddNodeFromEa(package, node);

                    node = previousNode;
                }
            }
        }

        // -------------------------------------------------------------------
        //              Retrieve elements from a package by GUID
        // -------------------------------------------------------------------
        public void LoadPackageElements(string packageGUID, TreeNode tn)
        {
            Package EAPackage = AddInRepository.Instance.Repository.GetPackageByGuid(packageGUID);
            if (EAPackage != null)
            {
                // List Use Cases and BDOs
                //
                foreach (Element element in EAPackage.Elements)
                {
                    if (element.Type == "Class" || element.Type == "UseCase"
                        || element.Type == "Requirement")
                    {
                        int imagenum = 2;
                        imagenum = GetImageIdUsingEaType(element.Type);
                        var elementNode = new TreeNode(element.Name, imagenum, 0);
                        tn.Nodes.Add(elementNode);
                        elementNode.Tag = element;
                    }
                }
            }
        }

        // -------------------------------------------------------------------
        //                     Retrieve EA element by GUID
        // -------------------------------------------------------------------
        private Element RetrieveEaElementByGuid(string guid)
        {
            Element EAElement = AddInRepository.Instance.Repository.GetElementByGuid(guid);
            if (EAElement == null)
            {
                dtMessageLog.AddErrorMessage("From Element not found " + guid);
            }

            return EAElement;
        }

        // -------------------------------------------------------------------
        //            Retrieve linked elements by stereotype
        // -------------------------------------------------------------------
        public ArrayList GetLinkedElementsByStereotype(Element inElement,
                                                       string filterStereotype)
        {
            var retList = new ArrayList();

            foreach (Connector conn in inElement.Connectors)
            {
                Element element = null;
                ElementConnector elementConnector;

                if (conn.ClientID == inElement.ElementID)
                {
                    // Selected element is the SOURCE
                    element = AddInRepository.Instance.Repository.GetElementByID(conn.SupplierID);
                }

                if (conn.SupplierID == inElement.ElementID)
                {
                    // Selected element is the TARGET
                    element = AddInRepository.Instance.Repository.GetElementByID(conn.ClientID);
                }

                if (element.Stereotype.Contains(filterStereotype))
                {
                    elementConnector.element = element;
                    elementConnector.connector = conn;

                    retList.Add(elementConnector);
                    // retList.Add(element);
                }
            }

            return retList;
        }

        // -------------------------------------------------------------------
        //            Delete all GEN% and table connections.
        // -------------------------------------------------------------------
        public ArrayList DeleteConnectionsGenTable(Element inElement)
        {
            var retList = new ArrayList();

            foreach (Connector conn in inElement.Connectors)
            {
                Element element = null;

                if (conn.ClientID == inElement.ElementID)
                {
                    // Selected element is the SOURCE
                    element = AddInRepository.Instance.Repository.GetElementByID(conn.SupplierID);
                }

                if (conn.SupplierID == inElement.ElementID)
                {
                    // Selected element is the TARGET
                    element = AddInRepository.Instance.Repository.GetElementByID(conn.ClientID);
                }

                if (element.Stereotype.Contains("GEN") ||
                    element.Stereotype == "table")
                {
                    // inElement.Connectors.Delete(
                }
            }

            return retList;
        }

        // -------------------------------------------------------
        //             Retrieve EA element
        // -------------------------------------------------------
        public string GetEaGuid(mtCaliberMapping eaInfo, Attribute EAattribute)

        {
            string ret = "NOT FOUND";
            Element EAElement = null;

            // Retrieve EA info by guid
            EAElement = AddInRepository.Instance.Repository.GetElementByGuid(eaInfo.EA_GUID);

            if (EAElement == null)
            {
                ret = "NOT FOUND";

                if (eaInfo.EAElementType == Derivedtype.Attribute.ToString())

                {
                    // It may be an attribute
                    EAElement = AddInRepository.Instance.Repository.GetElementByGuid(eaInfo.EAParentGUID);

                    if (EAElement != null)
                    {
                        // Find Attribute by GUID
                        foreach (Attribute eaAttri in EAElement.Attributes)
                        {
                            if (eaAttri.AttributeGUID == eaInfo.EA_GUID)
                            {
                                ret = "OK";
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                ret = "OK";
            }
            return ret;
        }


 
        // -------------------------------------------------------------------
        //                              Get element image
        // -------------------------------------------------------------------
        public int GetImageId(string type, bool existsInEA)
        {
            int image = 7;

            if (existsInEA)
            {
                switch (type)
                {
                    case "Actor":
                        image = 10;
                        break;
                    case "Package":
                        image = 2;
                        break;
                    case "Usecase":
                        image = 4;
                        break;
                    case "BDO":
                        image = 5;
                        break;
                    case "MESSAGE":
                        image = 8;
                        break;
                    case "Businessrule":
                        image = 8;
                        break;
                    case "Businessruleparent":
                        image = 8;
                        break;
                    case "Attribute":
                        image = 6;
                        break;
                    case "Category":
                        image = 9;
                        break;
                    case "TABLE":
                        image = 10;
                        break;
                    case "SCREEN":
                        image = 11;
                        break;
                    case "ELEMENT":
                        image = 12;
                        break;
                    case "DESIGNRULE":
                        image = 8;
                        break;
                    case "INTERFACE":
                        image = 8;
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "Actor":
                        image = 16;
                        break;
                    case "Package":
                        image = 21;
                        break;
                    case "Usecase":
                        image = 15;
                        break;
                    case "BDO":
                        image = 13;
                        break;
                    case "MESSAGE":
                        image = 14;
                        break;
                    case "Businessrule":
                        image = 14;
                        break;
                    case "Businessruleparent":
                        image = 14;
                        break;
                    case "Attribute":
                        image = 17;
                        break;
                    case "Category":
                        image = 9;
                        break;
                    case "TABLE":
                        image = 20;
                        break;
                    case "SCREEN":
                        image = 18;
                        break;
                    case "ELEMENT":
                        image = 19;
                        break;
                    case "DESIGNRULE":
                        image = 14;
                        break;
                }
            }
            return image;
        }


        // -------------------------------------------------------------------
        //                     Get element image based on EA Type
        // -------------------------------------------------------------------
        public static int GetImageIdUsingEaType(string type)
        {
            int image = 7;

            switch (type)
            {
                case "Actor":
                    image = 10;
                    break;
                case "Package":
                    image = 2;
                    break;
                case "UseCase":
                    image = 4;
                    break;
                case "Class":
                    image = 5;
                    break;
                case "Requirement":
                    image = 8;
                    break;
                case "Attribute":
                    image = 6;
                    break;
                case "Screen":
                    image = 11;
                    break;
                case "GUIObject":
                    image = 12;
                    break;
                case "Component":
                    image = 22;
                    break;
                case "Activity":
                    image = 23;
                    break;
                case "Decision":
                    image = 24;
                    break;
                case "ActivityPartition":
                    image = 38;
                    break;
                case "Event":
                    image = 39;
                    break;
                case "Note":
                    image = 25;
                    break;
                case "Collaboration":
                    image = 31;
                    break;
                case "Operation":
                    image = 32;
                    break;
                case "InteractionOccurrence":
                    image = 33;
                    break;
                case "Interface":
                    image = 34;
                    break;
                case "Artifact":
                    image = 35;
                    break;
                case "Text":
                    image = 36;
                    break;
                case "Boundary":
                    image = 37;
                    break;
                case "Entity":
                    image = 5;
                    break;
                case "UMLDiagram":
                    image = 37;
                    break;
                case "GUIElement":
                    image = 12;
                    break;
                case "Object":
                    image = 41;
                    break;
                case "InteractionFragment":
                    image = 42;
                    break;

            }

            return image;
        }


        // -------------------------------------------------------------------
        //                     Get element image based on EA Type
        // -------------------------------------------------------------------
        public static int GetDiagramImageId(string type)
        {
            int image = 28;

            switch (type)
            {
                case "Activity":
                    image = 26;
                    break;
                case "Sequence":
                    image = 27;
                    break;
                case "Object":
                    image = 28;
                    break;
                case "Custom":
                    image = 29;
                    break;
                case "Use Case":
                    image = 30;
                    break;
            }

            return image;
        }


        // -------------------------------------------------------------------
        //             Find or Create package for use case
        // -------------------------------------------------------------------
        private Package CreateEaPackage(Package parentPackage, string packageName)
        {
            Package package = null;

            package = (Package) parentPackage.Packages.AddNew(packageName, "type");
            try
            {
                package.Update();
            }
            catch
            {
                //MessageBox.Show("First Message: " + e.ToString() + " We will try again..");
                //MessageBox.Show("...and "+package.GetLastError());

                package.Name = packageName;
                package.StereotypeEx = "Package";


                try
                {
                    package.Update();
                }
                catch (Exception e2)
                {
                    MessageBox.Show("Second Try msg: " + e2);
                }
            }

            package.Packages.Refresh();
            parentPackage.Packages.Refresh();

            return package;
        }

        // -------------------------------------------------------------------
        //             Update GEN stereotype
        // -------------------------------------------------------------------
        public static void UpdateGenStereotype(Package package)
        {
            string st = "";

            foreach (Element element in package.Elements)
            {
                if (element.Type == "Class")
                {
                    if (element.StereotypeEx.Contains("COOLGen CAB"))
                        st = "GEN CAB";

                    if (element.StereotypeEx.Contains("COOLGen Wrapper"))
                        st = "GEN Wrapper";
                }

                if (st != "")
                {
                    element.StereotypeEx = "";
                    element.Stereotype = st;
                    try
                    {
                        element.Update();
                        element.Refresh();
                    }
                    catch 
                    {
                        MessageBox.Show("Element can't be updated by you. " + element.Name);
                    }
                }

                // Update CAB name
                // Remove extra "(" from the name
                if (element.Name.Contains("("))
                {
                    int p = element.Name.IndexOf("(");

                    string newName = element.Name.Substring(0, p);
                    newName.Trim();

                    element.Name = newName;
                    element.Update();
                    element.Refresh();
                }
            }
        }

        // -------------------------------------------------------------------
        //             Update EA stereotype
        // -------------------------------------------------------------------
        public static void UpdateEaStereotype(Package package, string stereotype)
        {
            if (stereotype == null)
                stereotype = "";

            foreach (Element element in package.Elements)
            {
                element.StereotypeEx = "";
                element.Stereotype = stereotype;
                try
                {
                    element.Update();
                    element.Refresh();
                }
                catch 
                {
                    MessageBox.Show("Element can't be updated by you. " + element.Name);
                }
            }
        }

        // -------------------------------------------------------------------
        //             Update EA stereotype
        // -------------------------------------------------------------------
        public static void UpdateEAStatus(Package package, string status)
        {
            if (status == null)
                status = "";

            foreach (Element element in package.Elements)
            {
                element.Status = "";
                element.Status = status;
                try
                {
                    element.Update();
                    element.Refresh();
                }
                catch
                {
                    MessageBox.Show("Element can't be updated by you. " + element.Name);
                }
            }
        }


        // -------------------------------------------------------------------
        //             Add audit footprint columns  (DEPRECATED)
        // -------------------------------------------------------------------
        public string AddAuditFootprintColumns(Element table)
        {
            string ret = "Done.";

            string[] col = {
                               "UPDATE_USER_ID"
                               , "UPDATE_DATE"
                               , "UPDATE_TIME"
                               , "CREATION_USER_ID"
                               , "CREATION_DATE"
                               , "CREATION_TIME"
                               , "INTEGRITY_CNTL_NUM"
                           };

            string[] type = {
                                "char"
                                , "date"
                                , "time"
                                , "char"
                                , "date"
                                , "time"
                                , "smallint"
                            };

            string[] create = {
                                  "YES"
                                  , "YES"
                                  , "YES"
                                  , "YES"
                                  , "YES"
                                  , "YES"
                                  , "YES"
                              };


            string[] length = {
                                  "8"
                                  , ""
                                  , ""
                                  , "8"
                                  , ""
                                  , ""
                                  , ""
                              };


            if (table.Stereotype != "table")
                ret = "Element selected is not a table";

            if (table.Type != "Class")
                ret = "Element selected is not a Class";

            // Check if footprint has been added
            int cnt = 0;
            foreach (Attribute attr in table.Attributes)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    if (attr.Name == col[i])
                    {
                        create[cnt] = "NO";
                        break;
                    }
                }
                cnt++;
            }

            // Create the missing ones
            for (int i = 0; i < col.Length; i++)
            {
                if (create[i] == "YES")
                {
                    Attribute eaAttribute;
                    eaAttribute = (Attribute) table.Attributes.AddNew(col[i], type[i]);
                    eaAttribute.AllowDuplicates = true;
                    eaAttribute.Stereotype = "column";
                    if (length[i] != "")
                    {
                        eaAttribute.Length = length[i];
                    }
                    eaAttribute.Update();
                    table.Update();
                    table.Refresh();
                }
            }

            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();
            if (diag != null)
            {
                diag.DiagramObjects.Refresh();
            }


            Package currentPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            if (currentPackage != null)
            {
                currentPackage.Elements.Refresh();
                currentPackage.Diagrams.Refresh();
                currentPackage.Packages.Refresh();
                currentPackage.Connectors.Refresh();
            }


            return ret;
        }


        // -------------------------------------------------------------------
        //       Clean class
        // -------------------------------------------------------------------
        public static void CleanClass(Element elementClass)
        {
            if (elementClass.Type != "Class")
                return;

            // Fix Name of the Class
            elementClass.Name = FixName(elementClass.Name);
            elementClass.Update();
            elementClass.Refresh();

            // Fix Attribute Name
            foreach (Attribute atr in elementClass.Attributes)
            {
                if (atr.Name.Contains("*"))
                {
                    atr.Stereotype = "mandatory";
                }
                atr.Name = FixName(atr.Name);
                atr.Update();
            }

            // Fix method name
            foreach (Method met in elementClass.Methods)
            {
                met.Name = FixName(met.Name);
                met.Update();
            }

            elementClass.Update();
            elementClass.Attributes.Refresh();
            elementClass.Methods.Refresh();
            elementClass.Refresh();

            // Look For missing methods
            var tempApply = new EaAccess();
            tempApply.applyTemplate(elementClass);

            Package currentPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            currentPackage.Elements.Refresh();
            currentPackage.Diagrams.Refresh();
            currentPackage.Packages.Refresh();
            currentPackage.Connectors.Refresh();
        }

        // -------------------------------------------------------------------
        //  Fix Name according to standards
        // -------------------------------------------------------------------
        public static string FixName(string name)
        {
            string ret = "";
            //
            //1) Make the start of each word Capitalised
            //2) Remove any funny symbols (^*&~_ )

            char[] toRemove = {
                                  '"', '!', '@', '^', '*', '&', '~', '(', ')', '[', ']', '{', '}',
                                  '!', '#', '$', '%', '-', '+', ' ', '_'
                              };
            bool makeNextUpper = true;
            bool lastWasUpper = false;

            for (int x = 0; x < name.Length; x++)
            {
                var chars = name.Substring(x, 1).ToCharArray();
                var nameChar = chars[0];

                if (toRemove.Contains(nameChar))
                {
                    makeNextUpper = true;
                    continue;
                }

                if (makeNextUpper)
                {
                    nameChar = Char.ToUpper(nameChar);
                }
                else if (Char.IsUpper(nameChar) && lastWasUpper)
                {
                    nameChar = Char.ToLower(nameChar);
                }

                if (chars[0] == Char.ToUpper(chars[0]) || makeNextUpper)
                {
                    lastWasUpper = true;
                }
                else
                {
                    lastWasUpper = false;
                }

                makeNextUpper = false;
                
                ret = ret + nameChar;
            }
            return ret;
        }

        public struct ElementConnector
        {
            public Connector connector;
            public Element element;
        }

        #endregion EA
    }
}
