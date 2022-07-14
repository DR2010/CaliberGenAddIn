using System;
using System.Collections;
using System.Data;
using System.Xml.Serialization;


namespace EAStructures
{
    public struct errorCode
    {
        public int returnCode;
        public int reasonCode;
        public string Message;
    }


    public struct EAelementSourceDataTable
    {
        public DataTable listOfCABs;

    }

    public struct GenEAList
    {   
        public string GENModule;
        public string GENElementName;
        public string GENElementType;
        public long MapTableUniqueID;
        public string EA_GUID;
        public string EAElementType;
        public string EAStereotype;
        public string EAStatus;
        public string SyncStatus;
    }
    
    public struct CABMappingRecord
    {
        public string ea_guid;
        public string CAB;
    }
    public struct TableMappingRecord
    {
        public string ea_guid;
        public string TableName;
    }


    public struct CheckDBAvailabilityRequest
    {
        public SecurityInfo secInfo;
    }

    public struct SecurityInfo
    {
        public string userID;
        public string password;
        public string dataBase;
        public string EAGenCaliberSQL2005Repository;
        public string EARepository;
        public string MigrateToolDB;
        public string GENCabNumSQLRepository;
        public string packageDestination;
        public bool sqlServer2005;


    }


}