using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Storage.CachedRepositories
{
    public class CachedCashgameRepository : ICashgameRepository
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedCashgameRepository(ICashgameRepository cashgameRepository, ICacheContainer cacheContainer)
        {
            _cashgameRepository = cashgameRepository;
            _cacheContainer = cacheContainer;
        }

        public Cashgame Get(string cashgameId)
        {
            return _cacheContainer.GetAndStore(_cashgameRepository.Get, cashgameId, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Cashgame> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_cashgameRepository.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> FindFinished(string bunchId, int? year = null)
        {
            return _cashgameRepository.FindFinished(bunchId, year);
        }

        public IList<string> FindByEvent(string eventId)
        {
            return _cashgameRepository.FindByEvent(eventId);
        }

        public IList<string> FindByPlayerId(string playerId)
        {
            return _cashgameRepository.FindByPlayerId(playerId);
        }

        public IList<string> FindRunning(string bunchId)
        {
            return _cashgameRepository.FindRunning(bunchId);
        }

        public IList<string> FindByCheckpoint(string checkpointId)
        {
            return _cashgameRepository.FindByCheckpoint(checkpointId);
        }

        public IList<int> GetYears(string bunchId)
        {
            return _cashgameRepository.GetYears(bunchId);
        }

        public void DeleteGame(string id)
        {
            _cashgameRepository.DeleteGame(id);
            _cacheContainer.Remove<Cashgame>(id);
        }

        public string AddGame(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameRepository.AddGame(bunch, cashgame);
        }

        public void UpdateGame(Cashgame cashgame)
        {
            _cashgameRepository.UpdateGame(cashgame);
            _cacheContainer.Remove<Cashgame>(cashgame.Id);
        }
    }
}