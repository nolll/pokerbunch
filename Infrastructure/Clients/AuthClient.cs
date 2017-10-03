using Infrastructure.Api.Connection;
using Infrastructure.Api.Models.Response;
using Newtonsoft.Json;

namespace Infrastructure.Api.Clients
{
    public class AuthClient : ApiClient
    {
        public AuthClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public SignInResponse SignIn(string userNameOrEmail, string password)
        {
            var responseString = ApiConnection.SignIn(userNameOrEmail, password);
            return JsonConvert.DeserializeObject<SignInResponse>(responseString);
        }
    }
}