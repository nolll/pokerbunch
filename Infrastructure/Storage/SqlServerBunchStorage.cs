using System.Collections.Generic;
using Core.Entities;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
    public class SqlServerBunchStorage : SqlServerStorageProvider, IBunchStorage
    {
	    public IList<int> GetAllIds()
        {
            const string sql = "SELECT h.HomegameID FROM homegame h";
            var reader = Query(sql);
	        return reader.ReadIntList("HomegameID");
        }

        public int? GetIdBySlug(string slug)
        {
            const string sql = "SELECT h.HomegameID FROM homegame h WHERE h.Name = @slug";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", slug)
                };
            var reader = Query(sql, parameters);
            return reader.ReadInt("HomegameID");
        }

        public IList<RawBunch> GetBunches(IList<int> ids)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE h.HomegameID IN(@ids)";
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = Query(sql, parameter);
            return reader.ReadList(CreateRawBunch);
        }

        public IList<RawBunch> GetBunchesByUserId(int userId)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h INNER JOIN player p on h.HomegameID = p.HomegameID WHERE p.UserID = @userId ORDER BY h.Name";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@userId", userId)
                };
            var reader = Query(sql, parameters);
            return reader.ReadList(CreateRawBunch);
        }

		public RawBunch GetBunchByName(string slug){
			const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE Name = @slug";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", slug)
                };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateRawBunch);
		}

        public RawBunch GetById(int id)
        {
            const string sql = "SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules FROM homegame h WHERE HomegameID = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateRawBunch);
        }
        
        public int GetBunchRole(int bunchId, int userId)
        {
            const string sql = "SELECT p.RoleID FROM player p WHERE p.UserID = @userId AND p.HomegameID = @homegameId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@homegameId", bunchId)
                };
            var reader = Query(sql, parameters);
            var role = reader.ReadInt("RoleID");
            return role.HasValue ? role.Value : (int)Role.Guest;
        }

	    public int AddBunch(RawBunch bunch)
        {
            const string sql = "INSERT INTO homegame (Name, DisplayName, Description, Currency, CurrencyLayout, Timezone, DefaultBuyin, CashgamesEnabled, TournamentsEnabled, VideosEnabled, HouseRules) VALUES (@slug, @displayName, @description, @currencySymbol, @currencyLayout, @timeZone, 0, @cashgamesEnabled, @tournamentsEnabled, @videosEnabled, @houseRules) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
	        var parameters = new List<SimpleSqlParameter>
	            {
                    new SimpleSqlParameter("@slug", bunch.Slug),
                    new SimpleSqlParameter("@displayName", bunch.DisplayName),
                    new SimpleSqlParameter("@description", bunch.Description),
                    new SimpleSqlParameter("@currencySymbol", bunch.CurrencySymbol),
                    new SimpleSqlParameter("@currencyLayout", bunch.CurrencyLayout),
                    new SimpleSqlParameter("@timeZone", bunch.TimezoneName),
                    new SimpleSqlParameter("@cashgamesEnabled", bunch.CashgamesEnabled),
                    new SimpleSqlParameter("@tournamentsEnabled", bunch.TournamentsEnabled),
                    new SimpleSqlParameter("@videosEnabled", bunch.VideosEnabled),
                    new SimpleSqlParameter("@houseRules", bunch.HouseRules)
                };
            return ExecuteInsert(sql, parameters);
        }

        public bool UpdateBunch(RawBunch bunch)
        {
            const string sql = "UPDATE homegame SET Name = @slug, DisplayName = @displayName, Description = @description, HouseRules = @houseRules, Currency = @currencySymbol, CurrencyLayout = @currencyLayout, Timezone = @timeZone, DefaultBuyin = @defaultBuyin, CashgamesEnabled = @cashgamesEnabled, TournamentsEnabled = @tournamentsEnabled, VideosEnabled = @videosEnabled WHERE HomegameID = @id";

            var parameters = new List<SimpleSqlParameter>
	            {
                    new SimpleSqlParameter("@slug", bunch.Slug),
                    new SimpleSqlParameter("@displayName", bunch.DisplayName),
                    new SimpleSqlParameter("@description", bunch.Description),
                    new SimpleSqlParameter("@houseRules", bunch.HouseRules),
                    new SimpleSqlParameter("@currencySymbol", bunch.CurrencySymbol),
                    new SimpleSqlParameter("@currencyLayout", bunch.CurrencyLayout),
                    new SimpleSqlParameter("@timeZone", bunch.TimezoneName),
                    new SimpleSqlParameter("@defaultBuyin", bunch.DefaultBuyin),
                    new SimpleSqlParameter("@cashgamesEnabled", bunch.CashgamesEnabled),
                    new SimpleSqlParameter("@tournamentsEnabled", bunch.TournamentsEnabled),
                    new SimpleSqlParameter("@videosEnabled", bunch.VideosEnabled),
                    new SimpleSqlParameter("@id", bunch.Id)
                };
            
            var rowCount = Execute(sql, parameters);
            return rowCount > 0;
        }

        public bool DeleteBunch(string slug)
        {
            const string sql = "DELETE FROM homegame WHERE Name = @slug";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@slug", slug)
                };
            var rowCount = Execute(sql, parameters);
            return rowCount > 0;
        }

	    private static RawBunch CreateRawBunch(IStorageDataReader reader)
        {
            return new RawBunch(
                reader.GetIntValue("HomegameID"),
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