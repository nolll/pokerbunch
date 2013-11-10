using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.Models.CashgameModels.Chart;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Chart{

	class CashgameChartPageModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
		private CashgameSuite _suite;
		private int? _year;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_suite = new CashgameSuite();
			_year = null;
		}

        [Test]
		public void test_ChartDataUrl_IsSet()
        {
            const string chartJsonUrl = "a";
            Mocks.UrlProviderMock.Setup(o => o.GetCashgameChartJsonUrl(_homegame, _year)).Returns(chartJsonUrl);

			var result = GetResult();

            Assert.AreEqual(chartJsonUrl, result.ChartDataUrl);
		}

	    private CashgameChartPageModel GetResult()
	    {
	        var years = new List<int> {1, 2, 3};
            return GetSut().Create(new FakeUser(), _homegame, _year, years, null);
		}

        private CashgameChartPageModelFactory GetSut()
		{
            return new CashgameChartPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.UrlProviderMock.Object,
                Mocks.CashgameNavigationModelFactoryMock.Object);
		}

	}

}