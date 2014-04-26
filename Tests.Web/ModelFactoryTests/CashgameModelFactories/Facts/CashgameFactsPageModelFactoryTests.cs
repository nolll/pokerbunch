using Application.Services;
using Core.Classes;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Facts{

	public class CashgameFactsPageModelFactoryTests : MockContainer
    {
        //[Test]
        //public void GameCount_SuiteHasGameCount_IsSet()
        //{
        //    const int gameCount = 1;
        //    var facts = new FakeCashgameFacts(gameCount);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual(gameCount, result.GameCount);
        //}

        //[Test]
        //public void TotalGameTime_SuiteHasTotalGameTime_IsSet()
        //{
        //    const string formattedDuration = "a";
        //    const int totalGameTime = 1;
        //    var facts = new FakeCashgameFacts(totalGameTime: totalGameTime);

        //    GetMock<IGlobalization>().Setup(o => o.FormatDuration(totalGameTime)).Returns(formattedDuration);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual(formattedDuration, result.TotalGameTime);
        //}

        //[Test]
        //public void TotalTurnover_SuiteHasTotalTurnover_IsSet()
        //{
        //    const string formattedTurnover = "a";
        //    const int totalTurnover = 1;
        //    var facts = new FakeCashgameFacts(totalTurnover: totalTurnover);

        //    GetMock<IGlobalization>().Setup(o => o.FormatCurrency(It.IsAny<CurrencySettings>(), totalTurnover)).Returns(formattedTurnover);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual(formattedTurnover, result.TotalTurnover);
        //}

        //[Test]
        //public void BestResultAmount_SuiteHasBestResult_IsSet()
        //{
        //    const string formattedWinnings = "a";
        //    const int winnings = 1;
        //    var cashgameResult = new FakeCashgameResult(winnings: winnings);
        //    var facts = new FakeCashgameFacts(bestResult: cashgameResult);

        //    GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedWinnings);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual(formattedWinnings, result.BestResultAmount);
        //}

        //[Test]
        //public void BestResultName_SuiteHasBestResult_IsSet(){
        //    const int playerId = 1;
        //    var player = new FakePlayer(displayName: "a");
        //    var cashgameResult = new FakeCashgameResult(playerId);
        //    var facts = new FakeCashgameFacts(bestResult: cashgameResult);

        //    GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId)).Returns(player);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual("a", result.BestResultName);
        //}

        //[Test]
        //public void WorstResultAmount_SuiteHasWorstResult_IsSet(){
        //    const string formattedWinnings = "a";
        //    const int winnings = 1;
        //    var cashgameResult = new FakeCashgameResult(winnings: winnings);
        //    var facts = new FakeCashgameFacts(worstResult: cashgameResult);

        //    GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedWinnings);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual(formattedWinnings, result.WorstResultAmount);
        //}

        //[Test]
        //public void WorstResultName_SuiteHasWorstResult_IsSet()
        //{
        //    const int playerId = 1;
        //    var player = new FakePlayer(displayName: "a");
        //    var cashgameResult = new FakeCashgameResult(playerId);
        //    var facts = new FakeCashgameFacts(worstResult: cashgameResult);

        //    GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId)).Returns(player);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual("a", result.WorstResultName);
        //}

        //[Test]
        //public void MostTimeDuration_SuiteHasBestResult_IsSet()
        //{
        //    const string formattedTime = "a";
        //    const int timePlayed = 1;
        //    var cashgameResult = new FakeCashgameTotalResult(timePlayed: timePlayed);
        //    var facts = new FakeCashgameFacts(mostTimeResult: cashgameResult);

        //    GetMock<IGlobalization>().Setup(o => o.FormatDuration(timePlayed)).Returns(formattedTime);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual(formattedTime, result.MostTimeDuration);
        //}

        //[Test]
        //public void MostTimeName_SuiteHasBestResult_IsSet()
        //{
        //    const string displayName = "a";
        //    const int playerId = 1;
        //    var player = new FakePlayer(displayName: displayName);
        //    var cashgameResult = new FakeCashgameTotalResult(playerId: playerId);
        //    var facts = new FakeCashgameFacts(mostTimeResult: cashgameResult);

        //    GetMock<IPlayerRepository>().Setup(o => o.GetById(playerId)).Returns(player);

        //    var sut = GetSut();
        //    var result = sut.Create(new FakeHomegame(), facts);

        //    Assert.AreEqual(displayName, result.MostTimeName);
        //}

        //private CashgameFactsPageModelFactory GetSut(){
        //    return new CashgameFactsPageModelFactory(
        //        GetMock<IPagePropertiesFactory>().Object,
        //        GetMock<IGlobalization>().Object,
        //        GetMock<IPlayerRepository>().Object,
        //        GetMock<ICashgamePageNavigationModelFactory>().Object,
        //        GetMock<ICashgameYearNavigationModelFactory>().Object);
        //}

	}

}