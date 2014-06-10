using System.Collections.Generic;
using Application.Services;
using Core.Entities;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Chart;
using Web.Models.UrlModels;
using Web.Services;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Chart
{
    class CashgameChartPageModelFactoryTests : MockContainer
    {
        private Homegame _homegame;
        private int? _year;

        [SetUp]
        public void SetUp()
        {
            _homegame = new FakeHomegame();
            _year = null;
        }

        [Test]
        public void test_ChartDataUrl_IsSet()
        {
            var result = GetResult();

            Assert.IsInstanceOf<CashgameChartJsonUrlModel>(result.ChartDataUrl);
        }

        private CashgameChartPageModel GetResult()
        {
            var years = new List<int> { 1, 2, 3 };
            return GetSut().Create(_homegame, _year, years);
        }

        private CashgameChartPageModelFactory GetSut()
        {
            return new CashgameChartPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<IUrlProvider>().Object,
                GetMock<ICashgamePageNavigationModelFactory>().Object,
                GetMock<ICashgameYearNavigationModelFactory>().Object);
        }
    }
}