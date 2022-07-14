using EAAddIn.Properties;

namespace EAAddIn
{
    public class dbConnections
    {
        private readonly string csEARepository;
        private readonly string csSecureEARepository;
        private readonly string eaRep1ConnectionString;
        private readonly string eaSecureConnectionString;
        private readonly string eaDMLocalConnectionString;

        public string csCaliberEAMapping;
        public string csMigrateTool;
        private string csDMLocalRepository;

        public dbConnections()
        {
            eaRep1ConnectionString = "initial catalog=EA_Release1;data source=DRPSQL007\\SQL07";
            eaSecureConnectionString = "initial catalog=EA_SECURE_DB;data source=DRPSQL007\\SQL07";
            eaDMLocalConnectionString = "initial catalog=EA_LOCAL;data source=FNOA0189\\SQL2005";
            
            //csEARepository =
            //    "Data Source=DRPSQL007\\SQL07;" +
            //    "Initial Catalog=EA_Release1;" +
            //    "Persist Security info=false;" +
            //    "integrated security=sspi;";

            csEARepository =
                Settings.Default.EA_Release1ConnectionString;

            csSecureEARepository =
                "Data Source=DRPSQL007\\SQL07;" +
                "Initial Catalog=EA_SECURE_DB;" +
                "Persist Security info=false;" +
                "integrated security=sspi;";

            //csCaliberEAMapping =
            //    "Data Source=DRPSQL007\\SQL07;" +
            //    "Initial Catalog=EA_CaliberCoolgen;" +
            //    "integrated security=True";

            csCaliberEAMapping = Settings.Default.EA_CaliberCoolgenConnectionString;

            // Using Mimi's machine to test
            //
            //csMigrateTool =
            //  "Data Source=FNOA1145;" +
            //  "Initial Catalog=Migrate;" +
            //  "Integrated Security=SSPI;" +
            //  "User ID=Daniel;Password=daniel";

            csMigrateTool =
                  "Data Source=EDSQL011\\SQL05;" +
                  "Initial Catalog=Migrate;" +
                  "Integrated Security=false;" +
                  "User ID=peter;Password=repet";

            //csMigrateTool =
            //    "Data Source=EDSQL011\\SQL05;" +
            //    "Initial Catalog=Migrate;" +
            //    "integrated security=True";

            csDMLocalRepository =
                "Data Source=FNOA0189\\SQL2005;" +
                "Initial Catalog=EA_LOCAL;" +
                "integrated security=True";

        }

        public string CSSecureEARepository
        {
            get { return csSecureEARepository; }
        }

        public string EASecureConnectionString
        {
            get { return eaSecureConnectionString; }
        }

        public string EADMLocalConnectionString
        {
            get { return eaDMLocalConnectionString; }
        }

        public string EARep1ConnectionString
        {
            get { return eaRep1ConnectionString; }
        }

        public string EARep2ConnectionString
        {
            get { return EARep2ConnectionString; }
        }

        public string CSEARepository
        {
            get { return csEARepository; }
        }

        public string CSDMLocalRepository
        {
            get { return csDMLocalRepository; }
        }
    }
}