using System;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfEAGenCaliber;

namespace TestProject1
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        private static ServiceHost svcHost;

        [ClassInitialize]
        public static void StartServiceHost(TestContext testContext)
        {
            svcHost = new ServiceHost(typeof(Service1));
            svcHost.Open();
        }

        [ClassCleanup]
        public static void StopServiceHost()
        {
            if (svcHost != null)
            {
                if (svcHost.State == CommunicationState.Opened)
                {
                    svcHost.Close();
                }
            }
        }

        
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        internal IService1 CreateIService1()
        {
            return new ChannelFactory<IService1>("IService1").CreateChannel();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //
            //  Client side code...
            //
            
            var x = CreateIService1();
            
            var t = x.GetData(4);

            // Assert.IsTrue(((ChannelFactory)x).State == CommunicationState.Opened);

            Assert.IsNotNull(x);
            Assert.IsTrue(t.Length > 0);

        }

        [TestMethod]
        public void TestMethod2()
        {

            var xSer = CreateIService1();

            string xx = xSer.GetEAElementID();
            Assert.AreEqual(xx,"Test");
            
        }

    }
}
