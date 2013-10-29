using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Data.Storage
{
    public class SqlServerPlayerStorage : IPlayerStorage
    {
	    private readonly IStorageProvider _storageProvider;
	    private readonly IRawPlayerFactory _rawPlayerFactory;

	    public SqlServerPlayerStorage(IStorageProvider storageProvider, IRawPlayerFactory rawPlayerFactory)
	    {
	        _storageProvider = storageProvider;
	        _rawPlayerFactory = rawPlayerFactory;
	    }

		public RawPlayer GetPlayerById(int id)
        {
			var baseSql = GetPlayersBaseSql();
			const string format = "{0} AND p.PlayerID = {1}";
            var sql = string.Format(format, baseSql, id);
			return GetPlayerFromSql(sql);
		}

        public int? GetPlayerIdByName(int homegameId, string name)
        {
            const string format = "SELECT p.PlayerID FROM player p JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = {0} AND (p.PlayerName = '{1}' OR u.DisplayName = '{1}')";
            var sql = string.Format(format, homegameId, name);
            return GetPlayerId(sql);
        }

        public int? GetPlayerIdByUserName(int homegameId, string userName)
        {
            const string format = "SELECT p.PlayerID FROM player p JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = {0} AND u.UserName = '{1}'";
            var sql = string.Format(format, homegameId, userName);
            return GetPlayerId(sql);
        }

        private int? GetPlayerId(string sql)
        {
            var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return reader.GetInt("PlayerID");
            }
            return null;
        }

        public IList<RawPlayer> GetPlayers(IEnumerable<int> ids)
        {
            var sql = GetPlayersBaseSql(ids);
            return GetPlayersFromSql(sql);
        }

        public IList<int> GetPlayerIds(int homegameId)
        {
            const string baseSql = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = {0}";
            var sql = string.Format(baseSql, homegameId);
            return GetPlayerIdsFromSql(sql);
        }

        private string GetPlayersBaseSql()
        {
            return "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, COALESCE(p.PlayerName, u.DisplayName) AS DisplayName, u.UserName, u.Email FROM player p LEFT JOIN [user] u on p.UserID = u.UserID ";
        }

        private string GetPlayerIdBaseSql(int homegameId)
        {
            return string.Format("SELECT p.PlayerID FROM player p WHERE p.HomegameID = {0} ", homegameId);
        }

        private string GetPlayersBaseSql(int homegameId)
        {
            var sql = string.Concat(GetPlayersBaseSql(), "WHERE p.HomegameID = {0} ");
            return string.Format(sql, homegameId);
        }

        private string GetPlayersBaseSql(IEnumerable<int> ids)
        {
            var idList = GetIdListForSql(ids);
            return string.Concat(GetPlayersBaseSql(), string.Format("WHERE p.PlayerID IN({0})", idList));
        }

        private string GetIdListForSql(IEnumerable<int> ids)
        {
            return string.Join(", ", ids.Select(o => string.Format("{0}", o)).ToArray());
        }

        public int AddPlayer(int homegameId, string playerName)
        {
            const string format = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) VALUES ({0}, {1}, 1, '{2}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var sql = string.Format(format, homegameId, (int)Role.Player, playerName);
			return _storageProvider.ExecuteInsert(sql);
		}

        public int AddPlayerWithUser(int homegameId, int userId, int role)
        {
            const string format = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) VALUES ({0}, {1}, {2}, 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var sql = string.Format(format, homegameId, userId, role);
            return _storageProvider.ExecuteInsert(sql);
		}

        public bool JoinHomegame(int playerId, int role, int homegameId, int userId)
        {
			const string format = "UPDATE player SET HomegameID = {0}, PlayerName = NULL, UserID = {1}, RoleID = {2}, Approved = 1 WHERE PlayerID = {3}";
			var sql = string.Format(format, homegameId, userId, role, playerId);
            var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool DeletePlayer(int playerId)
        {
			const string format = "DELETE FROM player WHERE PlayerID = {0}";
		    var sql = string.Format(format, playerId);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		private RawPlayer GetPlayerFromSql(string sql)
        {
            var reader = _storageProvider.Query(sql);
			while(reader.Read())
            {
				return _rawPlayerFactory.Create(reader);
			}
			return null;
		}

        private List<RawPlayer> GetPlayersFromSql(string sql)
        {
            var reader = _storageProvider.Query(sql);
            var players = new List<RawPlayer>();
            while (reader.Read())
            {
                players.Add(_rawPlayerFactory.Create(reader));
            }
            return players;
        }

        private List<int> GetPlayerIdsFromSql(string sql)
        {
            var reader = _storageProvider.Query(sql);
            var ids = new List<int>();
            while (reader.Read())
            {
                ids.Add(reader.GetInt("PlayerID"));
            }
            return ids;
        }

	}

}