using System;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public interface IRawCheckpointFactory
    {
        RawCheckpoint Create(StorageDataReader reader, TimeZoneInfo timeZone);
        RawCheckpoint Create(Checkpoint checkpoint);
    }
}