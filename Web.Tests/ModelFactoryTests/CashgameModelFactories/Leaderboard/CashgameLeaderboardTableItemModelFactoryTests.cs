using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Leaderboard{

	public class CashgameLeaderboardTableItemModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
	    private Player _player;
		private CashgameTotalResult _result;
		private int _rank;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_result = new CashgameTotalResult();
			_player = new Player {DisplayName = "player name"};
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
		public void TableItem_TotalResultIsSet(){
			_result.Winnings = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual("+$1", result.TotalResult);
		}

		[Test]
		public void TableItem_WinningsClassIsSet(){
            const string resultClass = "a";
            Mocks.ResultFormatterMock.Setup(o => o.GetWinningsCssClass(_result.Winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void TableItem_WithDuration_DurationIsSet(){
			_result.TimePlayed = 60;

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual("1h", result.GameTime);
		}

		[Test]
		public void TableItem_WithDuration_WinrateIsSet(){
			_result.WinRate = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

			Assert.AreEqual("$1/h", result.WinRate);
		}

		[Test]
		public void TableItem_PlayerUrlIsSet()
		{
		    const string playerUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetPlayerDetailsUrl(_homegame, _player)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _result, _rank);

            Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		private CashgameLeaderboardTableItemModelFactory GetSut(){
			return new CashgameLeaderboardTableItemModelFactory(
                Mocks.UrlProviderMock.Object,
                Mocks.ResultFormatterMock.Object);
		}

	}

}