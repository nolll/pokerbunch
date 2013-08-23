using Core.Classes;
using Core.Classes.Checkpoints;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ICheckpointStorage{

		int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint);
        bool UpdateCheckpoint(Checkpoint checkpoint);
        bool DeleteCheckpoint(int id);
		
	}

}