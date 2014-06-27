using System;
using System.Collections.Generic;
using Application.Urls;
using Application.UseCases.CashgameDetails;
using Core.Entities;
using NUnit.Framework;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class CashgameDetailsResultTests
    {
        [Test]
        public void Construct_WithoutCreatedGameAndNoStartOrEndTimes_DefaultPropertiesAreSet()
        {
            const string dateStr = "2000-01-01";
            const string location = "a";
            var startTime = DateTime.Parse("2000-01-01 01:01:01").ToUniversalTime();
            var endTime = DateTime.Parse("2000-01-01 02:01:01").ToUniversalTime();

            var homegame = new HomegameInTest(timezone: TimeZoneInfo.Utc);
            var cashgame = new CashgameInTest(dateString: dateStr, location: location, startTime: startTime, endTime: endTime);
            var players = new List<Player>();
            const bool isManager = false;

            var result = new CashgameDetailsResult(homegame, cashgame, players, isManager);

            Assert.AreEqual(dateStr, result.Date.IsoString);
            Assert.AreEqual(location, result.Location);
            Assert.AreEqual(60, result.Duration.Minutes);
            Assert.AreEqual(startTime, result.StartTime);
            Assert.AreEqual(endTime, result.EndTime);
            Assert.IsFalse(result.CanEdit);
            Assert.IsInstanceOf<EditCashgameUrl>(result.EditUrl);
            Assert.IsInstanceOf<CashgameDetailsChartJsonUrl>(result.ChartDataUrl);
            Assert.AreEqual(0, result.PlayerItems.Count);
        }

        [Test]
        public void Construct_WithResultsAndPlayers_PlayerResultItemsCountAndOrderIsCorrect()
        {
            const string dateStr = "2000-01-01";
            const string location = "a";
            const int playerId1 = 1;
            const int playerId2 = 2;
            var startTime = DateTime.Parse("2000-01-01 01:01:01").ToUniversalTime();
            var endTime = DateTime.Parse("2000-01-01 02:01:01").ToUniversalTime();
            const int worstWinnings = -1;
            const int bestWinnings = 1;
            
            var homegame = new HomegameInTest(timezone: TimeZoneInfo.Utc);
            var cashgameResult1 = new CashgameResultInTest(playerId1, winnings: worstWinnings);
            var cashgameResult2 = new CashgameResultInTest(playerId2, winnings: bestWinnings);
            var cashgameResults = new List<CashgameResult> {cashgameResult1, cashgameResult2};
            var cashgame = new CashgameInTest(dateString: dateStr, location: location, startTime: startTime, endTime: endTime, results: cashgameResults);
            var player1 = new PlayerInTest(playerId1);
            var player2 = new PlayerInTest(playerId2);
            var players = new List<Player>{player1, player2};
            const bool isManager = false;

            var result = new CashgameDetailsResult(homegame, cashgame, players, isManager);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(bestWinnings, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(worstWinnings, result.PlayerItems[1].Winnings.Amount);
        }

        [Test]
        public void Construct_WithManager_CanEditIsTrue()
        {
            const string dateStr = "2000-01-01";

            var homegame = new HomegameInTest(timezone: TimeZoneInfo.Utc);
            var cashgame = new CashgameInTest(dateString: dateStr);
            var players = new List<Player>();
            const bool isManager = true;

            var result = new CashgameDetailsResult(homegame, cashgame, players, isManager);

            Assert.IsTrue(result.CanEdit);
        }
    }
}