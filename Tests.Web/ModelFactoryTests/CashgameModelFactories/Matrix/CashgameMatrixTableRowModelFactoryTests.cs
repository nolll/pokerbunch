using System.Collections.Generic;
using Application.Services;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Matrix;
using Web.Models.UrlModels;

namespace Tests.Web.ModelFactoryTests.CashgameModelFactories.Matrix
{
    public class CashgameMatrixTableRowModelFactoryTests : MockContainer
    {
        private Homegame _homegame;
        private CashgameSuite _suite;
        private Player _player;
        private int _rank;

        [SetUp]
        public void SetUp()
        {
            _homegame = new HomegameInTest();
            _suite = GetSuite();
            _player = new PlayerInTest(displayName: "player name");
            _rank = 1;
        }

        [Test]
        public void TableRow_RankIsSet()
        {
            var totalResult = new CashgameTotalResultInTest();

            var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

            Assert.AreEqual(1, result.Rank);
        }

        [Test]
        public void TableRow_PlayerNameIsSet()
        {
            var totalResult = new CashgameTotalResultInTest();

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
            var totalResult = new CashgameTotalResultInTest(winnings);

            GetMock<IGlobalization>().Setup(o => o.FormatResult(It.IsAny<Currency>(), winnings)).Returns(formatted);

            var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

            Assert.AreEqual(formatted, result.TotalResult);
        }

        [Test]
        public void TableRow_WinningsClassIsSet()
        {
            const string resultClass = "pos-result";
            var totalResult = new CashgameTotalResultInTest(winnings: 1);

            var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

            Assert.AreEqual(resultClass, result.ResultClass);
        }

        [Test]
        public void TableRow_PlayerUrlIsSet()
        {
            var totalResult = new CashgameTotalResultInTest();

            var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

            Assert.IsInstanceOf<PlayerDetailsUrl>(result.PlayerUrl);
        }

        [Test]
        public void TableRow_CellModelsAreSet()
        {
            var totalResult = new CashgameTotalResultInTest();

            var cellModelList = new List<CashgameMatrixTableCellModel> { new CashgameMatrixTableCellModel() };
            GetMock<ICashgameMatrixTableCellModelFactory>().Setup(o => o.CreateList(_suite.Cashgames, _player)).Returns(cellModelList);

            var sut = GetSut();
            var result = sut.Create(_homegame, _suite, _player, totalResult, _rank);

            Assert.AreEqual(1, result.CellModels.Count);
        }

        [Test]
        public void Results_IsCorrectLength()
        {
            var homegame = new HomegameInTest();
            var suite = new CashgameSuiteInTest();
            var player = new PlayerInTest();
            var totalResult = new CashgameTotalResultInTest(player: player);
            var totalResults = new List<CashgameTotalResult> { totalResult, totalResult };

            var sut = GetSut();
            var result = sut.CreateList(homegame, suite, totalResults);

            Assert.AreEqual(2, result.Count);
        }

        private CashgameSuite GetSuite()
        {
            return new CashgameSuiteInTest
            (
                new List<Cashgame> { new CashgameInTest() }
            );
        }

        private CashgameMatrixTableRowModelFactory GetSut()
        {
            return new CashgameMatrixTableRowModelFactory(
                GetMock<ICashgameMatrixTableCellModelFactory>().Object,
                GetMock<IGlobalization>().Object);
        }
    }
}