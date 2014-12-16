using System;
using System.Collections.Generic;
using Core.Entities;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
    public class SqlServerCashgameStorage : SqlServerStorageProvider, ICashgameStorage
    {
        public int AddGame(Bunch bunch, RawCashgame cashgame)
        {
            const string sql = "INSERT INTO game (HomegameID, Location, Status, Date) VALUES (@homegameId, @location, @status, @date) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var timezoneAdjustedDate = TimeZoneInfo.ConvertTime(cashgame.Date, bunch.Timezone);
		    var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunch.Id),
                    new SimpleSqlParameter("@location", cashgame.Location),
                    new SimpleSqlParameter("@status", cashgame.Status),
                    new SimpleSqlParameter("@date", timezoneAdjustedDate)
                };
            return ExecuteInsert(sql, parameters);
		}

		public bool DeleteGame(int cashgameId)
        {
			const string sql = "DELETE FROM game WHERE GameID = @cashgameId";
		    var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
			var rowCount = Execute(sql, parameters);
			return rowCount > 0;
		}

        public RawCashgame GetGame(int cashgameId)
        {
            const string sql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID = @cashgameId ORDER BY g.GameId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@cashgameId", cashgameId)
		        };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateRawCashgame);
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
            var reader = Query(sql, parameters);
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
            var reader = Query(sql, parameters);
            return reader.ReadInt("GameID");
        }

        public IList<RawCashgame> GetGames(IList<int> idList)
        {
            const string sql = "SELECT g.GameID, g.HomegameID, g.Location, g.Status, g.Date FROM game g WHERE g.GameID IN (@idList) ORDER BY g.GameID";
            var parameter = new ListSqlParameter("@idList", idList);
            var reader = Query(sql, parameter);
            return reader.ReadList(CreateRawCashgame);
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
            var reader = Query(sql, parameters);
            return reader.ReadIntList("GameID");
        }

        public IList<int> GetGameIdsByEvent(int eventId)
        {
            const string sql = "SELECT g.GameID FROM game g WHERE g.GameID IN (SELECT ecg.GameID FROM eventcashgame ecg WHERE ecg.EventId = @eventId)";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@eventId", eventId),
		        };
            var reader = Query(sql, parameters);
            return reader.ReadIntList("GameID");
        }

        public IList<int> GetYears(int homegameId)
        {
			const string sql = "SELECT DISTINCT YEAR(ccp.Timestamp) as 'Year' FROM cashgamecheckpoint ccp LEFT JOIN game g ON ccp.GameID = g.GameID WHERE g.HomegameID = @homegameId ORDER BY 'Year' DESC";
		    var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@homegameId", homegameId)
		        };
			var reader = Query(sql, parameters);
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
		    var rowCount = Execute(sql, parameters);
		    return rowCount > 0;
		}

		public bool HasPlayed(int playerId)
        {
			const string sql = "SELECT DISTINCT PlayerID FROM cashgamecheckpoint WHERE PlayerId = @playerId";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@playerId", playerId)
		        };
            var reader = Query(sql, parameters);
			return reader.HasRows();
		}

		public IList<string> GetLocations(int bunchId)
        {
			const string sql = "SELECT DISTINCT g.Location FROM game g WHERE g.HomegameID = @id AND g.Location <> '' ORDER BY g.Location";
            var parameters = new List<SimpleSqlParameter>
		        {
                    new SimpleSqlParameter("@id", bunchId)
		        };
            var reader = Query(sql, parameters);
		    return reader.ReadStringList("Location");
		}

        public RawCashgame CreateRawCashgame(IStorageDataReader reader)
        {
            var location = reader.GetStringValue("Location");
            if (location == "")
            {
                location = null;
            }

            return new RawCashgame
            {
                Id = reader.GetIntValue("GameID"),
                BunchId = reader.GetIntValue("HomegameID"),
                Location = location,
                Status = reader.GetIntValue("Status"),
                Date = TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTimeValue("Date")),
            };
        }
	}
}