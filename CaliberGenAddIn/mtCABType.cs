using System;
using System.Data;
using System.Data.SqlClient;

namespace EAAddIn
{
    internal class mtCABType
    {
        public string abType;
        public int ID;
        public int lastNumber;
        public string modulePrefix;
        public string namePrefix;

        // ---------------------------------------------------------
        //                Generate a new CAB number
        // ---------------------------------------------------------
        public string getNewNumber(SqlConnection MyConnection)
        {
            int nextID = 0;
            string ret = "Ok";

            if (AddInRepository.Instance.ReadOnly)
            {
                ret = "Error - System is in Read-only.";
                return ret;
            }

            if (String.IsNullOrEmpty(abType))
            {
                ret = "abType not supplied.";
                return ret;
            }

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            sqlCommand1.CommandText =
                string.Format(
                    "SELECT ID, ABType, NamePrefix, ModulePrefix, LastNumber" +
                    " FROM ActionBlockType " +
                    " Where ABType = '{0}'",
                    abType);

            SqlDataReader reader;
            try
            {
                reader = sqlCommand1.ExecuteReader();
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
                return ret;
            }

            if (reader.Read())
            {
                ID = Convert.ToInt32(reader["ID"]);
                abType = reader["abType"].ToString();
                namePrefix = reader["NamePrefix"].ToString();
                modulePrefix = reader["ModulePrefix"].ToString();
                lastNumber = Convert.ToInt32(reader["LastNumber"]);

                nextID = lastNumber + 1;
            }

            reader.Close();

            if (nextID > 0)
            {
                var sqlCommand2 = new SqlCommand();
                sqlCommand2 = MyConnection.CreateCommand();

                sqlCommand2.CommandText =
                    string.Format(
                        "UPDATE ActionBlockType " +
                        "SET LastNumber = {0}" +
                        " Where ABType = '{1}'",
                        nextID, abType);

                try
                {
                    sqlCommand2.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ret = ex.ToString();
                    return ret;
                }

                lastNumber = nextID;
            }

            return ret;
        }

        // Structure to deal with coolgen module name
        //

        //
        // Derive coolgen stereotype
        //
        public static genModule getStereotypeForGen(SqlConnection MyConnection,
                                                    string inModule, string inName)
        {
            var ret = new genModule();
            ret.name = inName;
            ret.module = inModule;

            // Retrieve from DB using NamePrefix
            string moduleStartsWith = "";
            if (inModule.StartsWith("X"))
            {
                moduleStartsWith = "X";
            }
            else
            {
                moduleStartsWith = inModule.Substring(0, 4);
            }

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            sqlCommand1.CommandText =
                string.Format(
                    "SELECT ID, ABType, NamePrefix, ModulePrefix, LastNumber" +
                    " FROM ActionBlockType " +
                    " WHERE ModulePrefix = '{0}'", moduleStartsWith);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader.Read())
            {
                string modulePrefix = reader["ModulePrefix"].ToString();
                string stereotype = reader["abType"].ToString().Trim();

                ret.stereotype = stereotype;
            }
            else
            {
                ret.stereotype = "GEN CAB";
            }

            reader.Close();

            if (inModule.StartsWith("X"))
                ret.stereotype = "GEN External";

            // Remove extra "("
            if (inName != null && inName.Contains("("))
            {
                int p = inName.IndexOf("(");
                ret.name = inName.Substring(0, p);
            }

            return ret;
        }

