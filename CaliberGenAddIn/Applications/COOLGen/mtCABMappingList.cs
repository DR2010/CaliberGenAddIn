using System;
using System.Data;
using System.Data.SqlClient;

namespace EAAddIn
{
    public class mtCABMappingList
    {
        public DataTable cabMapList;

        public mtCABMappingList()
        {
            cabMapList = new DataTable();

            var CAB = new DataColumn("CAB", typeof (String));
            var CABName = new DataColumn("CABName", typeof (String));
            var CABPrefix = new DataColumn("CABPrefix", typeof (String));
            var CABType = new DataColumn("CABType", typeof (String));
            var CABnumber = new DataColumn("CABnumber", typeof (String));
            var EA_GUID = new DataColumn("EA_GUID", typeof (String));
            var EAStatus = new DataColumn("EaStatus", typeof (String));
            var Author = new DataColumn("Author", typeof (String));

            cabMapList.Columns.Add(CAB);
            cabMapList.Columns.Add(CABName);
            cabMapList.Columns.Add(CABPrefix);
            cabMapList.Columns.Add(CABType);
            cabMapList.Columns.Add(CABnumber);
            cabMapList.Columns.Add(EA_GUID);
            cabMapList.Columns.Add(EAStatus);
            cabMapList.Columns.Add(Author);
        }

        // ---------------------------------------
        //             Get CAB List
        // ---------------------------------------
        public void getCabList(SqlConnection MyConnection, string author)
        {
            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            string sauthor = "";
            if (author != "")
            {
                sauthor = "WHERE [Author] = '{0}'";
            }

            sqlCommand1.CommandText =
                string.Format(
                    "SELECT [CAB] " +
                    ",[EA_GUID]" +
                    ",[EaStatus]" +
                    ",[CABName]" +
                    ",[CABtype]" +
                    ",[CABPrefix]" +
                    ",[Author]" +
                    ",[CABNumber] " +
                    " FROM  CABMapping " +
                    sauthor
                    , author);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                DataRow dr = cabMapList.NewRow();
                dr["CAB"] = reader["CAB"].ToString();
                dr["EA_GUID"] = reader["EA_GUID"].ToString();
                dr["EaStatus"] = reader["EaStatus"].ToString();
                dr["CABName"] = reader["CABName"].ToString();
                dr["CABtype"] = reader["CABtype"].ToString();
                dr["CABPrefix"] = reader["CABPrefix"].ToString();
                dr["Author"] = reader["Author"].ToString();

                cabMapList.Rows.Add(dr);
            }

            reader.Close();

            return;
        }
    }
}