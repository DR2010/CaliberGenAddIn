using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace EaAuditConverter.AuditScheduler
{
    class AuditConverterHandler
    {
        private readonly SqlConnection _connection;
        private int _genertedAuditDetailId;

        public AuditConverterHandler(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public bool ConvertAudit(ref MessageHandler messageHandler)
        {
            var ok = true;
            var maxpostion = 0;
            var sw = Stopwatch.StartNew();
            sw.Start();

            // Get the native audit records
            _connection.Open();
            const string sql =
                "SELECT SnapshotID,SeriesID,Position,SnapshotName,Notes,Style,ElementID,ElementType,StrContent,BinContent1,BinContent2 " +
                "FROM t_snapshot " +
                "ORDER BY Position DESC,SnapshotID DESC ";
            var command = new SqlCommand(sql, _connection);

            var reader = command.ExecuteReader();

            var db = new AuditUpdate.AuditTables(_connection);
            var linqAuditHeader = new AuditUpdate.Auditheader();

            try
            {
                if (reader != null)
                    while (reader.Read() && ok)
                    {
                        if (maxpostion == 0)
                        {
                            maxpostion = reader.GetInt32(2);
                            db = AuditConverter.CreateAuditHeader(ref messageHandler, maxpostion,
                                                                                    out linqAuditHeader,
                                                                                    ref ok, _connection.ConnectionString);
                            if (ok)
                            {
                                messageHandler.AuditHeaderId = linqAuditHeader.Audit_header_id;
                                messageHandler.SetResultsStatus(ref messageHandler, "Audit Heading Created", "Created Heading");
                            }

                        }
                        if (ok && (reader.GetString(3) == "t_object"))
                        {
                            _genertedAuditDetailId++;
                            ok = AuditConverter.CreateAuditRows(reader, linqAuditHeader.Audit_header_id,
                                                                _genertedAuditDetailId,
                                                                db, ref messageHandler);
                        }

                    }
                if (ok)
                {
                    messageHandler.SetResultsStatus(ref messageHandler, "Finished Converting and adding all Audit, about to start Duplication",
                                     "Audit Conversion Finished");
                    ok = DuplicateSnapshotRows(_connection, maxpostion, ref messageHandler);
                }
            }
            finally
            {
                sw.Stop();
                messageHandler.WriteToConsole(
                        string.Format(
                            "{0} - Audit conversion and duplication of {1} rows successfully done in {2} hours, {3} minutes, {4} seconds from t_snapshot.postion {5}.",
                            DateTime.Now, _genertedAuditDetailId,
                            sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, maxpostion), MessageType.Result);

                if (reader != null) reader.Close();
            }
            return ok;

        }

        private static bool DuplicateSnapshotRows(SqlConnection connection, int maxposition, ref MessageHandler messageHandler)
        {
            var ok = true;
            var dupConnection = new SqlConnection(connection.ConnectionString);
            dupConnection.Open();

            messageHandler.SetResultsStatus(ref messageHandler, "Starting to duplicate Audit", "Duplicating Audit");

            var duplicationTransaction = dupConnection.BeginTransaction();
            var sqlInsertCommand = new SqlCommand("INSERT into auditsnapshot" +
                                                  "([SnapshotID],[SeriesID],[Position],[SnapshotName],[Notes],[Style],[ElementID],[ElementType],[StrContent],[BinContent1],[BinContent2]) " +
                                                  "SELECT SnapshotID,SeriesID,Position,SnapshotName,Notes,Style,ElementID,ElementType,StrContent,BinContent1,BinContent2 " +
                                                  "FROM t_snapshot WHERE Position <=" + maxposition, dupConnection, duplicationTransaction);
            try
            {
                var duprows = sqlInsertCommand.ExecuteNonQuery();
                messageHandler.WriteToConsole("Duplicating " + duprows + " rows to auditsnapshot",
                                              MessageType.Information);
                var sqlDeleteCommand = new SqlCommand("DELETE from t_snapshot WHERE Position <=" + maxposition, dupConnection, duplicationTransaction);

                var delrows = sqlDeleteCommand.ExecuteNonQuery();
                messageHandler.WriteToConsole("Deleting  " + delrows + " rows from t_snapshot", MessageType.Information);

                duplicationTransaction.Commit();
                messageHandler.WriteToConsole(
                    "Commit done, " + duprows + " added to auditnspahot and " + delrows +
                    " rows deleted from t_snapshot", MessageType.Information);
                if (delrows != duprows)
                    messageHandler.WriteToConsole(
                        "Serious problem, " + duprows + " added not equal to " + delrows + " rows deleted",
                        MessageType.Error);
            }
            catch (Exception ex)
            {
                messageHandler.WriteToConsole("Rollback Occured: Error Duplicating T_Snapshot: " + ex.Message,
                                              MessageType.Error);
                ok = false;

            }
            finally
            {
                messageHandler.SetResultsStatus(ref messageHandler, "Duplication Succesfull", "Duplication Succesfull");

                connection.Close();
            }
            return ok;
        }
    }
}
