using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
	public class SqlCashgameRepository : ICashgameRepository
    {
	    private readonly SqlServerStorageProvider _db;

	    public SqlCashgameRepository(SqlServerStorageProvider db)
	    {
	        _db = db;
	    }

        public IList<Cashgame> GetFinished(int bunchId, int? year = null)
        {
            var sql = "SELECT g.GameID FROM game g WHERE g.HomegameID = @homegameId";
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@homegameId", bunchId),
            };
            const int status = (int) GameStatus.Finished;
            sql = string.Concat(sql, " AND g.Status = @status");
            parameters.Add(new SimpleSqlParameter("@status", status));
            if (year.HasValue)
            {
                sql = string.Concat(sql, " AND YEAR(g.Date) = @year");
                parameters.Add(new SimpleSqlParameter("@year", year.Value));
            }
            var reader = _db.Query(sql, parameters);
            var ids = reader.ReadIntList("GameID");
            return GetList(ids);
        }
        
	    public IList<Cashgame> GetByEvent(int eventId)
	    {
            var ids = GetIdsByEvent(eventId);
            return GetList(ids);
	    } 

        public Cashgame GetRunning(int bunchId)
        {
            var id = GetIdByRunning(bunchId);
            return id.HasValue ? GetById(id.Value) : null;
        }
        
        public Cashgame GetById(int cashgameId)
        {
            const string sql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID = @cashgameId ORDER BY g.GameId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
            var reader = _db.Query(sql, parameters);
            var rawGame = reader.ReadOne(CreateRawCashgame);
            var rawCheckpoints = GetCheckpoints(cashgameId);
            var checkpoints = CreateCheckpoints(rawCheckpoints);
            var cashgame = CreateCashgame(rawGame);
            cashgame.AddCheckpoints(checkpoints);
            return cashgame;
        }

        private RawCashgame CreateRawCashgame(IStorageDataReader reader)
        {
            var id = reader.GetIntValue("GameID");
            var bunchId = reader.GetIntValue("HomegameID");
            var location = ReadLocation(reader);
            var status = reader.GetIntValue("Status");
            var date = TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTimeValue("Date"));

            return new RawCashgame(id, bunchId, location, status, date);
        }

        private static string ReadLocation(IStorageDataReader reader)
        {
            var location = reader.GetStringValue("Location");
            return location == "" ? null : location;
        }

        private int? GetIdByRunning(int bunchId)
        {
            const int status = (int)GameStatus.Running;
            const string sql = "SELECT g.GameID FROM game g WHERE g.HomegameID = @homegameId AND g.Status = @status";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@homegameId", bunchId),
                    new SimpleSqlParameter("@status", status)
		        };
            var reader = _db.Query(sql, parameters);
            return reader.ReadInt("GameID");
        }

        public IList<int> GetYears(int bunchId)
        {
            const string sql = "SELECT DISTINCT YEAR(g.[Date]) as 'Year' FROM Game g WHERE g.HomegameID = @homegameId AND g.Status = @status ORDER BY 'Year' DESC";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@homegameId", bunchId),
                    new SimpleSqlParameter("@status", (int)GameStatus.Finished)
		        };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("Year");
        }

        private IList<Cashgame> GetList(IList<int> ids)
        {
            const string sql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID IN (@idList) ORDER BY g.GameID";
            var parameter = new ListSqlParameter("@idList", ids);
            var reader = _db.Query(sql, parameter);
            var rawCashgames = reader.ReadList(CreateRawCashgame);
            var rawCheckpoints = GetCheckpoints(ids);
            return CreateCashgameList(rawCashgames, rawCheckpoints);
        }
        
        private IList<int> GetIdsByEvent(int eventId)
        {
            const string sql = "SELECT g.GameID FROM game g WHERE g.GameID IN (SELECT ecg.GameID FROM eventcashgame ecg WHERE ecg.EventId = @eventId)";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@eventId", eventId),
		        };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("GameID");
        }
        
        public IList<string> GetLocations(int bunchId)
        {
            const string sql = "SELECT DISTINCT g.Location FROM game g WHERE g.HomegameID = @id AND g.Location <> '' ORDER BY g.Location";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@id", bunchId)
		        };
            var reader = _db.Query(sql, parameters);
            return reader.ReadStringList("Location");
        }

		public bool DeleteGame(Cashgame cashgame){
            const string sql = "DELETE FROM game WHERE GameID = @cashgameId";
		    var parameters = new List<SimpleSqlParameter>
		    {
		        new SimpleSqlParameter("@cashgameId", cashgame.Id)
		    };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;
		}
        
		public int AddGame(Bunch bunch, Cashgame cashgame)
		{
		    var rawCashgame = CreateRawCashgame(cashgame);
            const string sql = "INSERT INTO game (HomegameID, Location, Status, Date) VALUES (@homegameId, @location, @status, @date) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var timezoneAdjustedDate = TimeZoneInfo.ConvertTime(rawCashgame.Date, bunch.Timezone);
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunch.Id),
                    new SimpleSqlParameter("@location", rawCashgame.Location),
                    new SimpleSqlParameter("@status", rawCashgame.Status),
                    new SimpleSqlParameter("@date", timezoneAdjustedDate)
                };
            return _db.ExecuteInsert(sql, parameters);
		}
        
		public bool UpdateGame(Cashgame cashgame)
        {
            var rawCashgame = CreateRawCashgame(cashgame);
            return UpdateGame(rawCashgame);
		}

		public bool EndGame(Bunch bunch, Cashgame cashgame)
        {
            var rawCashgame = CreateRawCashgame(cashgame, GameStatus.Finished);
            return UpdateGame(rawCashgame);
		}

	    private bool UpdateGame(RawCashgame cashgame)
        {
            const string sql = "UPDATE game SET Location = @location, Date = @date, Status = @status WHERE GameID = @cashgameId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@location", cashgame.Location),
                    new SimpleSqlParameter("@date", cashgame.Date),
                    new SimpleSqlParameter("@status", cashgame.Status),
                    new SimpleSqlParameter("@cashgameId", cashgame.Id)
		        };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;
        }

		public bool HasPlayed(int playerId)
        {
            const string sql = "SELECT DISTINCT PlayerID FROM cashgamecheckpoint WHERE PlayerId = @playerId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@playerId", playerId)
		        };
            var reader = _db.Query(sql, parameters);
            return reader.HasRows();
        }

        private RawCashgame CreateRawCashgame(Cashgame cashgame, GameStatus? status = null)
	    {
	        var rawStatus = status.HasValue ? (int) status.Value : (int) cashgame.Status;
	        var date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.UtcNow;
            
            return new RawCashgame(cashgame.Id, cashgame.BunchId, cashgame.Location, rawStatus, date);
        }

	    private static Cashgame CreateCashgame(RawCashgame rawGame)
	    {
            return new Cashgame(rawGame.BunchId, rawGame.Location, (GameStatus)rawGame.Status, rawGame.Id);
        }

        private static IList<Checkpoint> CreateCheckpoints(IEnumerable<RawCheckpoint> checkpoints)
	    {
            return checkpoints.Select(RawCheckpoint.CreateReal).ToList();
	    } 

	    private static IList<Cashgame> CreateCashgameList(IEnumerable<RawCashgame> rawGames, IEnumerable<RawCheckpoint> rawCheckpoints)
        {
            var checkpointMap = GetGameCheckpointMap(rawCheckpoints);

            var cashgames = new List<Cashgame>();
            foreach (var rawGame in rawGames)
            {
                IList<RawCheckpoint> gameCheckpoints;
                if (!checkpointMap.TryGetValue(rawGame.Id, out gameCheckpoints))
                {
                    continue;
                }
                var checkpoints = CreateCheckpoints(gameCheckpoints);
                var cashgame = CreateCashgame(rawGame);
                cashgame.AddCheckpoints(checkpoints);
                cashgames.Add(cashgame);
            }
            return cashgames;
        }

        private static IDictionary<int, IList<RawCheckpoint>> GetGameCheckpointMap(IEnumerable<RawCheckpoint> checkpoints)
        {
            var checkpointMap = new Dictionary<int, IList<RawCheckpoint>>();
            foreach (var checkpoint in checkpoints)
            {
                IList<RawCheckpoint> checkpointList;
                if (!checkpointMap.TryGetValue(checkpoint.CashgameId, out checkpointList))
                {
                    checkpointList = new List<RawCheckpoint>();
                    checkpointMap.Add(checkpoint.CashgameId, checkpointList);
                }
                checkpointList.Add(checkpoint);
            }
            return checkpointMap;
        }

        public int AddCheckpoint(Checkpoint checkpoint)
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
            return _db.ExecuteInsert(sql, parameters);
        }

        public bool UpdateCheckpoint(Checkpoint checkpoint)
        {
            const string sql = "UPDATE cashgamecheckpoint SET Timestamp = @timestamp, Amount = @amount, Stack = @stack WHERE CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@timestamp", checkpoint.Timestamp),
		            new SimpleSqlParameter("@amount", checkpoint.Amount),
		            new SimpleSqlParameter("@stack", checkpoint.Stack),
		            new SimpleSqlParameter("@checkpointId", checkpoint.Id)
		        };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;
        }

	    public bool DeleteCheckpoint(Checkpoint checkpoint)
        {
            const string sql = "DELETE FROM cashgamecheckpoint WHERE CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@checkpointId", checkpoint.Id)
		        };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.CheckpointID = @checkpointId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@checkpointId", checkpointId)
		        };
            var reader = _db.Query(sql, parameters);
            var rawCheckpoint = reader.ReadOne(CreateRawCheckpoint);
            return rawCheckpoint != null ? RawCheckpoint.CreateReal(rawCheckpoint) : null;
        }

        public IList<int> FindCheckpoints(int cashgameId)
        {
            var rawCheckpoints = GetCheckpoints(cashgameId);
            return rawCheckpoints.Select(o => o.Id).ToList();
        }

        public IList<int> FindCheckpoints(IList<int> cashgameIds)
        {
            var rawCheckpoints = GetCheckpoints(cashgameIds);
            return rawCheckpoints.Select(o => o.Id).ToList();
        }

	    private IList<RawCheckpoint> GetCheckpoints(int cashgameId)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID = @cashgameId ORDER BY cp.PlayerID, cp.Timestamp";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
            var reader = _db.Query(sql, parameters);
            return reader.ReadList(CreateRawCheckpoint);
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

        public IList<RawCheckpoint> GetCheckpoints(IList<int> cashgameIdList)
        {
            const string sql = "SELECT cp.GameID, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM cashgamecheckpoint cp WHERE cp.GameID IN (@cashgameIdList) ORDER BY cp.PlayerID, cp.Timestamp";
            var parameter = new ListSqlParameter("@cashgameIdList", cashgameIdList);
            var reader = _db.Query(sql, parameter);
            return reader.ReadList(CreateRawCheckpoint);
        }
	}
}