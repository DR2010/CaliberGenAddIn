using System.Data.SqlClient;
using System.Threading;
using EA;

namespace EAAddIn
{
    internal class AddInRepository
    {
        private const string EARelease1DB = "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";
        private const string EADBModelDB = "initial catalog=EA_DBModels;data source=DRPSQL007\\SQL07";
        private const string EAREP2ID = "initial catalog=EA_Release2;data source=DRPSQL007\\SQL07";
        private const string EASecureDB = "initial catalog=EA_Secure_DB;data source=DRPSQL007\\SQL07";
        private const string EALocalDB = "initial catalog=EA_LOCAL;";

        private const string OutputTabCaliberResults = "Caliber Import Results";
        private const string OutputTabGenResults = "GEN Results";
        private const string OutputTabAuditLogResults = "Audit Log Results";


        private static readonly Mutex mutex = new Mutex();
        private static AddInRepository instance;
        private Repository repository;

        private const string DBARole = "DBA";
        private const string BAAdminRole = "BA Administrator";
        private const string SolutionArchitectRole = "Solution Architecture";

        #region Properties

        public bool ReadOnly
        {
            get
            {
                if (repository == null) return true;

                if (IsRelease || IsSecure || IsLocal || IsDBModels)
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
                return repository.ConnectionString.ToUpper().Contains(EARelease1DB.ToUpper());
            }
        }
        public bool IsDBModels
        {
            get
            {
                return repository.ConnectionString.ToUpper().Contains(EADBModelDB.ToUpper());
            }
        }
        public bool IsSecure
        {
            get
            {
                return repository.ConnectionString.ToUpper().Contains(EASecureDB.ToUpper());
            }
        }
        public bool IsLocal
        {
            get
            {
                return repository.ConnectionString.ToUpper().Contains(EALocalDB.ToUpper());
            }
        }

        public string ConnectionStringshort
        {
            //
            // It retrieves the SQL server required part of the connection string
            //
            get
            {
                string constr = repository.ConnectionString.ToUpper();

                string [] constrpart = constr.Split(';');

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


                string tempRep = integrateSecurity + persistSecurity + initialCatalog + dataSource;

                return tempRep;
            }

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
    }
}