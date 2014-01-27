using System.Collections.Generic;
using System.Linq;
using Application.Factories;
using Core.Classes;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Factories{

	public class CashgameTotalResultFactoryTests {

        [Test]
        public void Create_WithTwoResults_ReturnsModelWithCorrectValues()
        {
            const int playerId = 1;
            const int singleGameWinnings = 1;
            const int singleGameTime = 1;
            const int singleGameBuyin = 1;
            const int singleGameStack = 1;

            const int expectedGameCount = 2;
            const int expectedTime = 2;
            const int expectedWinnings = 2;
            const int expectedBuyin = 2;
            const int expectedCashout = 2;
            const int expectedWinrate = 60;
            
            var player = new FakePlayer(playerId);
            var cashgameResult = new FakeCashgameResult(
                playerId,
                winnings: singleGameWinnings,
                playedTime: singleGameTime,
                buyin: singleGameBuyin,
                stack: singleGameStack);
            var cashgame1 = new FakeCashgame(results: new List<CashgameResult> { cashgameResult });
            var cashgame2 = new FakeCashgame(results: new List<CashgameResult> { cashgameResult });
            var cashgames = new List<Cashgame> {cashgame1, cashgame2};
            
            var sut = GetSut();
            var result = sut.Create(player, cashgames);

            Assert.AreEqual(expectedGameCount, result.GameCount);
            Assert.AreEqual(expectedWinnings, result.Winnings);
            Assert.AreEqual(expectedTime, result.TimePlayed);
            Assert.AreEqual(expectedBuyin, result.Buyin);
            Assert.AreEqual(expectedCashout, result.Cashout);
            Assert.AreEqual(expectedWinrate, result.WinRate);
        }

	    [Test]
	    public void CreateList_WithTwoResults_ReturnsTwoItemsOrderedByWinnings()
	    {
	        const int playerId1 = 1;
	        const int playerId2 = 2;
            var player1 = new FakePlayer(playerId1);
            var player2 = new FakePlayer(playerId2);
	        var players = new List<Player> {player1, player2};
            var cashgameResult1 = new FakeCashgameResult(playerId1, winnings: -1);
            var cashgameResult2 = new FakeCashgameResult(playerId2, winnings: 1);
            var cashgame1 = new FakeCashgame(results: new List<CashgameResult> { cashgameResult1, cashgameResult2 });
            var cashgame2 = new FakeCashgame(results: new List<CashgameResult> { cashgameResult1, cashgameResult2 });
            var cashgames = new List<Cashgame> {cashgame1, cashgame2};

	        const int expectedCount = 2;
	        const int expectedFirstPlayerId = playerId2;

	        var sut = GetSut();
	        var result = sut.CreateList(players, cashgames);

            Assert.AreEqual(expectedCount, result.Count);
            Assert.AreEqual(expectedFirstPlayerId, result.First().PlayerId);
	    }

        private CashgameTotalResultFactory GetSut()
        {
            return new CashgameTotalResultFactory();
        }

	}

}