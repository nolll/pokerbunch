using System;
using System.Collections.Generic;
using Core.Entities;

namespace Tests.Core.Data
{
    public static class CashgameData
    {
        public const string Id1 = "cashgame-id-1";
        public const string Id2 = "cashgame-id-2";

        private static readonly DateTime StartTime1 = TimeData.Utc("2001-01-01 12:00:00");
        private static readonly DateTime EndTime1 = TimeData.Utc("2001-01-01 13:02:00");
        private static readonly DateTime StartTime2 = TimeData.Utc("2001-01-02 12:00:00");
        private static readonly DateTime EndTime2 = TimeData.Utc("2001-01-02 13:02:00");
        private static readonly TimeZoneInfo Timezone = TimezoneData.Swedish;
        private static readonly Currency Currency = CurrencyData.Sek;

        public static DetailedCashgame GameWithTwoPlayers(Role role, bool isRunning = false) => new DetailedCashgame(
            Id1,
            StartTime1,
            EndTime1,
            isRunning,
            new CashgameBunch(BunchData.Id1, Timezone, Currency),
            role,
            new CashgameLocation(LocationData.Id1, LocationData.Name1),
            null,
            new List<DetailedCashgame.CashgamePlayer>
            {
                new DetailedCashgame.CashgamePlayer(
                    PlayerData.Id1,
                    PlayerData.Name1,
                    PlayerData.Color1,
                    350,
                    200,
                    StartTime1,
                    EndTime1),
                new DetailedCashgame.CashgamePlayer(
                    PlayerData.Id2,
                    PlayerData.Name2,
                    PlayerData.Color2,
                    50,
                    200,
                    StartTime1,
                    EndTime1)
            });

        public static DetailedCashgame GameWithoutPlayers(Role role, bool isRunning = false) => new DetailedCashgame(
            Id2,
            StartTime1,
            EndTime1,
            isRunning,
            new CashgameBunch(BunchData.Id1, Timezone, Currency),
            role,
            new CashgameLocation(LocationData.Id1, LocationData.Name1),
            null,
            new List<DetailedCashgame.CashgamePlayer>());

        public static IList<ListCashgame> TwoGamesOnSameYearWithTwoPlayers => TwoGamesWithTwoPlayers(StartTime1, EndTime1, StartTime2, EndTime2);

        private static IList<ListCashgame> TwoGamesWithTwoPlayers(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2) =>
            new List<ListCashgame>
            {
                new ListCashgame(
                    Id1,
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
                            350,
                            200,
                            startTime1,
                            endTime1),
                        new ListCashgame.CashgamePlayer(
                            PlayerData.Id2,
                            PlayerData.Name2,
                            PlayerData.Color2,
                            50,
                            200,
                            startTime1,
                            endTime1)
                    }),
                new ListCashgame(
                    Id2,
                    startTime2,
                    endTime2,
                    false,
                    new SmallLocation(LocationData.Id1, LocationData.Name1),
                    new List<ListCashgame.CashgamePlayer>
                    {
                        new ListCashgame.CashgamePlayer(
                            PlayerData.Id1,
                            PlayerData.Name1,
                            PlayerData.Color1,
                            350,
                            200,
                            startTime2,
                            endTime2),
                        new ListCashgame.CashgamePlayer(
                            PlayerData.Id2,
                            PlayerData.Name2,
                            PlayerData.Color2,
                            50,
                            200,
                            startTime2,
                            endTime2)
                    })
            };
    }
}