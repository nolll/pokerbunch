using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.SqlDb
{
	public class SqlPlayerDb
    {
        private const string DataSql = "SELECT p.HomegameID, h.Name as Slug, p.PlayerID, p.UserID, p.RoleID, ISNULL(p.PlayerName, u.DisplayName) AS PlayerName, p.Color FROM player p LEFT JOIN [user] u ON u.UserID = p.UserID JOIN homegame h ON h.HomegameId = p.HomegameId ";
        private const string SearchSql = "SELECT p.PlayerID FROM player p ";

	    private readonly SqlServerStorageProvider _db;

	    public SqlPlayerDb(SqlServerStorageProvider db)
	    {
	        _db = db;
	    }

	    public IList<string> Find(string bunchId)
	    {
            var sql = string.Concat(SearchSql, "JOIN homegame h on h.HomegameID = p.HomegameId WHERE h.Name = @slug");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", bunchId)
                };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("PlayerID").Select(o => o.ToString()).ToList();
	    }

	    public IList<string> FindByUserId(string bunchId, string userId)
	    {
            var sql = string.Concat(SearchSql, "JOIN homegame h ON h.HomegameId = p.HomegameId WHERE h.Name = @slug AND p.UserID = @userId");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", bunchId),
                    new SimpleSqlParameter("@userId", userId)
                };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("PlayerID").Select(o => o.ToString()).ToList();
        }

	    public IList<Player> Get(IList<string> ids)
	    {
            if(!ids.Any())
                return new List<Player>();
            var sql = string.Concat(DataSql, "WHERE p.PlayerID IN (@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _db.Query(sql, parameter);
            var rawPlayers = reader.ReadList(CreateRawPlayer);
            return rawPlayers.Select(CreatePlayer).ToList();
        }

        public string Add(Player player)
        {
            if (player.IsUser)
            {
                const string sql = "INSERT INTO player (HomegameID, UserID, RoleID, Approved, Color) SELECT h.HomegameID, @userId, @role, 1, @color FROM Homegame h WHERE h.Name = @homegameId SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
                var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", player.BunchId),
                    new SimpleSqlParameter("@userId", player.UserId),
                    new SimpleSqlParameter("@role", player.Role),
                    new SimpleSqlParameter("@color", player.Color)
                };
                return _db.ExecuteInsert(sql, parameters);
            }
            else
            {
                const string sql = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName, Color) SELECT h.HomegameID, @role, 1, @playerName, @color FROM Homegame h WHERE h.Name = @homegameId SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
                var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@homegameId", player.BunchId),
                    new SimpleSqlParameter("@role", (int)Role.Player),
                    new SimpleSqlParameter("@playerName", player.DisplayName),
                    new SimpleSqlParameter("@color", player.Color)
                };
                return _db.ExecuteInsert(sql, parameters);
            }
        }

		public bool JoinHomegame(Player player, Bunch bunch, string userId)
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

		public void Delete(string playerId)
        {
            const string sql = @"DELETE FROM player WHERE PlayerID = @playerId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@playerId", playerId)
                };
            _db.Execute(sql, parameters);
        }

	    private Player CreatePlayer(RawPlayer rawPlayer)
        {
            return new Player(
                rawPlayer.BunchId,
                rawPlayer.Id,
                rawPlayer.UserId,
                rawPlayer.DisplayName,
                (Role)rawPlayer.Role,
                rawPlayer.Color);
        }

        private static RawPlayer CreateRawPlayer(IStorageDataReader reader)
        {
            return new RawPlayer(
                reader.GetStringValue("Slug"),
                reader.GetIntValue("PlayerID").ToString(),
                reader.GetIntValue("UserID").ToString(),
                reader.GetStringValue("PlayerName"),
                reader.GetIntValue("RoleID"),
                reader.GetStringValue("Color"));
        }
	}
}