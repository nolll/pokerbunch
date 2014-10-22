using System.Collections.Generic;
using Infrastructure.SqlServer.Interfaces;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer
{
    public class SqlServerEventStorage : IEventStorage 
    {
	    private readonly IStorageProvider _storageProvider;

        public SqlServerEventStorage()
	    {
	        _storageProvider = new SqlServerStorageProvider();
	    }

        public RawEvent GetById(int id)
        {
            const string sql = "SELECT e.EventID, e.Name FROM [Event] e WHERE e.EventID = @id";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadOne(CreateRawEvent);
        }

        public IList<RawEvent> GetEventList(IList<int> ids)
        {
            const string sql = "SELECT e.EventID, e.Name FROM [Event] e WHERE e.EventID IN(@ids)";
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _storageProvider.Query(sql, parameter);
            return reader.ReadList(CreateRawEvent);
        }

        public IList<int> GetEventIdList()
        {
            const string sql = "SELECT e.EventID FROM [Event] e";
            var reader = _storageProvider.Query(sql);
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