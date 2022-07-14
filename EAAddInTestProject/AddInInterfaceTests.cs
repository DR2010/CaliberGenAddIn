using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using EA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EAAddInTestProject
{
    [TestClass]
    public class AddInInterfaceTests
    {
        private EAAddIn.EAAddIn addin = new EAAddIn.EAAddIn();
        private string version;
        Mock<Repository> mockRepository = new Mock<Repository>();
        private string message;

        public void SetMessage(string text)
        {
            message = text;    
        }

        [TestInitialize]
        public void Initialise()
        {
            version = "-&DEEWR AddIn v" + EAAddIn.EAAddIn.Version;
            message = string.Empty;
            addin = new EAAddIn.EAAddIn(SetMessage);
        }

        [TestMethod]
        public void GetVersion()
        {
            Assert.IsFalse(string.IsNullOrEmpty(EAAddIn.EAAddIn.Version));
        }

        [TestMethod]
        public void RootMenuReturned()
        {
            string Location = string.Empty;


            var menu = addin.EA_GetMenuItems(mockRepository.Object, Location, version);

            Assert.IsTrue(((string[])menu).Length > 0);
        }
        [TestMethod]
        public void AdminMenuReturned()
        {
            string Location = string.Empty;


            var menu = addin.EA_GetMenuItems(mockRepository.Object, Location, "-&Admin");

            Assert.IsTrue(((string[])menu).Length > 0);
        }
        [TestMethod]
        public void AdminClickLocalEAFileRaisesError()
        {
            mockRepository.ExpectGet(p => p.ConnectionString).Returns(string.Empty);

            addin.EA_MenuClick(mockRepository.Object, "", "", "", SetMessage);

            Assert.IsFalse(string.IsNullOrEmpty(message));
        }

        [TestMethod]
        public void AdminClickNoSecurityRaisesError()
        {
            mockRepository.ExpectGet(p => p.ConnectionString).Returns("hasConnectionString");
            mockRepository.ExpectGet(p => p.IsSecurityEnabled).Returns(false);

            addin.EA_MenuClick(mockRepository.Object, "", "", "" );

            Assert.IsFalse(string.IsNullOrEmpty(message));
        }
    }
}
