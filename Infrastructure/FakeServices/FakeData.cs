using System;
using System.Collections.Generic;
using Core.Entities;

namespace Infrastructure.Api.FakeServices
{
    public static class FakeData
    {
        public static string CurrentUserId = "user1";

        public static readonly List<User> Users = new List<User>
        {
            new User("user1", "user1", "User 1", "Firstname1 Lastname1", "user1@example.org", Role.Admin)
        };

        public static readonly List<Bunch> Bunches = new List<Bunch>
        {
            new Bunch("bunch1", "Bunch 1", "This is Bunch 1", "Bunch 1 House Rules", null, 200)
        };

        public static readonly List<Player> Players = new List<Player>
        {
            new Player("bunch1", "player1", "user1", "user1", "User 1", Role.Admin, "#555")
        };

        public static readonly List<DetailedCashgame> Cashgames = new List<DetailedCashgame>
        {
            new DetailedCashgame(
                "cashgame1",
                DateTime.Parse("2018-02-02T18:00:00Z"),
                DateTime.Parse("2018-02-02T19:00:00Z"),
                false,
                new CashgameBunch("bunch1", TimeZoneInfo.Utc, Currency.Default),
                Role.Admin,
                new CashgameLocation("location1", "Location 1"),
                null,
                new List<DetailedCashgame.CashgamePlayer>
                {
                    new DetailedCashgame.CashgamePlayer(
                        "player1",
                        "Player 1",
                        "#555",
                        55,
                        200,
                        DateTime.Parse("2018-02-02T18:00:00Z"),
                        DateTime.Parse("2018-02-02T18:30:00Z"))
                }),
            new DetailedCashgame(
                "cashgame2",
                DateTime.Parse("2018-02-03T18:00:00Z"),
                DateTime.Parse("2018-02-03T18:30:00Z"),
                true,
                new CashgameBunch("bunch1", TimeZoneInfo.Utc, Currency.Default),
                Role.Admin,
                new CashgameLocation("location1", "Location 1"),
                null,
                new List<DetailedCashgame.CashgamePlayer>
                {
                    new DetailedCashgame.CashgamePlayer(
                        "player1",
                        "Player 1",
                        "#555",
                        55,
                        200,
                        DateTime.Parse("2018-02-03T18:00:00Z"),
                        DateTime.Parse("2018-02-03T18:30:00Z"))
                })
        };
    }
}