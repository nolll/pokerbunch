using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;

namespace Tests.Core.UseCases.CashgameDetailsChartTests
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

        protected CashgameDetailsChart Sut;

        [SetUp]
        public void Setup()
        {
            var bunch = new DetailedCashgame.CashgameBunch(BunchId, _timezone, _currency);
            var location = new DetailedCashgame.CashgameLocation(LocationId, LocationName);
            var player1BuyinTime = DateTime.Parse("2001-01-01 11:00");
            var player1BuyinAction = new DetailedCashgame.CashgameAction("1", CheckpointType.Buyin, player1BuyinTime, 200, 200);
            var player1CashoutTime = DateTime.Parse("2001-01-01 12:00");
            var player1CashoutAction = new DetailedCashgame.CashgameAction("2", CheckpointType.Cashout, player1CashoutTime, 50, 0);
            var player1Actions = new List<DetailedCashgame.CashgameAction> {player1BuyinAction, player1CashoutAction};
            var player1 = new DetailedCashgame.CashgamePlayer("player-1-id", "player-1-name", "#000", 350, 200, _startTime, _endTime, player1Actions);
            var player2BuyinTime = DateTime.Parse("2001-01-01 11:05");
            var player2BuyinAction = new DetailedCashgame.CashgameAction("3", CheckpointType.Buyin, player2BuyinTime, 200, 200);
            var player2ReportTime = DateTime.Parse("2001-01-01 11:35");
            var player2ReportAction = new DetailedCashgame.CashgameAction("4", CheckpointType.Report, player2ReportTime, 250, 0);
            var player2CashoutTime = DateTime.Parse("2001-01-01 12:05");
            var player2CashoutAction = new DetailedCashgame.CashgameAction("5", CheckpointType.Cashout, player2CashoutTime, 350, 0);
            var player2Actions = new List<DetailedCashgame.CashgameAction> {player2BuyinAction, player2ReportAction, player2CashoutAction};
            var player2 = new DetailedCashgame.CashgamePlayer("player-2-id", "player-2-name", "#FFF", 50, 200, _startTime, _endTime, player2Actions);
            var players = new List<DetailedCashgame.CashgamePlayer> { player1, player2 };

            var cashgame = new DetailedCashgame(Id, _startTime, _endTime, IsRunning, bunch, Role, location, players);
            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.GetDetailedById(Id)).Returns(cashgame);

            Sut = new CashgameDetailsChart(crm.Object);
        }

        protected CashgameDetailsChart.Request Request => new CashgameDetailsChart.Request(Id);
    }
}
