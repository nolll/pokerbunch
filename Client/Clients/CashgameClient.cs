using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class CashgameClient : ApiClient
    {
        public CashgameClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Cashgame GetDetailedById(string id)
        {
            return ApiConnection.Get<Cashgame>(new ApiCashgameUrl(id));
        }

        public IList<CashgameSmall> PlayerList(string playerId)
        {
            return ApiConnection.Get<IList<CashgameSmall>>(new ApiPlayerCashgamesUrl(playerId));
        }
        
        public void Delete(string id)
        {
            ApiConnection.Delete(new ApiCashgameUrl(id));
        }

        public Cashgame Update(CashgameUpdate cashgameUpdate)
        {
            return ApiConnection.Put<Cashgame>(new ApiCashgameUrl(cashgameUpdate.CashgameId), cashgameUpdate);
        }
    }
}