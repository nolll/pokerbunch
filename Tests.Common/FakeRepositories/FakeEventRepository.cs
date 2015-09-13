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

        public IList<Event> Get(IList<int> ids)
        {
            return _list.Where(o => ids.Contains(o.Id)).ToList();
        }

        public IList<Event> FindOld(int bunchId)
        {
            return _list;
        }

        public IList<int> Find(int bunchId)
        {
            return _list.Where(o => o.BunchId == bunchId).Select(o => o.Id).ToList();
        }

        public Event Get(int id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
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