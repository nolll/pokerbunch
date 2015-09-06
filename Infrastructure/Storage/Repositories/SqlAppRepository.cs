using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlAppRepository : SqlServerStorageProvider, IAppRepository
    {
        private const string AppDataSql = "SELECT a.ID, a.AppKey, a.Name, a.UserId FROM [App] a ";
        private const string AppIdSql = "SELECT a.ID FROM [App] a ";

        public IList<App> ListApps()
        {
            var ids = GetAppIdList();
            return GetAppList(ids);
        }

        public IList<App> ListApps(int userId)
        {
            var ids = GetAppIdList(userId);
            return GetAppList(ids);
        }

        public App Get(int id)
        {
            var sql = string.Concat(AppDataSql, "WHERE a.Id = @appId");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@appId", id)
            };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateApp);
        }

        public App Get(string appKey)
        {
            var sql = string.Concat(AppDataSql, "WHERE a.AppKey = @appKey");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@appKey", appKey)
            };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateApp);
        }

        private IList<int> GetAppIdList()
        {
            var sql = string.Concat(AppIdSql, "ORDER BY a.Name");
            var reader = Query(sql);
            return reader.ReadIntList("ID");
        }

        public int Add(App app)
        {
            const string sql = "INSERT INTO [app] (AppKey, Name, UserId) VALUES (@appKey, @name, @userId) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@appKey", app.AppKey),
		            new SimpleSqlParameter("@name", app.Name),
		            new SimpleSqlParameter("@userId", app.UserId)
		        };
            return ExecuteInsert(sql, parameters);
        }

        public void Update(App app)
        {
            throw new System.NotImplementedException();
        }

        private IList<int> GetAppIdList(int userId)
        {
            var sql = string.Concat(AppIdSql, "WHERE a.UserId = @userId ORDER BY a.Name");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@userId", userId)
            };
            var reader = Query(sql, parameters);
            return reader.ReadIntList("ID");
        }

        private IList<App> GetAppList(IList<int> ids)
        {
            var sql = string.Concat(AppDataSql, "WHERE a.ID IN(@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = Query(sql, parameter);
            return reader.ReadList(CreateApp);
        }

        private static App CreateApp(IStorageDataReader reader)
        {
            return new App(
                reader.GetIntValue("ID"),
                reader.GetStringValue("AppKey"),
                reader.GetStringValue("Name"),
                reader.GetIntValue("USerID"));
        }
    }
}