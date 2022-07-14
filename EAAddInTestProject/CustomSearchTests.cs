using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EAAddIn.CustomSearches;
using EAAddIn;
using EA;
using EAAddIn.Applications;
using Moq;

namespace EAAddInTestProject
{
    [TestClass]
    public class CustomSearchTests
    {
        AuditSearch search = new AuditSearch();

        string dataSource = @"Data Source=WDE301253\SQLEXPRESS;Initial Catalog=EA_LOCAL;Integrated Security=True;";

        [TestMethod]
        public void AuditSearch_GetAuditRecordsForElement()
        {
            var searchResult = new EaAccess(dataSource).GetAuditEntriesForElement("{62B8BBFB-57F8-46a2-80B1-C34FCAF4804D}");

            Assert.IsTrue(searchResult.Rows.Count > 0);
        }
        [TestMethod]
        public void AuditSearch_GetAuditRecordsForElementWithConvertedAuditsOnly()
        {
            var searchResult = new EaAccess(dataSource).GetAuditEntriesForElement("{E0FAE36D-59F9-45b7-831A-4265EE802CFE}");

            Assert.IsTrue(searchResult.Rows.Count > 0);
        }
        [TestMethod]
        public void AuditSearch_GetAuditRecordsForElementWithNoAudits()
        {
            var searchResult = new EaAccess(dataSource).GetAuditEntriesForElement("{2453E87C-7106-47da-B6B8-76F465CAF206}");

            Assert.IsTrue(searchResult.Rows.Count == 0);
        }
        [TestMethod]
        public void AuditSearch_VerifyResultColumns()
        {
            var searchResult = new EaAccess(dataSource).GetAuditEntriesForElement("{2453E87C-7106-47da-B6B8-76F465CAF206}");

            Assert.IsTrue(searchResult.Fields[0].Name == "CLASSTYPE", "CLASSTYPE not found in results");
            Assert.IsTrue(searchResult.Fields[1].Name == "CLASSGUID", "CLASSGUID not found in results");
            Assert.IsTrue(searchResult.Fields[2].Name == "Name", "Name not found in results");
            Assert.IsTrue(searchResult.Fields[3].Name == "Change Type", "Change Type not found in results");
            Assert.IsTrue(searchResult.Fields[4].Name == "Property", "Property not found in results");
            Assert.IsTrue(searchResult.Fields[5].Name == "Old Value", "Old Value not found in results");
            Assert.IsTrue(searchResult.Fields[6].Name == "New Value", "New Value not found in results");
            Assert.IsTrue(searchResult.Fields[7].Name == "Audit User", "Audit User not found in results");
            Assert.IsTrue(searchResult.Fields[8].Name == "Audit DateTime", "Audit DateTime not found in results");
        }
        [TestMethod]
        public void AuditSearch_CallingAddIn()
        {
            var addIn = new EAAddIn.EAAddIn();

            Mock<Repository> mockRepository = new Mock<Repository>();
            Mock<Element> mockElement = new Mock<Element>();
            mockRepository.Expect(a => a.GetTreeSelectedObject()).Returns(mockElement.Object);
            mockElement.ExpectGet(p => p.ElementID).Returns(2000);
            mockElement.ExpectGet(p => p.ElementGUID).Returns("{62B8BBFB-57F8-46a2-80B1-C34FCAF4804D}");
            
            var searchTerm = string.Empty;
            string xmlResults = string.Empty;

            addIn.AuditSearch(mockRepository.Object, searchTerm, out xmlResults, new EaAccess(dataSource));

            Assert.IsFalse(string.IsNullOrEmpty(xmlResults));
        }
    }
}
