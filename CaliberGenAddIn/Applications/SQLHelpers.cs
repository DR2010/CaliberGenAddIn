using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace EAAddIn.Applications
{
    public static class SqlHelpers
    {

        public static string EaRelease1DbConnectionString = "INITIAL CATALOG=EA_RELEASE1;DATA SOURCE=DRPSQL007\\SQL07;";
        public static string EaSecureDbConnectionString = "INITIAL CATALOG=EA_SECURE_DB;DATA SOURCE=DRPSQL007\\SQL07;";
        public static string MappingDbConnectionString = "INITIAL CATALOG=EA_CALIBERCOOLGEN;DATA SOURCE=DRPSQL007\\SQL07;";
        public static string MigrationToolDbConnectionString = "Data Source=EDSQL011\\SQL05;Initial Catalog=Migrate;Integrated Security=true;";
        public static string EaDbModelsDbConnectionString = "INITIAL CATALOG=EA_DBMODELS;DATA SOURCE=DRPSQL007\\SQL07";
        public static string EaCR0370LocalMappingDbConnectionString = "INITIAL CATALOG=EA_MAPPING;DATA SOURCE=(LOCAL)\\SQL2008;";

        public static string EaTestDevDbConnectionString = "INITIAL CATALOG=EA_RELEASE1;DATA SOURCE=EDSQL019\\SQL01;";

        private static Dictionary<string, SqlConnection> dbConnections = new Dictionary<string, SqlConnection>();

        private static string integratedSecurity = "Integrated Security=true;Connection Timeout=60;";

        public static SqlConnection EaCurrentDbConnection
        {
            get { return GetDbConnectionForConnectionString(AddInRepository.Instance.ConnectionStringDbAndServer); }
        }
        public static SqlConnection EaRelease1DbConnection
        {
            get
            {
                return GetDbConnectionForConnectionString(EaRelease1DbConnectionString);
            }
        }


        public static SqlConnection MappingDbConnection
        {
            get
            {
                if (AddInRepository.Instance.ConnectionStringDbAndServer == EaRelease1DbConnectionString || AddInRepository.Instance.ConnectionStringDbAndServer == EaSecureDbConnectionString)
                    return GetDbConnectionForConnectionString(MappingDbConnectionString);

                if (AddInRepository.Instance.ConnectionStringDbAndServer == EaTestDevDbConnectionString && WindowsIdentity.GetCurrent().Name.Split('\\')[1] == "CR0370")
                    return GetDbConnectionForConnectionString(EaCR0370LocalMappingDbConnectionString);

                return null;
            }
        }

        public static SqlConnection MigrationToolDbConnection
        {
            get
            {
                return GetDbConnectionForConnectionString(MigrationToolDbConnectionString);
            }
        }

     
        public static DataTable GetDataTable(string select)
        {
            var dataTable = new DataTable();
            var adapter = new SqlDataAdapter(select, EaCurrentDbConnection);
            adapter.Fill(dataTable);
            adapter.Dispose();

            return dataTable;
        }

        public static DataRow GetRow(string select)
        {

            var adapter = new SqlDataAdapter(select, EaCurrentDbConnection);

            var dataTable = new DataTable();

            adapter.Fill(dataTable);
            adapter.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        internal static SqlConnection GetDbConnectionForConnectionString(string eaRepositoryConnectionString = null)
        {
            if (eaRepositoryConnectionString == null)
            {
                eaRepositoryConnectionString = AddInRepository.Instance.ConnectionStringDbAndServer;
            }

            if (dbConnections.ContainsKey(eaRepositoryConnectionString.ToUpper()))
            {
                return dbConnections[eaRepositoryConnectionString.ToUpper()];
            }

            var dbConnection = new SqlConnection(eaRepositoryConnectionString + integratedSecurity);

            dbConnection.Open();

            dbConnections.Add(eaRepositoryConnectionString.ToUpper(), dbConnection);
            return dbConnection;
        }
    }
}
