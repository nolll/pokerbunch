using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class CashgameActionClient : ApiClient
    {
        public CashgameActionClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public void Report(ApiReport apiReport)
        {
            ApiConnection.Post(new ApiCashgameReportUrl(apiReport.CashgameId), apiReport);
        }

        public void Buyin(ApiBuyin apiBuyin)
        {
            ApiConnection.Post(new ApiCashgameBuyinUrl(apiBuyin.CashgameId), apiBuyin);
        }

        public void Cashout(ApiCashout apiCashout)
        {
            ApiConnection.Post(new ApiCashgameCashoutUrl(apiCashout.CashgameId), apiCashout);
        }

        public void End(string cashgameId)
        {
            ApiConnection.Post(new ApiCashgameEndUrl(cashgameId));
        }

        public void Update(ApiUpdateCashgameAction updateObject)
        {
            ApiConnection.Put<ApiDetailedCashgame>(new ApiCashgameActionUrl(updateObject.CashgameId, updateObject.ActionId), updateObject);
        }

        public void Delete(string cashgameId, string actionId)
        {
            ApiConnection.Delete(new ApiCashgameActionUrl(cashgameId, actionId));
        }
    }
}