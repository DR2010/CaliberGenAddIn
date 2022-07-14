using System;
using System.Diagnostics;
using System.Linq;
using EaAuditConverter.AuditScheduler;

namespace EaAuditConverter
{
    class AuditConverterConsole
    {
        private readonly string[] _consoleArgs;
        private bool ok = true;
        
        public AuditConverterConsole(string[] args)
        {
            _consoleArgs = args;

        }

        public bool Execute()
        {
            var myWriter = new TextWriterTraceListener(Console.Out);
            Debug.Listeners.Add(myWriter);
            string errorConsoleMessage;


            var messageHandler = new MessageHandler();


            if (_consoleArgs.Length < 2)
            {
                messageHandler.WriteToConsole(@"2 parameters must be passed to the EA audit converter.",
                                              MessageType.Console);
                messageHandler.WriteToConsole(@"1) Database ", MessageType.Console);
                messageHandler.WriteToConsole(@"2) Database server ", MessageType.Console);
                messageHandler.WriteToConsole(
                    @"3) [\e(1|2)] Error level for messages to write to database - 1 is default - write error messages, 2 writes info messages",
                    MessageType.Console);
                messageHandler.WriteToConsole(@"4) [\k] Wait for a key press at start and end", MessageType.Console);
                messageHandler.WriteToConsole(@"5) [\d] Attach to Debugger", MessageType.Console);
                if (WaitForKeyPressed()) Console.ReadKey();
                return false;
            }

            messageHandler.SqlConnectionString =
                    string.Format(
                        "integrated security=sspi;persist security info=false;initial catalog={0};data source={1};", _consoleArgs[0],
                        _consoleArgs[1]);

            messageHandler.ErrorLevel = ErrorLevel();

            string argstring = null;
            foreach (string arg in _consoleArgs)
            {
                argstring = argstring + " " + arg;
            }

            if (WaitForKeyPressed())
            {
                messageHandler.WriteToConsole("Ready to convert log with Args: " + argstring,
                                              MessageType.Information + ", press any kety to continue.");
                Console.ReadKey();
            }

            if (AttachToDebugger()) Debugger.Launch();


            try
            {
                var auditConverterHandler = new AuditConverterHandler(messageHandler.SqlConnectionString);
                messageHandler.WriteToConsole("Connected to " + messageHandler.SqlConnectionString, MessageType.Information);
                ok = auditConverterHandler.ConvertAudit(ref messageHandler);

                if (ok)
                    messageHandler.WriteToConsole("Conversion and Duplication successful.", MessageType.Information);
                else
                    messageHandler.WriteToConsole(
                        "An error has occurred converting the EA audit logs.  Please refer to the AuditErrorLog table for more information.",
                        MessageType.Error);


                if (WaitForKeyPressed())
                {
                    messageHandler.WriteToConsole("Press any key to end application...", MessageType.Console);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                messageHandler.WriteToConsole("Unhandled exception: " + ex.Message, MessageType.Error);
                ok = false;
                throw;
            }

            messageHandler.WriteOutMessages();
            errorConsoleMessage = messageHandler.UpdateMessageHeader();
            if (errorConsoleMessage != null) Console.Write(errorConsoleMessage);
            return ok;
        }

        #region Helper Methods for Audit Converter Console

        private string ErrorLevel()
        {
            string errorLevel = "1";

            foreach (string arg in _consoleArgs)
            {
                if (arg.ToUpper().StartsWith(@"\E"))
                {
                    int level = 1;

                    if (Int32.TryParse(arg.ToUpper().Substring(2), out level))
                    {
                        errorLevel = level.ToString();
                    }

                    break;
                }
            }
            return errorLevel;
        }

        private bool WaitForKeyPressed()
        {
            return _consoleArgs.Contains(@"\k") || _consoleArgs.Contains(@"\K");
        }

        private bool AttachToDebugger()
        {
            return _consoleArgs.Contains(@"\d") || _consoleArgs.Contains(@"\D");
        }

        #endregion
    }
}
