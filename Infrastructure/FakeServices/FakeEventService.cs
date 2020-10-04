using System.Collections.Generic;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeEventService : IEventService
    {
        public Event Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            throw new System.NotImplementedException();
        }

        public string Add(Event e)
        {
            throw new System.NotImplementedException();
        }
    }
}