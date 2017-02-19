using System.Collections.Generic;
using Core.Entities;

namespace Infrastructure.Storage.SqlDb
{
    public class SqlEventDb
    {
        private readonly SqlServerStorageProvider _db;

        public SqlEventDb(SqlServerStorageProvider db)
        {
            _db = db;
        }

        public string Add(Event e)
        {
            const string sql = "INSERT INTO event (Name, BunchId) SELECT @name, h.HomegameID FROM Homegame h where h.Name = @bunchId SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@name", e.Name),
                    new SimpleSqlParameter("@bunchId", e.BunchId)
                };
            return _db.ExecuteInsert(sql, parameters);
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            const string sql = "INSERT INTO eventcashgame (EventId, GameId) VALUES (@eventId, @cashgameId)";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@eventId", eventId),
                    new SimpleSqlParameter("@cashgameId", cashgameId)
                };
            _db.ExecuteInsert(sql, parameters);
        }
    }
}