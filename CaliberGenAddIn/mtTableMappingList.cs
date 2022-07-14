using System;
using System.Data;
using System.Data.SqlClient;

namespace EAAddIn
{
    public class mtTableMappingList
    {
        public DataTable tableMapping;
        //
        // Retrieve a list of Tables
        //
        public void getTableList(SqlConnection MyConnection)
        {
            var tableName = new DataColumn("tableName", typeof (String));
            var EA_GUID = new DataColumn("EA_GUID", typeof(String));
            var alternateName = new DataColumn("alternateName", typeof (String));

            tableMapping = new DataTable("tableMapping");

            tableMapping.Columns.Add(tableName);
            tableMapping.Columns.Add(EA_GUID);
            tableMapping.Columns.Add(alternateName);

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            sqlCommand1.CommandText =
                "SELECT TableName, EA_GUID, AlternateName " +
                " from TableMapping ";

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                DataRow tableRow = tableMapping.NewRow();

                tableRow["tableName"] = reader["TableName"].ToString();
                tableRow["EA_GUID"] = reader["EA_GUID"].ToString();
                tableRow["AlternateName"] = reader["AlternateName"].ToString();

                tableMapping.Rows.Add(tableRow);
            }

            reader.Close();

            return;
        }
    }
}