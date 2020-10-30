using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models.Request;
using ApiEvent = PokerBunch.Client.Models.Response.Event;

namespace Infrastructure.Api.Services
{
    public class EventService : BaseService, IEventService
    {
        public EventService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            var apiEvents = ApiClient.Events.ListByBunch(bunchId);
            return apiEvents.Select(CreateEvent).ToList();
        }
        
        public string Add(Event e)
        {
            var postEvent = new EventAdd(e.Name);
            var apiEvent = ApiClient.Events.Add(e.BunchId, postEvent);
            return CreateEvent(apiEvent).Id;
        }

        private Event CreateEvent(ApiEvent e)
        {
            if (e.Location != null && e.StartDate != null)
            {
                var location = new SmallLocation(e.Location.Id, e.Location.Name);
                var startDate = new Date(e.StartDate);
                return new Event(e.Id, e.BunchId, e.Name, location, startDate);
            }
            return new Event(e.Id, e.BunchId, e.Name);
        }
    }
}