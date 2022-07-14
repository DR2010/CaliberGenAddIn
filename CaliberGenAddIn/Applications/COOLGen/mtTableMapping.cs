using System.Data.SqlClient;
using EA;
using EAAddIn.Applications;

namespace EAAddIn
{
    internal class mtTableMapping
    {
        public string alternateName;
        public string EA_GUID;
        public string EARepository;
        public string EATableName;
        public string MappingTable;
        public string tableName;

        public mtTableMapping()
        {
            //var dbcon = new dbConnections();

            //EARepository = dbcon.CSEARepository;
            //MappingTable = dbcon.csCaliberEAMapping;
        }

        //
        // Get EA GUID for a given CAB from the Mapping table
        //
        public bool getGuidByTableName(SqlConnection MyConnection)
        {
            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();
            bool found = false;

            //string EA_GUID = "";

            sqlCommand1.CommandText =
                string.Format("SELECT TableName, EA_GUID, AlternateName from TableMapping where TableName = '{0}'",
                              tableName);
            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader.Read())
            {
                EA_GUID = reader["EA_GUID"].ToString();
                tableName = reader["TableName"].ToString();
                alternateName = reader["AlternateName"].ToString();
                found = true;
            }
            else
            {
                EA_GUID = null;
                alternateName = "";
                found = false;
            }

            reader.Close();

            return found;
        }

        //
        // Add record to mapping table
        //
        public string add()
        {
            
            string ret = "Item created successfully";

            var sqlCommand = new SqlCommand(
                string.Format("INSERT into TableMapping " +
                              "(TableName, EA_GUID, AlternateName) " +
                              " VALUES ('{0}', '{1}', '{2}')",
                              tableName,
                              EA_GUID,
                              alternateName), SqlHelpers.MappingDbConnection);

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ret = ex.ToString();
            }

