using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;

namespace Infrastructure.Data.Storage {
    public class SqlServerCheckpointStorage : ICheckpointStorage
    {
	    private readonly IStorageProvider _storageProvider;
        private readonly IGlobalization _globalization;

        public SqlServerCheckpointStorage(
            IStorageProvider storageProvider,
            IGlobalization globalization)
        {
            _storageProvider = storageProvider;
            _globalization = globalization;
        }

        public int AddCheckpoint(int cashgameId, int playerId, RawCheckpoint checkpoint){
			var timestampStr = _globalization.FormatIsoDateTime(DateTimeFactory.ToUtc(checkpoint.Timestamp));
            var sql = "INSERT INTO cashgamecheckpoint (GameID, PlayerID, Type, Amount, Stack, Timestamp) OUTPUT INSERTED.ID VALUES ({0}, {1}, {2}, '{3}', '{4}', '{5}')";
            sql = string.Format(sql, cashgameId, playerId, checkpoint.Type, checkpoint.Amount, checkpoint.Stack, timestampStr);
			return _storageProvider.ExecuteInsert(sql);
		}

		public bool UpdateCheckpoint(RawCheckpoint checkpoint){
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