        //
        // Derive coolgen program name e.g. ZEIB9999, ZEIW9999, P_IES999
        //
        private static genModule obsolete_getModuleName(SqlConnection MyConnection,
                                                        string inModule, string inName)
        {
            var ret = new genModule();
            ret.name = inName;

            if (inModule == "")
            {
                //
                // If it is a wrapper, service or a batch program, it does not have a module name.
                //
                string nameStartsWith = inName.Substring(0, 2);

                // Retrieve from DB using NamePrefix

                var sqlCommand1 = new SqlCommand();
                sqlCommand1 = MyConnection.CreateCommand();

                sqlCommand1.CommandText =
                    string.Format(
                        "SELECT ID, ABType, NamePrefix, ModulePrefix, LastNumber" +
                        " FROM ActionBlockType " +
                        " WHERE NamePrefix = '{0}'", nameStartsWith);

                SqlDataReader reader = sqlCommand1.ExecuteReader();

                if (reader.Read())
                {
                    string modulePrefix = reader["ModulePrefix"].ToString();
                    string stereotype = reader["abType"].ToString().Trim();

                    if (modulePrefix == "NAME")
                    {
                        ret.module = inName.Substring(0, 8);
                        ret.stereotype = stereotype;
                    }
                    else
                    {
                        ret.module = modulePrefix + inName.Substring(inName.Length - 4, 4);
                        ret.stereotype = stereotype;
                    }
                }

                reader.Close();
            }
            else
            {
                ret.module = inModule;

                // Retrieve from DB using NamePrefix
                string moduleStartsWith = "";
                if (inModule.StartsWith("X"))
                {
                    moduleStartsWith = "X";
                }
                else
                {
                    moduleStartsWith = inModule.Substring(0, 4);
                }

                var sqlCommand1 = new SqlCommand();
                sqlCommand1 = MyConnection.CreateCommand();

                sqlCommand1.CommandText =
                    string.Format(
                        "SELECT ID, ABType, NamePrefix, ModulePrefix, LastNumber" +
                        " FROM ActionBlockType " +
                        " WHERE ModulePrefix = '{0}'", moduleStartsWith);

                SqlDataReader reader = sqlCommand1.ExecuteReader();

                if (reader.Read())
                {
                    string modulePrefix = reader["ModulePrefix"].ToString();
                    string stereotype = reader["abType"].ToString().Trim();

                    ret.stereotype = stereotype;
                }
                else
                {
                    ret.stereotype = "GEN CAB";
                }

                reader.Close();

                if (inModule.StartsWith("X"))
                    ret.stereotype = "GEN External";
            }

            // Remove extra "("
            if (inName != null && inName.Contains("("))
            {
                int p = inName.IndexOf("(");
                ret.name = inName.Substring(0, p);
            }

            return ret;
        }

        //
        // Derive coolgen program name e.g. ZEIB9999, ZEIW9999, P_IES999
        //
        public static string getGenPrefix(SqlConnection MyConnection, string stereotype)
        {
            string ret = "ERR";

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            sqlCommand1.CommandText =
                string.Format(
                    "SELECT ID, ABType, NamePrefix, ModulePrefix, LastNumber" +
                    " FROM ActionBlockType " +
                    " WHERE ABType = '{0}'", stereotype);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader.Read())
            {
                string modulePrefix = reader["ModulePrefix"].ToString();

                ret = modulePrefix;
            }

            reader.Close();

