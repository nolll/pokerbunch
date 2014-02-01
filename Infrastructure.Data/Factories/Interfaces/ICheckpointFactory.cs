using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Factories.Interfaces{

	public interface ICheckpointFactory{

        Checkpoint Create(RawCheckpoint rawCheckpoint);

	}
}