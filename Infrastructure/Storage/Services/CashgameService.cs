using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Services;
using JetBrains.Annotations;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Storage.Services
{
    public class CashgameService : ICashgameService
    {
        private readonly ApiConnection _api;

        public CashgameService(ApiConnection api)
        {
            _api = api;
        }

        public DetailedCashgame GetDetailedById(string id)
        {
            var apiDetailedCashgame = _api.Get<ApiDetailedCashgame>(new ApiCashgameUrl(id));
            return CreateDetailedCashgame(apiDetailedCashgame);
        }

        public DetailedCashgame GetCurrent(string bunchId)
        {
            var apiCashgames = _api.Get<IList<ApiListCashgame>>(new ApiBunchCashgamesCurrentUrl(bunchId));
            if(apiCashgames.Any())
                return GetDetailedById(apiCashgames.First().Id);
            return null;
        }

        public IList<ListCashgame> List(string bunchId, int? year = null)
        {
            var apiCashgames = _api.Get<IList<ApiListCashgame>>(new ApiBunchCashgamesUrl(bunchId, year));
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

        public void UpdateAction(string cashgameId, string actionId, DateTime timestamp, int stack, int added)
        {
            var updateObject = new ApiUpdateCashgameAction(timestamp, stack, added);
            _api.Put<ApiDetailedCashgame>(new ApiCashgameActionUrl(cashgameId, actionId), updateObject);
        }

        public void DeleteAction(string actionId)
        {
            throw new NotImplementedException();
        }

        private class ApiUpdateCashgame
        {
            public string LocationId { get; }
            public string EventId { get; }

            public ApiUpdateCashgame(string locationId, string eventId)
            {
                LocationId = locationId;
                EventId = eventId;
            }
        }

        private class ApiUpdateCashgameAction
        {
            public DateTime Timestamp { get; }
            public int Stack { get; }
            public int Added { get; }

            public ApiUpdateCashgameAction(DateTime timestamp, int stack, int added)
            {
                Timestamp = timestamp;
                Stack = stack;
                Added = added;
            }
        }

        private class ApiAddCashgame
        {
            public string LocationId { get; }

            public ApiAddCashgame(string locationId)
            {
                LocationId = locationId;
            }
        }

        private class ApiYear
        {
            public int Year { get; }

            public ApiYear(int year)
            {
                Year = year;
            }
        }

        private class ApiListCashgame
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public DateTime StartTime { get; set; }
            [UsedImplicitly]
            public DateTime UpdatedTime { get; set; }
            [UsedImplicitly]
            public bool IsRunning { get; set; }
            [UsedImplicitly]
            public Role Role { get; set; }
            [UsedImplicitly]
            public ApiCashgameLocation Location { get; set; }
            [UsedImplicitly]
            public IList<ApiListCashgamePlayer> Players { get; set; }

            public class ApiListCashgamePlayer
            {
                [UsedImplicitly]
                public string Id { get; set; }
                [UsedImplicitly]
                public string Name { get; set; }
                [UsedImplicitly]
                public string Color { get; set; }
                [UsedImplicitly]
                public int Stack { get; set; }
                [UsedImplicitly]
                public int Buyin { get; set; }
                [UsedImplicitly]
                public DateTime StartTime { get; set; }
                [UsedImplicitly]
                public DateTime UpdatedTime { get; set; }
            }

            public class ApiDetailedCashgameAction
            {
                [UsedImplicitly]
                public string Id { get; set; }
                [UsedImplicitly]
                public string Type { get; set; }
                [UsedImplicitly]
                public DateTime Time { get; set; }
                [UsedImplicitly]
                public int Stack { get; set; }
                [UsedImplicitly]
                public int Added { get; set; }
            }
        }

        public class ApiCashgameBunch
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Timezone { get; set; }
            [UsedImplicitly]
            public string CurrencySymbol { get; set; }
            [UsedImplicitly]
            public string CurrencyLayout { get; set; }
            [UsedImplicitly]
            public string ThousandSeparator { get; set; }
            [UsedImplicitly]
            public string Culture { get; set; }
            [UsedImplicitly]
            public string Role { get; set; }
        }

        public class ApiCashgameLocation
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
        }

        public class ApiCashgameEvent
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
        }

        private class ApiDetailedCashgame
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public DateTime StartTime { get; set; }
            [UsedImplicitly]
            public DateTime UpdatedTime { get; set; }
            [UsedImplicitly]
            public bool IsRunning { get; set; }
            [UsedImplicitly]
            public ApiCashgameBunch Bunch { get; set; }
            [UsedImplicitly]
            public Role Role { get; set; }
            [UsedImplicitly]
            public ApiCashgameLocation Location { get; set; }
            [UsedImplicitly]
            public ApiCashgameEvent Event { get; set; }
            [UsedImplicitly]
            public IList<ApiDetailedCashgamePlayer> Players { get; set; }

            public class ApiDetailedCashgamePlayer
            {
                [UsedImplicitly]
                public string Id { get; set; }
                [UsedImplicitly]
                public string Name { get; set; }
                [UsedImplicitly]
                public string Color { get; set; }
                [UsedImplicitly]
                public int Stack { get; set; }
                [UsedImplicitly]
                public int Buyin { get; set; }
                [UsedImplicitly]
                public DateTime StartTime { get; set; }
                [UsedImplicitly]
                public DateTime UpdatedTime { get; set; }
                [UsedImplicitly]
                public IList<ApiDetailedCashgameAction> Actions { get; set; }
            }

            public class ApiDetailedCashgameAction
            {
                [UsedImplicitly]
                public string Id { get; set; }
                [UsedImplicitly]
                public string Type { get; set; }
                [UsedImplicitly]
                public DateTime Time { get; set; }
                [UsedImplicitly]
                public int Stack { get; set; }
                [UsedImplicitly]
                public int Added { get; set; }
            }
        }

        public class ApiReport
        {
            [UsedImplicitly]
            public string PlayerId { get; set; }
            [UsedImplicitly]
            public int Stack { get; set; }

            public ApiReport(string playerId, int stack)
            {
                PlayerId = playerId;
                Stack = stack;
            }
        }

        public class ApiBuyin
        {
            [UsedImplicitly]
            public string PlayerId { get; set; }
            [UsedImplicitly]
            public int Added { get; set; }
            [UsedImplicitly]
            public int Stack { get; set; }

            public ApiBuyin(string playerId, int added, int stack)
            {
                PlayerId = playerId;
                Added = added;
                Stack = stack;
            }
        }

        public class ApiCashout
        {
            [UsedImplicitly]
            public string PlayerId { get; set; }
            [UsedImplicitly]
            public int Stack { get; set; }

            public ApiCashout(string playerId, int stack)
            {
                PlayerId = playerId;
                Stack = stack;
            }
        }
    }
}