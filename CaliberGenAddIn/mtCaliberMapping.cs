using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EA;
using EAStructures;

namespace EAAddIn
{
    // -------------------------------------------------------------
    //     Use Case Mapping Table
    // -------------------------------------------------------------
    public class mtCaliberMapping
    {
        public string UniqueID;
        public int CaliberID;
        public int EAElementID;
        public string CaliberName;
        public string CaliberHierarchy;
        public string EA_GUID;
        public string EAParentGUID;
        public int EAParentID;
        public string UIDesignRulePackage;
        public string EAElementType;
        public string CaliberFullDescription;
        public SecurityInfo secinfo;
        public EaCaliberGenEngine EAEngine;

        public mtCaliberMapping()
        {

            var dbcon = new dbConnections();
            secinfo.EARepository = dbcon.CSEARepository;
            secinfo.EAGenCaliberSQL2005Repository = dbcon.csCaliberEAMapping;
            secinfo.MigrateToolDB = dbcon.csMigrateTool;

            EAEngine = new EaCaliberGenEngine(secinfo, "caliber");

        }

        // 
        // Get EA GUID for a given CAB from the Mapping table
        //
        public void getDetails(int caliberID)
            //,SqlConnection MyConnection)
        {
            SqlConnection MyConnection = EaCaliberGenEngine.MappingTableConnection;

            if (AddInRepository.Instance.ReadOnly)
            {
                return;
            }

            SqlCommand sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            sqlCommand1.CommandText =
                string.Format("SELECT UniqueID, CaliberID, CaliberName , CaliberHierarchy, " +
                              "EA_GUID, EAParentGUID, EAElementType, CaliberFullDescription, "+
                              "EAElementID, UIDesignRulePackage  " +
                              "from CaliberMapping where CaliberID = {0}", caliberID.ToString());

            SqlDataReader reader = sqlCommand1.ExecuteReader();
            if (reader.Read())
            {
                UniqueID                = reader["UniqueID"].ToString();
                CaliberID               = Convert.ToInt32(reader["CaliberID"]);
                CaliberName             = reader["CaliberName"].ToString();
                CaliberHierarchy        = reader["CaliberHierarchy"].ToString();
                EA_GUID                 = reader["EA_GUID"].ToString().Trim();
                EAParentGUID            = reader["EAParentGUID"].ToString();
                EAElementType           = reader["EAElementType"].ToString();
                CaliberFullDescription  = reader["CaliberFullDescription"].ToString();
                UIDesignRulePackage     = reader["UIDesignRulePackage"].ToString();

                if (reader["EAElementID"] != null)
                {
                    EAElementID = Convert.ToInt32(reader["EAElementID"]);
                }
            }

            reader.Close();

            return;
        }



        // 
        // Get Info from Mapping table by EA GUID
        //
        public void getDetails(string EA_GUID)
            //,SqlConnection MyConnection)
        {
            SqlConnection MyConnection = EaCaliberGenEngine.MappingTableConnection;

            if (AddInRepository.Instance.ReadOnly)
            {
                return;
            }

            SqlCommand sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            sqlCommand1.CommandText =
                string.Format("SELECT UniqueID, CaliberID, CaliberName , CaliberHierarchy, " +
                              "EA_GUID, EAParentGUID, EAElementType, CaliberFullDescription, EAElementID, " +
                              " UIDesignRulePackage " +
                              "from CaliberMapping where EA_GUID = '{0}'", EA_GUID);

            SqlDataReader reader = sqlCommand1.ExecuteReader();
            if (reader.Read())
            {
                UniqueID = reader["UniqueID"].ToString();
                CaliberID = Convert.ToInt32(reader["CaliberID"]);
                CaliberName = reader["CaliberName"].ToString();
                CaliberHierarchy = reader["CaliberHierarchy"].ToString();
                EA_GUID = reader["EA_GUID"].ToString().Trim();
                EAParentGUID = reader["EAParentGUID"].ToString();
                EAElementType = reader["EAElementType"].ToString();
                CaliberFullDescription = reader["CaliberFullDescription"].ToString();
                UIDesignRulePackage = reader["UIDesignRulePackage"].ToString();

                if (reader["EAElementID"] != null)
                {
                    EAElementID = Convert.ToInt32(reader["EAElementID"]);
                }
            }

            reader.Close();

            return;
        }




        //
        // Update mapping table
        //
        public string update(SqlConnection MyConnection)
        {

            string ret = "Item updated successfully";

            if (AddInRepository.Instance.ReadOnly)
            {
                return ret;
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand = MyConnection.CreateCommand();

            sqlCommand.CommandText =
                    "UPDATE CaliberMapping " +
                    string.Format(
                    " SET UIDesignRulePackage    = '{0}'  ",
                        UIDesignRulePackage
                    ) + " WHERE " +
                    string.Format(" CaliberID = {0} ", CaliberID);

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
        // Delete record from mapping table
        //
        public string delete(SqlConnection MyConnection)
        {
            string ret = "Item deleted successfully";

            if (AddInRepository.Instance.ReadOnly)
            {
                return ret;
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand = MyConnection.CreateCommand();

            sqlCommand.CommandText =
                "DELETE from CaliberMapping " +
                "WHERE " +
                string.Format(" CaliberID = {0} ", CaliberID);

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
        // Delete record from mapping table
        //
        public string delete()
        {

            string ret = "Item deleted successfully";

            SqlConnection MyConnection = EaCaliberGenEngine.MappingTableConnection;

            if (AddInRepository.Instance.ReadOnly)
            {
                return "";
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand = MyConnection.CreateCommand();

            sqlCommand.CommandText =
                "DELETE from CaliberMapping " +
                "WHERE " +
                string.Format(" CaliberID = {0} ", CaliberID);

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
        // Add record to mapping table
        //
        public string add()
        {
            SqlConnection MyConnection = EaCaliberGenEngine.MappingTableConnection;
            
            string ret = "Item created successfully";

            if (AddInRepository.Instance.ReadOnly)
            {
                return ret;
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand = MyConnection.CreateCommand();

            sqlCommand.CommandText =
                "INSERT into CaliberMapping " +
                "( CaliberID, CaliberName, CaliberHierarchy, " +
                " EA_GUID, EAParentGUID, EAElementType, "+
                " CaliberFullDescription, EAElementID, UIDesignRulePackage ) " +
                string.Format(" VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}')",
              CaliberID,
              CaliberName.Replace("'", " "),
              CaliberHierarchy,
              EA_GUID,
              EAParentGUID,
              EAElementType,
              CaliberFullDescription.Replace("'", " "),
              EAElementID,
              UIDesignRulePackage
            );


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

    }

}
