using System.Collections.Generic;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
    public class SqlServerEventStorage : SqlServerStorageProvider, IEventStorage 
    {
        public RawEvent GetById(int id)
        {
            const string sql = "SELECT e.EventID, e.Name FROM [Event] e WHERE e.EventID = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateRawEvent);
        }

        public IList<RawEvent> GetEventList(IList<int> ids)
        {
            const string sql = "SELECT e.EventID, e.Name FROM [Event] e WHERE e.EventID IN(@ids)";
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = Query(sql, parameter);
            return reader.ReadList(CreateRawEvent);
        }

        public IList<int> GetEventIdList()
        {
            const string sql = "SELECT e.EventID FROM [Event] e";
            var reader = Query(sql);
            return reader.ReadIntList("EventID");
        }
        
        private static RawEvent CreateRawEvent(IStorageDataReader reader)
        {
            return new RawEvent(
                reader.GetIntValue("EventID"),
                reader.GetStringValue("Name"));
        }
	}
}