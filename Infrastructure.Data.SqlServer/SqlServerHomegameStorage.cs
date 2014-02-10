using System;
using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer {

	public class SqlServerHomegameStorage : IHomegameStorage
    {
	    private readonly IStorageProvider _storageProvider;

        public SqlServerHomegameStorage(
            IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

        public IList<int> GetAllIds()
        {
            const string sql = "SELECT h.HomegameID FROM homegame h";
            var reader = _storageProvider.Query(sql);
            var ids = new List<int>();
            while (reader.Read())
            {
                ids.Add(reader.GetInt("HomegameID"));
            }
            return ids;
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
            return GetRawHomegameList(sql, parameter);
        }

        public IList<RawHomegame> GetHomegamesByUserId(int userId)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h INNER JOIN player p on h.HomegameID = p.HomegameID WHERE p.UserID = @userId ORDER BY h.Name";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@userId", userId)
                };
            return GetRawHomegameList(sql, parameters);
        }

		public RawHomegame GetHomegameByName(string slug){
			const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE Name = @slug";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", slug)
                };
            
            return GetRawHomegame(sql, parameters);
		}

        public RawHomegame GetById(int id)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE HomegameID = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            return GetRawHomegame(sql, parameters);
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
                return reader.GetInt("HomegameID");
            }
            return null;
        }

		private RawHomegame CreateRawHomegame(IStorageDataReader reader){
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

        private IList<RawHomegame> GetRawHomegameList(string sql, ListSqlParameter parameter)
        {
            var reader = _storageProvider.Query(sql, parameter);
            return GetMany(CreateRawHomegame, reader);
        }

        private IList<RawHomegame> GetRawHomegameList(string sql, IList<SimpleSqlParameter> parameters)
        {
            var reader = _storageProvider.Query(sql, parameters);
            return GetMany(CreateRawHomegame, reader);
        }

        private IList<T> GetMany<T>(Func<IStorageDataReader, T> func, IStorageDataReader reader)
        {
            var list = new List<T>();
            while (reader.Read())
            {
                list.Add(func(reader));
            }
            return list;
        }

        private RawHomegame GetRawHomegame(string sql, IList<SimpleSqlParameter> parameters)
        {
            var reader = _storageProvider.Query(sql, parameters);
            return GetOne(CreateRawHomegame, reader);
        }

        private T GetOne<T>(Func<IStorageDataReader, T> func, IStorageDataReader reader)
        {
            return reader.Read() ? func(reader) : default(T);
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
            return reader.GetInt("RoleID");
        }

	}

}