using Core.Classes;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Facts{

	public class CashgameFactsPageModelFactoryTests : MockContainer
    {
		[Test]
        public void GameCount_SuiteHasGameCount_IsSet()
		{
		    const int gameCount = 1;
            var suite = new FakeCashgameSuite(gameCount:gameCount);

		    var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

			Assert.AreEqual(gameCount, result.GameCount);
		}

		[Test]
        public void TotalGameTime_SuiteHasTotalGameTime_IsSet()
		{
		    const string formattedDuration = "a";
            const int totalGameTime = 1;
            var suite = new FakeCashgameSuite(totalGameTime: totalGameTime);

            GetMock<IGlobalization>().Setup(o => o.FormatDuration(totalGameTime)).Returns(formattedDuration);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

			Assert.AreEqual(formattedDuration, result.TotalGameTime);
		}

		[Test]
        public void BestResultAmount_SuiteHasBestResult_IsSet()
		{
		    const string formattedWinnings = "a";
		    const int winnings = 1;
			var cashgameResult = new FakeCashgameResult(winnings: winnings);
            var suite = new FakeCashgameSuite(bestResult: cashgameResult);

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedWinnings);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

			Assert.AreEqual(formattedWinnings, result.BestResultAmount);
		}

		[Test]
        public void BestResultName_SuiteHasBestResult_IsSet(){
			var player = new FakePlayer(displayName: "a");
		    var cashgameResult = new FakeCashgameResult(player);
            var suite = new FakeCashgameSuite(bestResult: cashgameResult);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

			Assert.AreEqual("a", result.BestResultName);
		}

		[Test]
        public void WorstResultAmount_SuiteHasWorstResult_IsSet(){
            const string formattedWinnings = "a";
            const int winnings = 1;
			var cashgameResult = new FakeCashgameResult(winnings: winnings);
            var suite = new FakeCashgameSuite(worstResult: cashgameResult);

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedWinnings);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

            Assert.AreEqual(formattedWinnings, result.WorstResultAmount);
		}

		[Test]
        public void WorstResultName_SuiteHasWorstResult_IsSet(){
			var player = new FakePlayer(displayName: "a");
		    var cashgameResult = new FakeCashgameResult(player);
            var suite = new FakeCashgameSuite(worstResult: cashgameResult);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

			Assert.AreEqual("a", result.WorstResultName);
		}

		[Test]
        public void MostTimeDuration_SuiteHasBestResult_IsSet()
		{
		    const string formattedTime = "a";
		    const int timePlayed = 1;
			var cashgameResult = new FakeCashgameTotalResult(timePlayed: timePlayed);
            var suite = new FakeCashgameSuite(mostTimeResult: cashgameResult);

            GetMock<IGlobalization>().Setup(o => o.FormatDuration(timePlayed)).Returns(formattedTime);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

			Assert.AreEqual(formattedTime, result.MostTimeDuration);
		}

		[Test]
        public void MostTimeName_SuiteHasBestResult_IsSet()
		{
		    const string displayName = "a";
			var player = new FakePlayer(displayName: displayName);
		    var cashgameResult = new FakeCashgameTotalResult(player: player);
            var suite = new FakeCashgameSuite(mostTimeResult: cashgameResult);

            var sut = GetSut();
            var result = sut.Create(new FakeUser(), new FakeHomegame(), suite);

			Assert.AreEqual(displayName, result.MostTimeName);
		}

		private CashgameFactsPageModelFactory GetSut(){
            return new CashgameFactsPageModelFactory(
                GetMock<IPagePropertiesFactory>().Object,
                GetMock<ICashgameNavigationModelFactory>().Object,
                GetMock<IGlobalization>().Object);
		}

	}

}