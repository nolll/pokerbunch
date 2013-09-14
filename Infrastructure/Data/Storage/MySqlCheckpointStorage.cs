using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;

namespace Infrastructure.Data.Storage {
    public class MySqlCheckpointStorage : ICheckpointStorage
    {
	    private readonly IStorageProvider _storageProvider;

        public MySqlCheckpointStorage(IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

		public int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint){
			var timestampStr = Globalization.FormatIsoDateTime(DateTimeFactory.ToUtc(checkpoint.Timestamp));
			var sql = "INSERT INTO cashgamecheckpoint (GameID, PlayerID, Type, Amount, Stack, Timestamp) VALUES ({0}, {1}, {2}, '{3}', '{4}', '{5}')";
            sql = string.Format(sql, cashgame.Id, player.Id, (int)checkpoint.Type, checkpoint.Amount, checkpoint.Stack, timestampStr);
			return _storageProvider.ExecuteInsert(sql);
		}

		public bool UpdateCheckpoint(Checkpoint checkpoint){
			var sql = "UPDATE cashgamecheckpoint SET Amount = {0}, Stack = {1} WHERE CheckpointID = {2}";
		    sql = string.Format(sql, checkpoint.Amount, checkpoint.Stack, checkpoint.Id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool DeleteCheckpoint(int id){
			var sql = "DELETE FROM cashgamecheckpoint WHERE CheckpointID = {0}";
		    sql = string.Format(sql, id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

	}

}