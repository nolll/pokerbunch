using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    public class CashgameDetailsTests : TestBase
    {
        private const string Id = "cashgame-id";
        private readonly DateTime _startTime = DateTime.Parse("2001-01-01 12:00:00");
        private readonly DateTime _endTime = DateTime.Parse("2001-01-01 13:02:00");
        private const bool IsRunning = false;
        private const string BunchId = "bunch-id";
        private readonly TimeZoneInfo _timezone = TimeZoneInfo.Utc;
        private readonly Currency _currency = new Currency("kr", "{0} kr");
        private const Role Role = global::Core.Entities.Role.Manager;
        private const string LocationId = "location-id";
        private const string LocationName = "location-name";

        private CashgameDetails _sut;

        [SetUp]
        public void SetUp()
        {
            var bunch = new DetailedCashgame.CashgameBunch(BunchId, _timezone, _currency);
            var location = new DetailedCashgame.CashgameLocation(LocationId, LocationName);
            var player1 = new DetailedCashgame.CashgamePlayer("player-1-id", "player-1-name", "#000", 350, 200);
            var player2 = new DetailedCashgame.CashgamePlayer("player-2-id", "player-2-name", "#FFF", 50, 200);
            var players = new List<DetailedCashgame.CashgamePlayer> { player1, player2 };

            var cashgame = new DetailedCashgame(Id, _startTime, _endTime, IsRunning, bunch, Role, location, players);
            var cashgameRepoMock = new Mock<ICashgameRepository>();
            cashgameRepoMock.Setup(o => o.GetDetailedById(Id)).Returns(cashgame);

            _sut = new CashgameDetails(cashgameRepoMock.Object);
        }

        [Test]
        public void CashgameDetails_AllBaseValuesAreSet()
        {
            var request = new CashgameDetails.Request(Id);

            var result = _sut.Execute(request);

            Assert.AreEqual("2001-01-01", result.Date.IsoString);
            Assert.AreEqual(LocationName, result.LocationName);
            Assert.AreEqual(62, result.Duration.Minutes);
            Assert.AreEqual(DateTime.Parse("2001-01-01 11:00:00"), result.StartTime);
            Assert.AreEqual(DateTime.Parse("2001-01-01 12:02:00"), result.EndTime);
            Assert.IsFalse(result.CanEdit);
            Assert.AreEqual("cashgame-id", result.CashgameId);
            Assert.AreEqual(2, result.PlayerItems.Count);
        }

        [Test]
        public void CashgameDetails_WithResultsAndPlayers_PlayerResultItemsCountAndOrderIsCorrect()
        {
            var request = new CashgameDetails.Request(Id);

            var result = _sut.Execute(request);

            Assert.AreEqual(2, result.PlayerItems.Count);
            Assert.AreEqual(150, result.PlayerItems[0].Winnings.Amount);
            Assert.AreEqual(-150, result.PlayerItems[1].Winnings.Amount);
        }

        [Test]
        public void CashgameDetails_AllResultItemPropertiesAreSet()
        {
            var request = new CashgameDetails.Request(Id);

            var result = _sut.Execute(request);

            Assert.AreEqual("player-1-name", result.PlayerItems[0].Name);
            Assert.AreEqual(Id, result.PlayerItems[0].CashgameId);
            Assert.AreEqual("player-1-id", result.PlayerItems[0].PlayerId);
            Assert.AreEqual(200, result.PlayerItems[0].Buyin.Amount);
            Assert.AreEqual(350, result.PlayerItems[0].Cashout.Amount);
            Assert.AreEqual(150, result.PlayerItems[0].Winnings.Amount);
            //Assert.AreEqual(148, result.PlayerItems[0].WinRate.Amount);
        }

        [Test]
        public void CashgameDetails_WithManager_CanEditIsTrue()
        {
            var request = new CashgameDetails.Request(Id);

            var result = _sut.Execute(request);

            Assert.IsTrue(result.CanEdit);
        }
    }
}