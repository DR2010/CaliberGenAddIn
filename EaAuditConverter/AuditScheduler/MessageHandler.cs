using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Principal;

namespace EaAuditConverter.AuditScheduler
{
    public class MessageHandler
    {
        public string SqlConnectionString { get; set; }

        const int DefaultmessageHeadingId = 99999999;
        int _messagecount;
        readonly List<Message> _messages = new List<Message>();

        public string ErrorLevel;
        public int AuditHeaderId;

        #region Message Handler Publics

        public void WriteToConsole(string msg, string errorType)
        {
            Console.WriteLine(msg);

            switch (ErrorLevel)
            {
                case "1":
                    if (errorType.Equals(MessageType.Error) || errorType.Equals(MessageType.Result))
                        Addmessage(errorType, msg);
                    break;
                case "2":
                    Addmessage(errorType, msg);
                    break;
                default:
                    //Dont add to db, and just treat as console message
                    break;
            }
            //}
            //if (_errorLevel == "2")  //Write info and results
            //{
            //    messages.Add(new InformationMessage(msg));
            //    Console.WriteLine(msg);
            //}
            //else
            //{

            //}
        }

        public void SetResultsStatus(ref MessageHandler messages, string messageText, string status)
        {
            messages.Addmessage(MessageType.Information, messageText);
            UpdateStatus(status);
        }

        public string UpdateMessageHeader()
        {

            try
            {
                bool inserted;

                if (string.IsNullOrEmpty(SqlConnectionString)) return string.Empty;

                using (var connection = new SqlConnection(SqlConnectionString))
                {
                    connection.Open();

                    var sql = "UPDATE AuditErrorLog" +
                              "   SET [AuditHeaderId] = @P1 " +
                              "   WHERE  " +
                              "          AuditHeaderId = @P2";

                    var command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@P1", AuditHeaderId);
                    command.Parameters.AddWithValue("@P2", DefaultmessageHeadingId);

                    try
                    {
                        inserted = command.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    {
                        return "Exception updating errorlog ids " + ex.Message;
                    }
                }
                if (!inserted) return "Error updating: " + DefaultmessageHeadingId;
            }
            catch (Exception ex)
            {
                return "Exception updating '" + DefaultmessageHeadingId + "' to AuditErrorLog table: " + ex.Message + " " + SqlConnectionString;
            }
            return string.Empty;
        }

        public void WriteOutMessages()
        {
            foreach (var message in _messages)
            {
                if (message.Type == MessageType.Information && ErrorLevel != "2") continue;
                var writeMessage = WriteToErrorLog(message);
                if (!string.IsNullOrEmpty(writeMessage)) WriteToConsole(writeMessage, MessageType.Error);
            }
        }

        #endregion

        #region Message Handler Privates

        private void Addmessage(string messagetype, string message)
        {

            if (messagetype.Equals(MessageType.Information))
                _messages.Add(new InformationMessage(message));
            if (messagetype.Equals(MessageType.Error))
                _messages.Add(new ErrorMessage(message));
            if (messagetype.Equals(MessageType.Result))
                _messages.Add(new ResultMessage(message));
        }

        private string WriteToErrorLog(Message message)
        {
            if (string.IsNullOrEmpty(SqlConnectionString)) return string.Empty;

            _messagecount++;

            bool inserted;
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    "INSERT INTO  AuditErrorLog (DateTime, UserAccount, Description, ErrorType, AuditHeaderId, CreationOrder) VALUES ( @ErrorOccurred, @ServiceAccount, @ErrorDescription, @ErrorType, @AuditHeaderId, @CreationOrder )";

                command.Parameters.AddWithValue("ErrorOccurred", DateTime.Now);
                command.Parameters.AddWithValue("ServiceAccount", WindowsIdentity.GetCurrent().Name.Split('\\')[1]);
                command.Parameters.AddWithValue("ErrorDescription", message.Text);
                command.Parameters.AddWithValue("ErrorType", message.Type);
                command.Parameters.AddWithValue("AuditHeaderId", DefaultmessageHeadingId);
                command.Parameters.AddWithValue("CreationOrder", _messagecount);
                try
                {
                    inserted = command.ExecuteNonQuery() > 0;
                    return string.Empty;
                }
                catch (SqlException ex)
                {
                    return "Exception writing '" + message.Text + "' to AuditErrorLog table: " + ex.Message + " " +
                           SqlConnectionString;
                }
            }
        }

        private  void UpdateStatus(string status)
        {
            if (string.IsNullOrEmpty(SqlConnectionString)) return;

            var statusConnection = new SqlConnection(SqlConnectionString);
            statusConnection.Open();
            const string sql = "UPDATE auditheader" +
                               "   SET [Status] = @P1 " +
                               "   WHERE  " +
                               "          audit_header_id = @P2";

            var statuscommand = new SqlCommand(sql, statusConnection);

            statuscommand.Parameters.AddWithValue("@P1", status);

            statuscommand.Parameters.AddWithValue("@P2", AuditHeaderId);

            statuscommand.ExecuteNonQuery();
        }

        #endregion
    }
}
