using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services;
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
        private readonly IGlobalization _globalization;

        public SqlServerCashgameStorage(
            IStorageProvider storageProvider,
            IRawCashgameFactory rawCashgameFactory,
            IRawCheckpointFactory rawCheckpointFactory,
            IGlobalization globalization)
	    {
	        _storageProvider = storageProvider;
	        _rawCashgameFactory = rawCashgameFactory;
            _rawCheckpointFactory = rawCheckpointFactory;
            _globalization = globalization;
	    }

        public int AddGame(Homegame homegame, RawCashgameWithResults cashgame)
        {
            var sql = "INSERT INTO game (HomegameID, Location, Status, Date) VALUES ({0}, '{1}', {2}, '{3}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var timezoneAdjustedDate = TimeZoneInfo.ConvertTime(cashgame.Date, homegame.Timezone);
            var date = _globalization.FormatIsoDate(timezoneAdjustedDate);
		    sql = string.Format(sql, homegame.Id, cashgame.Location, cashgame.Status, date);
		    return _storageProvider.ExecuteInsert(sql);
		}

		public bool DeleteGame(int cashgameId)
        {
			var sql = "DELETE FROM game WHERE GameID = {0}";
		    sql = string.Format(sql, cashgameId);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

        private string GetGameSql()
        {
            return "SELECT g.GameID, g.Location, g.Status, g.Date, cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp FROM game g LEFT JOIN cashgamecheckpoint cp ON g.GameID = cp.GameID ";
        }

        private string GetGameIdSql(int homegameId)
        {
            var sql = "SELECT g.GameID FROM game g WHERE g.HomegameID = {0} ";
            sql = string.Format(sql, homegameId);
            return sql;
        }

        public RawCashgame GetGame(int cashgameId)
        {
            var sql = string.Concat(GetGameSql(), "WHERE g.GameID = {0}");
            sql = string.Format(sql, cashgameId);
            var reader = _storageProvider.Query(sql);
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
            var sql = string.Concat(GetGameIdSql(homegameId), "AND g.Status = {0}");
            sql = string.Format(sql, status, homegameId);
            var reader = _storageProvider.Query(sql);
            if (reader.Read())
            {
                return reader.GetIntValue("GameID");
            }
            return null;
        }

        public int? GetCashgameId(int homegameId, string dateStr)
        {
            var sql = GetGameIdSql(homegameId) + "AND g.Date = '{0}'";
            sql = string.Format(sql, dateStr);
            var reader = _storageProvider.Query(sql);
            if (reader.Read())
            {
                return reader.GetIntValue("GameID");
            }
            return null;
        }

        public IList<RawCashgameWithResults> GetGames(IEnumerable<int> ids)
        {
            var sql = GetGameSql();
            var idList = GetIdListForSql(ids);
            sql += string.Format("WHERE g.GameID IN ({0}) ", idList);
            sql += "ORDER BY g.GameID, cp.PlayerID, cp.Timestamp";
            var reader = _storageProvider.Query(sql);
            return GetGamesFromDbResult(reader);
        }

        private string GetIdListForSql(IEnumerable<int> ids)
        {
            return string.Join(", ", ids.Select(o => string.Format("{0}", o)).ToArray());
        }

        public IList<int> GetGameIds(int homegameId, int? status = null, int? year = null)
        {
            var sql = GetGameIdSql(homegameId);
            if (status.HasValue)
            {
                sql += string.Format("AND g.Status = {0} ", status.Value);
            }
            if (year.HasValue)
            {
                sql += string.Format("AND YEAR(g.Date) = {0} ", year.Value);
            }
            //sql += "ORDER BY g.GameID, cp.PlayerID, cp.Timestamp";
            var reader = _storageProvider.Query(sql);
            var ids = new List<int>();
            while (reader.Read())
            {
                ids.Add(reader.GetIntValue("GameID"));
            }
            return ids;
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
			var sql = "SELECT DISTINCT YEAR(ccp.Timestamp) as 'Year' FROM cashgamecheckpoint ccp LEFT JOIN game g ON ccp.GameID = g.GameID WHERE g.HomegameID = '{0}' ORDER BY 'Year' DESC";
		    sql = string.Format(sql, homegameId);
			var reader = _storageProvider.Query(sql);
			var years = new List<int>();
			while(reader.Read())
            {
				years.Add(reader.GetIntValue("Year"));
			}
			return years;
		}

        public bool UpdateGame(RawCashgameWithResults cashgame)
        {
            var sql = "UPDATE game SET Location = '{0}', Date = '{1}', Status = {2} WHERE GameID = {3}";
		    sql = string.Format(sql, cashgame.Location, cashgame.Date, cashgame.Status, cashgame.Id);
		    var rowCount = _storageProvider.Execute(sql);
		    return rowCount > 0;
		}

		public bool HasPlayed(int playerId)
        {
			var sql = "SELECT DISTINCT PlayerID FROM cashgamecheckpoint WHERE PlayerId = {0}";
		    sql = string.Format(sql, playerId);
		    var reader = _storageProvider.Query(sql);
			return reader.Read();
		}

		public IList<string> GetLocations(string slug)
        {
			var sql = "SELECT DISTINCT g.Location FROM game g LEFT JOIN homegame h ON g.HomegameID = h.HomegameID WHERE Name = '{0}' AND g.Location <> '' ORDER BY g.Location";
		    sql = string.Format(sql, slug);
			var reader = _storageProvider.Query(sql);
			var locations = new List<string>();
			while(reader.Read())
            {
				locations.Add(reader.GetStringValue("Location"));
			}
			return locations;
		}

		private RawCashgameResult RawCashgameResultFromDbRow(IStorageDataReader reader)
        {
			var playerId = reader.GetIntValue("PlayerID");
			return new RawCashgameResult(playerId);
		}

	}

}