using System.Collections.Generic;
using System.Data.SqlClient;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
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
            const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID = @id";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@id", id)
                };

            return GetPlayerFromSql(sql, parameters);
        }

        public IList<RawPlayer> GetPlayers(IList<int> ids)
        {
            const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID IN (@ids)";
            var parameter = new SqlListParameter("@ids", ids);
            return GetPlayersFromSql(sql, parameter);
        }

        public int? GetPlayerIdByName(int homegameId, string name)
        {
            const string sql = "SELECT p.PlayerID FROM player p LEFT JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND (p.PlayerName = @playerName OR u.DisplayName = @playerName)";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId),
                    new SqlParameter("@playerName", name)
                };
            return GetPlayerId(sql, parameters);
        }

        public int? GetPlayerIdByUserName(int homegameId, string userName)
        {
            const string sql = "SELECT p.PlayerID FROM player p JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND u.UserName = @userName";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId),
                    new SqlParameter("@userName", userName)
                };
            return GetPlayerId(sql, parameters);
        }

        public IList<int> GetPlayerIds(int homegameId)
        {
            const string sql = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = @homegameId";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId)
                };
            return GetPlayerIdList(sql, parameters);
        }

        public int AddPlayer(int homegameId, string playerName)
        {
            const string sql = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) VALUES (@homegameId, @role, 1, @playerName) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId),
                    new SqlParameter("@role", (int)Role.Player),
                    new SqlParameter("@playerName", playerName)
                };
            return _storageProvider.ExecuteInsert(sql, parameters);
        }

        public int AddPlayerWithUser(int homegameId, int userId, int role)
        {
            const string sql = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) VALUES (@homegameId, @userId, @role, 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId),
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@role", role)
                };
            return _storageProvider.ExecuteInsert(sql, parameters);
        }

        public bool JoinHomegame(int playerId, int role, int homegameId, int userId)
        {
            const string sql = "UPDATE player SET HomegameID = @homegameId, PlayerName = NULL, UserID = @userId, RoleID = @role, Approved = 1 WHERE PlayerID = @playerId";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@homegameId", homegameId),
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@role", role),
                    new SqlParameter("@playerId", playerId)
                };
            var rowCount = _storageProvider.Execute(sql, parameters);
            return rowCount > 0;
        }

        public bool DeletePlayer(int playerId)
        {
            const string sql = @"DELETE FROM player WHERE PlayerID = @playerId";
            var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@playerId", playerId)
                };
            var rowCount = _storageProvider.Execute(sql, parameters);
            return rowCount > 0;
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

        private IList<int> GetPlayerIdList(string sql, IList<SqlParameter> parameters)
        {
            return GetIntList(sql, parameters, "PlayerID");
        }

        private IList<int> GetIntList(string sql, IList<SqlParameter> parameters, string columnName)
        {
            var reader = _storageProvider.Query(sql, parameters);
            var ids = new List<int>();
            while (reader.Read())
            {
                ids.Add(reader.GetInt(columnName));
            }
            return ids;
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

        private List<RawPlayer> GetPlayersFromSql(string sql, SqlListParameter parameter)
        {
            var reader = _storageProvider.Query(sql, parameter);
            var players = new List<RawPlayer>();
            while (reader.Read())
            {
                players.Add(_rawPlayerFactory.Create(reader));
            }
            return players;
        }
 	}
}