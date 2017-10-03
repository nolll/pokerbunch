using Core.Services;
using Infrastructure.Api.Clients;

namespace Infrastructure.Api.Services
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public string SignIn(string userNameOrEmail, string password)
        {
            var response = ApiClient.Auth.SignIn(userNameOrEmail, password);
            return response.access_token;
        }
    }
}