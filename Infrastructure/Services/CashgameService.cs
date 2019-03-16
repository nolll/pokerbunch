using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using ApiCashgame = PokerBunch.Client.Models.Response.Cashgame;
using ApiCashgamePlayer = PokerBunch.Client.Models.Response.CashgamePlayer;
using ApiCashgameAction = PokerBunch.Client.Models.Response.CashgameAction;
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

        public DetailedCashgame GetCurrent(string bunchId)
        {
            var apiCashgames = ApiClient.Cashgames.GetCurrent(bunchId);
            if(apiCashgames.Any())
                return GetDetailedById(apiCashgames.First().Id);
            return null;
        }

        public IList<ListCashgame> EventList(string eventId)
        {
            var apiCashgames = ApiClient.Cashgames.EventList(eventId);
            return apiCashgames.Select(CreateListCashgame).ToList();
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

        public string Add(string bunchId, string locationId)
        {
            var addObject = new CashgameAdd(bunchId, locationId);
            var apiCashgame = ApiClient.Cashgames.Add(addObject);
            return CreateDetailedCashgame(apiCashgame).Id;
        }

        public DetailedCashgame Update(string id, string locationId, string eventId)
        {
            var updateObject = new CashgameUpdate(id, locationId, eventId);
            var apiCashgame = ApiClient.Cashgames.Update(updateObject);
            return CreateDetailedCashgame(apiCashgame);
        }

        public void UpdateAction(string cashgameId, string actionId, DateTime timestamp, int stack, int added)
        {
            var updateObject = new CashgameActionUpdate(cashgameId, actionId, timestamp, stack, added);
            ApiClient.Cashgames.Actions.Update(updateObject);
        }

        public void DeleteAction(string cashgameId, string actionId)
        {
            ApiClient.Cashgames.Actions.Delete(cashgameId, actionId);
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
            var actions = p.Actions.Select(o => CreateAction(p.Id, o)).ToList();
            return new DetailedCashgame.CashgamePlayer(p.Id, p.Name, p.Color, p.Stack, p.Buyin, startTime, updatedTime, actions);
        }

        private DetailedCashgame.CashgameAction CreateAction(string playerId, ApiCashgameAction a)
        {
            var time = DateTime.SpecifyKind(a.Time, DateTimeKind.Utc);
            return new DetailedCashgame.CashgameAction(a.Id, playerId, GetActionType(a.Type), time, a.Stack, a.Added);
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

        private CheckpointType GetActionType(string t)
        {
            if (t == "buyin")
                return CheckpointType.Buyin;
            if (t == "cashout")
                return CheckpointType.Cashout;
            return CheckpointType.Report;
        }
    }
}