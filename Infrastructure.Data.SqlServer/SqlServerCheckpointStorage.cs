using System.Collections.Generic;
using Core.Services.Interfaces;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
    public class SqlServerCheckpointStorage : ICheckpointStorage
    {
	    private readonly IStorageProvider _storageProvider;
        private readonly ITimeProvider _timeProvider;

        public SqlServerCheckpointStorage(
            IStorageProvider storageProvider,
            ITimeProvider timeProvider)
        {
            _storageProvider = storageProvider;
            _timeProvider = timeProvider;
        }

        public int AddCheckpoint(int cashgameId, int playerId, RawCheckpoint checkpoint)
        {
            const string sql = "INSERT INTO cashgamecheckpoint (GameID, PlayerID, Type, Amount, Stack, Timestamp) VALUES (@gameId, @playerId, @type, @amount, @stack, @timestamp) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@gameId", cashgameId),
                    new SimpleSqlParameter("@playerId", playerId),
                    new SimpleSqlParameter("@type", checkpoint.Type),
                    new SimpleSqlParameter("@amount", checkpoint.Amount),
                    new SimpleSqlParameter("@stack", checkpoint.Stack),
                    new SimpleSqlParameter("@timestamp", _timeProvider.ConvertToUtc(checkpoint.Timestamp))
                };
			return _storageProvider.ExecuteInsert(sql, parameters);
		}

		public bool UpdateCheckpoint(RawCheckpoint checkpoint)
        {
			const string sql = "UPDATE cashgamecheckpoint SET Timestamp = @timestamp, Amount = @amount, Stack = @stack WHERE CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@timestamp", checkpoint.Timestamp),
		            new SimpleSqlParameter("@amount", checkpoint.Amount),
		            new SimpleSqlParameter("@stack", checkpoint.Stack),
		            new SimpleSqlParameter("@checkpointId", checkpoint.Id)
		        };
			var rowCount = _storageProvider.Execute(sql, parameters);
			return rowCount > 0;
		}

		public bool DeleteCheckpoint(int id)
        {
            const string sql = "DELETE FROM cashgamecheckpoint WHERE CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@checkpointId", id)
		        };
			var rowCount = _storageProvider.Execute(sql, parameters);
			return rowCount > 0;
		}

        public IList<RawCheckpoint> GetCheckpoints(int cashgameId)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID = @cashgameId ORDER BY cp.PlayerID, cp.Timestamp";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadList(RawCheckpointFactory.Create);
        }

        public IList<RawCheckpoint> GetCheckpoints(IList<int> cashgameIdList)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID IN (@cashgameIdList) ORDER BY cp.PlayerID, cp.Timestamp";
            var parameter = new ListSqlParameter("@cashgameIdList", cashgameIdList);
            var reader = _storageProvider.Query(sql, parameter);
            return reader.ReadList(RawCheckpointFactory.Create);
        }

        public RawCheckpoint GetCheckpoint(int checkpointId)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@checkpointId", checkpointId)
		        };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadOne(RawCheckpointFactory.Create);
        }
    }
}