            return ret;
        }

        //
        // Get EA GUID for a given CAB from the Mapping table
        //
        public bool getTableNamebyGUID(SqlConnection MyConnection)
        {
            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();
            bool found = false;

            //string EA_GUID = "";

            sqlCommand1.CommandText =
                string.Format("SELECT TableName, EA_GUID, AlternateName from TableMapping where EA_GUID = '{0}'",
                              EA_GUID);
            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader.Read())
            {
                EA_GUID = reader["EA_GUID"].ToString();
                tableName = reader["TableName"].ToString();
                alternateName = reader["AlternateName"].ToString();
                found = true;
            }
            else
            {
                tableName = "";
                alternateName = "";
                found = false;
            }

            reader.Close();

            return found;
        }

        // ------------------------------------------------------
        //  Get EA GUID for a given Table 
        //  29/01/2009 - Using EA tagged value.
        // ------------------------------------------------------
        public Element getTableByTaggedValue()
        {
            // 29/01/2009 - Retrieve info from Tagged value

            var eaaccess = new EaAccess();
            Element EAElement = null;

            // Try to get element using GenLogicalName
            //
            EaAccess.sElementTag tableByTag =
                eaaccess.getElementByTaggedValue("GenLogicalName", alternateName, new string[] { "Snapshot" });

            if (tableByTag.elemguid == null)
            {
                tableByTag =
                eaaccess.getElementByTaggedValue("DB2PhysicalName", alternateName, new string[] { "Snapshot" });
            }

            if (string.IsNullOrEmpty(tableByTag.elemguid))
            {
                EA_GUID = null;
            }
            else
            {

                tableName = tableByTag.Name;
                EA_GUID = tableByTag.elemguid;

                EAElement = AddInRepository.Instance.Repository.GetElementByGuid(EA_GUID);

                if (EAElement != null)
                {
                    // Retrieve DB2 Physical Name also.
                    // It does not try GENLogical and then DB2 - it only uses GEN Logical to match.

                    var tv
                        = (TaggedValue)EAElement.TaggedValues.GetByName(
                                            "DB2PhysicalName");
                    if (tv != null)
                    {
                        EATableName = tv.Value;
                    }
                }
            }

            return EAElement;
        }

        public Element getTableByName()
        {
            // 29/01/2009 - Retrieve info from Tagged value


            EaAccess.sElementTag tableByTag = new EaAccess().getElementByStereotype("table", alternateName); ;

            if (string.IsNullOrEmpty(tableByTag.elemguid))
            {
                EA_GUID = null;
                return null;
            }
            else
            {

                tableName = tableByTag.Name;
                EA_GUID = tableByTag.elemguid;

                return AddInRepository.Instance.Repository.GetElementByGuid(EA_GUID);
            }
        }


        //
        // Check if mapping table record exists for a given EA GUID
        //
        public bool checkMappingRecordExistsGUID(SqlConnection MyConnection)
        {
            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();
            bool found = false;

            sqlCommand1.CommandText =
                string.Format("SELECT TableName, EA_GUID, AlternateName from TableMapping where EA_GUID = '{0}'",
                              EA_GUID);
            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader.Read())
            {
                found = true;
            }
            else
            {
                found = false;
            }

            reader.Close();

            return found;
        }


        //
        // Delete record from mapping table
        //
        public string delete(SqlConnection MyConnection)
        {
            string ret = "Item deleted successfully";

            var sqlCommand = new SqlCommand();
            sqlCommand = MyConnection.CreateCommand();

            if (tableName == "" || tableName == null)
            {
                ret = "Table Name must be supplied.";
                return ret;
            }

            sqlCommand.CommandText =
                "DELETE from TableMapping " +
                "WHERE " +
                string.Format(" TableName = '{0}' ", tableName);

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ret = ex.ToString();
            }

            return ret;
        }

        //
        // update record to the mapping table
        //
        public string update(SqlConnection MyConnection)
        {
            string ret = "Updated Successfully.";

            if (EA_GUID == null || EA_GUID == "")
            {
                ret = "EA_GUID must be supplied.";
                return ret;
            }

            if (tableName == null || tableName == "")
            {
                ret = "Table Name must be supplied.";
                return ret;
            }

            if (alternateName == null || alternateName == "")
            {
                ret = "Table Name must be supplied.";
                return ret;
            }

            var sqlCommand = new SqlCommand();
            sqlCommand = MyConnection.CreateCommand();

            sqlCommand.CommandText =
                string.Format("UPDATE TableMapping " +
                              " SET AlternateName = '{0}' " +
                              "    WHERE EA_GUID  = '{1}'",
                              alternateName,
                              EA_GUID);

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ret = ex.ToString();
            }

            return ret;
        }


        //
        // SAVE (add/ update) record to the mapping table
        //
        public string save(SqlConnection MapTableConnection)
        {
            string ret = "Saved Successfully.";

            if (EA_GUID == null || EA_GUID == "")
            {
                ret = "EA_GUID must be supplied.";
                return ret;
            }

            if (alternateName == null || alternateName == "")
            {
                ret = "Table Name must be supplied.";
                return ret;
            }

            // If mapping found, update.
            if (checkMappingRecordExistsGUID(MapTableConnection))
            {
                if (update(MapTableConnection) != "Updated Successfully.")
                {
                    ret = "Save failed.";
                }
            }
            else
            {
                if (tableName == null || tableName == "")
                {
                    ret = "Table Name must be supplied.";
                    return ret;
                }

                if (add() != "Item created successfully")
                {
                    ret = "Add Failed.";
                }
            }

            return ret;
        }


        //
        // Count orphan rows in Mapping Table
        //
        public int getOrfanRowsCount(SqlConnection MyConnection)
        {
            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();
            int numOfOrphanRows;

            //string EA_GUID = "";

            sqlCommand1.CommandText = string.Format("SELECT TableName, EA_GUID, AlternateName from TableMapping");
            SqlDataReader reader = sqlCommand1.ExecuteReader();

            numOfOrphanRows = 0;

            while (reader.Read())
            {
                string iEA_GUID = reader["EA_GUID"].ToString();
                //string itableName = reader["TableName"].ToString();
                //string ialternateName = reader["AlternateName"].ToString();

                // Retrieve EA info by guid
                Element EAElement = AddInRepository.Instance.Repository.GetElementByGuid(iEA_GUID);
                if (EAElement == null)
                {
                    numOfOrphanRows++;
                }
            }

            reader.Close();

            return numOfOrphanRows;
        }
    }
}