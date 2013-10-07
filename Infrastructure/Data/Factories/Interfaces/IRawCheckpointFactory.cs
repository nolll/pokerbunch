using System;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public interface IRawCheckpointFactory
    {
        RawCheckpoint Create(StorageDataReader reader);
        RawCheckpoint Create(Checkpoint checkpoint);
    }
}