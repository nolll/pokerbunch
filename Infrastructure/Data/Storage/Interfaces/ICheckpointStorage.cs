using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ICheckpointStorage{

		int AddCheckpoint(int cashgameId, int playerId, RawCheckpoint checkpoint);
        bool UpdateCheckpoint(RawCheckpoint checkpoint);
        bool DeleteCheckpoint(int id);
		
	}

}