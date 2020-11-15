using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class CashgameClient : ApiClient
    {
        public CashgameClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public IList<CashgameSmall> PlayerList(string playerId)
        {
            return ApiConnection.Get<IList<CashgameSmall>>(new ApiPlayerCashgamesUrl(playerId));
        }
    }
}