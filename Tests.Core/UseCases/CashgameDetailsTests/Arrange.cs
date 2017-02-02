using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameDetailsTests
{
    public abstract class Arrange
    {
        protected const string Id = "cashgame-id";
        protected readonly DateTime _startTime = DateTime.Parse("2001-01-01 12:00:00");
        protected readonly DateTime _endTime = DateTime.Parse("2001-01-01 13:02:00");
        protected const bool IsRunning = false;
        protected const string BunchId = "bunch-id";
        protected readonly TimeZoneInfo _timezone = TimeZoneInfo.Utc;
        protected readonly Currency _currency = new Currency("kr", "{0} kr");
        protected const string LocationId = "location-id";
        protected const string LocationName = "location-name";

        protected virtual Role Role => Role.Player;

        protected CashgameDetails Sut;

        [SetUp]
        public void Setup()
        {
            var bunch = new DetailedCashgame.CashgameBunch(BunchId, _timezone, _currency);
            var location = new DetailedCashgame.CashgameLocation(LocationId, LocationName);
            var player1Actions = new List<DetailedCashgame.CashgameAction>();
            var player1 = new DetailedCashgame.CashgamePlayer("player-1-id", "player-1-name", "#000", 350, 200, _startTime, _endTime, player1Actions);
            var player2Actions = new List<DetailedCashgame.CashgameAction>();
            var player2 = new DetailedCashgame.CashgamePlayer("player-2-id", "player-2-name", "#FFF", 50, 200, _startTime, _endTime, player2Actions);
            var players = new List<DetailedCashgame.CashgamePlayer> { player1, player2 };

            var cashgame = new DetailedCashgame(Id, _startTime, _endTime, IsRunning, bunch, Role, location, players);
            var cashgameRepoMock = new Mock<ICashgameRepository>();
            cashgameRepoMock.Setup(o => o.GetDetailedById(Id)).Returns(cashgame);

            Sut = new CashgameDetails(cashgameRepoMock.Object);
        }

        protected CashgameDetails.Request Request => new CashgameDetails.Request(Id);
    }
}