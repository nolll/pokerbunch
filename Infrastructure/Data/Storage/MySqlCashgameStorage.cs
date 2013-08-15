using System;
using System.Collections.Generic;
using System.Data;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Storage {
    public class MySqlCashgameStorage : ICashgameStorage
    {
	    private readonly IStorageProvider _storageProvider;

	    public MySqlCashgameStorage(IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

		public int AddGame(Homegame homegame, Cashgame cashgame){
			var sql =	"INSERT INTO game " +
					"(HomegameID, Location, Status) " +
					"VALUES " +
					"({0}, '{1}', {2})";
		    sql = string.Format(sql, homegame.Id, cashgame.Location, cashgame.Status);
			return _storageProvider.ExecuteInsert(sql);
		}

		public bool DeleteGame(Cashgame cashgame){
			var sql =	"DELETE FROM game " +
					"WHERE GameID = {0}";
		    sql = string.Format(sql, cashgame.Id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint){
			var timestampStr = Globalization.FormatIsoDateTime(DateTimeFactory.ToUtc(checkpoint.Timestamp));
			var sql =	"INSERT INTO cashgamecheckpoint " +
					"(GameID, PlayerID, Type, Amount, Stack, Timestamp) " +
					"VALUES " +
					"({0}, {1}, {2}, '{3}', '{4}', '{5}')";
            sql = string.Format(sql, cashgame.Id, player.Id, checkpoint.Type, checkpoint.Amount, checkpoint.Stack, timestampStr);
			return _storageProvider.ExecuteInsert(sql);
		}

		public bool UpdateCheckpoint(Checkpoint checkpoint){
			var sql =	"UPDATE cashgamecheckpoint " +
					"SET " +
					"Amount = {$checkpoint.getAmount()}, " +
					"Stack = {$checkpoint.getStack()} " +
					"WHERE CheckpointID = {$checkpoint.getId()}";
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool DeleteCheckpoint(int id){
			var sql =	"DELETE FROM cashgamecheckpoint WHERE CheckpointID = {0}";
		    sql = string.Format(sql, id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		private string GetGameSql(Homegame homegame){
			var sql =	"SELECT " +
					"g.GameID, g.Location, g.Status, g.Date, " +
					"cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp " +
					"FROM game g " +
					"LEFT JOIN cashgamecheckpoint cp ON g.GameID = cp.GameID " +
					"WHERE g.HomegameID = {0} ";
		    sql = string.Format(sql, homegame.Id);
		    return sql;
		}

		public RawCashgame GetGame(Homegame homegame, DateTime date){
			var dateStr = Globalization.FormatIsoDate(date);
			var sql =	GetGameSql(homegame) +
					"AND g.Date = '{0}' " +
					"ORDER BY cp.PlayerID, cp.Timestamp";
            sql = string.Format(sql, dateStr);
			var reader = _storageProvider.Query(sql);
			var cashgames = GetGamesFromDbResult(homegame, reader);
			if(cashgames.Count == 0){
				return null;
			}
			return cashgames[0];
		}

		public List<RawCashgame> GetGames(Homegame homegame, GameStatus? status = null, int? year = null){
			var sql = GetGameSql(homegame);
			if(status.HasValue){
				sql += string.Format("AND g.Status = {0} ", (int)status.Value);
			}
			if(year.HasValue){
				sql += string.Format("AND YEAR(g.Date) = {0} ", year.Value);
			}
			sql += "ORDER BY g.GameID, cp.PlayerID, cp.Timestamp";
            var reader = _storageProvider.Query(sql);
			return GetGamesFromDbResult(homegame, reader);
		}

		private List<RawCashgame> GetGamesFromDbResult(Homegame homegame, IDataReader reader){
			var cashgames = new List<RawCashgame>();
			RawCashgame currentGame = null;
			int currentGameId = -1;
			RawCashgameResult currentResult = null;
			int currentPlayerId = -1;
			while(reader.Read())
			{
			    var gameId = reader.GetInt32(reader.GetOrdinal("GameID"));
				if(gameId != currentGameId){
					currentGame = RawCashgameFromDbRow(reader);
					currentGameId = currentGame.Id;
					cashgames.Add(currentGame);
					currentResult = null;
					currentPlayerId = -1;
				}
			    var playerId = reader.GetInt32(reader.GetOrdinal("PlayerID"));
				if(playerId != currentPlayerId){
					if(playerId != 0){ // this was a null-check in the php site
						currentResult = RawCashgameResultFromDbRow(reader);
						currentPlayerId = currentResult.PlayerId;
						currentGame.AddResult(currentResult);
					}
				}
			    var checkpointId = reader.GetInt32(reader.GetOrdinal("CheckpointID"));
				if(checkpointId != 0){ // this was a null-check in the php site
					var checkpoint = CheckpointFromDbRow(reader, homegame.Timezone);
					currentResult.AddCheckpoint(checkpoint);
				}
			}
			return cashgames;
		}

		public List<int> GetYears(Homegame homegame){
			var sql =	"SELECT DISTINCT YEAR(ccp.Timestamp) as 'Year' " +
					"FROM cashgamecheckpoint ccp " +
					"LEFT JOIN game g ON ccp.GameID = g.GameID " +
					"LEFT JOIN homegame h ON g.HomegameID = h.HomegameID " +
					"WHERE h.Name = '{0}' " +
					"ORDER BY 'Year' DESC";
		    sql = string.Format(sql, homegame.Slug);
			var reader = _storageProvider.Query(sql);
			var years = new List<int>();
			while(reader.Read()){
				years.Add(reader.GetInt32(reader.GetOrdinal("Year")));
			}
			return years;
		}

		public bool UpdateGame(RawCashgame cashgame){
            var sql = "UPDATE game SET Location = {0}, Date = {1}, Status = {2} WHERE GameID = {3}";
		    sql = string.Format(sql, cashgame.Location, cashgame.Date, cashgame.Status, cashgame.Id);
		    var rowCount = _storageProvider.Execute(sql);
		    return rowCount > 0;
		}

		public bool HasPlayed(Player player){
			var sql =	"SELECT DISTINCT PlayerID " +
					"FROM cashgamecheckpoint " +
					"WHERE PlayerId = {0}";
		    sql = string.Format(sql, player.Id);
		    var reader = _storageProvider.Query(sql);
			return reader.Read();
		}

		public List<string> GetLocations(Homegame homegame){
			var sql =	"SELECT DISTINCT g.Location " +
					"FROM game g " +
					"LEFT JOIN homegame h ON g.HomegameID = h.HomegameID " +
					"WHERE Name = '{$homegame.getSlug()}' " +
					"AND g.Location <> '' " +
					"ORDER BY g.Location";
			var reader = _storageProvider.Query(sql);
			var locations = new List<string>();
			while(reader.Read()){
				locations.Add(reader.GetString(reader.GetOrdinal("Location")));
			}
			return locations;
		}

		private RawCashgame RawCashgameFromDbRow(IDataReader reader){
			var id = reader.GetInt32(reader.GetOrdinal("GameID"));
		    var location = reader.GetString(reader.GetOrdinal("Location"));
            if (location == "")
            {
                location = null;
            }
			var status = reader.GetInt32(reader.GetOrdinal("Status"));
			var date = reader.GetString(reader.GetOrdinal("Date"));
			return new RawCashgame(id, location, (GameStatus)status, date);
		}

		private RawCashgameResult RawCashgameResultFromDbRow(IDataReader reader){
			var playerId = reader.GetInt32(reader.GetOrdinal("PlayerID"));
			return new RawCashgameResult(playerId);
		}

		private Checkpoint CheckpointFromDbRow(IDataReader reader, TimeZoneInfo timezone)
		{
            var id = reader.GetInt32(reader.GetOrdinal("CheckpointID"));
			var type = reader.GetInt32(reader.GetOrdinal("Type"));
			var amount = reader.GetInt32(reader.GetOrdinal("Amount"));
			var stack = reader.GetInt32(reader.GetOrdinal("Stack"));
		    var timestamp = reader.GetDateTime(reader.GetOrdinal("TimeStamp"));
		    //todo: adjust for timezone
			var checkpoint = CreateCheckpoint(type, timestamp, stack, amount);
			checkpoint.Id = id;
			return checkpoint;
		}

		private Checkpoint CreateCheckpoint(int type, DateTime timestamp, int stack, int amount){
			if(type == 1){
				return new BuyinCheckpoint(timestamp, stack, amount);
			}
            if (type == 2){
				return new CashoutCheckpoint(timestamp, stack);
			}
			return new ReportCheckpoint(timestamp, stack);
		}

	}

}