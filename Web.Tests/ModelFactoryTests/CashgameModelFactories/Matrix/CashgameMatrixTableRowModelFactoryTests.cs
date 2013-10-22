using System.Collections.Generic;
using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Matrix;

namespace Web.Tests.ModelFactoryTests.CashgameModelFactories.Matrix{

    public class CashgameMatrixTableRowModelFactoryTests : WebMockContainer {

		private Homegame _homegame;
        private CashgameSuite _suite;
        private Player _player;
		private CashgameTotalResult _result;
		private int _rank;

        [SetUp]
		public void SetUp(){
			_homegame = new Homegame();
            _suite = GetSuite();
            _player = new Player
                {
                    DisplayName = "player name"
                };
            _result = new CashgameTotalResult
                {
                    Player = _player
                };
            _rank = 1;
		}

        [Test]
		public void TableRow_RankIsSet(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual(1, result.Rank);
		}

		[Test]
		public void TableRow_PlayerNameIsSet(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual("player name", result.Name);
			Assert.AreEqual("player%20name", result.UrlEncodedName);
		}

		[Test]
		public void TableRow_TotalResultIsSet(){
			_result.Winnings = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual("+$1", result.TotalResult);
		}

		[Test]
		public void TableRow_WithPositiveResult_WinningsClassIsSetToPosResult(){
			_result.Winnings = 1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual("pos-result", result.ResultClass);
		}

		[Test]
		public void TableRow_WithPositiveResult_WinningsClassIsSetToNegResult(){
			_result.Winnings = -1;

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual("neg-result", result.ResultClass);
		}

		[Test]
		public void TableRow_PlayerUrlIsSet()
		{
		    const string playerUrl = "a";
		    Mocks.UrlProviderMock.Setup(o => o.GetPlayerDetailsUrl(_homegame, _player)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		[Test]
		public void TableRow_CellModelsAreSet(){
			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual(1, result.CellModels.Count);
			Assert.IsInstanceOf<CashgameMatrixTableCellModel>(result.CellModels[0]);
		}

		private CashgameMatrixTableRowModelFactory GetSut()
		{
		    return new CashgameMatrixTableRowModelFactory(
                Mocks.UrlProviderMock.Object);
		}

        private CashgameSuite GetSuite()
        {
            return new CashgameSuite
            {
                Cashgames = new List<Cashgame>{new Cashgame()}
            };
        }

	}

}