using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeEventRepository : IEventRepository
    {
        public IList<Event> Find(int bunchId)
        {
            return new List<Event>
            {
                new Event(Constants.EventIdA, Constants.EventNameA),
                new Event(Constants.EventIdB, Constants.EventNameB)
            };
        }

        public Event GetById(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}