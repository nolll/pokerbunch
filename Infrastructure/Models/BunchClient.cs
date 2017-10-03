using System.Collections.Generic;
using Infrastructure.Api.Clients;
using Infrastructure.Api.Connection;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Models
{
    public class BunchClient : ApiClient
    {
        public BunchClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public ApiBunch Get(string id)
        {
            return ApiConnection.Get<ApiBunch>(new ApiBunchUrl(id));
        }

        public IList<ApiSmallBunch> List()
        {
            return ApiConnection.Get<IList<ApiSmallBunch>>(new ApiBunchesUrl());
        }

        public IList<ApiSmallBunch> ListForUser()
        {
            return ApiConnection.Get<IList<ApiSmallBunch>>(new ApiUserBunchesUrl());
        }

        public ApiBunch Add(ApiBunch bunch)
        {
            return ApiConnection.Post<ApiBunch>(new ApiBunchesUrl(), bunch);
        }

        public ApiBunch Update(ApiBunch bunch)
        {
            return ApiConnection.Post<ApiBunch>(new ApiBunchUrl(bunch.Id), bunch);
        }

        public void Join(ApiJoin apiJoin)
        {
            ApiConnection.Post(new ApiBunchJoinUrl(apiJoin.BunchId), apiJoin);
        }
    }
}