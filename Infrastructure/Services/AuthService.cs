using Core.Services;
using Infrastructure.Api.Clients;

namespace Infrastructure.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly PokerBunchClient _apiClient;

        public AuthService(PokerBunchClient apiClient)
        {
            _apiClient = apiClient;
        }

        public string SignIn(string userNameOrEmail, string password)
        {
            var response = _apiClient.Auth.SignIn(userNameOrEmail, password);
            return response.access_token;
        }
    }
}