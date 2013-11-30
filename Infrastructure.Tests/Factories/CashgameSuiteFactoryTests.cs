using System.Collections.Generic;
using Core.Classes;
using Core.Factories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Factories{

	class CashgameSuiteFactoryTests : MockContainer
    {
		private List<Cashgame> _cashgames;
		private List<Player> _players;

        [SetUp]
		public void SetUp(){
			_cashgames = new List<Cashgame>();
			_players = new List<Player>();
		}

        [Test]
		public void GetTotalResults_ReturnsCorrectTotalsSortedOnWinningsDescending(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

			Assert.AreEqual(2, result.TotalResults.Count);
			Assert.AreEqual(3, result.TotalResults[0].Winnings);
			Assert.AreEqual(-3, result.TotalResults[1].Winnings);
		}

		[Test]
		public void GetCashgames_ReturnsTheCashgames(){
			var cashgame = new FakeCashgame(id: 1);
		    _cashgames.Add(cashgame);

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

			Assert.AreEqual(1, result.Cashgames.Count);
			Assert.AreEqual(1, result.Cashgames[0].Id);
		}

		private void SetUpTwoGamesWithOneWinningAndOneLosingPlayer(){
            var totalResult1 = new FakeCashgameTotalResult(winnings: 3, gameCount: 2, timePlayed: 4);
            var totalResult2 = new FakeCashgameTotalResult(winnings: -3, gameCount: 2, timePlayed: 2);
            var totalResultList = new List<CashgameTotalResult> {totalResult1, totalResult2};
            GetMock<ICashgameTotalResultFactory>().Setup(o => o.CreateList(It.IsAny<IEnumerable<Player>>(), It.IsAny<IDictionary<int, IList<CashgameResult>>>())).Returns(totalResultList);
		}

        private CashgameSuiteFactory GetSut()
        {
            return new CashgameSuiteFactory(GetMock<ICashgameTotalResultFactory>().Object);
        }

	}

}