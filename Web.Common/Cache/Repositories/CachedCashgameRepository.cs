using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
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

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<Cashgame> GetByEvent(int eventId)
        {
            throw new System.NotImplementedException();
        }

        public Cashgame GetRunning(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public Cashgame GetById(int cashgameId)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> GetYears(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public IList<string> GetLocations(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteGame(Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public int AddGame(Bunch bunch, Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateGame(Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            throw new System.NotImplementedException();
        }

        public bool HasPlayed(int playerId)
        {
            throw new System.NotImplementedException();
        }
    }
}