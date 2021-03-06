using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;
using EAAddIn.Applications;
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

        
        public string Note;
        public int ElementID;
        private readonly SqlConnection EaDbConnection;

        public EadbUpdate( )
        {
            EaDbConnection = SqlHelpers.GetDbConnectionForConnectionString();
        }

      

      
        public string UpdateEaElementNotes()
        {
            string ret = "Item updated successfully";

            if (AddInRepository.Instance.ReadOnly)
                return "Error. No EA Instance.";

            var commandString = string.Format(
                "UPDATE t_object" +
                "   SET [Note] = '{0}' " +
                "   WHERE  " +
                "          Object_ID = {1}", Note, ElementID);

            var command = new SqlCommand(commandString, EaDbConnection);
            command.ExecuteNonQuery();
            AddInRepository.Instance.Repository.RefreshOpenDiagrams(false);
            return ret;
        }


        // ----------------------------------------------------------
        //       Retrieve tagged value for an element
        // ----------------------------------------------------------
        public SqlTaggedValue SqlGetTaggedValue(string tag, int elementID)
        {
            SqlTaggedValue ret = new SqlTaggedValue();

            var sqlCommand1 = new SqlCommand(string.Format(
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
                , tag), EaDbConnection);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            try
            {

                if (reader.Read())
                {
                    ret.XPropertyId = Convert.ToInt32(reader["PropertyID"]);
                    ret.XValue = reader["Value"].ToString();
                    ret.XElementId = elementID;
                    ret.XNotes = reader["Notes"].ToString();
                    ret.XProperty = reader["Property"].ToString();
                }
            }
            finally
            {
                reader.Close();

            }
            return ret;
        }


        // ----------------------------------------------
        //     SAVE tagged value (using in-line SQL)
        // ----------------------------------------------
        public SqlTaggedValue SqlSaveTaggedValue(SqlTaggedValue element)
        {
            var ret = new SqlTaggedValue();

            // Element is mandatory
            if (element.XElementId <= 0)
                return ret;

            var sqltv = 
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

            string guid = "{" + System.Guid.NewGuid().ToString() + "}";


            var sqlCommand = new SqlCommand(string.Format(
                "INSERT into [t_objectproperties] " +
                "( [Object_ID],[Property],[Value],[Notes],[ea_guid] ) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                element.XElementId,
                element.XProperty,
                element.XValue,
                element.XNotes,
                guid), EaDbConnection);

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


            var sqlCommand = new SqlCommand(
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
                    ), EaDbConnection);

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


