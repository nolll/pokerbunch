using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;
using Infrastructure.Api.Clients;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using Infrastructure.Api.Models.Request;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class CashgameClient : ApiClient
    {
        public CashgameActionClient Actions { get; }

        public CashgameClient(ApiConnection apiConnection) : base(apiConnection)
        {
            Actions = new CashgameActionClient(apiConnection);
        }

        public ApiDetailedCashgame GetDetailedById(string id)
        {
            return ApiConnection.Get<ApiDetailedCashgame>(new ApiCashgameUrl(id));
        }

        public IList<ApiListCashgame> GetCurrent(string bunchId)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiBunchCashgamesCurrentUrl(bunchId));
        }

        public IList<ApiListCashgame> List(string bunchId, int? year = null)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiBunchCashgamesUrl(bunchId, year));
        }

        public IList<ApiListCashgame> EventList(string eventId)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiEventCashgamesUrl(eventId));
        }

        public IList<ApiListCashgame> PlayerList(string playerId)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiPlayerCashgamesUrl(playerId));
        }
    }

    public class CashgameActionClient : ApiClient
    {
        public CashgameActionClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        
    }

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

        public IList<ListCashgame> List(string bunchId, int? year = null)
        {
            var apiCashgames = ApiClient.Cashgames.
            return apiCashgames.Select(CreateListCashgame).ToList();
        }

        public IList<ListCashgame> EventList(string eventId)
        {
            var apiCashgames = _api.Get<IList<ApiListCashgame>>(new ApiEventCashgamesUrl(eventId));
            return apiCashgames.Select(CreateListCashgame).ToList();
        }

        public IList<ListCashgame> PlayerList(string playerId)
        {
            var apiCashgames = _api.Get<IList<ApiListCashgame>>(new ApiPlayerCashgamesUrl(playerId));
            return apiCashgames.Select(CreateListCashgame).ToList();
        }

        public IList<int> GetYears(string bunchId)
        {
            var apiYears = _api.Get<IList<ApiYear>>(new ApiBunchCashgameYearsUrl(bunchId));
            return apiYears.Select(o => o.Year).ToList();
        }

        public void Report(string cashgameId, string playerId, int stack)
        {
            var apiReport = new ApiReport(playerId, stack);
            _api.Post(new ApiCashgameReportUrl(cashgameId), apiReport);
        }

        public void Buyin(string cashgameId, string playerId, int added, int stack)
        {
            var apiBuyin = new ApiBuyin(playerId, added, stack);
            _api.Post(new ApiCashgameBuyinUrl(cashgameId), apiBuyin);
        }

        public void Cashout(string cashgameId, string playerId, int stack)
        {
            var apiCashout = new ApiCashout(playerId, stack);
            _api.Post(new ApiCashgameCashoutUrl(cashgameId), apiCashout);
        }

        public void End(string cashgameId)
        {
            _api.Post(new ApiCashgameEndUrl(cashgameId));
        }

        public void DeleteGame(string id)
        {
            _api.Delete(new ApiCashgameUrl(id));
        }

        public string Add(string bunchId, string locationId)
        {
            var addObject = new ApiAddCashgame(locationId);
            var apiCashgame = _api.Post<ApiDetailedCashgame>(new ApiBunchCashgamesUrl(bunchId), addObject);
            return CreateDetailedCashgame(apiCashgame).Id;
        }

        public DetailedCashgame Update(string id, string locationId, string eventId)
        {
            var updateObject = new ApiUpdateCashgame(locationId, eventId);
            var apiCashgame = _api.Put<ApiDetailedCashgame>(new ApiCashgameUrl(id), updateObject);
            return CreateDetailedCashgame(apiCashgame);
        }

        public void UpdateAction(string cashgameId, string actionId, DateTime timestamp, int stack, int added)
        {
            var updateObject = new ApiUpdateCashgameAction(timestamp, stack, added);
            _api.Put<ApiDetailedCashgame>(new ApiCashgameActionUrl(cashgameId, actionId), updateObject);
        }

        public void DeleteAction(string cashgameId, string actionId)
        {
            _api.Delete(new ApiCashgameActionUrl(cashgameId, actionId));
        }

        private ListCashgame CreateListCashgame(ApiListCashgame c)
        {
            var location = new SmallLocation(c.Location.Id, c.Location.Name);
            var players = c.Players.Select(CreatePlayer).ToList();
            var startTime = DateTime.SpecifyKind(c.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(c.UpdatedTime, DateTimeKind.Utc);
            return new ListCashgame(c.Id, startTime, updatedTime, c.IsRunning, location, players);
        }

        private ListCashgame.CashgamePlayer CreatePlayer(ApiListCashgame.ApiListCashgamePlayer p)
        {
            var startTime = DateTime.SpecifyKind(p.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(p.UpdatedTime, DateTimeKind.Utc);
            return new ListCashgame.CashgamePlayer(p.Id, p.Name, p.Color, p.Stack, p.Buyin, startTime, updatedTime);
        }

        private DetailedCashgame CreateDetailedCashgame(ApiDetailedCashgame c)
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

        private DetailedCashgame.CashgamePlayer CreatePlayer(ApiDetailedCashgame.ApiDetailedCashgamePlayer p)
        {
            var startTime = DateTime.SpecifyKind(p.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(p.UpdatedTime, DateTimeKind.Utc);
            var actions = p.Actions.Select(o => CreateAction(p.Id, o)).ToList();
            return new DetailedCashgame.CashgamePlayer(p.Id, p.Name, p.Color, p.Stack, p.Buyin, startTime, updatedTime, actions);
        }

        private DetailedCashgame.CashgameAction CreateAction(string playerId, ApiDetailedCashgame.ApiDetailedCashgameAction a)
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