using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using EAAddIn;
using EAAddIn.Applications.AuditConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EAAddInTestProject
{
    [TestClass]
    public class AuditConverterTests
    {
        [TestMethod]
        public void ExtractAndConvertLogs()
        {
            DateTime from = new DateTime();
            DateTime to = new DateTime();

            var sql = "SELECT count(*) FROM [audit] where audit_datetime between @from and @to";

            // verify no audits exist for date range

            bool rowsExist = true;
            Assert.IsFalse(rowsExist, "No converted audit records should exist for this period before test");

            var messages = new List<Message>();

            // add a date range, parameter to not clear
            new AuditConverter().ExtractAuditLogs(ref messages);
            Assert.IsFalse(true);

            // verify audits exist for date range

            rowsExist = false;
            Assert.IsTrue(rowsExist, "Converted audit records should exist for this period");

        }
    }
}
