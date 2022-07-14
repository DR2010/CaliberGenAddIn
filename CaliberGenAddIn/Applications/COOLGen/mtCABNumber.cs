using System.Data;
using System.Data.SqlClient;

namespace EAAddIn
{
    internal class mtCABNumber
    {
        public string ActionBlockName;
        public string ComponentContact;
        public string Developer;
        public string loadName;
        public int number;
        public string Release;
        public string Train;
        public string type;

        // -------------------------------------------------------
        //  Generate the new cab number
        // -------------------------------------------------------
        public string getNewLoadModuleName(SqlConnection MyConnection)
        {
            string loadModule = "Error";


            //
            // Stored Procedure parms
            //
            var mySqlCommand = new SqlCommand("insertCommonNumber_1", MyConnection);
            mySqlCommand.CommandType = CommandType.StoredProcedure;

            mySqlCommand.Parameters.Add(new SqlParameter("@type_1", type));
            mySqlCommand.Parameters.Add(new SqlParameter("@ComponentContact_5", Developer));
            mySqlCommand.Parameters.Add(new SqlParameter("@Release_7", Release));
            mySqlCommand.Parameters.Add(new SqlParameter("@Developer_4", Developer));
            mySqlCommand.Parameters.Add(new SqlParameter("@number_2", 1));
            mySqlCommand.Parameters.Add(new SqlParameter("@Train_6", ""));
            mySqlCommand.Parameters.Add(new SqlParameter("@LName_8", SqlDbType.Char,
                                                         8, ParameterDirection.Output, true, 0, 0,
                                                         "LoadName", DataRowVersion.Default, null));

            mySqlCommand.Parameters.Add(new SqlParameter("@ActionBlockName_3", SqlDbType.Char,
                                                         32, ParameterDirection.InputOutput, true, 0, 0,
                                                         "ActionblockName", DataRowVersion.Default, null));

            // Set action block name
            mySqlCommand.Parameters["@ActionBlockName_3"].Value = ActionBlockName;

            try
            {
                int i = mySqlCommand.ExecuteNonQuery();

                // LName_8
                // ActionBlockName_3
                // number_2

                loadName = mySqlCommand.Parameters["@LName_8"].Value.ToString();
                ActionBlockName = mySqlCommand.Parameters["@ActionBlockName_3"].Value.ToString();
            }
            catch (SqlException ex)
            {
                loadName = "Error: " + ex.Message;
            }

            loadModule = loadName;

            return loadModule;
        }
    }
}