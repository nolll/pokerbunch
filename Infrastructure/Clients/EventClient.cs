using System.Collections.Generic;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class EventClient : ApiClient
    {
        public EventClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public ApiEvent Get(string id)
        {
            return ApiConnection.Get<ApiEvent>(new ApiEventUrl(id));
        }

        public IList<ApiEvent> ListByBunch(string bunchId)
        {
            return ApiConnection.Get<IList<ApiEvent>>(new ApiBunchEventsUrl(bunchId));
        }

        public ApiEvent Add(ApiEvent e)
        {
            return ApiConnection.Post<ApiEvent>(new ApiBunchEventsUrl(e.BunchId), e);
        }

        public void AddCashgame(ApiEventCashgame eventCashgame)
        {
            ApiConnection.Post<ApiEventCashgame>(new ApiEventCashgamesUrl(eventCashgame.EventId), eventCashgame);
        }
    }
}