using System;
using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
    public class SqlServerCashgameStorage : ICashgameStorage
    {
	    private readonly IStorageProvider _storageProvider;
        private readonly IRawCashgameFactory _rawCashgameFactory;
        private readonly IRawCheckpointFactory _rawCheckpointFactory;

        public SqlServerCashgameStorage(
            IStorageProvider storageProvider,
            IRawCashgameFactory rawCashgameFactory,
            IRawCheckpointFactory rawCheckpointFactory)
	    {
	        _storageProvider = storageProvider;
	        _rawCashgameFactory = rawCashgameFactory;
            _rawCheckpointFactory = rawCheckpointFactory;
	    }

        public int AddGame(Homegame homegame, RawCashgameWithResults cashgame)
        {
            const string sql = "INSERT INTO game (HomegameID, Location, Status, Date) VALUES (@homegameId, @location, @status, @date) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var timezoneAdjustedDate = TimeZoneInfo.ConvertTime(cashgame.Date, homegame.Timezone);
		    var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegame.Id),
                    new SimpleSqlParameter("@location", cashgame.Location),
                    new SimpleSqlParameter("@status", cashgame.Status),
                    new SimpleSqlParameter("@date", timezoneAdjustedDate)
                };
            return _storageProvider.ExecuteInsert(sql, parameters);
		}

		public bool DeleteGame(int cashgameId)
        {
			const string sql = "DELETE FROM game WHERE GameID = @cashgameId";
		    var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
			var rowCount = _storageProvider.Execute(sql, parameters);
			return rowCount > 0;
		}

        public RawCashgame GetGame(int cashgameId)
        {
            const string sql = "SELECT g.GameID, g.Location, g.Status, g.Date, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM game g LEFT JOIN cashgamecheckpoint cp ON g.GameID = cp.GameID WHERE g.GameID = @cashgameId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
            var reader = _storageProvider.Query(sql, parameters);
            var cashgames = GetGamesFromDbResult(reader);
            if (cashgames.Count == 0)
            {
                return null;
            }
            return cashgames[0];
        }

        public int? GetRunningCashgameId(int homegameId)
        {
            const int status = (int)GameStatus.Running;
            const string sql = "SELECT g.GameID FROM game g WHERE g.HomegameID = @homegameId AND g.Status = @status";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@status", status)
		        };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadInt("GameID");
        }

        public int? GetCashgameId(int homegameId, string dateStr)
        {
            const string sql = "SELECT g.GameID FROM game g WHERE g.HomegameID = @homegameId AND g.Date = @date";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@date", dateStr)
		        };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadInt("GameID");
        }

        public IList<RawCashgameWithResults> GetGames(IList<int> idList)
        {
            const string sql = "SELECT g.GameID, g.Location, g.Status, g.Date, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM game g LEFT JOIN cashgamecheckpoint cp ON g.GameID = cp.GameID WHERE g.GameID IN (@idList) ORDER BY g.GameID, cp.PlayerID, cp.Timestamp";
            var parameter = new ListSqlParameter("@idList", idList);
            var reader = _storageProvider.Query(sql, parameter);
            return GetGamesFromDbResult(reader);
        }

        public IList<int> GetGameIds(int homegameId, int? status = null, int? year = null)
        {
            var sql = "SELECT g.GameID FROM game g WHERE g.HomegameID = @homegameId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@homegameId", homegameId),
		        };
            if (status.HasValue)
            {
                sql = string.Concat(sql, " AND g.Status = @status");
                parameters.Add(new SimpleSqlParameter("@status", status.Value));
            }
            if (year.HasValue)
            {
                sql = string.Concat(sql, " AND YEAR(g.Date) = @year");
                parameters.Add(new SimpleSqlParameter("@year", year.Value));
            }
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadIntList("GameID");
        }

		private List<RawCashgameWithResults> GetGamesFromDbResult(IStorageDataReader reader)
        {
            var cashgames = new List<RawCashgameWithResults>();
            RawCashgameWithResults currentGame = null;
			var currentGameId = -1;
			RawCashgameResult currentResult = null;
			var currentPlayerId = -1;
			while(reader.Read())
			{
			    var gameId = reader.GetIntValue("GameID");
				if(gameId != currentGameId)
				{
				    currentGame = _rawCashgameFactory.Create(reader);
					currentGameId = currentGame.Id;
					cashgames.Add(currentGame);
					currentResult = null;
					currentPlayerId = -1;
				}
			    var playerId = reader.GetIntValue("PlayerID");
				if(playerId != currentPlayerId){
                    if (playerId != 0) // this was a null-check in the php site
                    {
						currentResult = RawCashgameResultFromDbRow(reader);
						currentPlayerId = currentResult.PlayerId;
						currentGame.AddResult(currentResult);
					}
				}
			    var checkpointId = reader.GetIntValue("CheckpointID");
                if (checkpointId != 0) // this was a null-check in the php site
                {
					var checkpoint = _rawCheckpointFactory.Create(reader);
					currentResult.AddCheckpoint(checkpoint);
				}
			}
			return cashgames;
		}

		public IList<int> GetYears(int homegameId)
        {
			const string sql = "SELECT DISTINCT YEAR(ccp.Timestamp) as 'Year' FROM cashgamecheckpoint ccp LEFT JOIN game g ON ccp.GameID = g.GameID WHERE g.HomegameID = @homegameId ORDER BY 'Year' DESC";
		    var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@homegameId", homegameId)
		        };
			var reader = _storageProvider.Query(sql, parameters);
		    return reader.ReadIntList("Year");
		}

        public bool UpdateGame(RawCashgameWithResults cashgame)
        {
            const string sql = "UPDATE game SET Location = @location, Date = @date, Status = @status WHERE GameID = @cashgameId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@location", cashgame.Location),
                    new SimpleSqlParameter("@date", cashgame.Date),
                    new SimpleSqlParameter("@status", cashgame.Status),
                    new SimpleSqlParameter("@cashgameId", cashgame.Id)
		        };
		    var rowCount = _storageProvider.Execute(sql, parameters);
		    return rowCount > 0;
		}

		public bool HasPlayed(int playerId)
        {
			const string sql = "SELECT DISTINCT PlayerID FROM cashgamecheckpoint WHERE PlayerId = @playerId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@playerId", playerId)
		        };
            var reader = _storageProvider.Query(sql, parameters);
			return reader.HasRows();
		}

		public IList<string> GetLocations(string slug)
        {
			const string sql = "SELECT DISTINCT g.Location FROM game g LEFT JOIN homegame h ON g.HomegameID = h.HomegameID WHERE Name = @slug AND g.Location <> '' ORDER BY g.Location";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@slug", slug)
		        };
            var reader = _storageProvider.Query(sql, parameters);
		    return reader.ReadStringList("Location");
		}

		private RawCashgameResult RawCashgameResultFromDbRow(IStorageDataReader reader)
        {
			var playerId = reader.GetIntValue("PlayerID");
			return new RawCashgameResult(playerId);
		}

	}

}