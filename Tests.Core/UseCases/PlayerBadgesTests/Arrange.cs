using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.UseCases;
using Tests.Core.Data;

namespace Tests.Core.UseCases.PlayerBadgesTests
{
    public abstract class Arrange : UseCaseTest<PlayerBadges>
    {
        protected PlayerBadges.Result Result;

        private const string PlayerId = PlayerData.Id1;
        protected abstract int NumberOfGames { get; }

        protected override void Setup()
        {
            var cashgames = GetGames(NumberOfGames);

            Mock<ICashgameRepository>().Setup(o => o.PlayerList(PlayerId)).Returns(cashgames);
        }

        protected override void Execute()
        {
            Result = Subject.Execute(new PlayerBadges.Request(PlayerId));
        }

        private IList<ListCashgame> GetGames(int numberOfGames)
        {
            var startTime = DateTime.Parse("2001-01-01 10:00:00");
            var endTime = DateTime.Parse("2001-01-01 11:00:00");
            var location = new SmallLocation(LocationData.Id1, LocationData.Name1);
            var players = new List<ListCashgame.CashgamePlayer>();
            var games = new List<ListCashgame>();
            for (var i = 0; i < numberOfGames; i++)
            {
                games.Add(new ListCashgame(CashgameData.Id1, startTime, endTime, false, location, players));
            }
            return games;
        } 
    }
}