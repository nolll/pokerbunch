using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.Services;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Leaderboard{

	public class CashgameLeaderboardTableItemModelFactoryTests : MockContainer {

		private Homegame _homegame;
	    private Player _player;
		private CashgameTotalResult _result;
		private int _rank;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_result = new CashgameTotalResult();
			_player = new FakePlayer(displayName: "player name");
            _result.Player = _player;
			_rank = 1;
		}

        [Test]
		public void TableItem_RankIsSet(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual(1, result.Rank);
		}

		[Test]
		public void TableItem_PlayerNameIsSet(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual("player name", result.Name);
			Assert.AreEqual("player%20name", result.UrlEncodedName);
		}

		[Test]
		public void TableItem_TotalResultIsSet()
		{
		    const string formattedResult = "a";
			_result.Winnings = 1;

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), _result.Winnings)).Returns(formattedResult);

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual(formattedResult, result.TotalResult);
		}

		[Test]
		public void TableItem_WinningsClassIsSet(){
            const string resultClass = "a";
            GetMock<IResultFormatter>().Setup(o => o.GetWinningsCssClass(_result.Winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void TableItem_WithDuration_DurationIsSet()
		{
		    const string formattedTime = "a";
            _result.TimePlayed = 60;

            GetMock<IGlobalization>().Setup(o => o.FormatDuration(_result.TimePlayed)).Returns(formattedTime);

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

            Assert.AreEqual(formattedTime, result.GameTime);
		}

		[Test]
		public void TableItem_WithDuration_WinrateIsSet()
		{
		    const string formattedWinRate = "a";
            _result.WinRate = 1;

            GetMock<IGlobalization>().Setup(o => o.FormatWinrate(It.IsAny<CurrencySettings>(), _result.WinRate)).Returns(formattedWinRate);

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual(formattedWinRate, result.WinRate);
		}

		[Test]
		public void TableItem_PlayerUrlIsSet()
		{
		    const string playerUrl = "a";
		    GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(_homegame, _player)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

            Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		private CashgameLeaderboardTableItemModelFactory GetSut(){
			return new CashgameLeaderboardTableItemModelFactory(
                GetMock<IUrlProvider>().Object,
                GetMock<IResultFormatter>().Object,
                GetMock<IGlobalization>().Object);
		}

	}

}