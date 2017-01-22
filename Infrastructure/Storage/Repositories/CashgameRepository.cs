using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
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
            var apiDetailedCashgame = _api.Get<ApiDetailedCashgame>($"cashgame/{id}");
            return CreateDetailedCashgame(apiDetailedCashgame);
        }

        public Cashgame GetById(string cashgameId)
        {
            return _cacheContainer.GetAndStore(_cashgameDb.Get, cashgameId, TimeSpan.FromMinutes(CacheTime.Long));
        }

        private IList<Cashgame> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_cashgameDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
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
            _cashgameDb.DeleteGame(id);
            _cacheContainer.Remove<Cashgame>(id);
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

        private DetailedCashgame CreateDetailedCashgame(ApiDetailedCashgame c)
        {
            var currency = new Currency(c.Bunch.CurrencySymbol, c.Bunch.CurrencyLayout);
            var bunch = new DetailedCashgame.CashgameBunch(c.Bunch.Id, c.Bunch.Timezone, currency);
            var role = Role.Manager;
            var location = new DetailedCashgame.CashgameLocation(c.Location.Id, c.Location.Name);
            return new DetailedCashgame(c.Id, c.StartTime, c.EndTime, c.IsRunning, bunch, role, location, );
        }

        private class ApiDetailedCashgame
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public DateTime StartTime { get; set; }
            [UsedImplicitly]
            public DateTime? EndTime { get; set; }
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
                public TimeZoneInfo Timezone { get; set; }
                [UsedImplicitly]
                public string CurrencySymbol { get; set; }
                [UsedImplicitly]
                public string CurrencyLayout { get; set; }
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
            }
        }
    }
}