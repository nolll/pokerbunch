using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.Models.CashgameModels.Facts;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Facts{

	public class CashgameFactsPageModelFactoryTests : MockContainer
    {
        private CashgameSuite _suite;

        [SetUp]
		public void SetUp(){
			_suite = new CashgameSuite();
		}

		[Test]
        public void GameCount_SuiteHasGameCount_IsSet(){
			_suite.GameCount = 1;
			
            var result = GetResult();

			Assert.AreEqual(1, result.GameCount);
		}

		[Test]
        public void TotalGameTime_SuiteHasTotalGameTime_IsSet()
		{
		    const string formattedDuration = "a";
            _suite.TotalGameTime = 1;

		    Mocks.GlobalizationMock.Setup(o => o.FormatDuration(_suite.TotalGameTime)).Returns(formattedDuration);
			
            var result = GetResult();

			Assert.AreEqual(formattedDuration, result.TotalGameTime);
		}

		[Test]
        public void BestResultAmount_SuiteHasBestResult_IsSet()
		{
		    const string formattedWinnings = "a";
		    const int winnings = 1;
			var cashgameResult = new FakeCashgameResult(winnings: winnings);
		    _suite.BestResult = cashgameResult;

            Mocks.GlobalizationMock.Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedWinnings);

			var result = GetResult();

			Assert.AreEqual(formattedWinnings, result.BestResultAmount);
		}

		[Test]
        public void BestResultName_SuiteHasBestResult_IsSet(){
			var player = new FakePlayer(displayName: "a");
		    var cashgameResult = new FakeCashgameResult(player);
		    _suite.BestResult = cashgameResult;
			
            var result = GetResult();

			Assert.AreEqual("a", result.BestResultName);
		}

		[Test]
        public void WorstResultAmount_SuiteHasWorstResult_IsSet(){
            const string formattedWinnings = "a";
            const int winnings = 1;
			var cashgameResult = new FakeCashgameResult(winnings: winnings);
		    _suite.WorstResult = cashgameResult;

            Mocks.GlobalizationMock.Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedWinnings);

            var result = GetResult();

            Assert.AreEqual(formattedWinnings, result.WorstResultAmount);
		}

		[Test]
        public void WorstResultName_SuiteHasWorstResult_IsSet(){
			var player = new FakePlayer(displayName: "a");
		    var cashgameResult = new FakeCashgameResult(player);
		    _suite.WorstResult = cashgameResult;

			var result = GetResult();

			Assert.AreEqual("a", result.WorstResultName);
		}

		[Test]
        public void MostTimeDuration_SuiteHasBestResult_IsSet()
		{
		    const string formattedTime = "a";
		    const int timePlayed = 1;
			var cashgameResult = new CashgameTotalResult {TimePlayed = timePlayed};
		    _suite.MostTimeResult = cashgameResult;

            Mocks.GlobalizationMock.Setup(o => o.FormatDuration(timePlayed)).Returns(formattedTime);

            var result = GetResult();

			Assert.AreEqual(formattedTime, result.MostTimeDuration);
		}

		[Test]
        public void MostTimeName_SuiteHasBestResult_IsSet()
		{
		    const string displayName = "a";
			var player = new FakePlayer(displayName: displayName);
		    var cashgameResult = new CashgameTotalResult {Player = player};
		    _suite.MostTimeResult = cashgameResult;
			
            var result = GetResult();

			Assert.AreEqual(displayName, result.MostTimeName);
		}

        private CashgameFactsPageModel GetResult()
        {
            return GetSut().Create(new FakeUser(), new FakeHomegame(), _suite);
        }

		private CashgameFactsPageModelFactory GetSut(){
            return new CashgameFactsPageModelFactory(
                Mocks.PagePropertiesFactoryMock.Object,
                Mocks.CashgameNavigationModelFactoryMock.Object,
                Mocks.GlobalizationMock.Object);
		}

	}

}