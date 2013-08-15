using System;
using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Storage {

	public class MySqlHomegameStorage : IHomegameStorage
    {
	    private readonly IStorageProvider _storageProvider;

	    public MySqlHomegameStorage(IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

		public List<Homegame> GetHomegames(){
			var sql = GetHomegameBaseSql();
			sql += "ORDER BY h.DisplayName";
			return GetHomegamesFromSql(sql);
		}

		public List<Homegame> GetHomegamesByRole(string token, int role){
			var sql = GetHomegameBaseSql();
			sql += "INNER JOIN player p on h.HomegameID = p.HomegameID " +
					"INNER JOIN user u on p.UserID = u.UserID " +
					"WHERE u.Token = '{0}' " +
					"AND p.RoleID >= {1} " +
					"ORDER BY h.Name";
		    sql = string.Format(sql, token, role);
			return GetHomegamesFromSql(sql);
		}

		public Homegame GetHomegameByName(string homegameName){
			var sql = GetHomegameBaseSql();
			sql += "WHERE Name = '{0}'";
            sql = string.Format(sql, homegameName);
			return GetHomegameFromSql(sql);
		}

		public RawHomegame GetRawHomegameByName(string homegameName){
			var sql = GetHomegameBaseSql();
			sql += "WHERE Name = '{0}'";
            sql = string.Format(sql, homegameName);
            return GetRawHomegameFromSql(sql);
		}

		public int GetHomegameRole(Homegame homegame, User user){
			var sql =	"SELECT p.RoleID " +
					"FROM player p " +
					"WHERE p.UserID = {0} " +
					"AND p.HomegameID = {1}";
            sql = string.Format(sql, user.Id, homegame.Id);
            return GetRoleFromSql(sql);
		}

		private string GetHomegameBaseSql()
		{
		    return "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules " +
		                       "FROM homegame h ";
		}

	    private Homegame GetHomegameFromSql(string sql){
			var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return HomegameFromDbRow(reader);
            }
			return null;
		}

		private RawHomegame GetRawHomegameFromSql(string sql){
			var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return RawHomegameFromDbRow(reader);
            }
			return null;
		}

		private int GetRoleFromSql(string sql){
			var reader = _storageProvider.Query(sql);
			while (reader.Read())
            {
                return RoleFromDbRow(reader);
            }
			return (int)Role.Guest;
		}

		private List<Homegame> GetHomegamesFromSql(string sql){
            var reader = _storageProvider.Query(sql);
		    var homegames = new List<Homegame>();
            while (reader.Read())
            {
                homegames.Add(HomegameFromDbRow(reader));
            }
			return homegames;
		}

		public Homegame AddHomegame(Homegame homegame){
			var currency = homegame.Currency;
			var sql =	"INSERT INTO homegame " +
					"(Name, DisplayName, Description, Currency, CurrencyLayout, Timezone, DefaultBuyin, CashgamesEnabled, TournamentsEnabled, VideosEnabled, HouseRules) " +
					"VALUES " +
					"('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 0, {6}, {7}, {8}, '{9}')";
		    sql = string.Format(sql, homegame.Slug, homegame.DisplayName, homegame.Description, currency.Symbol,
		                        currency.Layout, homegame.Timezone.Id, _storageProvider.BoolToInt(homegame.CashgamesEnabled),
		                        _storageProvider.BoolToInt(homegame.TournamentsEnabled),
		                        _storageProvider.BoolToInt(homegame.VideosEnabled), homegame.HouseRules);
			var id = _storageProvider.ExecuteInsert(sql);
			homegame.Id = id;
			return homegame;
		}

		public bool UpdateHomegame(Homegame homegame){
			var currency = homegame.Currency;
			var sql =	"UPDATE homegame " +
				"SET " +
				"Name = '{0}', " +
				"DisplayName = '{1}', " +
				"Description = '{2}', " +
				"HouseRules = '{3}', " +
				"Currency = '{4}', " +
				"CurrencyLayout = '{5}', " +
				"Timezone = '{6}', " +
				"DefaultBuyin = {7}, " +
				"CashgamesEnabled = {8}, " +
				"TournamentsEnabled = {9}, " +
				"VideosEnabled = {10} " +
				"WHERE HomegameID = {11}";
            sql = string.Format(sql, homegame.Slug, homegame.DisplayName, homegame.Description, homegame.HouseRules, currency.Symbol, currency.Layout, homegame.Timezone.Id, homegame.DefaultBuyin, _storageProvider.BoolToInt(homegame.CashgamesEnabled), _storageProvider.BoolToInt(homegame.TournamentsEnabled), _storageProvider.BoolToInt(homegame.VideosEnabled), homegame.Id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool DeleteHomegame(Homegame homegame){
			var sql =	"DELETE FROM homegame " +
					"WHERE Name = '{0}'";
            sql = string.Format(sql, homegame.Slug);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		private Homegame HomegameFromDbRow(MySqlDataReader reader){
			return new Homegame
			    {
			        Id = reader.GetInt32("HomegameID"),
			        Slug = reader.GetString("Name"),
			        DisplayName = reader.GetString("DisplayName"),
			        Description = reader.GetString("Description"),
			        HouseRules = reader.GetString("HouseRules"),
			        Currency = new CurrencySettings(reader.GetString("Currency"), reader.GetString("CurrencyLayout")),
			        Timezone = TimeZoneInfo.FindSystemTimeZoneById(reader.GetString("Timezone")),
			        DefaultBuyin = reader.GetInt32("DefaultBuyin"),
			        CashgamesEnabled = reader.GetBoolean("CashgamesEnabled"),
			        TournamentsEnabled = reader.GetBoolean("TournamentsEnabled"),
			        VideosEnabled = reader.GetBoolean("VideosEnabled")
			    };
		}

		private RawHomegame RawHomegameFromDbRow(MySqlDataReader reader){
			return new RawHomegame
			    {
			        Id = reader.GetInt32("HomegameID"),
			        Slug = reader.GetString("Name"),
			        DisplayName = reader.GetString("DisplayName"),
			        Description = reader.GetString("Description"),
			        HouseRules = reader.GetString("HouseRules"),
			        CurrencyLayout = reader.GetString("CurrencyLayout"),
			        CurrencySymbol = reader.GetString("Currency"),
			        TimezoneName = reader.GetString("Timezone"),
			        DefaultBuyin = reader.GetInt32("DefaultBuyin"),
			        CashgamesEnabled = reader.GetBoolean("CashgamesEnabled"),
			        TournamentsEnabled = reader.GetBoolean("TournamentsEnabled"),
			        VideosEnabled = reader.GetBoolean("VideosEnabled")
			    };
		}

		private int RoleFromDbRow(MySqlDataReader reader){
			return reader.GetInt32("RoleID");
		}

	}

}