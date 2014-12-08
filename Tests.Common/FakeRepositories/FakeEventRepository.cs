using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeEventRepository : IEventRepository
    {
        public IList<Event> Find(int bunchId)
        {
            throw new System.NotImplementedException();
        }

        public Event GetById(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}