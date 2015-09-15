using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Checkpoints;
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

        public Cashgame Get(int cashgameId)
        {
            return _cashgameRepository.Get(cashgameId);
        }

        public IList<Cashgame> Get(IList<int> ids)
        {
            return _cashgameRepository.Get(ids);
        }

        public IList<int> FindFinished(int bunchId, int? year = null)
        {
            return _cashgameRepository.FindFinished(bunchId, year);
        }

        public IList<int> FindByEvent(int eventId)
        {
            return _cashgameRepository.FindByEvent(eventId);
        }

        public IList<int> FindByPlayerId(int playerId)
        {
            return _cashgameRepository.FindByPlayerId(playerId);
        }

        public IList<int> FindRunning(int bunchId)
        {
            return _cashgameRepository.FindRunning(bunchId);
        }

        public IList<int> GetYears(int bunchId)
        {
            return _cashgameRepository.GetYears(bunchId);
        }

        public IList<string> GetLocations(int bunchId)
        {
            return _cashgameRepository.GetLocations(bunchId);
        }

        public bool DeleteGame(int id)
        {
            return _cashgameRepository.DeleteGame(id);
        }

        public int AddGame(Bunch bunch, Cashgame cashgame)
        {
            return _cashgameRepository.AddGame(bunch, cashgame);
        }

        public bool UpdateGame(Cashgame cashgame)
        {
            return _cashgameRepository.UpdateGame(cashgame);
        }

        public int AddCheckpoint(Checkpoint checkpoint)
        {
            return _cashgameRepository.AddCheckpoint(checkpoint);
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            return _cashgameRepository.UpdateCheckpoint(checkpoint);
        }

        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            return _cashgameRepository.DeleteCheckpoint(checkpoint);
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            return _cashgameRepository.GetCheckpoint(checkpointId);
        }

        public IList<int> FindCheckpoints(int cashgameId)
        {
            return _cashgameRepository.FindCheckpoints(cashgameId);
        }

        public IList<int> FindCheckpoints(IList<int> cashgameIds)
        {
            return _cashgameRepository.FindCheckpoints(cashgameIds);
        }
    }
}