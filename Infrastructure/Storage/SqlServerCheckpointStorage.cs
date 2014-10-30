using System;
using System.Collections.Generic;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
    public class SqlServerCheckpointStorage : SqlServerStorageProvider, ICheckpointStorage
    {
        public int AddCheckpoint(RawCheckpoint checkpoint)
        {
            const string sql = "INSERT INTO cashgamecheckpoint (GameID, PlayerID, Type, Amount, Stack, Timestamp) VALUES (@gameId, @playerId, @type, @amount, @stack, @timestamp) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@gameId", checkpoint.CashgameId),
                    new SimpleSqlParameter("@playerId", checkpoint.PlayerId),
                    new SimpleSqlParameter("@type", checkpoint.Type),
                    new SimpleSqlParameter("@amount", checkpoint.Amount),
                    new SimpleSqlParameter("@stack", checkpoint.Stack),
                    new SimpleSqlParameter("@timestamp", checkpoint.Timestamp.ToUniversalTime())
                };
			return ExecuteInsert(sql, parameters);
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
			var rowCount = Execute(sql, parameters);
			return rowCount > 0;
		}

		public bool DeleteCheckpoint(int id)
        {
            const string sql = "DELETE FROM cashgamecheckpoint WHERE CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@checkpointId", id)
		        };
			var rowCount = Execute(sql, parameters);
			return rowCount > 0;
		}

        public IList<RawCheckpoint> GetCheckpoints(int cashgameId)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID = @cashgameId ORDER BY cp.PlayerID, cp.Timestamp";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
            var reader = Query(sql, parameters);
            return reader.ReadList(CreateRawCheckpoint);
        }

        public IList<RawCheckpoint> GetCheckpoints(IList<int> cashgameIdList)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID IN (@cashgameIdList) ORDER BY cp.PlayerID, cp.Timestamp";
            var parameter = new ListSqlParameter("@cashgameIdList", cashgameIdList);
            var reader = Query(sql, parameter);
            return reader.ReadList(CreateRawCheckpoint);
        }

        public RawCheckpoint GetCheckpoint(int checkpointId)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@checkpointId", checkpointId)
		        };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateRawCheckpoint);
        }

        private static RawCheckpoint CreateRawCheckpoint(IStorageDataReader reader)
        {
            return new RawCheckpoint(
                reader.GetIntValue("GameID"),
                reader.GetIntValue("PlayerID"),
                reader.GetIntValue("Amount"),
                reader.GetIntValue("Stack"),
                TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTimeValue("TimeStamp")),
                reader.GetIntValue("CheckpointID"),
                reader.GetIntValue("Type"));
        }
    }
}