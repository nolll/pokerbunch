using System.Collections.Generic;
using Core.UseCases;
using Core.UseCases.ShowBunchList;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.List;
using Web.Models.PageBaseModels;

namespace Tests.Web.PageBuilderTests
{
    class BunchListPageBuilderTests : MockContainer
    {
        private BunchListPageBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new BunchListPageBuilder(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IBunchListItemModelFactory>().Object,
                GetMock<IShowBunchListInteractor>().Object);
        }

        [Test]
        public void Build_BrowserTitleIsSet()
        {
            var showBunchListResult = new ShowBunchListResult();

            GetMock<IShowBunchListInteractor>().Setup(o => o.Execute()).Returns(showBunchListResult);

            var result = _sut.Build();

            Assert.AreEqual("Bunches", result.BrowserTitle);
        }

        [Test]
        public void Build_PagePropertiesIsSet()
        {
            var pageProperties = new PageProperties();
            var showBunchListResult = new ShowBunchListResult();

            GetMock<IShowBunchListInteractor>().Setup(o => o.Execute()).Returns(showBunchListResult);
            GetMock<IPagePropertiesFactory>().Setup(o => o.Create()).Returns(pageProperties);

            var result = _sut.Build();

            Assert.IsNotNull(result.PageProperties);
        }

        [Test]
        public void Build_BunchModelsAreSet()
        {
            var bunchItems = new List<BunchListItem>();
            var showBunchListResult = new ShowBunchListResult { Bunches = bunchItems };
            var models = new List<BunchListItemModel>();
            
            GetMock<IShowBunchListInteractor>().Setup(o => o.Execute()).Returns(showBunchListResult);
            GetMock<IBunchListItemModelFactory>().Setup(o => o.CreateList(bunchItems)).Returns(models);

            var result = _sut.Build();

            Assert.AreEqual(bunchItems, result.BunchModels);
        }
    }
}
