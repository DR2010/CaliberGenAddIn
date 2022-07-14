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
            var dbcon = new dbConnections();

            EARepository = dbcon.CSEARepository;
        }

        //
        // Duplicate tagged values list
        //
        public ArrayList duplicateTaggedValue(string taggedValue)
        {
            var retArray = new ArrayList();

            //
            // EA SQL database
            //
            EADBConnection = new SqlConnection(EARepository);
            EADBConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = EADBConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                " SELECT                                             " +
                "          Value    tagValue,                        " +
                "          count(*) tagCount                         " +
                " FROM [EA_Release1].[dbo].[t_objectproperties] tag, " +
                "      dbo.t_object obj " +
                " WHERE  " +
                "       tag.[Object_ID] = obj.[Object_ID] " +
                "   AND [Property] = '{0}' " +
                " GROUP BY Value " +
                "   HAVING count(*) > 1 ",
                taggedValue);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                var td = new taggedDuplicate();
                td.tagValue = reader["tagValue"].ToString();
                td.tagCount = reader["tagCount"].ToString();

                retArray.Add(td);
            }

            reader.Close();

            EADBConnection.Close();

            return retArray;
        }


        //
        // List one duplicated item
        //
        public ArrayList getDuplicateInfo(string tagProp, string tagValue)
        {
            var retArray = new ArrayList();

            if (tagValue == "")
                return retArray;

            //
            // EA SQL database
            //
            EADBConnection = new SqlConnection(EARepository);
            EADBConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = EADBConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                " SELECT                                             " +
                "          tag.Object_ID  tagID,                     " +
                "          obj.Object_ID  objectID,                  " +
                "          obj.ea_guid    objectEAGUID,              " +
                "          obj.Stereotype objectStereotype,          " +
                "          obj.Name       objectName,                " +
                "          tag.Value      tagValue                   " +
                " FROM dbo.t_objectproperties tag, " +
                "      dbo.t_object obj " +
                " WHERE  " +
                "       tag.[Object_ID] = obj.[Object_ID] " +
                "   AND tag.Property = '{0}' " +
                "   AND tag.Value = '{1}' ",
                tagProp,
                tagValue);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                var td = new objectTag();
                td.tagID = Convert.ToInt32(reader["tagID"].ToString());
                td.tagValue = reader["tagValue"].ToString();
                td.objectEAGUID = reader["objectEAGUID"].ToString();
                td.objectID = Convert.ToInt32(reader["objectID"].ToString());
                td.objectStereotype = reader["objectStereotype"].ToString();
                td.objectName = reader["objectName"].ToString();

                retArray.Add(td);
            }

            reader.Close();

            EADBConnection.Close();

            return retArray;
        }

        #region Nested type: objectTag

        public struct objectTag
        {
            public string objectEAGUID;
            public int objectID;
            public string objectName;
            public string objectStereotype;
            public int tagID;
            public string tagValue;
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