using System.Collections.Generic;
using Core.UseCases;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
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
                GetMock<IHomegameListItemModelFactory>().Object,
                GetMock<IShowBunchList>().Object);
        }

        [Test]
        public void Build_BrowserTitleIsSet()
        {
            MockShowBunchListResult();

            var result = _sut.Create();

            Assert.AreEqual("Bunches", result.BrowserTitle);
        }

        [Test]
        public void Build_PagePropertiesIsSet()
        {
            MockShowBunchListResult();
            MockPageProperties();

            var result = _sut.Create();

            Assert.IsNotNull(result.PageProperties);
        }

        [Test]
        public void Build_BunchModelsAreSet()
        {
            var bunchItem = new BunchItem();
            var bunchItems = new List<BunchItem> {bunchItem};
            MockShowBunchListResult(bunchItems);
            MockPageProperties();

            var result = _sut.Create();

            Assert.AreEqual(1, result.BunchModels.Count);
        }

        private void MockPageProperties()
        {
            var pageProperties = new PageProperties();
            GetMock<IPagePropertiesFactory>().Setup(o => o.Create()).Returns(pageProperties);
        }

        private void MockShowBunchListResult(List<BunchItem> bunchItems = null)
        {
            if (bunchItems == null)
                bunchItems = new List<BunchItem>();
            var showBunchListResult = new ShowBunchListResult {Bunches = bunchItems};
            GetMock<IShowBunchList>().Setup(o => o.Execute()).Returns(showBunchListResult);
        }
    }
}
