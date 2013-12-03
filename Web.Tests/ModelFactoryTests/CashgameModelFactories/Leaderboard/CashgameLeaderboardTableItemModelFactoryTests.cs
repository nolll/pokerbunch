using Core.Classes;
using Core.Services;
using Infrastructure.System;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.Models.CashgameModels.Leaderboard;
using Web.Services;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Leaderboard{

	public class CashgameLeaderboardTableItemModelFactoryTests : MockContainer {

		private Homegame _homegame;
	    private Player _player;
		private int _rank;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
			_player = new FakePlayer(displayName: "player name");
			_rank = 1;
		}

        [Test]
		public void TableItem_RankIsSet(){
            var totalResult = new FakeCashgameTotalResult();
            
            var sut = GetSut();
            var result = sut.Create(_homegame, _player, totalResult, _rank, LeaderboardSortOrder.winnings);

			Assert.AreEqual(1, result.Rank);
		}

		[Test]
		public void TableItem_PlayerNameIsSet(){
            var totalResult = new FakeCashgameTotalResult();
            
            var sut = GetSut();
            var result = sut.Create(_homegame, _player, totalResult, _rank, LeaderboardSortOrder.winnings);

			Assert.AreEqual("player name", result.Name);
			Assert.AreEqual("player%20name", result.UrlEncodedName);
		}

		[Test]
		public void TableItem_TotalResultIsSet()
		{
		    const string formattedResult = "a";
		    const int winnings = 1;
            var totalResult = new FakeCashgameTotalResult(winnings:winnings);

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), winnings)).Returns(formattedResult);

			var sut = GetSut();
            var result = sut.Create(_homegame, _player, totalResult, _rank, LeaderboardSortOrder.winnings);

			Assert.AreEqual(formattedResult, result.TotalResult);
		}

		[Test]
		public void TableItem_WinningsClassIsSet(){
            const string resultClass = "a";
            var totalResult = new FakeCashgameTotalResult();

            GetMock<IResultFormatter>().Setup(o => o.GetWinningsCssClass(It.IsAny<int>())).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_homegame, _player, totalResult, _rank, LeaderboardSortOrder.winnings);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void TableItem_WithDuration_DurationIsSet()
		{
		    const string formattedTime = "a";
		    const int timePlayed = 60;
            var totalResult = new FakeCashgameTotalResult(timePlayed:timePlayed);

            GetMock<IGlobalization>().Setup(o => o.FormatDuration(timePlayed)).Returns(formattedTime);

			var sut = GetSut();
            var result = sut.Create(_homegame, _player, totalResult, _rank, LeaderboardSortOrder.winnings);

            Assert.AreEqual(formattedTime, result.GameTime);
		}

		[Test]
		public void TableItem_WithDuration_WinrateIsSet()
		{
		    const string formattedWinRate = "a";
            const int winRate = 1;
            var totalResult = new FakeCashgameTotalResult(winRate: winRate);

            GetMock<IGlobalization>().Setup(o => o.FormatWinrate(It.IsAny<CurrencySettings>(), winRate)).Returns(formattedWinRate);

			var sut = GetSut();
            var result = sut.Create(_homegame, _player, totalResult, _rank, LeaderboardSortOrder.winnings);

			Assert.AreEqual(formattedWinRate, result.WinRate);
		}

		[Test]
		public void TableItem_PlayerUrlIsSet()
		{
		    const string playerUrl = "a";
            var totalResult = new FakeCashgameTotalResult();

		    GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(_homegame, _player)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _player, totalResult, _rank, LeaderboardSortOrder.winnings);

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