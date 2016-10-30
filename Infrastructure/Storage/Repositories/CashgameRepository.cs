using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

namespace Infrastructure.Storage.Repositories
{
    public class CashgameRepository : ICashgameRepository
    {
        private readonly SqlCashgameDb _cashgameDb;
        private readonly ICacheContainer _cacheContainer;

        public CashgameRepository(SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _cashgameDb = new SqlCashgameDb(db);
            _cacheContainer = cacheContainer;
        }

        public Cashgame Get(string cashgameId)
        {
            return _cacheContainer.GetAndStore(_cashgameDb.Get, cashgameId, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Cashgame> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_cashgameDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> FindFinished(string bunchId, int? year = null)
        {
            return _cashgameDb.FindFinished(bunchId, year);
        }

        public IList<string> FindByEvent(string eventId)
        {
            return _cashgameDb.FindByEvent(eventId);
        }

        public IList<string> FindByPlayerId(string playerId)
        {
            return _cashgameDb.FindByPlayerId(playerId);
        }

        public IList<string> FindRunning(string bunchId)
        {
            return _cashgameDb.FindRunning(bunchId);
        }

        public IList<string> FindByCheckpoint(string checkpointId)
        {
            return _cashgameDb.FindByCheckpoint(checkpointId);
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

        public string AddGame(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameDb.AddGame(bunch, cashgame);
        }

        public void UpdateGame(Cashgame cashgame)
        {
            _cashgameDb.UpdateGame(cashgame);
            _cacheContainer.Remove<Cashgame>(cashgame.Id);
        }
    }
}