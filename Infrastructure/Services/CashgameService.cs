using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using ApiCashgame = PokerBunch.Client.Models.Response.Cashgame;
using ApiCashgamePlayer = PokerBunch.Client.Models.Response.CashgamePlayer;
using CashgameBunch = Core.Entities.CashgameBunch;
using CashgameEvent = Core.Entities.CashgameEvent;
using CashgameLocation = Core.Entities.CashgameLocation;

namespace Infrastructure.Api.Services
{
    public class CashgameService : BaseService, ICashgameService
    {
        public CashgameService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public DetailedCashgame GetDetailedById(string id)
        {
            var apiDetailedCashgame = ApiClient.Cashgames.GetDetailedById(id);
            return CreateDetailedCashgame(apiDetailedCashgame);
        }

        public IList<ListCashgame> PlayerList(string playerId)
        {
            var apiCashgames = ApiClient.Cashgames.PlayerList(playerId);
            return apiCashgames.Select(CreateListCashgame).ToList();
        }

        public void DeleteGame(string id)
        {
            ApiClient.Cashgames.Delete(id);
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

        private DetailedCashgame CreateDetailedCashgame(ApiCashgame c)
        {
            var culture = CultureInfo.CreateSpecificCulture(c.Bunch.Culture);
            var currency = new Currency(c.Bunch.CurrencySymbol, c.Bunch.CurrencyLayout, culture, c.Bunch.ThousandSeparator);
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(c.Bunch.Timezone);
            var bunch = new CashgameBunch(c.Bunch.Id, timezone, currency);
            var role = GetRole(c.Bunch.Role);
            var location = new CashgameLocation(c.Location.Id, c.Location.Name);
            var @event = c.Event != null ? new CashgameEvent(c.Event.Id, c.Event.Name) : null;
            var players = c.Players.Select(CreatePlayer).ToList();
            var startTime = DateTime.SpecifyKind(c.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(c.UpdatedTime, DateTimeKind.Utc);
            return new DetailedCashgame(c.Id, startTime, updatedTime, c.IsRunning, bunch, role, location, @event, players);
        }

        private DetailedCashgame.CashgamePlayer CreatePlayer(ApiCashgamePlayer p)
        {
            var startTime = DateTime.SpecifyKind(p.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(p.UpdatedTime, DateTimeKind.Utc);
            return new DetailedCashgame.CashgamePlayer(p.Id, p.Name, p.Color, p.Stack, p.Buyin, startTime, updatedTime);
        }

        private Role GetRole(string r)
        {
            if (r == "manager")
                return Role.Manager;
            if (r == "player")
                return Role.Player;
            if (r == "guest")
                return Role.Guest;
            return Role.None;
        }
    }
}