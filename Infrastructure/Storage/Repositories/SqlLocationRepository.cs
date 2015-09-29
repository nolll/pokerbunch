using System.Collections.Generic;
using Core.Repositories;

namespace Infrastructure.Storage.Repositories
{
    public class SqlLocationRepository : ILocationRepository
    {
        private readonly SqlServerStorageProvider _db;

        public SqlLocationRepository(SqlServerStorageProvider db)
        {
            _db = db;
        }

        public IList<string> GetLocations(int bunchId)
        {
            const string sql = "SELECT DISTINCT g.Location FROM game g WHERE g.HomegameID = @id AND g.Location <> '' ORDER BY g.Location";
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@id", bunchId)
            };
            var reader = _db.Query(sql, parameters);
            return reader.ReadStringList("Location");
        }
    }
}