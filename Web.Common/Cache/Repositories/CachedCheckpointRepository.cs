using Core.Entities.Checkpoints;
using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
{
    public class CachedCheckpointRepository : ICheckpointRepository
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedCheckpointRepository(ICheckpointRepository checkpointRepository, ICacheContainer cacheContainer)
        {
            _checkpointRepository = checkpointRepository;
            _cacheContainer = cacheContainer;
        }

        public int Add(Checkpoint checkpoint)
        {
            return _checkpointRepository.Add(checkpoint);
        }

        public bool Update(Checkpoint checkpoint)
        {
            return _checkpointRepository.Update(checkpoint);
        }

        public bool Delete(Checkpoint checkpoint)
        {
            return _checkpointRepository.Delete(checkpoint);
        }

        public Checkpoint Get(int checkpointId)
        {
            return _checkpointRepository.Get(checkpointId);
        }
    }
}