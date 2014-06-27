using Application.Urls;
using Application.UseCases.CashgameDetails;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class PlayerResultItemTests
    {
        [Test]
        public void Construct_AllPropertiesAreSet()
        {
            const string playerName = "a";
            const int buyin = 1;
            const int cashout = 2;
            const int winnings = 3;
            const int winRate = 4;

            var homegame = new HomegameInTest();
            var cashgame = new CashgameInTest();
            var player = new PlayerInTest(displayName: playerName);
            var cashgameResult = new CashgameResultInTest(buyin: buyin, stack: cashout, winnings: winnings, winRate: winRate);

            var result = new PlayerResultItem(homegame, cashgame, player, cashgameResult);

            Assert.AreEqual(playerName, result.Name);
            Assert.IsInstanceOf<CashgameActionUrl>(result.PlayerUrl);
            Assert.AreEqual(buyin, result.Buyin.Amount);
            Assert.AreEqual(cashout, result.Cashout.Amount);
            Assert.AreEqual(winnings, result.Winnings.Amount);
            Assert.AreEqual(winRate, result.WinRate.Amount);
        }
    }
}