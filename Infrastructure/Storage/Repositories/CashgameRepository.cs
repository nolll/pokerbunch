using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Repositories
{
    public class CashgameRepository : ICashgameRepository
    {
        private readonly SqlCashgameDb _cashgameDb;
        private readonly ApiConnection _api;
        private readonly ICacheContainer _cacheContainer;

        public CashgameRepository(ApiConnection api, SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _cashgameDb = new SqlCashgameDb(db);
            _api = api;
            _cacheContainer = cacheContainer;
        }

        public DetailedCashgame GetDetailedById(string id)
        {
            var apiDetailedCashgame = _api.Get<ApiDetailedCashgame>($"cashgames/{id}");
            return CreateDetailedCashgame(apiDetailedCashgame);
        }

        private IList<Cashgame> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_cashgameDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<ListCashgame> List(string bunchId, int? year = null)
        {
            var url = year.HasValue ? $"bunches/{bunchId}/cashgames/{year}" : $"bunches/{bunchId}/cashgames";
            var apiListCashgames = _api.Get<IList<ApiListCashgame>>(url);
            return apiListCashgames.Select(CreateListCashgame).ToList();
        }

        public IList<ListCashgame> EventList(string eventId)
        {
            var apiListCashgames = _api.Get<IList<ApiListCashgame>>($"events/{eventId}/cashgames");
            return apiListCashgames.Select(CreateListCashgame).ToList();
        }

        public IList<ListCashgame> PlayerList(string playerId)
        {
            var apiListCashgames = _api.Get<IList<ApiListCashgame>>($"players/{playerId}/cashgames");
            return apiListCashgames.Select(CreateListCashgame).ToList();
        }

        public IList<Cashgame> ListFinished(string bunchId, int? year = null)
        {
            var ids = _cashgameDb.FindFinished(bunchId, year);
            return Get(ids);
        }

        public IList<Cashgame> ListByEvent(string eventId)
        {
            var ids = _cashgameDb.FindByEvent(eventId);
            return Get(ids);
        }

        public IList<Cashgame> ListByPlayer(string playerId)
        {
            var ids = _cashgameDb.FindByPlayerId(playerId);
            return Get(ids);
        }

        public Cashgame GetRunning(string bunchId)
        {
            var ids = _cashgameDb.FindRunning(bunchId);
            return Get(ids).FirstOrDefault();
        }

        public Cashgame GetByCheckpoint(string checkpointId)
        {
            var ids = _cashgameDb.FindByCheckpoint(checkpointId);
            return Get(ids).FirstOrDefault();
        }

        public IList<int> GetYears(string bunchId)
        {
            return _cashgameDb.GetYears(bunchId);
        }

        public void DeleteGame(string id)
        {
            _api.Delete($"cashgames/{id}");
        }

        public string Add(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameDb.AddGame(bunch, cashgame);
        }

        public void Update(Cashgame cashgame)
        {
            _cashgameDb.UpdateGame(cashgame);
            _cacheContainer.Remove<Cashgame>(cashgame.Id);
        }

        public DetailedCashgame Update(string id, string locationId, string eventId)
        {
            var updateObject = new ApiUpdateCashgame(locationId, eventId);
            var apiCashgame = _api.Put<ApiDetailedCashgame>($"cashgames/{id}", updateObject);
            return CreateDetailedCashgame(apiCashgame);
        }

        private ListCashgame CreateListCashgame(ApiListCashgame c)
        {
            var culture = CultureInfo.CreateSpecificCulture(c.Bunch.Culture);
            var currency = new Currency(c.Bunch.CurrencySymbol, c.Bunch.CurrencyLayout, culture, c.Bunch.ThousandSeparator);
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(c.Bunch.Timezone);
            var bunch = new ListCashgame.CashgameBunch(c.Bunch.Id, timezone, currency);
            var role = GetRole(c.Bunch.Role);
            var location = new ListCashgame.CashgameLocation(c.Location.Id, c.Location.Name);
            var players = c.Players.Select(CreatePlayer).ToList();
            var startTime = DateTime.SpecifyKind(c.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(c.UpdatedTime, DateTimeKind.Utc);
            return new ListCashgame(c.Id, startTime, updatedTime, c.IsRunning, bunch, role, location, players);
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
            var bunch = new DetailedCashgame.CashgameBunch(c.Bunch.Id, timezone, currency);
            var role = GetRole(c.Bunch.Role);
            var location = new DetailedCashgame.CashgameLocation(c.Location.Id, c.Location.Name);
            var players = c.Players.Select(CreatePlayer).ToList();
            var startTime = DateTime.SpecifyKind(c.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(c.UpdatedTime, DateTimeKind.Utc);
            return new DetailedCashgame(c.Id, startTime, updatedTime, c.IsRunning, bunch, role, location, players);
        }

        private DetailedCashgame.CashgamePlayer CreatePlayer(ApiDetailedCashgame.ApiDetailedCashgamePlayer p)
        {
            var startTime = DateTime.SpecifyKind(p.StartTime, DateTimeKind.Utc);
            var updatedTime = DateTime.SpecifyKind(p.UpdatedTime, DateTimeKind.Utc);
            var actions = p.Actions.Select(CreateAction).ToList();
            return new DetailedCashgame.CashgamePlayer(p.Id, p.Name, p.Color, p.Stack, p.Buyin, startTime, updatedTime, actions);
        }

        private DetailedCashgame.CashgameAction CreateAction(ApiDetailedCashgame.ApiDetailedCashgameAction a)
        {
            var time = DateTime.SpecifyKind(a.Time, DateTimeKind.Utc);
            return new DetailedCashgame.CashgameAction(a.Id, GetActionType(a.Type), time, a.Stack, a.Added);
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
            public ApiListCashgameBunch Bunch { get; set; }
            [UsedImplicitly]
            public Role Role { get; set; }
            [UsedImplicitly]
            public ApiListCashgameLocation Location { get; set; }
            [UsedImplicitly]
            public IList<ApiListCashgamePlayer> Players { get; set; }

            public class ApiListCashgameBunch
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

            public class ApiListCashgameLocation
            {
                [UsedImplicitly]
                public string Id { get; set; }
                [UsedImplicitly]
                public string Name { get; set; }
            }

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
            public ApiDetailedCashgameBunch Bunch { get; set; }
            [UsedImplicitly]
            public Role Role { get; set; }
            [UsedImplicitly]
            public ApiDetailedCashgameLocation Location { get; set; }
            [UsedImplicitly]
            public IList<ApiDetailedCashgamePlayer> Players { get; set; }

            public class ApiDetailedCashgameBunch
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

            public class ApiDetailedCashgameLocation
            {
                [UsedImplicitly]
                public string Id { get; set; }
                [UsedImplicitly]
                public string Name { get; set; }
            }

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
    }
}