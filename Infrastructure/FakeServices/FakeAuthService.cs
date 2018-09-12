using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeAuthService : IAuthService
    {
        public string SignIn(string userNameOrEmail, string password)
        {
            return "FakeToken";
        }
    }
}