using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Data.Storage {

	public class MySqlHomegameStorage : IHomegameStorage
    {
	    private readonly IStorageProvider _storageProvider;

	    public MySqlHomegameStorage(IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

		public IList<RawHomegame> GetHomegames(){
			var sql = GetHomegameBaseSql();
			sql += "ORDER BY h.DisplayName";
			return GetRawHomegameList(sql);
		}

        public IList<string> GetAllSlugs()
        {
            const string sql = "SELECT h.Name FROM homegame h";
            var reader = _storageProvider.Query(sql);
            var slugs = new List<string>();
            while (reader.Read())
            {
                slugs.Add(reader.GetString("Name"));
            }
            return slugs;
        }

        public IList<RawHomegame> GetHomegames(IList<string> slugs)
        {
            var sql = GetHomegameBaseSql(slugs);
            return GetRawHomegameList(sql);
        }

        public IList<RawHomegame> GetHomegamesByUserId(int userId)
        {
            var sql = GetHomegameBaseSql();
            sql += "INNER JOIN player p on h.HomegameID = p.HomegameID WHERE p.UserID = {0} ORDER BY h.Name";
            sql = string.Format(sql, userId);
            return GetRawHomegameList(sql);
        }

		public RawHomegame GetHomegameByName(string homegameName){
			var sql = GetHomegameBaseSql();
			sql += "WHERE Name = '{0}'";
            sql = string.Format(sql, homegameName);
            return GetRawHomegame(sql);
		}

        public int GetHomegameRole(int homegameId, int userId)
        {
            var sql = "SELECT p.RoleID FROM player p WHERE p.UserID = {0} AND p.HomegameID = {1}";
            sql = string.Format(sql, userId, homegameId);
            return GetRole(sql);
        }

		private string GetHomegameBaseSql()
		{
		    return "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h ";
		}

        private string GetHomegameBaseSql(IList<string> slugs)
        {
            var slugList = GetSlugListForSql(slugs);
            return string.Concat(GetHomegameBaseSql(), string.Format("WHERE h.Name IN({0})", slugList));
        }

        private string GetSlugListForSql(IList<string> slugs)
        {
            return string.Join(", ", slugs.Select(o => string.Format("'{0}'", o)).ToArray());
        }

		public RawHomegame AddHomegame(RawHomegame homegame){
			var sql = "INSERT INTO homegame (Name, DisplayName, Description, Currency, CurrencyLayout, Timezone, DefaultBuyin, CashgamesEnabled, TournamentsEnabled, VideosEnabled, HouseRules) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 0, {6}, {7}, {8}, '{9}')";
		    sql = string.Format(sql, homegame.Slug, homegame.DisplayName, homegame.Description, homegame.CurrencySymbol,
		                        homegame.CurrencyLayout, homegame.TimezoneName, _storageProvider.BoolToInt(homegame.CashgamesEnabled),
		                        _storageProvider.BoolToInt(homegame.TournamentsEnabled),
		                        _storageProvider.BoolToInt(homegame.VideosEnabled), homegame.HouseRules);
			var id = _storageProvider.ExecuteInsert(sql);
			homegame.Id = id;
			return homegame;
		}

		public bool UpdateHomegame(RawHomegame homegame){
			var sql =	"UPDATE homegame SET Name = '{0}', DisplayName = '{1}', Description = '{2}', HouseRules = '{3}', Currency = '{4}', CurrencyLayout = '{5}', Timezone = '{6}', DefaultBuyin = {7}, CashgamesEnabled = {8}, TournamentsEnabled = {9}, VideosEnabled = {10} WHERE HomegameID = {11}";
            sql = string.Format(sql, homegame.Slug, homegame.DisplayName, homegame.Description, homegame.HouseRules, homegame.CurrencySymbol, homegame.CurrencyLayout, homegame.TimezoneName, homegame.DefaultBuyin, _storageProvider.BoolToInt(homegame.CashgamesEnabled), _storageProvider.BoolToInt(homegame.TournamentsEnabled), _storageProvider.BoolToInt(homegame.VideosEnabled), homegame.Id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool DeleteHomegame(string slug){
			var sql = "DELETE FROM homegame WHERE Name = '{0}'";
            sql = string.Format(sql, slug);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		private RawHomegame CreateRawHomegame(StorageDataReader reader){
			return new RawHomegame
			    {
			        Id = reader.GetInt("HomegameID"),
			        Slug = reader.GetString("Name"),
			        DisplayName = reader.GetString("DisplayName"),
			        Description = reader.GetString("Description"),
			        HouseRules = reader.GetString("HouseRules"),
			        CurrencyLayout = reader.GetString("CurrencyLayout"),
			        CurrencySymbol = reader.GetString("Currency"),
			        TimezoneName = reader.GetString("Timezone"),
			        DefaultBuyin = reader.GetInt("DefaultBuyin"),
			        CashgamesEnabled = reader.GetBoolean("CashgamesEnabled"),
			        TournamentsEnabled = reader.GetBoolean("TournamentsEnabled"),
                    VideosEnabled = reader.GetBoolean("VideosEnabled")
			    };
		}

        private IList<RawHomegame> GetRawHomegameList(string sql)
        {
            var reader = _storageProvider.Query(sql);
            var homegames = new List<RawHomegame>();
            while (reader.Read())
            {
                homegames.Add(CreateRawHomegame(reader));
            }
            return homegames;
        }

        private RawHomegame GetRawHomegame(string sql)
        {
            var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return CreateRawHomegame(reader);
            }
            return null;
        }

        private int GetRole(string sql)
        {
            var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return CreateRole(reader);
            }
            return (int)Role.Guest;
        }

        private int CreateRole(StorageDataReader reader)
        {
            return reader.GetInt("RoleID");
        }

	}

}