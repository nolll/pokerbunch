using System;
using System.Collections.Generic;
using Application.Exceptions;
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
            const string location = "b";
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
            var request = new CashgameDetailsRequest("a", "2000-01-01");

            SetupHomegame();
            SetupCashgameWithResults();

            var result = Sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(1, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(-1, result.PlayerItems[1].Winnings.Amount);
        }

        [Test]
        public void CashgameDetails_AllResultItemPropertiesAreSet()
        {
            var request = new CashgameDetailsRequest("a", "2000-01-01");

            SetupHomegame();
            SetupCashgameWithResults();

            var result = Sut.Execute(request);

            Assert.AreEqual("c", result.PlayerItems[0].Name);
            Assert.IsInstanceOf<CashgameActionUrl>(result.PlayerItems[0].PlayerUrl);
            Assert.AreEqual(2, result.PlayerItems[0].Buyin.Amount);
            Assert.AreEqual(3, result.PlayerItems[0].Cashout.Amount);
            Assert.AreEqual(1, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(4, result.PlayerItems[0].WinRate.Amount);
        }

        [Test]
        public void AddCashgameOptions_WithRunningCashgame_ThrowsException()
        {
            const string slug = "a";
            var request = new CashgameDetailsRequest(slug, "2000-01-01");

            Assert.Throws<CashgameNotFoundException>(() => Sut.Execute(request));
        }

        [Test]
        public void CashgameDetails_WithManager_CanEditIsTrue()
        {
            const string dateStr = "2000-01-01";
            var cashgame = new CashgameInTest(dateString: dateStr);
            var request = new CashgameDetailsRequest("a", dateStr);

            SetupHomegame();
            SetupCashgame(cashgame);
            SetupManager();
            
            var result = Sut.Execute(request);

            Assert.IsTrue(result.CanEdit);
        }

        private void SetupCashgameWithResults()
        {
            const string dateStr = "2000-01-01";
            const string location = "a";
            const int playerId1 = 1;
            const int playerId2 = 2;
            var startTime = DateTime.Parse("2000-01-01 01:01:01").ToUniversalTime();
            var endTime = DateTime.Parse("2000-01-01 02:01:01").ToUniversalTime();
            
            var cashgameResult1 = new CashgameResultInTest(playerId1, winnings: -1);
            var cashgameResult2 = new CashgameResultInTest(playerId2, winnings: 1, buyin: 2, stack: 3, winRate: 4);
            var cashgameResults = new List<CashgameResult> { cashgameResult1, cashgameResult2 };
            var cashgame = new CashgameInTest(dateString: dateStr, location: location, startTime: startTime, endTime: endTime, results: cashgameResults);
            SetupCashgame(cashgame);
            var player1 = new PlayerInTest(playerId1, displayName: "b");
            var player2 = new PlayerInTest(playerId2, displayName: "c");
            var players = new List<Player> { player1, player2 };
            GetMock<IPlayerRepository>().Setup(o => o.GetList(It.IsAny<IList<int>>())).Returns(players);
        }

        private void SetupHomegame()
        {
            var homegame = new BunchInTest(timezone: TimeZoneInfo.Utc);
            GetMock<IBunchRepository>().Setup(o => o.GetBySlug(It.IsAny<string>())).Returns(homegame);
        }

        private void SetupCashgame(Cashgame cashgame)
        {
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(It.IsAny<Bunch>(), It.IsAny<string>())).Returns(cashgame);
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
                    GetMock<IBunchRepository>().Object,
                    GetMock<ICashgameRepository>().Object,
                    GetMock<IAuth>().Object,
                    GetMock<IPlayerRepository>().Object);
            }
        }
    }
}