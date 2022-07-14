using System;
using System.Collections;
using System.Data.SqlClient;

namespace EAAddIn
{
    internal class DEEWREAReports
    {
        private readonly string EARepository;
        private SqlConnection EADBConnection;

        public DEEWREAReports()
        {
            //var dbcon = new dbConnections();

            //EARepository = dbcon.CSEARepository;
        }

        //
        // List one duplicated item
        //
        
        #region Nested type: objectTag

        public struct objectTag
        {
            public string objectEAGUID;
            public int objectID;
            public string objectName;
            public string objectStereotype;
            public string objectType;
            public int tagID;
            public string tagValue;

            public string UniqueID;
            public int CaliberID;
            public string CaliberName;
            public string CaliberHierarchy;
            public string EA_GUID;
            public string EAParentGUID;
            public string EAElementType;
            public string CaliberFullDescription;
            public string UIDesignRulePackage;


        }

        #endregion

        #region Nested type: taggedDuplicate

        public struct taggedDuplicate
        {
            public string tagCount;
            public string tagValue;
        }

        #endregion
    }
}