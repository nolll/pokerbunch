using System.Collections.Generic;
using System.Linq;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
    public class SqlServerEventStorage : SqlServerStorageProvider, IEventStorage 
    {
        private const string EventSql = @"SELECT e.EventID, e.Name, g.Location, g.Date
                                        FROM [Event] e
                                        LEFT JOIN EventCashgame ecg on e.EventId = ecg.EventId
                                        LEFT JOIN Game g on ecg.GameId = g.GameID
                                        {0}
                                        ORDER BY e.EventId, g.Date";

        public RawEvent GetById(int id)
        {
            const string whereClause = "WHERE e.EventID = @id";
            var sql = string.Format(EventSql, whereClause);
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@id", id)
                };
            var reader = Query(sql, parameters);
            var rawEvents = CreateRawEvents(reader);
            return rawEvents.FirstOrDefault();
        }

        public IList<RawEvent> GetEventList(IList<int> ids)
        {
            const string whereClause = "WHERE e.EventID IN(@ids)";
            var sql = string.Format(EventSql, whereClause);
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = Query(sql, parameter);
            return CreateRawEvents(reader);
        }

        public IList<int> GetEventIdList()
        {
            const string sql = "SELECT e.EventID FROM [Event] e";
            var reader = Query(sql);
            return reader.ReadIntList("EventID");
        }

        private static IList<RawEvent> CreateRawEvents(IStorageDataReader reader)
        {
            var rawEventDays = reader.ReadList(CreateRawEventDay);
            var map = new Dictionary<int, IList<RawEventDay>>();
            foreach (var day in rawEventDays)
            {
                IList<RawEventDay> list;
                if (map.ContainsKey(day.Id))
                {
                    list = map[day.Id];
                }
                else
                {
                    list = new List<RawEventDay>();
                    map[day.Id] = list;
                }
                list.Add(day);
            }

            var rawEvents = new List<RawEvent>();
            foreach (var key in map.Keys)
            {
                var item = map[key];
                var firstItem = item.First();
                var lastItem = item.Last();
                rawEvents.Add(new RawEvent(firstItem.Id, firstItem.Name, firstItem.Location, firstItem.Date, lastItem.Date));
            }
            return rawEvents;
        } 

        private static RawEventDay CreateRawEventDay(IStorageDataReader reader)
        {
            return new RawEventDay(
                reader.GetIntValue("EventID"),
                reader.GetStringValue("Name"),
                reader.GetStringValue("Location"),
                reader.GetDateTimeValue("Date"));
        }
	}
}