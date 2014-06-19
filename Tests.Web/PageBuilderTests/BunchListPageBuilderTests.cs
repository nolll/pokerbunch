using System.Collections.Generic;
using Application.UseCases.AppContext;
using Application.UseCases.BunchList;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.HomegameModelFactories;

namespace Tests.Web.PageBuilderTests
{
    class BunchListPageBuilderTests : MockContainer
    {
        private BunchListPageBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new BunchListPageBuilder(
                GetMock<IAppContextInteractor>().Object,
                GetMock<IBunchListInteractor>().Object);
        }

        [Test]
        public void Build_BrowserTitleIsSet()
        {
            var showBunchListResult = new BunchListResultInTest();

            GetMock<IBunchListInteractor>().Setup(o => o.Execute()).Returns(showBunchListResult);
            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(new AppContextResultInTest());
            
            var result = _sut.Build();

            Assert.AreEqual("Bunches", result.BrowserTitle);
        }

        [Test]
        public void Build_PagePropertiesIsSet()
        {
            var showBunchListResult = new BunchListResultInTest();

            GetMock<IBunchListInteractor>().Setup(o => o.Execute()).Returns(showBunchListResult);
            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(new AppContextResultInTest());

            var result = _sut.Build();

            Assert.IsNotNull(result.PageProperties);
        }

        [Test]
        public void Build_BunchModelsAreSet()
        {
            var bunchItems = new List<BunchListItem>();
            var showBunchListResult = new BunchListResultInTest(bunchItems);
            
            GetMock<IBunchListInteractor>().Setup(o => o.Execute()).Returns(showBunchListResult);
            GetMock<IAppContextInteractor>().Setup(o => o.Execute()).Returns(new AppContextResultInTest());

            var result = _sut.Build();

            Assert.AreEqual(bunchItems, result.BunchModels);
        }
    }
}
