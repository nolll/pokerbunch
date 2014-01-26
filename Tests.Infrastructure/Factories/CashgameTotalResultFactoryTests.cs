using System.Collections.Generic;
using Core.Classes;
using Core.Factories;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Infrastructure.Factories{

	public class CashgameTotalResultFactoryTests {

        [Test]
        public void GetWinnings_WithTwoResults_ReturnsSumOfWinnings()
        {
            const int playerId = 1;
            const int singleGameWinnings = 1;
            const int expectedWinnings = 2;
            var player = new FakePlayer(playerId);
            var cashgameResult = new FakeCashgameResult(playerId, winnings: singleGameWinnings);
            var cashgame1 = new FakeCashgame(results: new List<CashgameResult> { cashgameResult });
            var cashgame2 = new FakeCashgame(results: new List<CashgameResult> { cashgameResult });
            var cashgames = new List<Cashgame> {cashgame1, cashgame2};
            
            var sut = GetSut();
            var result = sut.Create(player, cashgames);

            Assert.AreEqual(expectedWinnings, result.Winnings);
        }
        
        private CashgameTotalResultFactory GetSut()
        {
            return new CashgameTotalResultFactory();
        }

	}

}