using System.Collections.Generic;
using Core.Classes;
using Core.Factories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Infrastructure.Tests.Factories{

	class CashgameFactsFactoryTests : MockContainer
    {
		private List<Cashgame> _cashgames;
		private List<Player> _players;

        [SetUp]
		public void SetUp(){
			_cashgames = new List<Cashgame>();
			_players = new List<Player>();
		}

		[Test]
		public void GetGameCount_ReturnsCorrectCount(){
            SetUpTwoGamesWithOneWinningAndOneLosingPlayer();
            
            _cashgames = new List<Cashgame>{new FakeCashgame()};

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

			Assert.AreEqual(1, result.GameCount);
		}

		[Test]
		public void GetBestTotalResult_ReturnsTheTotalResultWithTheHighestWinnings(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

			Assert.AreEqual(3, result.BestTotalResult.Winnings);
		}

		[Test]
		public void GetBestResult_ReturnsTheResultWithTheHighestWinnings(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

			Assert.AreEqual(2, result.BestResult.Winnings);
		}

		[Test]
		public void GetWorstResult_ReturnsTheResultWithTheLowestWinnings(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

			Assert.AreEqual(-2, result.WorstResult.Winnings);
		}

		[Test]
		public void GetMostTime_ReturnsTheResultWithTheMostTimePlayed(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

			Assert.AreEqual(4, result.MostTimeResult.TimePlayed);
		}

        [Test]
        public void GetTotalGameTime_ReturnsTheSumOfTheGameDurations()
        {
            var cashgame1 = new FakeCashgame(duration: 1);
            var cashgame2 = new FakeCashgame(duration: 2);
            _cashgames = new List<Cashgame> { cashgame1, cashgame2 };

            GetMock<ICashgameTotalResultFactory>().Setup(o => o.CreateList(It.IsAny<IEnumerable<Player>>(), It.IsAny<IDictionary<int, IList<CashgameResult>>>())).Returns(new List<CashgameTotalResult>());

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

            Assert.AreEqual(3, result.TotalGameTime);
        }

        [Test]
        public void GetTotalTurnover_ReturnsTheSumOfAllTurnovers()
        {
            var cashgame1 = new FakeCashgame(turnover: 1);
            var cashgame2 = new FakeCashgame(turnover: 2);
            _cashgames = new List<Cashgame> { cashgame1, cashgame2 };

            GetMock<ICashgameTotalResultFactory>().Setup(o => o.CreateList(It.IsAny<IEnumerable<Player>>(), It.IsAny<IDictionary<int, IList<CashgameResult>>>())).Returns(new List<CashgameTotalResult>());

            var sut = GetSut();
            var result = sut.Create(_cashgames, _players);

            Assert.AreEqual(3, result.TotalTurnover);
        }

		private void SetUpTwoGamesWithOneWinningAndOneLosingPlayer()
		{
		    const int playerId1 = 1;
		    const int playerId2 = 2;
			var player1 = new FakePlayer(playerId1);
            var player2 = new FakePlayer(playerId2);

		    var resultList1 = new List<CashgameResult>
		        {
		            new FakeCashgameResult(winnings: -1, playedTime: 1, playerId: playerId1),
		            new FakeCashgameResult(winnings: 1, playedTime: 2, playerId: playerId2)
		        };
		    var cashgame1 = new FakeCashgame(results: resultList1);

		    var resultList2 = new List<CashgameResult>
		        {
		            new FakeCashgameResult(winnings: -2, playedTime: 1, playerId: playerId1),
		            new FakeCashgameResult(winnings: 2, playedTime: 2, playerId: playerId2)
		        };
		    var cashgame2 = new FakeCashgame(results: resultList2);

		    _cashgames = new List<Cashgame>{cashgame1, cashgame2};
			_players = new List<Player>{player1, player2};

            var totalResult1 = new FakeCashgameTotalResult(winnings: 3, gameCount: 2, timePlayed: 4);
            var totalResult2 = new FakeCashgameTotalResult(winnings: -3, gameCount: 2, timePlayed: 2);
            var totalResultList = new List<CashgameTotalResult> {totalResult1, totalResult2};
            GetMock<ICashgameTotalResultFactory>().Setup(o => o.CreateList(It.IsAny<IEnumerable<Player>>(), It.IsAny<IDictionary<int, IList<CashgameResult>>>())).Returns(totalResultList);
		}

        private CashgameFactsFactory GetSut()
        {
            return new CashgameFactsFactory(GetMock<ICashgameTotalResultFactory>().Object);
        }

	}

}