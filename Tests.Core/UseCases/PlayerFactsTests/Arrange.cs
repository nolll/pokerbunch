using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Moq;
using NUnit.Framework;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerFactsTests
{
    public abstract class Arrange
    {
        public static readonly DateTime StartTime1 = TimeData.Utc("2001-01-01 12:00:00");
        public static readonly DateTime EndTime1 = TimeData.Utc("2001-01-01 13:02:00");
        public static readonly DateTime StartTime2 = TimeData.Utc("2001-01-02 12:00:00");
        public static readonly DateTime EndTime2 = TimeData.Utc("2001-01-02 14:02:00");
        protected const string BunchId = BunchData.Id1;
        protected const string PlayerId = PlayerData.Id1;
        protected int? Year = null;

        protected PlayerFacts Sut;

        [SetUp]
        public void Setup()
        {
            var brm = new Mock<IBunchRepository>();
            brm.Setup(o => o.Get(BunchId))
                .Returns(BunchData.Bunch1(Role.Player));

            var crm = new Mock<ICashgameRepository>();
            crm.Setup(o => o.PlayerList(PlayerId)).Returns(Games);

            var prm = new Mock<IPlayerRepository>();
            prm.Setup(o => o.Get(PlayerId))
                .Returns(new Player(BunchId, PlayerId, null));

            Sut = new PlayerFacts(brm.Object, crm.Object, prm.Object);
        }

        protected PlayerFacts.Request Request => new PlayerFacts.Request(PlayerId);

        private static IList<ListCashgame> Games => TwoGamesWithTwoPlayers(StartTime1, EndTime1, StartTime2, EndTime2);
        private static IList<ListCashgame> TwoGamesWithTwoPlayers(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2) =>
            new List<ListCashgame>
            {
                new ListCashgame(
                    CashgameData.Id1,
                    startTime1,
                    endTime1,
                    false,
                    new ListCashgame.CashgameLocation(LocationData.Id1, LocationData.Name1),
                    new List<ListCashgame.CashgamePlayer>
                    {
                        new ListCashgame.CashgamePlayer(
                            PlayerData.Id1,
                            PlayerData.Name1,
                            PlayerData.Color1,
                            500,
                            400,
                            startTime1,
                            endTime1),
                        new ListCashgame.CashgamePlayer(
                            PlayerData.Id2,
                            PlayerData.Name2,
                            PlayerData.Color2,
                            100,
                            200,
                            startTime1,
                            endTime1)
                    }),
                new ListCashgame(
                    CashgameData.Id2,
                    startTime2,
                    endTime2,
                    false,
                    new ListCashgame.CashgameLocation(LocationData.Id2, LocationData.Name2),
                    new List<ListCashgame.CashgamePlayer>
                    {
                        new ListCashgame.CashgamePlayer(
                            PlayerData.Id1,
                            PlayerData.Name1,
                            PlayerData.Color1,
                            400,
                            200,
                            startTime2,
                            endTime2),
                        new ListCashgame.CashgamePlayer(
                            PlayerData.Id2,
                            PlayerData.Name2,
                            PlayerData.Color2,
                            0,
                            200,
                            startTime2,
                            endTime2)
                    })
            };
    }
}