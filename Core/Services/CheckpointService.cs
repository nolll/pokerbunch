﻿using Core.Entities.Checkpoints;
using Core.Repositories;

namespace Core.Services
{
    public class CheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;

        public CheckpointService(ICheckpointRepository checkpointRepository)
        {
            _checkpointRepository = checkpointRepository;
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