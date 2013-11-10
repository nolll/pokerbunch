using System.Collections.Generic;
using Core.Classes;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
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
			_homegame = new FakeHomegame();
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
		public void TableRow_TotalResultIsSet()
		{
		    const string formatted = "a";
			_result.Winnings = 1;

            Mocks.GlobalizationMock.Setup(o => o.FormatResult(It.IsAny<CurrencySettings>(), _result.Winnings)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual(formatted, result.TotalResult);
		}

		[Test]
		public void TableRow_WinningsClassIsSet(){
			const string resultClass = "a";
            Mocks.ResultFormatterMock.Setup(o => o.GetWinningsCssClass(_result.Winnings)).Returns(resultClass);

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _result, _rank);

			Assert.AreEqual(resultClass, result.ResultClass);
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
		}

		private CashgameSuite GetSuite()
        {
            return new CashgameSuite
            {
                Cashgames = new List<Cashgame>{new FakeCashgame()}
            };
        }
    
        private CashgameMatrixTableRowModelFactory GetSut()
		{
		    return new CashgameMatrixTableRowModelFactory(
                Mocks.UrlProviderMock.Object,
                Mocks.CashgameMatrixTableCellModelFactoryMock.Object,
                Mocks.ResultFormatterMock.Object,
                Mocks.GlobalizationMock.Object);
		}
	}

}