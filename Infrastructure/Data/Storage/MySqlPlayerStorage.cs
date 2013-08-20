using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;

namespace Infrastructure.Data.Storage {
    public class MySqlPlayerStorage : IPlayerStorage
    {
	    private readonly IStorageProvider _storageProvider;
	    private readonly IPlayerFactory _playerFactory;

	    public MySqlPlayerStorage(IStorageProvider storageProvider, IPlayerFactory playerFactory)
	    {
	        _storageProvider = storageProvider;
	        _playerFactory = playerFactory;
	    }

		public Player GetPlayerById(Homegame homegame, int id){
			var sql = GetPlayersBaseSql(homegame);
			sql += "AND p.PlayerID = {0}";
		    sql = string.Format(sql, id);
			return GetPlayerFromSql(sql);
		}

		public Player GetPlayerByName(Homegame homegame, string name){
			var sql = GetPlayersBaseSql(homegame);
			sql += "AND (p.PlayerName = '{0}' OR u.DisplayName = '{0}')";
		    sql = string.Format(sql, name);
			return GetPlayerFromSql(sql);
		}

		public Player GetPlayerByUserName(Homegame homegame, string userName){
			var sql = GetPlayersBaseSql(homegame);
			sql += "AND u.UserName = '{0}'";
		    sql = string.Format(sql, userName);
			return GetPlayerFromSql(sql);
		}

		public List<Player> GetPlayers(Homegame homegame){
			var sql = GetPlayersBaseSql(homegame);
			sql += "ORDER BY DisplayName";
			return GetPlayersFromSql(sql);
		}

		private string GetPlayersBaseSql(Homegame homegame)
		{
		    const string sql = "SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, COALESCE(p.PlayerName, u.DisplayName) AS DisplayName, u.UserName, u.Email FROM player p LEFT JOIN user u on p.UserID = u.UserID WHERE p.HomegameID = {0} ";
		    return string.Format(sql, homegame.Id);
		}

        public int AddPlayer(Homegame homegame, string playerName){
			var sql = "INSERT INTO player (HomegameID, RoleID, Approved, PlayerName) VALUES ({0}, {1}, 1, '{2}')";
		    sql = string.Format(sql, homegame.Id, (int) Role.Player, playerName);
			return _storageProvider.ExecuteInsert(sql);
		}

		public int AddPlayerWithUser(Homegame homegame, User user, int role){
			var sql = "INSERT INTO player (HomegameID, UserID, RoleID, Approved) VALUES ({0}, {1}, {2}, 1)";
			sql = string.Format(sql, homegame.Id, user.Id, role);
            return _storageProvider.ExecuteInsert(sql);
		}

		public bool JoinHomegame(Player player, Homegame homegame, User user){
			var sql = "UPDATE player SET HomegameID = {0}, PlayerName = NULL, UserID = {1}, RoleID = {2}, Approved = 1 WHERE PlayerID = {3}";
			sql = string.Format(sql, homegame.Id, user.Id, (int)player.Role, player.Id);
            var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool DeletePlayer(Player player){
			var sql = "DELETE FROM player WHERE PlayerID = {0}";
		    sql = string.Format(sql, player.Id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		private Player GetPlayerFromSql(string sql){
            var reader = _storageProvider.Query(sql);
			while(reader.Read()){
				return PlayerFromDbRow(reader);
			}
			return null;
		}

		private List<Player> GetPlayersFromSql(string sql){
            var reader = _storageProvider.Query(sql);
		    var players = new List<Player>();
			while(reader.Read()){
				players.Add(PlayerFromDbRow(reader));
			}
			return players;
		}

		private Player PlayerFromDbRow(StorageDataReader reader)
		{
		    var displayName = reader.GetString("DisplayName");
            var role = reader.GetInt("RoleID");
            var userName = reader.GetString("UserName");
            var playerId = reader.GetInt("PlayerID");
			return _playerFactory.Create(displayName, (Role)role, userName, playerId);
		}

	}

}