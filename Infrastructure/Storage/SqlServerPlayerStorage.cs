using System.Collections.Generic;
using Core.Entities;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
    public class SqlServerPlayerStorage : SqlServerStorageProvider, IPlayerStorage
    {
        public RawPlayer GetPlayerById(int id)
        {
            const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateRawPlayer);
        }

        public IList<int> GetPlayerIdsByUserId(int bunchId, int userId)
        {
            const string sql = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = @homegameId AND p.UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunchId),
                    new SimpleSqlParameter("@userId", userId)
                };
            var reader = Query(sql, parameters);
            return reader.ReadIntList("PlayerID");
        }

        public IList<RawPlayer> GetPlayerList(IList<int> ids)
        {
            const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p WHERE p.PlayerID IN (@ids)";
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = Query(sql, parameter);
            return reader.ReadList(CreateRawPlayer);
        }

        public int? GetPlayerIdByUserId(int bunchId, int userId)
        {
            const string sql = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = @homegameId AND p.UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunchId),
                    new SimpleSqlParameter("@userId", userId)
                };
            var reader = Query(sql, parameters);
            return reader.ReadInt("PlayerID");
        }

        public IList<int> GetPlayerIdsByName(int homegameId, string name)
        {
            const string sql = "SELECT p.PlayerID FROM player p LEFT JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND (p.PlayerName = @playerName OR u.DisplayName = @playerName)";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId),
                    new SimpleSqlParameter("@playerName", name)
                };
            var reader = Query(sql, parameters);
            return reader.ReadIntList("PlayerID");
        }

        public IList<int> GetPlayerIdList(int homegameId)
        {
            const string sql = "SELECT p.PlayerID FROM player p WHERE p.HomegameID = @homegameId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", homegameId)
                };
            var reader = Query(sql, parameters);
            return reader.ReadIntList("PlayerID");
        }

        public int AddPlayer(RawPlayer player)
        {
            if (player.IsUser)
            {
                const string sql = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) VALUES (@homegameId, @userId, @role, 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
                var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", player.BunchId),
                    new SimpleSqlParameter("@userId", player.UserId),
                    new SimpleSqlParameter("@role", player.Role)
                };
                return ExecuteInsert(sql, parameters);
            }
            else
            {
                const string sql = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) VALUES (@homegameId, @role, 1, @playerName) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
                var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", player.BunchId),
                    new SimpleSqlParameter("@role", (int)Role.Player),
                    new SimpleSqlParameter("@playerName", player.DisplayName)
                };
                return ExecuteInsert(sql, parameters);
            }
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
            var rowCount = Execute(sql, parameters);
            return rowCount > 0;
        }

        public bool DeletePlayer(int playerId)
        {
            const string sql = @"DELETE FROM player WHERE PlayerID = @playerId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@playerId", playerId)
                };
            var rowCount = Execute(sql, parameters);
            return rowCount > 0;
        }

        private static RawPlayer CreateRawPlayer(IStorageDataReader reader)
        {
            return new RawPlayer(
                reader.GetIntValue("HomegameID"),
                reader.GetIntValue("PlayerID"),
                reader.GetIntValue("UserID"),
                reader.GetStringValue("PlayerName"),
                reader.GetIntValue("RoleID"));
        }
 	}
}