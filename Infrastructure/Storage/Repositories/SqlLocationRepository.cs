using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlLocationRepository : ILocationRepository
    {
        private const string LocationDataSql = "SELECT l.Id, l.Name, l.BunchId FROM Location l ";
        private const string LocationIdSql = "SELECT l.Id FROM Location l ";

        private readonly SqlServerStorageProvider _db;

        public SqlLocationRepository(SqlServerStorageProvider db)
        {
            _db = db;
        }

        public Location Get(int id)
        {
            var sql = string.Concat(LocationDataSql, "WHERE l.Id = @id");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = _db.Query(sql, parameters);
            return reader.ReadOne(CreateLocation);
        }
        
        public IList<Location> Get(IList<int> ids)
        {
            if (!ids.Any())
                return new List<Location>();
            var sql = string.Concat(LocationDataSql, "WHERE l.Id IN (@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _db.Query(sql, parameter);
            return reader.ReadList(CreateLocation);
        }

        public IList<int> Find(int bunchId)
        {
            var sql = string.Concat(LocationIdSql, "WHERE l.BunchId = @bunchId");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@bunchId", bunchId)
            };
            var reader = _db.Query(sql, parameters);
            return reader.ReadIntList("Id");
        }

        private Location CreateLocation(IStorageDataReader reader)
        {
            return new Location(
                reader.GetIntValue("Id"),
                reader.GetStringValue("Name"),
                reader.GetIntValue("BunchId"));
        }
    }
}