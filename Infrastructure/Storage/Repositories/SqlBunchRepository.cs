using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class ApiBunchRepository : IBunchRepository
    {
        private readonly ApiConnection _apiConnection;

        public ApiBunchRepository(ApiConnection apiConnection)
        {
            _apiConnection = apiConnection;
        }

        public Bunch Get(string slug)
        {
            throw new NotImplementedException();
        }

        public IList<Bunch> Get(IList<string> ids)
        {
            throw new NotImplementedException();
        }

        public IList<string> Search()
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchBySlug(string slug)
        {
            throw new NotImplementedException();
        }

        public IList<string> SearchByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public string Add(Bunch bunch)
        {
            throw new NotImplementedException();
        }

        public void Update(Bunch bunch)
        {
            throw new NotImplementedException();
        }
    }

	public class SqlBunchRepository : IBunchRepository
	{
        private const string DataSql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h";
        private const string SearchSql = "SELECT h.HomegameID FROM homegame h";
        private readonly SqlServerStorageProvider _db;
        
        public SqlBunchRepository(SqlServerStorageProvider db)
	    {
            _db = db;
	    }

	    public IList<Bunch> Get(IList<string> ids)
	    {
	        var sql = string.Concat(DataSql, " WHERE h.HomegameID IN(@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _db.Query(sql, parameter);
            var rawHomegames = reader.ReadList(CreateRawBunch);
            return rawHomegames.Select(CreateBunch).ToList();
	    }

        public Bunch Get(string slug)
        {
            var sql = string.Concat(DataSql, " WHERE Name = @slug");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@slug", slug)
            };
            var reader = _db.Query(sql, parameters);
            var rawHomegame = reader.ReadOne(CreateRawBunch);
            return rawHomegame != null ? CreateBunch(rawHomegame) : null;
        }

	    public IList<string> Search()
	    {
            var reader = _db.Query(SearchSql);
            return reader.ReadStringList("HomegameID");
	    }

	    public IList<string> SearchBySlug(string slug)
	    {
	        var sql = string.Concat(SearchSql, " WHERE h.Name = @slug");
            var parameters = new SqlParameters(new SimpleSqlParameter("@slug", slug));
            var reader = _db.Query(sql, parameters);
            var id = reader.ReadString("HomegameID");
            if(!string.IsNullOrEmpty(id))
                return new List<string>{id};
            return new List<string>();
	    }

	    public IList<string> SearchByUser(string userId)
	    {
            var sql = string.Concat(SearchSql, "  INNER JOIN player p on h.HomegameID = p.HomegameID WHERE p.UserID = @userId ORDER BY h.Name");
	        var parameters = new List<SimpleSqlParameter>
	        {
	            new SimpleSqlParameter("@userId", userId)
	        };
            var reader = _db.Query(sql, parameters);
            return reader.ReadStringList("HomegameID");
	    }
        
        public string Add(Bunch bunch)
        {
            var rawBunch = RawBunch.Create(bunch);
            const string sql = "INSERT INTO homegame (Name, DisplayName, Description, Currency, CurrencyLayout, Timezone, DefaultBuyin, CashgamesEnabled, TournamentsEnabled, VideosEnabled, HouseRules) VALUES (@slug, @displayName, @description, @currencySymbol, @currencyLayout, @timeZone, 0, @cashgamesEnabled, @tournamentsEnabled, @videosEnabled, @houseRules) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@slug", rawBunch.Slug),
                new SimpleSqlParameter("@displayName", rawBunch.DisplayName),
                new SimpleSqlParameter("@description", rawBunch.Description),
                new SimpleSqlParameter("@currencySymbol", rawBunch.CurrencySymbol),
                new SimpleSqlParameter("@currencyLayout", rawBunch.CurrencyLayout),
                new SimpleSqlParameter("@timeZone", rawBunch.TimezoneName),
                new SimpleSqlParameter("@cashgamesEnabled", rawBunch.CashgamesEnabled),
                new SimpleSqlParameter("@tournamentsEnabled", rawBunch.TournamentsEnabled),
                new SimpleSqlParameter("@videosEnabled", rawBunch.VideosEnabled),
                new SimpleSqlParameter("@houseRules", rawBunch.HouseRules)
            };
            return _db.ExecuteInsert(sql, parameters);
        }

        public void Update(Bunch bunch)
        {
            var rawBunch = RawBunch.Create(bunch);
            const string sql = "UPDATE homegame SET Name = @slug, DisplayName = @displayName, Description = @description, HouseRules = @houseRules, Currency = @currencySymbol, CurrencyLayout = @currencyLayout, Timezone = @timeZone, DefaultBuyin = @defaultBuyin, CashgamesEnabled = @cashgamesEnabled, TournamentsEnabled = @tournamentsEnabled, VideosEnabled = @videosEnabled WHERE HomegameID = @id";

            var parameters = new List<SimpleSqlParameter>
	            {
                    new SimpleSqlParameter("@slug", rawBunch.Slug),
                    new SimpleSqlParameter("@displayName", rawBunch.DisplayName),
                    new SimpleSqlParameter("@description", rawBunch.Description),
                    new SimpleSqlParameter("@houseRules", rawBunch.HouseRules),
                    new SimpleSqlParameter("@currencySymbol", rawBunch.CurrencySymbol),
                    new SimpleSqlParameter("@currencyLayout", rawBunch.CurrencyLayout),
                    new SimpleSqlParameter("@timeZone", rawBunch.TimezoneName),
                    new SimpleSqlParameter("@defaultBuyin", rawBunch.DefaultBuyin),
                    new SimpleSqlParameter("@cashgamesEnabled", rawBunch.CashgamesEnabled),
                    new SimpleSqlParameter("@tournamentsEnabled", rawBunch.TournamentsEnabled),
                    new SimpleSqlParameter("@videosEnabled", rawBunch.VideosEnabled),
                    new SimpleSqlParameter("@id", rawBunch.Id)
                };

            _db.Execute(sql, parameters);
        }
        
	    private static Bunch CreateBunch(RawBunch rawBunch)
        {
            var culture = CultureInfo.CreateSpecificCulture("sv-SE");
            var currency = new Currency(rawBunch.CurrencySymbol, rawBunch.CurrencyLayout, culture);

            return new Bunch(
                rawBunch.Id,
                rawBunch.Slug,
                rawBunch.DisplayName,
                rawBunch.Description,
                rawBunch.HouseRules,
                TimeZoneInfo.FindSystemTimeZoneById(rawBunch.TimezoneName),
                rawBunch.DefaultBuyin,
                currency);
        }
        
        public bool DeleteBunch(int id)
        {
            const string sql = "DELETE FROM homegame WHERE Id = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;
        }

        private static RawBunch CreateRawBunch(IStorageDataReader reader)
        {
            return new RawBunch(
                reader.GetStringValue("HomegameID"),
                reader.GetStringValue("Name"),
                reader.GetStringValue("DisplayName"),
                reader.GetStringValue("Description"),
                reader.GetStringValue("HouseRules"),
                reader.GetStringValue("Timezone"),
                reader.GetIntValue("DefaultBuyin"),
                reader.GetStringValue("CurrencyLayout"),
                reader.GetStringValue("Currency"),
                reader.GetBooleanValue("CashgamesEnabled"),
                reader.GetBooleanValue("TournamentsEnabled"),
                reader.GetBooleanValue("VideosEnabled"));
        }
	}
}