            return ret;
        }

        #region TO_BE_REPLACED

        //
        // Derive coolgen program name e.g. ZEIB9999, ZEIW9999, P_IES999
        //
        public static string getGenPrefixold(string stereotype)
        {
            string ret = "ERR";

            if (stereotype == "")
                return ret;

            switch (stereotype)
            {
                case "GEN Service":
                    ret = "ZEIS";
                    break;
                case "GEN Orchestrator":
                    ret = "ZEIO";
                    break;
                case "GEN Public":
                    ret = "ZEIP";
                    break;
                case "GEN Private":
                    ret = "ZEIV";
                    break;
                case "GEN Generic":
                    ret = "ZEIC";
                    break;
                case "GEN Wrapper":
                    ret = "ZEIW";
                    break;
                case "GEN CAB":
                    ret = "ZEIB";
                    break;
                case "GEN I": // ????
                    ret = "ZEII";
                    break;
                case "GEN External":
                    ret = "ZEIX";
                    break;
                case "GEN Infra": // ???????
                    ret = "XXCB";
                    break;

                default:
                    break;
            }

            return ret;
        }


        //
        // Derive coolgen program name e.g. ZEIB9999, ZEIW9999, P_IES999
        //
        public static genModule getModuleNameold(string inModule, string inName)
        {
            //
            // S_   - SOA Service                         - GEN Service        - ZEIS
            // O_   - Orchestrator                        - GEN Orchestrator   - ZEIO
            // P_   - SOA Public operation                - GEN Public         - ZEIP
            // V_   - SOA Private operation               - GEN External       - ZEIV
            // G_   - SOA Generic CAB                     - GEN Generic        - ZEIX
            // I_   -                                     - GEN I_             - ZEII
            // X_   - External                            - GEN External       - 
            // W_   - Wrapper (decomissioned on Dec 2008) - GEN Wrapper        - ZEIW
            // C_   - CAB (decomissioned on Dec 2008)     - GEN CAB            - ZEIB
            // E_                                         - GEN E_             - ZEI
            // XXI_ or XX_                                - GEN Infrastructure - XXCB
            //

            var ret = new genModule();
            ret.name = inName;

            if (inModule == "")
            {
                //
                // If it is a wrapper, service or a batch program, it does not have a module name.
                //
                if (inName.StartsWith("W"))
                {
                    ret.module = "ZEIW" + inName.Substring(inName.Length - 4, 4);
                    ret.stereotype = "GEN Wrapper";
                }
                if (inName.StartsWith("S"))
                {
                    ret.module = "ZEIS" + inName.Substring(inName.Length - 4, 4);
                    ret.stereotype = "GEN Service";
                }
                if (inName.StartsWith("P"))
                {
                    ret.module = inName.Substring(0, 8);
                    ret.stereotype = "GEN Batch";
                }
                if (inName.StartsWith("C"))
                {
                    ret.module = "ZEIB" + inName.Substring(inName.Length - 4, 4);
                    ret.stereotype = "GEN CAB";
                }
            }
            else
            {
                // If the module exists

                // S_   - SOA Service                         - GEN Service        - ZEIS
                // O_   - Orchestrator                        - GEN Orchestrator   - ZEIO
                // P_   - SOA Public operation                - GEN Public         - ZEIP
                // V_   - SOA Private operation               - GEN External       - ZEIV
                // G_   - SOA Generic CAB                     - GEN Generic        - ZEIX
                // I_   -                                     - GEN I_             - ZEII
                // X_   - External                            - GEN External       - 
                // W_   - Wrapper (decomissioned on Dec 2008) - GEN Wrapper        - ZEIW
                // C_   - CAB (decomissioned on Dec 2008)     - GEN CAB            - ZEIB
                // E_                                         - GEN E_             - ZEI
                // XXI_ or XX_                                - GEN Infra          - XXCB


                ret.module = inModule;

                if (inModule.StartsWith("ZEIS"))
                    ret.stereotype = "GEN Service";

                if (inModule.StartsWith("ZEIO"))
                    ret.stereotype = "GEN Orchestrator";

                if (inModule.StartsWith("ZEIP"))
                    ret.stereotype = "GEN Public";

                if (inModule.StartsWith("ZEIV"))
                    ret.stereotype = "GEN Private";

                if (inModule.StartsWith("ZEIG"))
                    ret.stereotype = "GEN Global";

                if (inModule.StartsWith("ZEII"))
                    ret.stereotype = "GEN I_";

                if (inModule.StartsWith("X"))
                    ret.stereotype = "GEN External";

                if (inModule.StartsWith("ZEIW"))
                    ret.stereotype = "GEN Wrapper";

                // E_ = I don't know what to do.

                // If everything else fails, make it a CAB.
                if (ret.stereotype == null)
                {
                    ret.stereotype = "GEN CAB";
                }
            }

            // Remove extra "("
            if (inName != null && inName.Contains("("))
            {
                int p = inName.IndexOf("(");
                ret.name = inName.Substring(0, p);
            }

            return ret;
        }

        #endregion TO_BE_REPLACED

        #region Nested type: genModule

        public struct genModule
        {
            public string module;
            public string name;
            public string stereotype;
        }

        #endregion
    }

    internal class mtCABTypeList
    {
        public mtCABType[] cabTypeList;
        public DataTable dtcabTypeList;

        public mtCABTypeList()
        {
            dtcabTypeList = new DataTable();
            var ID = new DataColumn("ID", typeof (String));
            var abType = new DataColumn("abType", typeof (String));
            var namePrefix = new DataColumn("namePrefix", typeof (String));
            var modulePrefix = new DataColumn("modulePrefix", typeof (String));
            var lastNumber = new DataColumn("lastNumber", typeof (String));

            dtcabTypeList.Columns.Add(ID);
            dtcabTypeList.Columns.Add(abType);
            dtcabTypeList.Columns.Add(namePrefix);
            dtcabTypeList.Columns.Add(modulePrefix);
            dtcabTypeList.Columns.Add(lastNumber);
        }

        // ---------------------------------------------------------
        //                Retrieve a list of AB Types
        // ---------------------------------------------------------
        public void getTypeList(SqlConnection MyConnection)
        {
            cabTypeList = new mtCABType[20];

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = MyConnection.CreateCommand();

            sqlCommand1.CommandText =
                string.Format(
                    "SELECT ID, ABType, NamePrefix, ModulePrefix, LastNumber" +
                    " FROM ActionBlockType ");

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                cabTypeList[i] = new mtCABType();

                cabTypeList[i].ID = Convert.ToInt32(reader["ID"]);
                cabTypeList[i].abType = reader["abType"].ToString();
                cabTypeList[i].namePrefix = reader["NamePrefix"].ToString();
                cabTypeList[i].modulePrefix = reader["ModulePrefix"].ToString();
                cabTypeList[i].lastNumber = Convert.ToInt32(reader["LastNumber"]);

                DataRow dr = dtcabTypeList.NewRow();

                dr["ID"] = reader["ID"].ToString();
                dr["abType"] = reader["abType"].ToString();
                dr["namePrefix"] = reader["NamePrefix"].ToString();
                dr["modulePrefix"] = reader["ModulePrefix"].ToString();
                dr["lastNumber"] = reader["LastNumber"].ToString();

                dtcabTypeList.Rows.Add(dr);

                i++;
            }

            reader.Close();

            return;
        }
    }
}