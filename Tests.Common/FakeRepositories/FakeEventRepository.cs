using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeEventRepository : IEventRepository
    {
        public Event Added { get; private set; }
        public string AddedCashgameId { get; private set; }

        private readonly IList<Event> _list;

        public FakeEventRepository()
        {
            _list = CreateEventList();
        }

        public Event Get(string id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }
        
        public IList<Event> ListByBunch(string bunchId)
        {
            return _list.Where(o => o.BunchId == bunchId).ToList();
        }

        public string Add(Event e)
        {
            Added = e;
            return "1";
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            AddedCashgameId = cashgameId;
        }

        private IList<Event> CreateEventList()
        {
            return new List<Event>
            {
                new Event(TestData.EventIdA, TestData.BunchA.Id, TestData.EventNameA, new SmallLocation(TestData.LocationIdA, TestData.LocationNameA), new Date(TestData.StartTimeA), new Date(TestData.StartTimeA.AddDays(1))),
                new Event(TestData.EventIdB, TestData.BunchA.Id, TestData.EventNameB, new SmallLocation(TestData.LocationIdB, TestData.LocationNameB), new Date(TestData.StartTimeB), new Date(TestData.StartTimeB.AddDays(1)))
            };
        }
    }
}