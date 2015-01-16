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

        public Event GetById(int eventId)
        {
            return _list.FirstOrDefault(o => o.Id == eventId);
        }

        private IList<Event> CreateEventList()
        {
            return new List<Event>
            {
                new Event(Constants.EventIdA, Constants.EventNameA, Constants.LocationA, new Date(Constants.StartTimeA), new Date(Constants.StartTimeA.AddDays(1))),
                new Event(Constants.EventIdB, Constants.EventNameB, Constants.LocationB, new Date(Constants.StartTimeB), new Date(Constants.StartTimeB.AddDays(1)))
            };
        }
    }
}