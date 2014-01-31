using System.Collections.Generic;
using Application.Services;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Data.Storage {
    public class SqlServerCheckpointStorage : ICheckpointStorage
    {
	    private readonly IStorageProvider _storageProvider;
        private readonly IGlobalization _globalization;
        private readonly ITimeProvider _timeProvider;
        private readonly IRawCheckpointFactory _rawCheckpointFactory;

        public SqlServerCheckpointStorage(
            IStorageProvider storageProvider,
            IGlobalization globalization,
            ITimeProvider timeProvider,
            IRawCheckpointFactory rawCheckpointFactory)
        {
            _storageProvider = storageProvider;
            _globalization = globalization;
            _timeProvider = timeProvider;
            _rawCheckpointFactory = rawCheckpointFactory;
        }

        public int AddCheckpoint(int cashgameId, int playerId, RawCheckpoint checkpoint){
			var timestampStr = _globalization.FormatIsoDateTime(_timeProvider.ConvertToUtc(checkpoint.Timestamp));
            var sql = "INSERT INTO cashgamecheckpoint (GameID, PlayerID, Type, Amount, Stack, Timestamp) VALUES ({0}, {1}, {2}, '{3}', '{4}', '{5}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
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

        public IList<RawCheckpoint> GetCheckpoints(int cashgameId)
        {
            const string format = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID = {0} ORDER BY cp.PlayerID, cp.Timestamp";
            var sql = string.Format(format, cashgameId);
            var reader = _storageProvider.Query(sql);
            return GetCheckpointsFromDbResult(reader);
        }

        private List<RawCheckpoint> GetCheckpointsFromDbResult(StorageDataReader reader)
        {
            var checkpoints = new List<RawCheckpoint>();
            while (reader.Read())
            {
                var checkpoint = _rawCheckpointFactory.Create(reader);
                checkpoints.Add(checkpoint);
            }
            return checkpoints;
        }

    }

}