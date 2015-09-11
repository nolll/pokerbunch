using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeEventRepository : IEventRepository
    {
        private readonly IList<Event> _list;

        public FakeEventRepository()
        {
            _list = CreateEventList();
        }

        public IList<Event> Find(int bunchId)
        {
            return _list;
        }

        public Event Get(int eventId)
        {
            return _list.FirstOrDefault(o => o.Id == eventId);
        }

        private IList<Event> CreateEventList()
        {
            return new List<Event>
            {
                new Event(TestData.EventIdA, TestData.BunchA.Id, TestData.EventNameA, TestData.LocationA, new Date(TestData.StartTimeA), new Date(TestData.StartTimeA.AddDays(1))),
                new Event(TestData.EventIdB, TestData.BunchA.Id, TestData.EventNameB, TestData.LocationB, new Date(TestData.StartTimeB), new Date(TestData.StartTimeB.AddDays(1)))
            };
        }
    }
}