using System.Collections.Generic;
using Application.UseCases.CashgameFacts;
using Core.Entities;
using Core.Factories;
using Core.Factories.Interfaces;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Factories
{
    internal class FactBuilderTests : MockContainer
    {
        private List<Cashgame> _cashgames;
        private List<Player> _players;

        [SetUp]
        public void SetUp()
        {
            _cashgames = new List<Cashgame>();
            _players = new List<Player>();
        }

        [Test]
        public void GetGameCount_ReturnsCorrectCount()
        {
            SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            _cashgames = new List<Cashgame> {new CashgameInTest()};

            var result = new FactBuilder(_cashgames, _players);

            Assert.AreEqual(1, result.GameCount);
        }

        [Test]
        public void GetBestTotalResult_ReturnsTheTotalResultWithTheHighestWinnings()
        {
            SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var result = new FactBuilder(_cashgames, _players);

            Assert.AreEqual(3, result.BestTotalResult.Winnings);
        }

        [Test]
        public void GetBestResult_ReturnsTheResultWithTheHighestWinnings()
        {
            SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var result = new FactBuilder(_cashgames, _players);

            Assert.AreEqual(2, result.BestResult.Winnings);
        }

        [Test]
        public void GetWorstResult_ReturnsTheResultWithTheLowestWinnings()
        {
            SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var result = new FactBuilder(_cashgames, _players);

            Assert.AreEqual(-2, result.WorstResult.Winnings);
        }

        [Test]
        public void GetMostTime_ReturnsTheResultWithTheMostTimePlayed()
        {
            SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var result = new FactBuilder(_cashgames, _players);

            Assert.AreEqual(4, result.MostTimeResult.TimePlayed);
        }

        [Test]
        public void GetTotalGameTime_ReturnsTheSumOfTheGameDurations()
        {
            var cashgame1 = new CashgameInTest(duration: 1);
            var cashgame2 = new CashgameInTest(duration: 2);
            _cashgames = new List<Cashgame> {cashgame1, cashgame2};

            GetMock<ICashgameTotalResultFactory>()
                .Setup(o => o.CreateList(It.IsAny<IList<Player>>(), It.IsAny<IList<Cashgame>>()))
                .Returns(new List<CashgameTotalResult>());

            var result = new FactBuilder(_cashgames, _players);

            Assert.AreEqual(3, result.TotalGameTime);
        }

        [Test]
        public void GetTotalTurnover_ReturnsTheSumOfAllTurnovers()
        {
            var cashgame1 = new CashgameInTest(turnover: 1);
            var cashgame2 = new CashgameInTest(turnover: 2);
            _cashgames = new List<Cashgame> {cashgame1, cashgame2};

            GetMock<ICashgameTotalResultFactory>()
                .Setup(o => o.CreateList(It.IsAny<IList<Player>>(), It.IsAny<IList<Cashgame>>()))
                .Returns(new List<CashgameTotalResult>());

            var result = new FactBuilder(_cashgames, _players);

            Assert.AreEqual(3, result.TotalTurnover);
        }

        private void SetUpTwoGamesWithOneWinningAndOneLosingPlayer()
        {
            const int playerId1 = 1;
            const int playerId2 = 2;
            var player1 = new PlayerInTest(playerId1);
            var player2 = new PlayerInTest(playerId2);

            var resultList1 = new List<CashgameResult>
                {
                    new CashgameResultInTest(winnings: -1, playedTime: 1, playerId: playerId1),
                    new CashgameResultInTest(winnings: 1, playedTime: 2, playerId: playerId2)
                };
            var cashgame1 = new CashgameInTest(results: resultList1);

            var resultList2 = new List<CashgameResult>
                {
                    new CashgameResultInTest(winnings: -2, playedTime: 1, playerId: playerId1),
                    new CashgameResultInTest(winnings: 2, playedTime: 2, playerId: playerId2)
                };
            var cashgame2 = new CashgameInTest(results: resultList2);

            _cashgames = new List<Cashgame> {cashgame1, cashgame2};
            _players = new List<Player> {player1, player2};

            var totalResult1 = new CashgameTotalResultInTest(3, gameCount: 2, timePlayed: 4);
            var totalResult2 = new CashgameTotalResultInTest(-3, gameCount: 2, timePlayed: 2);
            var totalResultList = new List<CashgameTotalResult> {totalResult1, totalResult2};
            GetMock<ICashgameTotalResultFactory>()
                .Setup(o => o.CreateList(It.IsAny<IList<Player>>(), It.IsAny<IList<Cashgame>>()))
                .Returns(totalResultList);
        }
    }
}