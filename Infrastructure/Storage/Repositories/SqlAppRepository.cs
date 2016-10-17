using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlAppRepository : IAppRepository
    {
        private const string DataSql = "SELECT a.ID, a.AppKey, a.Name, a.UserId FROM [App] a ";
        private const string SearchSql = "SELECT a.ID FROM [App] a ";

        private readonly SqlServerStorageProvider _db;

        public SqlAppRepository(SqlServerStorageProvider db)
        {
            _db = db;
        }

        public IList<App> ListApps()
        {
            var ids = GetAppIdList();
            return GetAppList(ids);
        }

        public IList<App> ListApps(string userId)
        {
            var ids = GetAppIdListByUser(userId);
            return GetAppList(ids);
        }

        public App Get(string id)
        {
            var sql = string.Concat(DataSql, "WHERE a.Id = @appId");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@appId", id)
            };
            var reader = _db.Query(sql, parameters);
            return reader.ReadOne(CreateApp);
        }

        public IList<App> GetList(IList<string> ids)
        {
            return GetAppList(ids);
        }

        public IList<string> Find()
        {
            return GetAppIdList();
        }

        public IList<string> FindByUser(string userId)
        {
            return GetAppIdListByUser(userId);
        }

        public IList<string> FindByAppKey(string appKey)
        {
            return GetAppIdListByAppKey(appKey);
        }

        private IList<string> GetAppIdList()
        {
            var sql = string.Concat(SearchSql, "ORDER BY a.Name");
            var reader = _db.Query(sql);
            return reader.ReadIntList("ID").Select(o => o.ToString()).ToList();
        }

        public string Add(App app)
        {
            const string sql = "INSERT INTO [app] (AppKey, Name, UserId) VALUES (@appKey, @name, @userId) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@appKey", app.AppKey),
		            new SimpleSqlParameter("@name", app.Name),
		            new SimpleSqlParameter("@userId", app.UserId)
		        };
            return _db.ExecuteInsert(sql, parameters);
        }

        public void Update(App app)
        {
            throw new System.NotImplementedException();
        }

        private IList<string> GetAppIdListByUser(string userId)
        {
            var sql = string.Concat(SearchSql, "WHERE a.UserId = @userId ORDER BY a.Name");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@userId", userId)
            };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("ID").Select(o => o.ToString()).ToList();
        }

        private IList<string> GetAppIdListByAppKey(string appKey)
        {
            var sql = string.Concat(DataSql, "WHERE a.AppKey = @appKey");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@appKey", appKey)
            };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("ID").Select(o => o.ToString()).ToList();
        }

        private IList<App> GetAppList(IList<string> ids)
        {
            var sql = string.Concat(DataSql, "WHERE a.ID IN(@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _db.Query(sql, parameter);
            return reader.ReadList(CreateApp);
        }

        private static App CreateApp(IStorageDataReader reader)
        {
            return new App(
                reader.GetIntValue("ID").ToString(),
                reader.GetStringValue("AppKey"),
                reader.GetStringValue("Name"),
                reader.GetIntValue("UserID").ToString());
        }
    }
}