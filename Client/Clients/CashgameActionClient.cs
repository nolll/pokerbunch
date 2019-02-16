using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class CashgameActionClient : ApiClient
    {
        public CashgameActionClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public void Add(CashgameActionAdd cashgameActionAdd)
        {
            ApiConnection.Post(new ApiCashgameActionsUrl(cashgameActionAdd.CashgameId), cashgameActionAdd);
        }

        public void Update(CashgameActionUpdate updateObject)
        {
            ApiConnection.Put<Cashgame>(new ApiCashgameActionUrl(updateObject.CashgameId, updateObject.ActionId), updateObject);
        }

        public void Delete(string cashgameId, string actionId)
        {
            ApiConnection.Delete(new ApiCashgameActionUrl(cashgameId, actionId));
        }
    }
}