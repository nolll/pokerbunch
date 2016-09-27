using Core.Repositories;

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
            return _apiConnection.GetToken(userName, password);
        }
    }
}