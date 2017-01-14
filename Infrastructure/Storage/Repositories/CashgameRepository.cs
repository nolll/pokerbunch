using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

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

        public Cashgame GetById(string cashgameId)
        {
            return _cacheContainer.GetAndStore(_cashgameDb.Get, cashgameId, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Cashgame> Get(IList<string> ids)
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
    }
}