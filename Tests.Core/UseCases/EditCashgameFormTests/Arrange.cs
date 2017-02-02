using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.EditCashgameFormTests
{
    public class Arrange
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

        protected EditCashgameForm Sut;

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

            var location1 = new Location("location-id-1", "location-name-1", BunchId);
            var location2 = new Location("location-id-2", "location-name-2", BunchId);
            var locationList = new List<Location> {location1, location2};
            var locationRepoMock = new Mock<ILocationRepository>();
            locationRepoMock.Setup(o => o.List(BunchId)).Returns(locationList);

            var event1 = new Event("event-id-1", "event-name-1", BunchId);
            var event2 = new Event("event-id-2", "event-name-2", BunchId);
            var eventList = new List<Event> { event1, event2 };
            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(o => o.ListByBunch(BunchId)).Returns(eventList);

            Sut = new EditCashgameForm(cashgameRepoMock.Object, locationRepoMock.Object, eventRepoMock.Object);
        }

        protected EditCashgameForm.Request Request => new EditCashgameForm.Request(Id);
    }
}