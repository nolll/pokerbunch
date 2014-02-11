using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer {

	public class SqlServerHomegameStorage : IHomegameStorage
    {
	    private readonly IStorageProvider _storageProvider;
	    private readonly IRawHomegameFactory _rawHomegameFactory;

	    public SqlServerHomegameStorage(
            IStorageProvider storageProvider,
            IRawHomegameFactory rawHomegameFactory)
        {
            _storageProvider = storageProvider;
            _rawHomegameFactory = rawHomegameFactory;
        }

	    public IList<int> GetAllIds()
        {
            const string sql = "SELECT h.HomegameID FROM homegame h";
            var reader = _storageProvider.Query(sql);
	        return reader.GetIntList("HomegameID");
        }

        public int? GetIdBySlug(string slug)
        {
            const string sql = "SELECT h.HomegameID FROM homegame h WHERE h.Name = @slug";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", slug)
                };
            return GetHomegameId(sql, parameters);
        }

        public IList<RawHomegame> GetHomegames(IList<int> ids)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE h.HomegameID IN(@ids)";
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _storageProvider.Query(sql, parameter);
            return reader.GetList(_rawHomegameFactory.Create);
        }

        public IList<RawHomegame> GetHomegamesByUserId(int userId)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h INNER JOIN player p on h.HomegameID = p.HomegameID WHERE p.UserID = @userId ORDER BY h.Name";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@userId", userId)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.GetList(_rawHomegameFactory.Create);
        }

		public RawHomegame GetHomegameByName(string slug){
			const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE Name = @slug";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", slug)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.GetOne(_rawHomegameFactory.Create);
		}

        public RawHomegame GetById(int id)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE HomegameID = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.GetOne(_rawHomegameFactory.Create);
        }
        
        public int GetHomegameRole(int homegameId, int userId)
        {
            const string sql = "SELECT p.RoleID FROM player p WHERE p.UserID = @userId AND p.HomegameID = @homegameId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@homegameId", homegameId)
                };
            return GetRole(sql, parameters);
        }

	    public RawHomegame AddHomegame(RawHomegame homegame)
        {
            const string sql = "INSERT INTO homegame (Name, DisplayName, Description, Currency, CurrencyLayout, Timezone, DefaultBuyin, CashgamesEnabled, TournamentsEnabled, VideosEnabled, HouseRules) VALUES (@slug, @displayName, @description, @currencySymbol, @currencyLayout, @timeZone, 0, @cashgamesEnabled, @tournamentsEnabled, @videosEnabled, @houseRules) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
	        var parameters = new List<SimpleSqlParameter>
	            {
                    new SimpleSqlParameter("@slug", homegame.Slug),
                    new SimpleSqlParameter("@displayName", homegame.DisplayName),
                    new SimpleSqlParameter("@description", homegame.Description),
                    new SimpleSqlParameter("@currencySymbol", homegame.CurrencySymbol),
                    new SimpleSqlParameter("@currencyLayout", homegame.CurrencyLayout),
                    new SimpleSqlParameter("@timeZone", homegame.TimezoneName),
                    new SimpleSqlParameter("@cashgamesEnabled", homegame.CashgamesEnabled),
                    new SimpleSqlParameter("@tournamentsEnabled", homegame.TournamentsEnabled),
                    new SimpleSqlParameter("@videosEnabled", homegame.VideosEnabled),
                    new SimpleSqlParameter("@houseRules", homegame.HouseRules)
                };
            var id = _storageProvider.ExecuteInsert(sql, parameters);
            homegame.Id = id;
            return homegame;
        }

        public bool UpdateHomegame(RawHomegame homegame)
        {
            const string sql = "UPDATE homegame SET Name = @slug, DisplayName = @displayName, Description = @description, HouseRules = @houseRules, Currency = @currencySymbol, CurrencyLayout = @currencyLayout, Timezone = @timeZone, DefaultBuyin = @defaultBuyin, CashgamesEnabled = @cashgamesEnabled, TournamentsEnabled = @tournamentsEnabled, VideosEnabled = @videosEnabled WHERE HomegameID = @id";

            var parameters = new List<SimpleSqlParameter>
	            {
                    new SimpleSqlParameter("@slug", homegame.Slug),
                    new SimpleSqlParameter("@displayName", homegame.DisplayName),
                    new SimpleSqlParameter("@description", homegame.Description),
                    new SimpleSqlParameter("@houseRules", homegame.HouseRules),
                    new SimpleSqlParameter("@currencySymbol", homegame.CurrencySymbol),
                    new SimpleSqlParameter("@currencyLayout", homegame.CurrencyLayout),
                    new SimpleSqlParameter("@timeZone", homegame.TimezoneName),
                    new SimpleSqlParameter("@defaultBuyin", homegame.DefaultBuyin),
                    new SimpleSqlParameter("@cashgamesEnabled", homegame.CashgamesEnabled),
                    new SimpleSqlParameter("@tournamentsEnabled", homegame.TournamentsEnabled),
                    new SimpleSqlParameter("@videosEnabled", homegame.VideosEnabled),
                    new SimpleSqlParameter("@id", homegame.Id)
                };
            
            var rowCount = _storageProvider.Execute(sql, parameters);
            return rowCount > 0;
        }

        public bool DeleteHomegame(string slug)
        {
            const string sql = "DELETE FROM homegame WHERE Name = @slug";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", slug)
                };
            var rowCount = _storageProvider.Execute(sql, parameters);
            return rowCount > 0;
        }

        private int? GetHomegameId(string sql, IList<SimpleSqlParameter> parameters)
        {
            var reader = _storageProvider.Query(sql, parameters);
            while (reader.Read())
            {
                return reader.GetIntValue("HomegameID");
            }
            return null;
        }

        private int GetRole(string sql, IList<SimpleSqlParameter> parameters)
        {
            var reader = _storageProvider.Query(sql, parameters);
            while (reader.Read())
            {
                return CreateRole(reader);
            }
            return (int)Role.Guest;
        }

        private int CreateRole(IStorageDataReader reader)
        {
            return reader.GetIntValue("RoleID");
        }

	}

}