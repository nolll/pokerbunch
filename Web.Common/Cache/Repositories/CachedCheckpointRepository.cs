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

        public int AddCheckpoint(Checkpoint checkpoint)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            throw new System.NotImplementedException();
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            throw new System.NotImplementedException();
        }
    }
}