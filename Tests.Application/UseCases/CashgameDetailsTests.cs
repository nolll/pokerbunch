using System;
using System.Collections.Generic;
using Application.Services;
using Application.Urls;
using Application.UseCases.CashgameDetails;
using Core.Entities;
using Core.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Application.UseCases
{
    class CashgameDetailsTests : MockContainer
    {
        [Test]
        public void CashgameDetails_AllBaseValuesAreSet()
        {
            const string dateStr = "2000-01-01";
            const string location = "a";
            var startTime = DateTime.Parse("2000-01-01 01:01:01").ToUniversalTime();
            var endTime = DateTime.Parse("2000-01-01 02:01:01").ToUniversalTime();

            var cashgame = new CashgameInTest(dateString: dateStr, location: location, startTime: startTime, endTime: endTime);
            
            var request = new CashgameDetailsRequest("a", "2000-01-01");

            SetupHomegame();
            SetupCashgame(cashgame);
            
            var result = Sut.Execute(request);

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
        public void CashgameDetails_WithResultsAndPlayers_PlayerResultItemsCountAndOrderIsCorrect()
        {
            const string dateStr = "2000-01-01";
            const string location = "a";
            const int playerId1 = 1;
            const int playerId2 = 2;
            var startTime = DateTime.Parse("2000-01-01 01:01:01").ToUniversalTime();
            var endTime = DateTime.Parse("2000-01-01 02:01:01").ToUniversalTime();
            const int worstWinnings = -1;
            const int bestWinnings = 1;
            
            var cashgameResult1 = new CashgameResultInTest(playerId1, winnings: worstWinnings);
            var cashgameResult2 = new CashgameResultInTest(playerId2, winnings: bestWinnings);
            var cashgameResults = new List<CashgameResult> {cashgameResult1, cashgameResult2};
            var cashgame = new CashgameInTest(dateString: dateStr, location: location, startTime: startTime, endTime: endTime, results: cashgameResults);
            var player1 = new PlayerInTest(playerId1);
            var player2 = new PlayerInTest(playerId2);

            var request = new CashgameDetailsRequest("a", "2000-01-01");

            SetupHomegame();
            SetupCashgame(cashgame);
            SetupPlayers(player1, player2);

            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(bestWinnings, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(worstWinnings, result.PlayerItems[1].Winnings.Amount);
        }

        private void SetupPlayers(Player player1, Player player2)
        {
            var players = new List<Player> { player1, player2 };
            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<IList<int>>())).Returns(players);
        }

        [Test]
        public void CashgameDetails_WithManager_CanEditIsTrue()
        {
            const string dateStr = "2000-01-01";

            var cashgame = new CashgameInTest(dateString: dateStr);

            var request = new CashgameDetailsRequest("a", "2000-01-01");

            SetupHomegame();
            SetupCashgame(cashgame);
            SetupManager();
            
            var result = Sut.Execute(request);

            Assert.IsTrue(result.CanEdit);
        }

        private void SetupHomegame()
        {
            var homegame = new HomegameInTest(timezone: TimeZoneInfo.Utc);
            GetMock<IHomegameRepository>().Setup(o => o.GetBySlug(It.IsAny<string>())).Returns(homegame);
        }

        private void SetupCashgame(Cashgame cashgame)
        {
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(It.IsAny<Homegame>(), It.IsAny<string>())).Returns(cashgame);
        }

        private void SetupManager()
        {
            GetMock<IAuth>().Setup(o => o.IsInRole(It.IsAny<string>(), It.IsAny<Role>())).Returns(true);
        }

        private CashgameDetailsInteractor Sut
        {
            get
            {
                return new CashgameDetailsInteractor(
                    GetMock<IHomegameRepository>().Object,
                    GetMock<ICashgameRepository>().Object,
                    GetMock<IAuth>().Object,
                    GetMock<IPlayerRepository>().Object);
            }
        }
    }
}