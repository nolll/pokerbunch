using System.Collections.Generic;
using Core.Entities;
using Infrastructure.SqlServer.Classes;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Interfaces;

namespace Infrastructure.SqlServer
{
    public class SqlServerPlayerStorage : IPlayerStorage
    {
        private readonly IStorageProvider _storageProvider;

	    public SqlServerPlayerStorage()
	    {
            _storageProvider = new SqlServerStorageProvider();
	    }

        public RawPlayer GetPlayerById(int id)
        {
            const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadOne(RawPlayerFactory.Create);
        }

        public IList<RawPlayer> GetPlayerList(IList<int> ids)
        {
            const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID IN (@ids)";
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _storageProvider.Query(sql, parameter);
            return reader.ReadList(RawPlayerFactory.Create);
        }

        public int? GetPlayerIdByName(int homegameId, string name)
        {
            const string sql = "SELECT p.PlayerID FROM player p LEFT JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND (p.PlayerName = @playerName OR u.DisplayName = @playerName)";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@playerName", name)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadInt("PlayerID");
        }

        public int? GetPlayerIdByUserName(int homegameId, string userName)
        {
            const string sql = "SELECT p.PlayerID FROM player p JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND u.UserName = @userName";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@userName", userName)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadInt("PlayerID");
        }

        public IList<int> GetPlayerIdList(int homegameId)
        {
            const string sql = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = @homegameId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadIntList("PlayerID");
        }

        public int AddPlayer(int homegameId, string playerName)
        {
            const string sql = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) VALUES (@homegameId, @role, 1, @playerName) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@role", (int)Role.Player),
                    new SimpleSqlParameter("@playerName", playerName)
                };
            return _storageProvider.ExecuteInsert(sql, parameters);
        }

        public int AddPlayerWithUser(int homegameId, int userId, int role)
        {
            const string sql = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) VALUES (@homegameId, @userId, @role, 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@role", role)
                };
            return _storageProvider.ExecuteInsert(sql, parameters);
        }

        public bool JoinHomegame(int playerId, int role, int homegameId, int userId)
        {
            const string sql = "UPDATE player SET HomegameID = @homegameId, PlayerName = NULL, UserID = @userId, RoleID = @role, Approved = 1 WHERE PlayerID = @playerId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@role", role),
                    new SimpleSqlParameter("@playerId", playerId)
                };
            var rowCount = _storageProvider.Execute(sql, parameters);
            return rowCount > 0;
        }

        public bool DeletePlayer(int playerId)
        {
            const string sql = @"DELETE FROM player WHERE PlayerID = @playerId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@playerId", playerId)
                };
            var rowCount = _storageProvider.Execute(sql, parameters);
            return rowCount > 0;
        }
 	}
}