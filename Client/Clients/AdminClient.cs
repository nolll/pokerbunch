using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class AdminClient : ApiClient
    {
        public AdminClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public string ClearCache()
        {
            return ApiConnection.Post<MessageWrapper>(new ApiAdminClearCacheUrl()).Message;
        }

        public string SendEmail()
        {
            return ApiConnection.Post<MessageWrapper>(new ApiAdminSendEmailUrl()).Message;
        }
    }
}