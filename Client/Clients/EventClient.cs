using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class EventClient : ApiClient
    {
        public EventClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Event Get(string id)
        {
            return ApiConnection.Get<Event>(new ApiEventUrl(id));
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            return ApiConnection.Get<IList<Event>>(new ApiBunchEventsUrl(bunchId));
        }

        public Event Add(string bunchId, EventAdd e)
        {
            return ApiConnection.Post<Event>(new ApiBunchEventsUrl(bunchId), e);
        }

        public void AddCashgame(string eventId, EventCashgameAdd eventCashgame)
        {
            ApiConnection.Post<EventCashgame>(new ApiEventCashgamesUrl(eventId), eventCashgame);
        }
    }
}