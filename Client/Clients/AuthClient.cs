using PokerBunch.Client.Connection;

namespace PokerBunch.Client.Clients
{
    public class AuthClient : ApiClient
    {
        public AuthClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public string GetToken(string userNameOrEmail, string password)
        {
            return ApiConnection.SignIn(userNameOrEmail, password).Trim('"');
        }
    }
}