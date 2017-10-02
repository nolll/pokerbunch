using Core.Services;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using Newtonsoft.Json;

namespace Infrastructure.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApiConnection _api;

        public AuthService(ApiConnection api)
        {
            _api = api;
        }

        public string SignIn(string userNameOrPassword, string password)
        {
            var responseString = _api.SignIn(userNameOrPassword, password);
            var response = JsonConvert.DeserializeObject<SignInResponse>(responseString);
            return response.access_token;
        }
    }
}