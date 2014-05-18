using Core.Entities.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public interface ICheckpointDataMapper
    {
        Checkpoint Map(RawCheckpoint rawCheckpoint);
    }
}