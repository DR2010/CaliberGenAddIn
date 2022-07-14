using System;
using System.Collections;
using System.Data.SqlClient;
using EA;

namespace EAAddIn
{
    public class AuditHistoryAccess
    {
        private readonly string EARepository;
        private SqlConnection EADBConnection;

        public AuditHistoryAccess()
        {
            var dbcon = new dbConnections();

            // EaRepository = dbcon.CSEARepository;

            // 01 Jul 2009
            // DM0874 Still under evaluation
            //
            EARepository = AddInRepository.Instance.ConnectionStringshort;


        }

        public ArrayList getAuditHistory(Element element)
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
                "SELECT [SnapshotID] " +
                "       ,[SeriesID] " +
                "       ,[Position] " +
                "       ,[SnapshotName] " +
                "       ,[Notes] " +
                "       ,[Style] " +
                "       ,[ElementID] " +
                "       ,[ElementType] " +
                "       ,[StrContent] " +
                "       ,[BinContent1] " +
                "       ,[BinContent2] " +
                // " FROM [EA_Release1].[dbo].[t_snapshot] " +
                " FROM [dbo].[t_snapshot] " +
                " WHERE  ElementID = '{0}'",
                element.ElementID);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                var al = new auditStruct();
                al.SnapshotID = reader["SnapshotID"].ToString();
                al.SeriesID = reader["SeriesID"].ToString();
                al.Position = Convert.ToInt32(reader["Position"].ToString());
                al.SnapshotName = reader["SnapshotName"].ToString();
                al.Notes = reader["Notes"].ToString();
                al.Style = reader["Style"].ToString();
                al.ElementID = reader["ElementID"].ToString();
                al.ElementType = reader["ElementType"].ToString();
                al.StrContent = reader["StrContent"].ToString();

                retArray.Add(al);
            }

            reader.Close();

            EADBConnection.Close();

            return retArray;
        }

        public string deleteAudit(string fromDate, string toDate, string delOnly)
        {
            AddInRepository.Instance.InitialiseAuditResults();

            string ret = "File created successfully.";

            DateTime startDate = Convert.ToDateTime(fromDate);
            DateTime EndDate = Convert.ToDateTime(toDate);

            string fromDateTxt = fromDate.Substring(06, 04) +
                                 fromDate.Substring(03, 02) +
                                 fromDate.Substring(00, 02);

            string toDateTxt   = toDate.Substring(06, 04) +
                                 toDate.Substring(03, 02) +
                                 toDate.Substring(00, 02);

            DateTime logDate = startDate;

            string filePath = "\\\\edmgt022\\eakeystore$\\AuditLogSaved\\D";

            // string fileName = "F_" + fromDateTxt + "T_" + toDateTxt + ".xml";


            while (logDate <= EndDate)
            {
                string fileName = "D" + logDate.ToFileTime().ToString() + ".xml";

                string filePathName = filePath + fileName;

                DateTime logEndDate = logDate.AddDays(1);

                if (System.IO.File.Exists(filePathName))
                {

                    ret = logDate.ToString() + "WARNING: Log already saved.";
                }
                else
                {

                    if (delOnly != "Yes")
                    {
                        // Save audit first
                        if (AddInRepository.Instance.Repository.SaveAuditLogs(
                            filePathName, logDate, logEndDate))
                        {
                        }
                        else
                        {
                            ret = logDate.ToString() + 
                                " Error saving logs. Logs have not been deleted.";
                            delOnly = "No";
                        }

                    }

                    if (delOnly == "Yes")
                    {

                        // Delete audit
                        if (AddInRepository.Instance.Repository.ClearAuditLogs(logDate, logEndDate))
                        {
                            ret = logDate.ToString() + "Logs saved and deleted successfully.";
                        }
                        else
                        {
                            ret = logDate.ToString() + " Error deleting audit";
                        }
                    }
                }
                
                AddInRepository.Instance.WriteAuditResults(ret, 1000);

                logDate = logDate.AddDays(1);

            }
            return ret;
        }

        #region Nested type: auditStruct

        public struct auditStruct
        {
            public object BinContent1;
            public object BinContent2;
            public string ElementID;
            public string ElementType;
            public string Notes;
            public int Position;
            public string SeriesID;
            public string SnapshotID;
            public string SnapshotName;
            public string StrContent;
            public string Style;
        }

        #endregion
    }
}