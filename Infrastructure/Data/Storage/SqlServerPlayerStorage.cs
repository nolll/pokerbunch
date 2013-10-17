using System.Collections.Generic;
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

		public RawPlayer GetPlayerById(int homegameId, int id)
        {
			var baseSql = GetPlayersBaseSql(homegameId);
			const string format = "{0} AND p.PlayerID = {1}";
            var sql = string.Format(format, baseSql, id);
			return GetPlayerFromSql(sql);
		}

		public RawPlayer GetPlayerByName(int homegameId, string name)
        {
            var baseSql = GetPlayersBaseSql(homegameId);
			const string format = "{0} AND (p.PlayerName = '{1}' OR u.DisplayName = '{1}')";
		    var sql = string.Format(format, baseSql, name);
			return GetPlayerFromSql(sql);
		}

		public RawPlayer GetPlayerByUserName(int homegameId, string userName)
        {
            var baseSql = GetPlayersBaseSql(homegameId);
			const string format = "{0} AND u.UserName = '{1}'";
            var sql = string.Format(format, baseSql, userName);
			return GetPlayerFromSql(sql);
		}

		public IList<RawPlayer> GetPlayers(int homegameId)
        {
            var baseSql = GetPlayersBaseSql(homegameId);
			const string format = "{0} ORDER BY DisplayName";
		    var sql = string.Format(format, baseSql);
			return GetPlayersFromSql(sql);
		}

		private string GetPlayersBaseSql(int homegameId)
		{
		    const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, COALESCE(p.PlayerName, u.DisplayName) AS DisplayName, u.UserName, u.Email FROM player p LEFT JOIN [user] u on p.UserID = u.UserID WHERE p.HomegameID = {0} ";
		    return string.Format(sql, homegameId);
		}

        public int AddPlayer(int homegameId, string playerName)
        {
            const string format = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) OUTPUT INSERTED.ID VALUES ({0}, {1}, 1, '{2}')";
            var sql = string.Format(format, homegameId, (int)Role.Player, playerName);
			return _storageProvider.ExecuteInsert(sql);
		}

        public int AddPlayerWithUser(int homegameId, int userId, int role)
        {
            const string format = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) OUTPUT INSERTED.ID VALUES ({0}, {1}, {2}, 1)";
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
			while(reader.Read())
            {
				players.Add(_rawPlayerFactory.Create(reader));
			}
			return players;
		}

	}

}