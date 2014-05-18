using System.Collections.Generic;
using Application.Factories;
using Core.Entities;
using Core.Factories;
using Core.Factories.Interfaces;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.Factories
{
	class CashgameSuiteFactoryTests : MockContainer
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
        public void TotalResults_ReturnsCorrectTotalsSortedOnWinningsDescending()
        {
            SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

            Assert.AreEqual(2, result.TotalResults.Count);
            Assert.AreEqual(3, result.TotalResults[0].Winnings);
            Assert.AreEqual(-3, result.TotalResults[1].Winnings);
        }

        [Test]
        public void Cashgames_ReturnsTheCashgames()
        {
            var cashgame = new FakeCashgame(id: 1);
            _cashgames.Add(cashgame);

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

            Assert.AreEqual(1, result.Cashgames.Count);
            Assert.AreEqual(1, result.Cashgames[0].Id);
        }

        [Test]
        public void Players_ReturnsThePlayers()
        {
            var player = new FakePlayer(id: 1);
            _players.Add(player);

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

            Assert.AreEqual(1, result.Players.Count);
            Assert.AreEqual(1, result.Players[0].Id);
        }

		private void SetUpTwoGamesWithOneWinningAndOneLosingPlayer()
		{
		    const int winningAmount = 3;
		    const int winningTimePlayed = 4;
		    const int losingAmount = -3;
		    const int losingTimePlayed = 2;
		    const int gameCount = 2;
            var totalResult1 = new FakeCashgameTotalResult(winningAmount, gameCount, winningTimePlayed);
            var totalResult2 = new FakeCashgameTotalResult(losingAmount, gameCount, losingTimePlayed);
            var totalResultList = new List<CashgameTotalResult> {totalResult1, totalResult2};
            GetMock<ICashgameTotalResultFactory>().Setup(o => o.CreateList(It.IsAny<IList<Player>>(), It.IsAny<IList<Cashgame>>())).Returns(totalResultList);
		}

        private CashgameSuiteFactory GetSut()
        {
            return new CashgameSuiteFactory(
                GetMock<ICashgameTotalResultFactory>().Object);
        }

	}

}