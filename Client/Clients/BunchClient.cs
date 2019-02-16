using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class BunchClient : ApiClient
    {
        public BunchClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Bunch Get(string id)
        {
            return ApiConnection.Get<Bunch>(new ApiBunchUrl(id));
        }

        public IList<BunchSmall> List()
        {
            return ApiConnection.Get<IList<BunchSmall>>(new ApiBunchesUrl());
        }

        public IList<BunchSmall> ListForUser()
        {
            return ApiConnection.Get<IList<BunchSmall>>(new ApiUserBunchesUrl());
        }

        public Bunch Add(BunchAdd bunch)
        {
            return ApiConnection.Post<Bunch>(new ApiBunchesUrl(), bunch);
        }

        public Bunch Update(BunchUpdate bunch)
        {
            return ApiConnection.Post<Bunch>(new ApiBunchUrl(bunch.Id), bunch);
        }

        public void Join(BunchJoin bunchJoin)
        {
            ApiConnection.Post(new ApiBunchJoinUrl(bunchJoin.BunchId), bunchJoin);
        }
    }
}