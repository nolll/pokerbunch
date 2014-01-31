using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Chart;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Chart{

	class CashgameChartPageModelFactoryTests : MockContainer {

		private Homegame _homegame;
		private CashgameSuite _suite;
		private int? _year;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
            _suite = new FakeCashgameSuite();
			_year = null;
		}

        [Test]
		public void test_ChartDataUrl_IsSet()
        {
            const string chartJsonUrl = "a";
            GetMock<IUrlProvider>().Setup(o => o.GetCashgameChartJsonUrl(_homegame.Slug, _year)).Returns(chartJsonUrl);

			var result = GetResult();

            Assert.AreEqual(chartJsonUrl, result.ChartDataUrl);
		}

	    private CashgameChartPageModel GetResult()
	    {
	        var years = new List<int> {1, 2, 3};
            return GetSut().Create(new FakeUser(), _homegame, _year, years);
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