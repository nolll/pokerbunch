using Core.Services;
using PokerBunch.Client.Clients;

namespace Infrastructure.Api.Services
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public string SignIn(string userNameOrEmail, string password)
        {
            return ApiClient.Auth.GetToken(userNameOrEmail, password);
        }
    }
}