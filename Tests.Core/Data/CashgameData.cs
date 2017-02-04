using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;

namespace Tests.Core.Data
{
    public static class CashgameData
    {
        public const string Id1 = "cashgame-id-1";
        public static readonly DateTime StartTime = TimeData.Utc("2001-01-01 12:00:00");
        public static readonly DateTime EndTime = TimeData.Utc("2001-01-01 13:02:00");
        public const bool IsRunning = false;
        public static readonly TimeZoneInfo Timezone = TimezoneData.Swedish;
        public static readonly Currency Currency = CurrencyData.Sek;

        public static DetailedCashgame EndedGameWithTwoPlayers(Role role) => new DetailedCashgame(
            Id1,
            StartTime,
            EndTime,
            IsRunning,
            new DetailedCashgame.CashgameBunch(BunchData.Id1, Timezone, Currency),
            role,
            new DetailedCashgame.CashgameLocation(LocationData.Id1, LocationData.Name1),
            new List<DetailedCashgame.CashgamePlayer>
            {
                new DetailedCashgame.CashgamePlayer(
                    PlayerData.Id1,
                    PlayerData.Name1,
                    PlayerData.Color1,
                    350,
                    200,
                    StartTime,
                    EndTime,
                    new List<DetailedCashgame.CashgameAction>
                    {
                        new DetailedCashgame.CashgameAction(
                            "1",
                            CheckpointType.Buyin,
                            TimeData.Utc("2001-01-01 12:00"),
                            200,
                            200),
                        new DetailedCashgame.CashgameAction(
                            "2",
                            CheckpointType.Cashout,
                            TimeData.Utc("2001-01-01 13:00"),
                            50,
                            0)
                    }),
                new DetailedCashgame.CashgamePlayer(
                    PlayerData.Id2,
                    PlayerData.Name2,
                    PlayerData.Color2,
                    50,
                    200,
                    StartTime,
                    EndTime,
                    new List<DetailedCashgame.CashgameAction>
                    {
                        new DetailedCashgame.CashgameAction(
                            "3",
                            CheckpointType.Buyin,
                            TimeData.Utc("2001-01-01 12:05"),
                            200,
                            200),
                        new DetailedCashgame.CashgameAction(
                            "4",
                            CheckpointType.Report,
                            TimeData.Utc("2001-01-01 12:35"),
                            250,
                            0),
                        new DetailedCashgame.CashgameAction(
                            "5",
                            CheckpointType.Cashout,
                            TimeData.Utc("2001-01-01 13:00"),
                            350,
                            0)
                    })
            });
    }
}