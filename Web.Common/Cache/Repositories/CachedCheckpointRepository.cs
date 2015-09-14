//using Core.Entities.Checkpoints;
//using Core.Repositories;
//using Core.Services;

//namespace Web.Common.Cache.Repositories
//{
//    public class CachedCheckpointRepository : ICheckpointRepository
//    {
//        private readonly ICheckpointRepository _checkpointRepository;
//        private readonly ICacheContainer _cacheContainer;

//        public CachedCheckpointRepository(ICheckpointRepository checkpointRepository, ICacheContainer cacheContainer)
//        {
//            _checkpointRepository = checkpointRepository;
//            _cacheContainer = cacheContainer;
//        }

//        public int AddCheckpoint(Checkpoint checkpoint)
//        {
//            return _checkpointRepository.AddCheckpoint(checkpoint);
//        }

//        public bool UpdateCheckpoint(Checkpoint checkpoint)
//        {
//            return _checkpointRepository.UpdateCheckpoint(checkpoint);
//        }

//        public bool DeleteCheckpoint(Checkpoint checkpoint)
//        {
//            return _checkpointRepository.DeleteCheckpoint(checkpoint);
//        }

//        public Checkpoint GetCheckpoint(int checkpointId)
//        {
//            return _checkpointRepository.GetCheckpoint(checkpointId);
//        }
//    }
//}