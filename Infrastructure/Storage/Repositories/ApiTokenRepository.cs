using Core.Repositories;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Infrastructure.Storage.Repositories
{
    public class ApiTokenRepository : ITokenRepository
    {
        private readonly ApiConnection _apiConnection;

        public ApiTokenRepository(ApiConnection apiConnection)
        {
            _apiConnection = apiConnection;
        }

        public string Get(string userName, string password)
        {
            var responseString = _apiConnection.GetToken(userName, password);
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