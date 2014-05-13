using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Matrix;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Matrix{

    public class CashgameMatrixTableRowModelFactoryTests : MockContainer {

		private Homegame _homegame;
        private CashgameSuite _suite;
        private Player _player;
		private int _rank;

        [SetUp]
		public void SetUp(){
			_homegame = new FakeHomegame();
            _suite = GetSuite();
            _player = new FakePlayer(displayName: "player name");
            _rank = 1;
		}

        [Test]
		public void TableRow_RankIsSet(){
            var totalResult = new FakeCashgameTotalResult();

            var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

			Assert.AreEqual(1, result.Rank);
		}

		[Test]
		public void TableRow_PlayerNameIsSet(){
            var totalResult = new FakeCashgameTotalResult();

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

			Assert.AreEqual("player name", result.Name);
			Assert.AreEqual("player%20name", result.UrlEncodedName);
		}

		[Test]
		public void TableRow_TotalResultIsSet()
		{
		    const string formatted = "a";
		    const int winnings = 1;
            var totalResult = new FakeCashgameTotalResult(winnings);

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<Currency>(), winnings)).Returns(formatted);

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

			Assert.AreEqual(formatted, result.TotalResult);
		}

		[Test]
		public void TableRow_WinningsClassIsSet(){
			const string resultClass = "pos-result";
            var totalResult = new FakeCashgameTotalResult(winnings:1);

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

			Assert.AreEqual(resultClass, result.ResultClass);
		}

		[Test]
		public void TableRow_PlayerUrlIsSet()
		{
		    const string playerUrl = "a";
            var totalResult = new FakeCashgameTotalResult();

		    GetMock<IUrlProvider>().Setup(o => o.GetPlayerDetailsUrl(_homegame.Slug, _player.DisplayName)).Returns(playerUrl);

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

			Assert.AreEqual(playerUrl, result.PlayerUrl);
		}

		[Test]
		public void TableRow_CellModelsAreSet(){
            var totalResult = new FakeCashgameTotalResult();

		    var cellModelList = new List<CashgameMatrixTableCellModel> {new CashgameMatrixTableCellModel()};
            GetMock<ICashgameMatrixTableCellModelFactory>().Setup(o => o.CreateList(_suite.Cashgames, _player)).Returns(cellModelList);

			var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

			Assert.AreEqual(1, result.CellModels.Count);
		}

        [Test]
        public void Results_IsCorrectLength()
        {
            var homegame = new FakeHomegame();
            var suite = new FakeCashgameSuite();
            var totalResult = new FakeCashgameTotalResult();
            var totalResults = new List<CashgameTotalResult>{totalResult, totalResult};
            var player = new FakePlayer();

            GetMock<IPlayerRepository>().Setup(o => o.GetById(It.IsAny<int>())).Returns(player);

            var sut = GetSut();
            var result = sut.CreateList(homegame, suite, totalResults);

            Assert.AreEqual(2, result.Count);
        }

		private CashgameSuite GetSuite()
        {
            return new FakeCashgameSuite
            (
                new List<Cashgame>{new FakeCashgame()}
            );
        }
    
        private CashgameMatrixTableRowModelFactory GetSut()
		{
		    return new CashgameMatrixTableRowModelFactory(
                GetMock<IUrlProvider>().Object,
                GetMock<ICashgameMatrixTableCellModelFactory>().Object,
                GetMock<IGlobalization>().Object,
                GetMock<IPlayerRepository>().Object);
		}
	}

}