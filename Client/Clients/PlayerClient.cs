using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class PlayerClient : ApiClient
    {
        public PlayerClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Player Get(string id)
        {
            return ApiConnection.Get<Player>(new ApiPlayerUrl(id));
        }
    }
}