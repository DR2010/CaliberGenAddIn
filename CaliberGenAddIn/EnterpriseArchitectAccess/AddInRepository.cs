using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using EA;
using EAAddIn.Applications;

namespace EAAddIn
{
    public class AddInRepository
    {
        private const string OutputTabCaliberResults = "Caliber Import Results";
        private const string OutputTabGenResults = "GEN Results";
        private const string OutputTabAuditLogResults = "Audit Log Results";
        private const string OutputEADeleteResults = "EA Delete Results";


        private static readonly Mutex mutex = new Mutex();
        private static AddInRepository instance;
        private Repository repository;

        private const string DBARole = "DBA";
        private const string BAAdminRole = "BA Administrator";
        private const string SolutionArchitectRole = "Solution Architecture";

        public List<int> OpenDiagrams = new List<int>();
        private string _connectionStringDbAndServer;
        private string _connectionStringSql;
        private List<Action<string, ObjectType>> contextWatchers = new List<Action<string, ObjectType>>();

        #region Properties

        public bool ReadOnly
        {
            get
            {
                if (repository == null) return true;

                if (IsRelease || IsSecure || IsLocal || IsDbModel)
                {
                    return false;
                }

                return true;

            }
        }

        public bool IsRelease
        {
            get
            {
                return repository.ConnectionString.ToUpper().Contains(SqlHelpers.EaRelease1DbConnectionString);
            }
        }
        public bool IsDbModel
        {
            get
            {
                return repository.ConnectionString.ToUpper().Contains(SqlHelpers.EaDbModelsDbConnectionString);
            }
        }
        public bool IsSecure
        {
            get
            {
                return repository.ConnectionString.ToUpper().Contains(SqlHelpers.EaSecureDbConnectionString);
            }
        }
        public bool IsLocal
        {
            get
            {
                return repository.ConnectionString.ToUpper().Contains("LOCAL");
            }
        }

        public string ConnectionStringDbAndServer
        {
            //
            // It retrieves the SQL server required part of the connection string
            //
            get
            {
                if (repository == null) return string.Empty;

                if (string.IsNullOrEmpty(_connectionStringDbAndServer))
                {
                    BuildConnectionStrings();
                }

                return _connectionStringDbAndServer;
            }

        }


        public string ConnectionStringSql
        {
            get
            {
                if (repository == null) return string.Empty;

                if (string.IsNullOrEmpty(_connectionStringSql))
                {
                    BuildConnectionStrings();
                } 
                return _connectionStringSql;
            }
        }

        private void BuildConnectionStrings()
        {
            string constr = repository.ConnectionString.ToUpper();

            string[] constrpart = constr.Split(';');

            string integrateSecurity = "";
            string persistSecurity = "";
            string initialCatalog = "";
            string dataSource = "";
            foreach (string s in constrpart)
            {
                string upS = s.ToUpper();

                if (s.StartsWith("INTEGRATED"))
                    integrateSecurity = s + ";";

                if (s.StartsWith("PERSIST"))
                    persistSecurity = s + ";";

                if (s.StartsWith("INITIAL"))
                    initialCatalog = s + ";";

                if (s.StartsWith("DATA SOURCE"))
                    dataSource = s + ";";

            }
            _connectionStringDbAndServer = initialCatalog + dataSource;
            _connectionStringSql = _connectionStringDbAndServer + integrateSecurity + persistSecurity;
        }

        public Repository Repository
        {
            get { return repository; }
            set { repository = value; }
        }

        public static AddInRepository Instance
        {
            get
            {
                mutex.WaitOne();
                if (instance == null)
                {
                    instance = new AddInRepository();
                }
                mutex.ReleaseMutex();
                return instance;
            }
        }


        #endregion

        #region Caliber Results Logging

        public void InitialiseCaliberResults()
        {
            Instance.repository.CreateOutputTab(OutputTabCaliberResults);
            Instance.repository.ClearOutput(OutputTabCaliberResults);
            Instance.repository.EnsureOutputVisible(OutputTabCaliberResults);
        }

        public void WriteCaliberResults(string Text, int ID)
        {
            Instance.repository.WriteOutput(OutputTabCaliberResults, Text, ID);
        }
        #endregion

        #region Gen Results Logging

        public void InitialiseGenResults()
        {
            Instance.repository.CreateOutputTab(OutputTabGenResults);
            Instance.repository.ClearOutput(OutputTabGenResults);
            Instance.repository.EnsureOutputVisible(OutputTabGenResults);
        }

        public void WriteGenResults(string Text, int ID)
        {
            Instance.repository.WriteOutput(OutputTabGenResults, Text, ID);
        }

        #endregion

        #region EA Delete Results 

        public void InitialiseEADeleteResults()
        {
            Instance.repository.CreateOutputTab(OutputEADeleteResults);
            Instance.repository.ClearOutput(OutputEADeleteResults);
            Instance.repository.EnsureOutputVisible(OutputEADeleteResults);
        }

        public void WriteEADeleteResults(string Text, int ID)
        {
            Instance.repository.WriteOutput(OutputEADeleteResults, Text, ID);
        }

        #endregion




        #region Audit Delete Results Logging

        public void InitialiseAuditResults()
        {
            Instance.repository.CreateOutputTab(OutputTabAuditLogResults);
            Instance.repository.ClearOutput(OutputTabAuditLogResults);
            Instance.repository.EnsureOutputVisible(OutputTabAuditLogResults);
        }

        public void WriteAuditResults(string Text, int ID)
        {
            Instance.repository.WriteOutput(OutputTabAuditLogResults, Text, ID);
        }
        #endregion



        #region Security

        private bool doesUserHaveRole(string role)
        {
            var groups = new EaAccess().SecurityGroupList(Instance.repository.GetCurrentLoginUser(false));

            foreach (string group in groups)
            {
                if (string.Compare(group,role,true) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool UserHasBAAdministratorRole
        {
            get { return doesUserHaveRole(BAAdminRole); }
        }
        public bool UserHasDBARole
        {
            get { return doesUserHaveRole(DBARole); }
        }
        public bool UserHasSolutionArchitectRole
        {
            get { return doesUserHaveRole(SolutionArchitectRole); }
        }


        #endregion

        public void RegisterContextWatcher(Action<string, ObjectType> contextWatcher)
        {
            contextWatchers.Add(contextWatcher);
        }

        public IEnumerable<Action<string, ObjectType>> ContextWatchers
        {
            get { return contextWatchers; }
        }
    }
}