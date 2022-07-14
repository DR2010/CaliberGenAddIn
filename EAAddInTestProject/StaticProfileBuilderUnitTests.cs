using EAAddIn.Presenters;
using EAAddIn.Windows.Interfaces;
using EAAddIn.Applications.StaticProfileBuilder;
using EAAddInTestProject.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EA;

namespace EAAddInTestProject
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class StaticProfileBuilderUnitTests
    {
        private Mock<Repository> mockRepository;
        private StaticProfileBuilder staticProfileBuilder;
        private Mock<IStaticProfileBuilder> mockView;
        private StaticProfileBuilderPresenter presenter;
        private Mock<Diagram> mockDiagram;

        public StaticProfileBuilderUnitTests()
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

        public void SetupModelViewPresenter()
        {
            mockDiagram = new Mock<Diagram>();
            mockDiagram.Expect(a => a.Name).Returns("DefaultDiagram");

            mockRepository = new Mock<Repository>();
            mockRepository.Expect(a => a.GetCurrentDiagram()).Returns(mockDiagram.Object);

            staticProfileBuilder = new StaticProfileBuilder(mockRepository.Object);
            mockView = new Mock<IStaticProfileBuilder>();
            presenter = new StaticProfileBuilderPresenter(staticProfileBuilder, mockView.Object);
        }

        //[TestMethod]
        //public void CreateXMLFromDiagram()
        //{
        //    SetupModelViewPresenter();

        //    staticProfileBuilder.CreateXMLFromDiagram();
            
        //    Assert.IsNotNull(staticProfileBuilder.Xml);
        //}

        [TestMethod]
        public void CreateXMLRequiresDiagram()
        {
            SetupModelViewPresenter();

            mockDiagram.Expect(a => a.Name).Returns(string.Empty);

            Assert.IsFalse(staticProfileBuilder.CreateXMLFromDiagram());
        }

        [TestMethod]
        public void DefaultDiagramLoadsFromModel()
        {
            SetupModelViewPresenter();

            Assert.IsFalse(string.IsNullOrEmpty(staticProfileBuilder.Diagram));
        }

        [TestMethod]
        public void PresenterInstatiatesModelAndView()
        {
            SetupModelViewPresenter();

            Assert.IsNotNull(presenter.View);
            Assert.IsNotNull(presenter.Model);
        }

        [TestMethod]
        public void ViewLoadsDiagramFromModel()
        {
            var mockDiagram = new Mock<Diagram>();
            mockDiagram.Expect(a => a.Name).Returns("DefaultDiagram");

            mockRepository = new Mock<Repository>();
            mockRepository.Expect(a => a.GetCurrentDiagram()).Returns(mockDiagram.Object);

            staticProfileBuilder = new StaticProfileBuilder(mockRepository.Object);

            var view = new StaticProfileBuilderViewStub();
            presenter = new StaticProfileBuilderPresenter(staticProfileBuilder, view);

            Assert.IsFalse(string.IsNullOrEmpty(presenter.View.Diagram));
        }
    }
}
