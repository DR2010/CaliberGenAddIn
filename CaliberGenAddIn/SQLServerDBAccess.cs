using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Text;
using EAStructures;


namespace SQLServerDBAccess
{
    public class SQLDBAccess
    {

        //  
        // SQL Server 2005 database
        //
        public static SqlConnection dbSQLServer2005Connect(SecurityInfo inSecInfo)
        {

            SqlConnection MyConnection = new SqlConnection(inSecInfo.EAGenCaliberSQL2005Repository);
            try
            {
                MyConnection.Open();
            }
            finally
            {
                // Do nothing
            }

            return MyConnection;

        }

        public static void dbSQLServer2005Disconnect(SqlConnection ConUL)
        {
            ConUL.Close();
        }


        //
        // Check Database availability
        //
        public SecurityInfo checkDatabaseAvailability()
        {

            SecurityInfo securityDataBase = new SecurityInfo();

            securityDataBase.EAGenCaliberSQL2005Repository= "Data Source=EDSQL011\\SQL05 ;Initial Catalog=DanMacTest;Integrated Security=True";
            // securityDataBase.DB2ConnectionString = "Dsn=DBD1;uid=dm0874;pwd=kadk0806;";

            SqlConnection ConSQL;

            ConSQL = dbSQLServer2005Connect(securityDataBase);

            if (ConSQL.State.ToString() == "Open")
            {
                securityDataBase.sqlServer2005 = true;
                dbSQLServer2005Disconnect(ConSQL);
            }
            else
            {
                securityDataBase.sqlServer2005 = false;
            }


            return securityDataBase;
        }
    }
}
