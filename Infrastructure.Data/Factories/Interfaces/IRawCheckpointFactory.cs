using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public interface IRawCheckpointFactory
    {
        RawCheckpoint Create(IStorageDataReader reader);
        RawCheckpoint Create(Cashgame cashgame, Checkpoint checkpoint);
    }
}