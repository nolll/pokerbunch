using System.Collections.Generic;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using Infrastructure.Api.Models.Request;
using Infrastructure.Api.Services;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class CashgameClient : ApiClient
    {
        public CashgameActionClient Actions { get; }

        public CashgameClient(ApiConnection apiConnection) : base(apiConnection)
        {
            Actions = new CashgameActionClient(apiConnection);
        }

        public ApiDetailedCashgame GetDetailedById(string id)
        {
            return ApiConnection.Get<ApiDetailedCashgame>(new ApiCashgameUrl(id));
        }

        public IList<ApiListCashgame> GetCurrent(string bunchId)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiBunchCashgamesCurrentUrl(bunchId));
        }

        public IList<ApiListCashgame> List(string bunchId, int? year = null)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiBunchCashgamesUrl(bunchId, year));
        }

        public IList<ApiListCashgame> EventList(string eventId)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiEventCashgamesUrl(eventId));
        }

        public IList<ApiListCashgame> PlayerList(string playerId)
        {
            return ApiConnection.Get<IList<ApiListCashgame>>(new ApiPlayerCashgamesUrl(playerId));
        }

        public IList<ApiYear> GetYears(string bunchId)
        {
            return ApiConnection.Get<IList<ApiYear>>(new ApiBunchCashgameYearsUrl(bunchId));
        }

        public void Delete(string id)
        {
            ApiConnection.Delete(new ApiCashgameUrl(id));
        }

        public ApiDetailedCashgame Add(ApiAddCashgame apiAddCashgame)
        {
            return ApiConnection.Post<ApiDetailedCashgame>(new ApiBunchCashgamesUrl(apiAddCashgame.BunchId), apiAddCashgame);
        }

        public ApiDetailedCashgame Update(ApiUpdateCashgame apiUpdateCashgame)
        {
            return ApiConnection.Put<ApiDetailedCashgame>(new ApiCashgameUrl(apiUpdateCashgame.CashgameId), apiUpdateCashgame);
        }
    }
}