using Newtonsoft.Json;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Response;

namespace PokerBunch.Client.Clients
{
    public class AuthClient : ApiClient
    {
        public AuthClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public string GetToken(string userNameOrEmail, string password)
        {
            var responseString = ApiConnection.SignIn(userNameOrEmail, password);
            var responseObject = JsonConvert.DeserializeObject<SignInResponse>(responseString);
            return responseObject?.access_token;
        }
    }
}