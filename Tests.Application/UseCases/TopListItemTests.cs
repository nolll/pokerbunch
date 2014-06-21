using Application.UseCases.CashgameTopList;
using Core.Entities;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class TopListItemTests
    {
        [Test]
        public void Construct_AllPropertiesAreSet()
        {
            const string playerName = "a";
            const int playerId = 1;
            const int buyin = 2;
            const int cashout = 3;
            const int gamesPlayed = 4;
            const int minutesPlayed = 5;
            const int winnings = 6;
            const int winRate = 7;
            const int expectedRank = 1;
            var player = new PlayerInTest(playerId, displayName: playerName);
            var totalResult = new CashgameTotalResultInTest(player: player, buyin: buyin, cashout: cashout, gameCount: gamesPlayed, timePlayed: minutesPlayed, winnings: winnings, winRate: winRate);
            const int index = 0;
            var currency = Currency.Default;

            var result = new TopListItem(totalResult, index, currency);

            Assert.AreEqual(expectedRank, result.Rank);
            Assert.AreEqual(buyin, result.Buyin.Amount);
            Assert.AreEqual(cashout, result.Cashout.Amount);
            Assert.AreEqual(gamesPlayed, result.GamesPlayed);
            Assert.AreEqual(minutesPlayed, result.TimePlayed.Minutes);
            Assert.AreEqual(playerName, result.Name);
            Assert.AreEqual(playerId, result.PlayerId);
            Assert.AreEqual(winnings, result.Winnings.Amount);
            Assert.AreEqual(winRate, result.WinRate.Amount);
        }
    }
}