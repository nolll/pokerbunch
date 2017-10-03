using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Clients;
using Infrastructure.Api.Models;

namespace Infrastructure.Api.Services
{
    public class EventService : BaseService, IEventService
    {
        public EventService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public Event Get(string id)
        {
            var apiEvent = ApiClient.Events.Get(id);
            return CreateEvent(apiEvent);
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            var apiEvents = ApiClient.Events.ListByBunch(bunchId);
            return apiEvents.Select(CreateEvent).ToList();
        }
        
        public string Add(Event e)
        {
            var postEvent = new ApiEvent(e.Name, e.BunchId);
            var apiEvent = ApiClient.Events.Add(postEvent);
            return CreateEvent(apiEvent).Id;
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            var postCashame = new ApiEventCashgame(eventId, cashgameId);
            ApiClient.Events.AddCashgame(postCashame);
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