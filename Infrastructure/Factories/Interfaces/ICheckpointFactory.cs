using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories{

	public interface ICheckpointFactory{

        Checkpoint Create(RawCheckpoint rawCheckpoint);

	}
}