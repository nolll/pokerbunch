using PokerBunch.Client.Connection;
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

        public Bunch Update(BunchUpdate bunch)
        {
            return ApiConnection.Put<Bunch>(new ApiBunchUrl(bunch.Id), bunch);
        }
    }
}