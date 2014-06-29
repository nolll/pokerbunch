using System;
using System.Collections.Generic;
using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
    public class SqlServerCashgameStorage : ICashgameStorage
    {
	    private readonly IStorageProvider _storageProvider;
        private readonly IRawCashgameFactory _rawCashgameFactory;

        public SqlServerCashgameStorage(
            IStorageProvider storageProvider,
            IRawCashgameFactory rawCashgameFactory)
	    {
	        _storageProvider = storageProvider;
	        _rawCashgameFactory = rawCashgameFactory;
	    }

        public int AddGame(Homegame homegame, RawCashgame cashgame)
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
            const string sql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID = @cashgameId ORDER BY g.GameId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadOne(_rawCashgameFactory.Create);
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

        public IList<RawCashgame> GetGames(IList<int> idList)
        {
            const string sql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID IN (@idList) ORDER BY g.GameID";
            var parameter = new ListSqlParameter("@idList", idList);
            var reader = _storageProvider.Query(sql, parameter);
            return reader.ReadList(_rawCashgameFactory.Create);
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

        public bool UpdateGame(RawCashgame cashgame)
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
	}
}