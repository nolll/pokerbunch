using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
    public class SqlServerPlayerStorage : IPlayerStorage
    {
        private const string GetPlayerStatement = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p";

        private readonly IStorageProvider _storageProvider;
	    private readonly IRawPlayerFactory _rawPlayerFactory;

	    public SqlServerPlayerStorage(IStorageProvider storageProvider, IRawPlayerFactory rawPlayerFactory)
	    {
	        _storageProvider = storageProvider;
	        _rawPlayerFactory = rawPlayerFactory;
	    }

        public RawPlayer GetPlayerById(int id)
        {
            const string statement = "{0} WHERE p.PlayerID = @id";
            var sql = string.Format(statement, GetPlayerStatement);
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@id", id)
                };

            return GetPlayerFromSql(sql, parameters);
        }

        public IList<RawPlayer> GetPlayers(IList<int> ids)
        {
            const string statement = "{0} WHERE p.PlayerID IN ({1})";
            var parameterNames = GetIdParameterNames(ids);
            var parameters = GetIdParameters(ids);
            var sql = string.Format(statement, GetPlayerStatement, parameterNames);
            return GetPlayersFromSql(sql, parameters);
        }

        public int? GetPlayerIdByName(int homegameId, string name)
        {
            const string statement = "SELECT p.PlayerID FROM player p LEFT JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND (p.PlayerName = @playerName OR u.DisplayName = @playerName)";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId),
                    new SqlParameter("@playerName", name)
                };
            return GetPlayerId(statement, parameters);
        }

        public int? GetPlayerIdByUserName(int homegameId, string userName)
        {
            const string statement = "SELECT p.PlayerID FROM player p JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND u.UserName = @userName";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId),
                    new SqlParameter("@userName", userName)
                };
            return GetPlayerId(statement, parameters);
        }

        public IList<int> GetPlayerIds(int homegameId)
        {
            const string statement = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = {0}";
            var sql = string.Format(statement, homegameId);
            return GetPlayerIdsFromSql(sql);
        }

        public int AddPlayer(int homegameId, string playerName)
        {
            const string statement = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) VALUES ({0}, {1}, 1, '{2}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var sql = string.Format(statement, homegameId, (int)Role.Player, playerName);
            return _storageProvider.ExecuteInsert(sql);
        }

        public int AddPlayerWithUser(int homegameId, int userId, int role)
        {
            const string statement = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) VALUES ({0}, {1}, {2}, 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var sql = string.Format(statement, homegameId, userId, role);
            return _storageProvider.ExecuteInsert(sql);
        }

        public bool JoinHomegame(int playerId, int role, int homegameId, int userId)
        {
            const string statement = "UPDATE player SET HomegameID = {0}, PlayerName = NULL, UserID = {1}, RoleID = {2}, Approved = 1 WHERE PlayerID = {3}";
            var sql = string.Format(statement, homegameId, userId, role, playerId);
            var rowCount = _storageProvider.Execute(sql);
            return rowCount > 0;
        }

        public bool DeletePlayer(int playerId)
        {
            const string statement = "DELETE FROM player WHERE PlayerID = {0}";
            var sql = string.Format(statement, playerId);
            var rowCount = _storageProvider.Execute(sql);
            return rowCount > 0;
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

        private int? GetPlayerId(string sql, IList<SqlParameter> parameters)
        {
            return GetInt(sql, parameters, "PlayerID");
        }

        private int? GetInt(string sql, IList<SqlParameter> parameters, string columnName)
        {
            var reader = _storageProvider.Query(sql, parameters);
            while (reader.Read())
            {
                return reader.GetInt(columnName);
            }
            return null;
        }

        private string GetIdParameterNames(IList<int> ids)
        {
            var list = new List<string>();
            for (var i = 0; i < ids.Count; i++)
            {
                list.Add(GetIdParameterName(i));
            }
            return string.Join(",", list);
        }

        private IList<SqlParameter> GetIdParameters(IList<int> ids)
        {
            return ids.Select((t, i) => new SqlParameter(GetIdParameterName(i), t)).ToList();
        }

        private string GetIdParameterName(int index)
        {
            return string.Format("@id{0}", index);
        }

        private RawPlayer GetPlayerFromSql(string sql, IList<SqlParameter> parameters)
        {
            var reader = _storageProvider.Query(sql, parameters);
            while (reader.Read())
            {
                return _rawPlayerFactory.Create(reader);
            }
            return null;
        }

        private List<RawPlayer> GetPlayersFromSql(string sql, IList<SqlParameter> parameters)
        {
            var reader = _storageProvider.Query(sql, parameters);
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