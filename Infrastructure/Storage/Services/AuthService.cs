using Core.Services;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Infrastructure.Storage.Services
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
        
        private class SignInResponse
        {
            [UsedImplicitly]
            // ReSharper disable once InconsistentNaming
            public string access_token { get; set; }

            [UsedImplicitly]
            // ReSharper disable once InconsistentNaming
            public string token_type { get; set; }

            [UsedImplicitly]
            // ReSharper disable once InconsistentNaming
            public string expires_in { get; set; }
        }
    }
}