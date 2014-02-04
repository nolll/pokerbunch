using System.Collections.Generic;
using System.Data.SqlClient;
using Application.Services;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
    public class SqlServerCheckpointStorage : ICheckpointStorage
    {
	    private readonly IStorageProvider _storageProvider;
        private readonly ITimeProvider _timeProvider;
        private readonly IRawCheckpointFactory _rawCheckpointFactory;

        public SqlServerCheckpointStorage(
            IStorageProvider storageProvider,
            ITimeProvider timeProvider,
            IRawCheckpointFactory rawCheckpointFactory)
        {
            _storageProvider = storageProvider;
            _timeProvider = timeProvider;
            _rawCheckpointFactory = rawCheckpointFactory;
        }

        public int AddCheckpoint(int cashgameId, int playerId, RawCheckpoint checkpoint)
        {
            const string sql = "INSERT INTO cashgamecheckpoint (GameID, PlayerID, Type, Amount, Stack, Timestamp) VALUES (@gameId, @playerId, @type, @amount, @stack, timestamp) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
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
			const string sql = "UPDATE cashgamecheckpoint SET Amount = @amount, Stack = @stack WHERE CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
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
            return GetRawCheckpoints(reader);
        }

        private List<RawCheckpoint> GetRawCheckpoints(IStorageDataReader reader)
        {
            var checkpoints = new List<RawCheckpoint>();
            while (reader.Read())
            {
                checkpoints.Add(_rawCheckpointFactory.Create(reader));
            }
            return checkpoints;
        }
    }
}