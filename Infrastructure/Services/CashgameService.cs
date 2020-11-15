using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models.Response;

namespace Infrastructure.Api.Services
{
    public class CashgameService : BaseService, ICashgameService
    {
        public CashgameService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public IList<ListCashgame> PlayerList(string playerId)
        {
            var apiCashgames = ApiClient.Cashgames.PlayerList(playerId);
            return apiCashgames.Select(CreateListCashgame).ToList();
        }

        private ListCashgame CreateListCashgame(CashgameSmall c)
        {
            var location = new SmallLocation(c.Location.Id, c.Location.Name);
            var players = c.Players.Select(CreatePlayer).ToList();
            var startTime = DateTime.SpecifyKind(c.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(c.UpdatedTime, DateTimeKind.Utc);
            return new ListCashgame(c.Id, startTime, updatedTime, c.IsRunning, location, players);
        }

        private ListCashgame.CashgamePlayer CreatePlayer(CashgameSmallPlayer p)
        {
            var startTime = DateTime.SpecifyKind(p.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(p.UpdatedTime, DateTimeKind.Utc);
            return new ListCashgame.CashgamePlayer(p.Id, p.Name, p.Color, p.Stack, p.Buyin, startTime, updatedTime);
        }
    }
}