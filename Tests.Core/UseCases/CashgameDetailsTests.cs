using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using Core.UseCases.CashgameDetails;
using Moq;
using NUnit.Framework;
using Tests.Common;
using Tests.Common.FakeClasses;

namespace Tests.Core.UseCases
{
    class CashgameDetailsTests : TestBase
    {
        [Test]
        public void CashgameDetails_AllBaseValuesAreSet()
        {
            const string dateStr = "2000-01-01";
            const string location = "b";
            var startTime = DateTime.Parse("2000-01-01 01:01:01").ToUniversalTime();
            var endTime = DateTime.Parse("2000-01-01 02:01:01").ToUniversalTime();

            var cashgame = A.Cashgame.WithDateString(dateStr).WithLocation(location).WithStartTime(startTime).WithEndTime(endTime).Build();
            
            var request = new CashgameDetailsRequest(Constants.SlugA, "2000-01-01");

            SetupCashgame(cashgame);
            
            var result = Execute(request);

            Assert.AreEqual(dateStr, result.Date.IsoString);
            Assert.AreEqual(location, result.Location);
            Assert.AreEqual(60, result.Duration.Minutes);
            Assert.AreEqual(startTime, result.StartTime);
            Assert.AreEqual(endTime, result.EndTime);
            Assert.IsFalse(result.CanEdit);
            Assert.IsInstanceOf<EditCashgameUrl>(result.EditUrl);
            Assert.AreEqual(0, result.PlayerItems.Count);
        }

        [Test]
        public void CashgameDetails_WithResultsAndPlayers_PlayerResultItemsCountAndOrderIsCorrect()
        {
            var request = new CashgameDetailsRequest(Constants.SlugA, "2000-01-01");

            SetupCashgameWithResults();

            var result = Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(1, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(-1, result.PlayerItems[1].Winnings.Amount);
        }

        [Test]
        public void CashgameDetails_AllResultItemPropertiesAreSet()
        {
            var request = new CashgameDetailsRequest(Constants.SlugA, "2000-01-01");

            SetupCashgameWithResults();

            var result = Execute(request);

            Assert.AreEqual(Constants.PlayerNameB, result.PlayerItems[0].Name);
            Assert.IsInstanceOf<CashgameActionUrl>(result.PlayerItems[0].PlayerUrl);
            Assert.AreEqual(2, result.PlayerItems[0].Buyin.Amount);
            Assert.AreEqual(3, result.PlayerItems[0].Cashout.Amount);
            Assert.AreEqual(1, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(4, result.PlayerItems[0].WinRate.Amount);
        }

        [Test]
        public void CashgameDetails_WithManager_CanEditIsTrue()
        {
            const string dateStr = "2000-01-01";
            var cashgame = A.Cashgame.WithDateString(dateStr).Build();
            var request = new CashgameDetailsRequest(Constants.SlugA, dateStr);

            SetupCashgame(cashgame);
            Services.Auth.SetCurrentRole(Role.Manager);
            
            var result = Execute(request);

            Assert.IsTrue(result.CanEdit);
        }

        private void SetupCashgameWithResults()
        {
            const string dateStr = "2000-01-01";
            const string location = "a";
            var startTime = DateTime.Parse("2000-01-01 01:01:01").ToUniversalTime();
            var endTime = DateTime.Parse("2000-01-01 02:01:01").ToUniversalTime();
            
            var cashgameResult1 = new CashgameResultInTest(Constants.PlayerIdA, winnings: -1);
            var cashgameResult2 = new CashgameResultInTest(Constants.PlayerIdB, winnings: 1, buyin: 2, stack: 3, winRate: 4);
            var cashgameResults = new List<CashgameResult> { cashgameResult1, cashgameResult2 };
            var cashgame = A.Cashgame.WithDateString(dateStr).WithLocation(location).WithStartTime(startTime).WithEndTime(endTime).WithResults(cashgameResults).Build();
            SetupCashgame(cashgame);
        }

        private void SetupCashgame(Cashgame cashgame)
        {
            GetMock<ICashgameRepository>().Setup(o => o.GetByDateString(It.IsAny<int>(), It.IsAny<string>())).Returns(cashgame);
        }

        private CashgameDetailsResult Execute(CashgameDetailsRequest request)
        {
            return CashgameDetailsInteractor.Execute(
                Repos.Bunch,
                GetMock<ICashgameRepository>().Object,
                Services.Auth,
                Repos.Player,
                request);
        }
    }
}