using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.CashgameListTests
{
    public abstract class Arrange : UseCaseTest<CashgameList>
    {
        protected CashgameList.Result Result;

        public static readonly DateTime StartTime1 = TimeData.Utc("2001-01-01 12:00:00");
        public static readonly DateTime EndTime1 = TimeData.Utc("2001-01-01 13:02:00");
        public static readonly DateTime StartTime2 = TimeData.Utc("2001-01-02 12:00:00");
        public static readonly DateTime EndTime2 = TimeData.Utc("2001-01-02 14:02:00");
        private string BunchIdWithGames = BunchData.Id1;
        protected string BunchIdWithoutGames = BunchData.Id2;

        protected virtual CashgameList.SortOrder SortOrder => CashgameList.SortOrder.Date;
        protected virtual int? Year => null;
        protected virtual string BunchId => BunchIdWithGames;

        protected override void Setup()
        {
            var bunchWithGames = BunchData.Bunch1(Role.Player);
            var bunchWithoutGames = BunchData.Bunch2(Role.Player);
            var cashgames = Games;

            Mock<IBunchRepository>().Setup(o => o.Get(BunchIdWithGames)).Returns(bunchWithGames);
            Mock<IBunchRepository>().Setup(o => o.Get(BunchIdWithoutGames)).Returns(bunchWithoutGames);
            Mock<ICashgameRepository>().Setup(o => o.List(BunchIdWithGames, Year)).Returns(cashgames);
            Mock<ICashgameRepository>().Setup(o => o.List(BunchIdWithoutGames, Year)).Returns(new List<ListCashgame>());
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new CashgameList.Request(BunchId, SortOrder, Year));
        }

        private static IList<ListCashgame> Games => TwoGamesWithTwoPlayers(StartTime1, EndTime1, StartTime2, EndTime2);
        private static IList<ListCashgame> TwoGamesWithTwoPlayers(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2) =>
            new List<ListCashgame>
            {
                new ListCashgame(
                    CashgameData.Id1,
                    startTime1,
                    endTime1,
                    false,
                    new SmallLocation(LocationData.Id1, LocationData.Name1),
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
                    new SmallLocation(LocationData.Id2, LocationData.Name2),
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
