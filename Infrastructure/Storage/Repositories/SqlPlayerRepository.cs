using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
	public class SqlPlayerRepository : IPlayerRepository
    {
        private const string PlayerDataSql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, p.PlayerName FROM player p ";
        private const string PlayerIdSql = "SELECT p.PlayerID FROM player p ";

	    private readonly SqlServerStorageProvider _db;

	    public SqlPlayerRepository(SqlServerStorageProvider db)
	    {
	        _db = db;
	    }

	    public IList<int> Find(int bunchId)
	    {
            var sql = string.Concat(PlayerIdSql, "WHERE p.HomegameID = @homegameId");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunchId)
                };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("PlayerID");

	    }

	    public IList<int> Find(int bunchId, string name)
	    {
            var sql = string.Concat(PlayerIdSql, "LEFT JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = @homegameId AND (p.PlayerName = @playerName OR u.DisplayName = @playerName)");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunchId),
                    new SimpleSqlParameter("@playerName", name)
                };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("PlayerID");

	    }

	    public IList<int> Find(int bunchId, int userId)
	    {
            var sql = string.Concat(PlayerIdSql, "WHERE p.HomegameID = @homegameId AND p.UserID = @userId");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunchId),
                    new SimpleSqlParameter("@userId", userId)
                };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("PlayerID");
	    }

	    public IList<Player> Get(IList<int> ids)
	    {
            var sql = string.Concat(PlayerDataSql, "WHERE p.PlayerID IN (@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _db.Query(sql, parameter);
            var rawPlayers = reader.ReadList(CreateRawPlayer);
            return rawPlayers.Select(CreatePlayer).ToList();
        }

        public Player Get(int id)
        {
            var sql = string.Concat(PlayerDataSql, "WHERE p.PlayerID = @id");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = _db.Query(sql, parameters);
            var rawPlayer = reader.ReadOne(CreateRawPlayer);
            return rawPlayer != null ? CreatePlayer(rawPlayer) : null;
        }

        public int Add(Player player)
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
                return _db.ExecuteInsert(sql, parameters);
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
                return _db.ExecuteInsert(sql, parameters);
            }
        }

		public bool JoinHomegame(Player player, Bunch bunch, int userId)
        {
            const string sql = "UPDATE player SET HomegameID = @homegameId, PlayerName = NULL, UserID = @userId, RoleID = @role, Approved = 1 WHERE PlayerID = @playerId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", bunch.Id),
                    new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@role", (int)player.Role),
                    new SimpleSqlParameter("@playerId", player.Id)
                };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;

		}

		public bool Delete(int playerId)
        {
            const string sql = @"DELETE FROM player WHERE PlayerID = @playerId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@playerId", playerId)
                };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;
        }

	    private Player CreatePlayer(RawPlayer rawPlayer)
        {
            return new Player(
                rawPlayer.BunchId,
                rawPlayer.Id,
                rawPlayer.UserId,
                rawPlayer.DisplayName,
                (Role)rawPlayer.Role);
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