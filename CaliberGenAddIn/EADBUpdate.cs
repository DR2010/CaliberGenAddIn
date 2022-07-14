using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;
using EAAddIn.Windows;
using EAStructures;
using File = System.IO.File;
using System.Data;
using System.Data.SqlClient;
using EA;
using Attribute=EA.Attribute;

namespace EAAddIn
{
    public class EadbUpdate
    {

        private readonly string _eaRepository;
        private SqlConnection _eadbConnection;
        
        public string Author;
        public string EA_GUID;
        public string EAStatus;
        public string Module;
        public string Name;
        public string Note;
        public int ElementID;

        public EadbUpdate()
        {
            var dbcon = new dbConnections();

            _eaRepository = AddInRepository.Instance.ConnectionStringshort;
        }

        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.Minor + "." + 
                         Assembly.GetExecutingAssembly().GetName().Version.Build; }
        }

        //-----------------------------------------------------
        // Update EA Element with existing SQL Connection
        //-----------------------------------------------------
        public string UpdateEaElement_old()
        {

            _eadbConnection = new SqlConnection(_eaRepository);
            _eadbConnection.Open();

            SqlCommand sqlCommand1 = _eadbConnection.CreateCommand();

            string ret = "Item updated successfully";

            if (AddInRepository.Instance.ReadOnly)
            {
                return ret;
            }

            sqlCommand1.CommandText = string.Format(
                "UPDATE t_object" +
                "   SET [Note] = '{0}' " +
                "   WHERE  " +
                "          Object_ID = {1}", this.Note, this.ElementID);

            try
            {
                sqlCommand1.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ret = ex.ToString();
            }

            _eadbConnection.Close();

            AddInRepository.Instance.Repository.RefreshOpenDiagrams(false);

            return ret;
        }

        //-----------------------------------------------------
        // Update EA Element with existing SQL Connection
        //-----------------------------------------------------
        public string UpdateEaElementNotes()
        {
            string ret = "Item updated successfully";

            if (AddInRepository.Instance.ReadOnly)
                return "Error. No EA Instance.";

            using (var connection = new SqlConnection(_eaRepository))
            {
                var commandString = string.Format(
                    "UPDATE t_object" +
                    "   SET [Note] = '{0}' " +
                    "   WHERE  " +
                    "          Object_ID = {1}", Note, ElementID);

                using (var command = new SqlCommand(
                    commandString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            AddInRepository.Instance.Repository.RefreshOpenDiagrams(false);

            return ret;
        }

        
        // ----------------------------------------------------------
        //       Retrieve tagged value for an element
        // ----------------------------------------------------------
        public SqlTaggedValue SqlGetTaggedValue(string tag, int elementID)
        {
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(_eaRepository);
            _eadbConnection.Open();

            // Get object by tagged value
            //
            SqlTaggedValue ret = new SqlTaggedValue();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   obj.ea_guid elemguid" +
                "  ,obj.Object_ID " +
                "  ,obj.Object_Type " +
                "  ,obj.Diagram_ID " +
                "  ,obj.Name " +
                "  ,obj.Package_ID " +
                "  ,obj.Stereotype " +
                "  ,obj.Status " +
                "  ,obj.Author " +
                "  ,obj.ParentID " +
                "  ,tag.PropertyID " +
                "  ,tag.Property " +
                "  ,tag.Value " +
                "  ,tag.Notes " +
                "  ,tag.ea_guid tagguid " +
                " FROM t_object obj, " +
                "     t_objectproperties tag" +
                " WHERE " +
                "      tag.[Object_ID] = obj.[Object_ID] " +
                " AND  tag.[Object_ID] = '{0}' " +
                " AND  tag.[Property]  = '{1}' "
                , elementID
                , tag);

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        ret.XPropertyId = Convert.ToInt32( reader["PropertyID"] );
                        ret.XValue = reader["Value"].ToString();
                        ret.XElementId = elementID;
                        ret.XNotes = reader["Notes"].ToString();
                        ret.XProperty = reader["Property"].ToString();
                    }

                    reader.Close();
                }
            }
            catch (Exception)
            {
                ret.XValue = "";
            }
            _eadbConnection.Close();

            return ret;
        }


        // ----------------------------------------------
        //     SAVE tagged value (using in-line SQL)
        // ----------------------------------------------
        public SqlTaggedValue SqlSaveTaggedValue(SqlTaggedValue element)
        {
            SqlTaggedValue ret = new SqlTaggedValue();

            // Element is mandatory
            if (element.XElementId <= 0)
                return ret;

            SqlTaggedValue sqltv =
                SqlGetTaggedValue(element.XProperty, element.XElementId);

            sqltv.XValue = element.XValue;

            if (sqltv.XElementId > 0)
            {

                // Tag value exists, update
                SqlUpdateTaggedValue(sqltv);

            }
            else
            {
                sqltv.XElementId = element.XElementId;
                sqltv.XNotes = element.XNotes;
                sqltv.XProperty = element.XProperty;
                sqltv.XValue = element.XValue;

                // New tagged value
                ret = 
                        SqlAddTaggedValue(sqltv);
            }

            return ret;
        }

        // ----------------------------------------------
        //          Structure SqlTaggedValue
        // ----------------------------------------------
        public struct SqlTaggedValue
        {
            public int XPropertyId;
            public int XElementId;
            public string XProperty;
            public string XValue;
            public string XNotes;
            public string XGuid;
        }

        // ----------------------------------------------
        //   ADD tagged value (using in-line SQL)
        // ----------------------------------------------
        public SqlTaggedValue SqlAddTaggedValue(SqlTaggedValue element)
        {

            //
            // Create link in mapping table
            //

            SqlTaggedValue ret = new SqlTaggedValue();

            _eadbConnection = new SqlConnection(_eaRepository);
            _eadbConnection.Open();

            string guid = "{" + System.Guid.NewGuid().ToString() + "}";


            new SqlCommand();
            var sqlCommand = _eadbConnection.CreateCommand();

            sqlCommand.CommandText =
                string.Format(
                    "INSERT into [t_objectproperties] " +
                    "( [Object_ID],[Property],[Value],[Notes],[ea_guid] ) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    element.XElementId,
                    element.XProperty,
                    element.XValue,
                    element.XNotes,
                    guid
                    );

            try
            {
                sqlCommand.ExecuteNonQuery();

                ret.XElementId = element.XElementId;
                ret.XNotes = element.XNotes;
                ret.XProperty = element.XProperty;
                ret.XValue = element.XValue;
                ret.XGuid = guid;
            }
            catch (SqlException)
            {
                
            }

            _eadbConnection.Close();

            return ret;
        }


        // ----------------------------------------------
        //   UPDATE tagged value (using in-line SQL)
        // ----------------------------------------------
        public string SqlUpdateTaggedValue(SqlTaggedValue element)
        {

            if (element.XPropertyId <= 0)
                return "Error = Property ID is empty.";

            if (string.IsNullOrEmpty(element.XProperty))
                return "Error = Property is spaces.";

            if (string.IsNullOrEmpty(element.XValue))
                return "Error = Value is spaces.";

            var ret = "Ok";

            _eadbConnection = new SqlConnection(_eaRepository);
            _eadbConnection.Open();

            new SqlCommand();
            var sqlCommand = _eadbConnection.CreateCommand();

            sqlCommand.CommandText =
                string.Format(
                                "UPDATE [t_objectproperties] " +
                                "SET  " +
                                "   [Value] = '{0}' " +
                                "  ,[Notes] = '{1}' " +
                                " WHERE " +
                                "   [PropertyID] =  {2} ",
                                element.XValue,
                                element.XNotes,
                                element.XPropertyId
                              );

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ret = ex.ToString();
            }

            _eadbConnection.Close();

            return ret;
        }

 
    }
}


