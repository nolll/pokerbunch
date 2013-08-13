using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Factories;
using Moq;
using NUnit.Framework;

namespace Infrastructure.Tests.Factories{

	class CashgameSuiteFactoryTests {

		private List<Cashgame> _cashgames;
		private List<Player> _players;

		private Mock<ICashgameTotalResultFactory> _cashgameTotalResultFactoryMock;

        [SetUp]
		public void SetUp(){
			_cashgames = new List<Cashgame>();
			_players = new List<Player>();
			_cashgameTotalResultFactoryMock = new Mock<ICashgameTotalResultFactory>();
		}

        [Test]
		public void GetTotalResults_ReturnsCorrectTotalsSortedOnWinningsDescending(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

			var result = GetResult();

			Assert.AreEqual(2, result.TotalResults.Count);
			Assert.AreEqual(3, result.TotalResults[0].Winnings);
			Assert.AreEqual(-3, result.TotalResults[1].Winnings);
		}

		[Test]
		public void GetCashgames_ReturnsTheCashgames(){
			var cashgame = new Cashgame {Id = 1};
		    _cashgames.Add(cashgame);

		    var result = GetResult();

			Assert.AreEqual(1, result.Cashgames.Count);
			Assert.AreEqual(1, result.Cashgames[0].Id);
		}

		[Test]
		public void GetGameCount_ReturnsCorrectCount(){
			var cashgame = new Cashgame();
			_cashgames.Add(cashgame);

		    var result = GetResult();

			Assert.AreEqual(1, result.GameCount);
		}

		[Test]
		public void GetBestTotalResult_ReturnsTheTotalResultWithTheHighestWinnings(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

			var result = GetResult();

			Assert.AreEqual(3, result.BestTotalResult.Winnings);
		}

		[Test]
		public void GetBestResult_ReturnsTheResultWithTheHighestWinnings(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

			var result = GetResult();

			Assert.AreEqual(2, result.BestResult.Winnings);
		}

		[Test]
		public void GetWorstResult_ReturnsTheResultWithTheLowestWinnings(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

			var result = GetResult();

			Assert.AreEqual(-2, result.WorstResult.Winnings);
		}

		[Test]
		public void GetMostTime_ReturnsTheResultWithTheMostTimePlayed(){
			SetUpTwoGamesWithOneWinningAndOneLosingPlayer();

			var result = GetResult();

			Assert.AreEqual(4, result.MostTimeResult.TimePlayed);
		}

		[Test]
		public void GetTotalGameTime_ReturnsTheSumOfTheGameDurations(){
			var cashgame1 = new Cashgame {Duration = 1};
		    var cashgame2 = new Cashgame {Duration = 2};

		    _cashgames = new List<Cashgame>{cashgame1, cashgame2};

			var result = GetResult();

			Assert.AreEqual(3, result.TotalGameTime);
		}

		private void SetUpTwoGamesWithOneWinningAndOneLosingPlayer(){
			var player1 = new Player {Id = 1};
            var player2 = new Player {Id = 2};

		    var resultList1 = new List<CashgameResult>
		        {
		            new CashgameResult {Winnings = -1, PlayedTime = 1, Player = player1},
		            new CashgameResult {Winnings = 1, PlayedTime = 2, Player = player2}
		        };
		    var cashgame1 = new Cashgame {Results = resultList1};

		    var resultList2 = new List<CashgameResult>
		        {
		            new CashgameResult {Winnings = -2, PlayedTime = 1, Player = player1},
		            new CashgameResult {Winnings = 2, PlayedTime = 2, Player = player2}
		        };
		    var cashgame2 = new Cashgame {Results = resultList2};

		    _cashgames = new List<Cashgame>{cashgame1, cashgame2};
			_players = new List<Player>{player1, player2};

		    _cashgameTotalResultFactoryMock.Setup(o => o.Create(It.IsAny<Player>(), It.IsAny<List<CashgameResult>>())).Returns<Player, List<CashgameResult>>(GetReturnedTotalResult);
		}

        private CashgameTotalResult GetReturnedTotalResult(Player player, List<CashgameResult> results)
        {
            if (player.Id == 1)
            {
                return new CashgameTotalResult {Player = player, Winnings = -3, TimePlayed = 2};
            }
            return new CashgameTotalResult { Player = player, Winnings = 3, TimePlayed = 4 };
        }

		private CashgameSuite GetResult(){
			var cashgameSuiteFactory = new CashgameSuiteFactory(_cashgameTotalResultFactoryMock.Object);
			return cashgameSuiteFactory.Create(_cashgames, _players);
		}

	}

}