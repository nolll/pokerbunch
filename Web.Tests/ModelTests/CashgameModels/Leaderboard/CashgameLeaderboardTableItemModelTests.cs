using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.UrlModels;

namespace Web.Tests.ModelTests.CashgameModels.Leaderboard{

	public class CashgameLeaderboardTableItemModelTests : WebMockContainer {

		private Homegame _homegame;
		private CashgameTotalResult _result;
		private int _rank;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
			_result = new CashgameTotalResult();
			var player = new Player {DisplayName = "player name"};
            _result.Player = player;
			_rank = 1;
		}

        [Test]
		public void TableItem_RankIsSet(){
			var sut = GetSut();

			Assert.AreEqual(1, sut.Rank);
		}

		[Test]
		public void TableItem_PlayerNameIsSet(){
			var sut = GetSut();

			Assert.AreEqual("player name", sut.Name);
			Assert.AreEqual("player%20name", sut.UrlEncodedName);
		}

		[Test]
		public void TableItem_TotalResultIsSet(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("+$1", sut.TotalResult);
		}

		[Test]
		public void TableItem_WithPositiveResult_WinningsClassIsSetToPosResult(){
			_result.Winnings = 1;

			var sut = GetSut();

			Assert.AreEqual("pos-result", sut.ResultClass);
		}

		[Test]
		public void TableItem_WithPositiveResult_WinningsClassIsSetToNegResult(){
			_result.Winnings = -1;

			var sut = GetSut();

			Assert.AreEqual("neg-result", sut.ResultClass);
		}

		[Test]
		public void TableItem_WithDuration_DurationIsSet(){
			_result.TimePlayed = 60;

			var sut = GetSut();

			Assert.AreEqual("1h", sut.GameTime);
		}

		[Test]
		public void TableItem_WithDuration_WinrateIsSet(){
			_result.WinRate = 1;

			var sut = GetSut();

			Assert.AreEqual("$1/h", sut.WinRate);
		}

		[Test]
		public void TableItem_PlayerUrlIsSet(){
			var sut = GetSut();

            Assert.IsInstanceOf<PlayerDetailsUrlModel>(sut.PlayerUrl);
		}

		private CashgameLeaderboardTableItemModel GetSut(){
			return new CashgameLeaderboardTableItemModel(_homegame, _result, _rank);
		}

	}

}